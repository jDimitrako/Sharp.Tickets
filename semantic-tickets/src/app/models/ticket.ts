export interface ITicketsEnvelope {
  tickets: ITicket[];
  ticketCount: number;
}

export interface ITicket {
  id: string;
  title: string;
  description: string;
  priority: string;
  category: string;
  participating: boolean;
  isHost: boolean;
  dateFirst: Date;
  dateModified: Date;
  dateDeadline: Date;
  attendees: IAttendee[];
  comments: IComment[];
}

export interface IComment {
  id: string;
  createdAt: Date;
  body: string;
  username: string;
  displayName: string;
  image: string;
}

export interface ITicketFormValues extends Partial<ITicket> {
  time?: Date;
}

export class TicketFormValues implements ITicketFormValues {
  id?: string = undefined;
  title: string = "";
  description: string = "";
  priority: string = "";
  category: string = "";
  dateFirst?: Date = undefined;
  time?: Date = undefined;
  dateModified?: Date = undefined;
  dateDeadline?: Date = undefined;

  constructor(init?: ITicketFormValues) {
    if (init && init.dateDeadline) {
      init.time = init.dateDeadline;
    }

    Object.assign(this, init);
  }
}

export interface IAttendee {
  username: string;
  displayName: string;
  image: string;
  isHost: boolean;
  following?: boolean;
}
