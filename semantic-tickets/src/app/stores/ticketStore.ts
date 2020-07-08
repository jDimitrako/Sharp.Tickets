import { observable, action, computed, runInAction, reaction } from "mobx";
import { SyntheticEvent } from "react";
import { ITicket } from "../models/ticket";
import agent from "../api/agent";
import { history } from "../..";
import { toast } from "react-toastify";
import { RootStore } from "./rootStore";
import { setTicketProps, createAttendee } from "../common/util/util";
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";

const LIMIT = 2;

export default class TicketStore {
  rootStore: RootStore;
  constructor(rootStore: RootStore) {
    this.rootStore = rootStore;

    reaction(
      () => this.predicate.keys(),
      () => {
        this.page = 0;
        this.ticketRegistry.clear();
        this.loadTickets();
      }
    );
  }

  @observable ticketRegistry = new Map();
  @observable ticket: ITicket | null = null;
  @observable loadingInitial = false;
  @observable submitting = false;
  @observable target = "";
  @observable loading = false;
  @observable.ref hubConnection: HubConnection | null = null;
  @observable ticketCount = 0;
  @observable page = 0;
  @observable predicate = new Map();

  @action setPredicate = (predicate: string, value: string | Date) => {
    this.predicate.clear();
    if (predicate !== "all") {
      this.predicate.set(predicate, value);
    }
  };

  @computed get axiosParams() {
    const params = new URLSearchParams();
    params.append("limit", String(LIMIT));
    params.append("offset", `${this.page ? this.page * LIMIT : 0}`);
    this.predicate.forEach((value, key) => {
      if (key === "startDate") {
        params.append(key, value.toISOString());
      } else {
        params.append(key, value);
      }
    });
    return params;
  }

  @computed get totalPages() {
    return Math.ceil(this.ticketCount / LIMIT);
  }

  @action setPage = (page: number) => {
    this.page = page;
  };

  @action createHubConnection = (ticketId: string) => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(process.env.REACT_APP_API_CHAT_URL!, {
        accessTokenFactory: () => this.rootStore.commonStore.token!,
      })
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log(this.hubConnection!.state))
      .then(() => {
        console.log("Attempting to join group");
        this.hubConnection!.invoke("AddToGroup", ticketId);
      })
      .catch((error) => console.log("Error establishing connection:", error));

    this.hubConnection.on("ReceiveComment", (comment) => {
      runInAction(() => {
        this.ticket!.comments.push(comment);
      });
    });

    this.hubConnection.on("Send", (message) => {
      toast.info(message);
    });
  };

  @action stopHubConnection = () => {
    this.hubConnection!.invoke("RemoveFromGroup", this.ticket!.id)
      .then(() => {
        this.hubConnection!.stop();
      })
      .then(() => console.log("Connection stopped"))
      .catch((error) => console.log(error));
  };

  @action addComment = async (values: any) => {
    values.ticketId = this.ticket!.id;
    try {
      await this.hubConnection!.invoke("SendComment", values);
    } catch (error) {
      console.log(error);
    }
  };

  @computed get ticketsByDate() {
    return this.groupTicketsByDate(Array.from(this.ticketRegistry.values()));
  }

  groupTicketsByDate(tickets: ITicket[]) {
    const sortedTickets = tickets.sort(
      (a, b) => a.dateFirst.getTime() - b.dateFirst.getTime()
    );
    return Object.entries(
      sortedTickets.reduce((tickets, ticket) => {
        const dateCreated = ticket.dateFirst.toISOString().split("T")[0];
        tickets[dateCreated] = tickets[dateCreated]
          ? [...tickets[dateCreated], ticket]
          : [ticket];
        return tickets;
      }, {} as { [key: string]: ITicket[] })
    );
  }

  @action loadTickets = async () => {
    this.loadingInitial = true;
    try {
      const ticketsEnvelope = await agent.Tickets.list(this.axiosParams);
      const { tickets, ticketCount } = ticketsEnvelope;
      runInAction("loading tickets", () => {
        tickets.forEach((ticket) => {
          setTicketProps(ticket, this.rootStore.userStore.user!);
          this.ticketRegistry.set(ticket.id, ticket);
        });
        this.ticketCount = ticketCount;
        this.loadingInitial = false;
      });
    } catch (error) {
      runInAction("loading tickets error", () => {
        this.loadingInitial = false;
      });
      console.log(error);
    }
  };

  @action loadTicket = async (id: string) => {
    let ticket = this.getTicket(id);
    if (ticket) {
      this.ticket = ticket;
      return ticket;
    } else {
      this.loadingInitial = true;
      try {
        ticket = await agent.Tickets.details(id);
        runInAction("getting ticket", () => {
          setTicketProps(ticket, this.rootStore.userStore.user!);
          this.ticket = ticket;
          this.ticketRegistry.set(ticket.id, ticket);
          this.loadingInitial = false;
        });
        return ticket;
      } catch (error) {
        runInAction("getting ticket", () => {
          this.loadingInitial = false;
        });
      }
    }
  };

  @action clearTicket = () => {
    this.ticket = null;
  };

  getTicket = (id: string) => {
    return this.ticketRegistry.get(id);
  };

  @action createTicket = async (ticket: ITicket) => {
    this.submitting = true;
    try {
      await agent.Tickets.create(ticket);
      const attendee = createAttendee(this.rootStore.userStore.user!);
      attendee.isHost = true;
      let attendees = [];
      attendees.push(attendee);
      ticket.attendees = attendees;
      ticket.comments = [];
      ticket.isHost = true;
      runInAction("create ticket", () => {
        this.ticketRegistry.set(ticket.id, ticket);
        this.submitting = false;
      });
      history.push(`/tickets/${ticket.id}`);
    } catch (error) {
      runInAction("create ticket action", () => {
        this.submitting = false;
      });
      toast.error("Problem submitting data");
      console.log(error.response);
    }
  };

  @action editTicket = async (ticket: ITicket) => {
    this.submitting = true;
    try {
      await agent.Tickets.update(ticket);
      runInAction("edit ticket", () => {
        this.ticketRegistry.set(ticket.id, ticket);
        this.ticket = ticket;
        this.submitting = false;
      });
    } catch (error) {
      runInAction("edit ticket error", () => {
        this.submitting = false;
      });
      console.log(error);
    }
  };

  @action deleteTicket = async (
    event: SyntheticEvent<HTMLButtonElement>,
    id: string
  ) => {
    this.submitting = true;
    this.target = event.currentTarget.name;
    try {
      await agent.Tickets.delete(id);
      runInAction("delete ticket", () => {
        this.ticketRegistry.delete(id);
        this.submitting = false;
        this.target = "";
      });
    } catch (error) {
      runInAction("delete ticket error", () => {
        this.submitting = false;
        this.target = "";
      });
      console.log(error);
    }
  };

  @action attendTicket = async () => {
    const attendee = createAttendee(this.rootStore.userStore.user!);
    this.loading = true;
    try {
      await agent.Tickets.attend(this.ticket!.id);
      runInAction(() => {
        if (this.ticket) {
          this.ticket.attendees.push(attendee);
          this.ticket.participating = true;
          this.ticketRegistry.set(this.ticket.id, this.ticket);
          this.loading = false;
        }
      });
    } catch (error) {
      runInAction(() => {
        this.loading = false;
      });
      toast.error("Problem signing up to ticket");
    }
  };

  @action cancelAttendance = async () => {
    this.loading = true;
    try {
      await agent.Tickets.unattend(this.ticket!.id);
      runInAction(() => {
        if (this.ticket) {
          this.ticket.attendees = this.ticket.attendees.filter(
            (a) => a.username !== this.rootStore.userStore.user!.username
          );
          this.ticket.participating = false;
          this.ticketRegistry.set(this.ticket.id, this.ticket);
          this.loading = false;
        }
      });
    } catch (error) {
      runInAction(() => {
        this.loading = false;
      });
      toast.error("Problem cancelling attendance");
    }
  };
}
