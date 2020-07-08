import { ITicket, IAttendee } from "../../models/ticket";
import { IUser } from "../../models/user";

export const combineDateAndTime = (date: Date, time: Date) => {
  const timeString = time.getHours() + ":" + time.getMinutes() + ":00";

  const year = date.getFullYear();
  const month = date.getMonth() + 1;
  const day = date.getDate();
  const dateString = `${year}-${month}-${day}`;

  return new Date(dateString + " " + timeString);
};

export const setTicketProps = (ticket: ITicket, user: IUser) => {
  ticket.dateFirst = new Date(ticket.dateFirst);
  ticket.dateDeadline = new Date(ticket.dateDeadline);
  ticket.dateModified = new Date(ticket.dateModified);
  ticket.participating = ticket.attendees.some(
    (a) => a.username === user.username
  );
  ticket.isHost = ticket.attendees.some(
    (a) => a.username === user.username && a.isHost
  );
  return ticket;
};

export const createAttendee = (user: IUser): IAttendee => {
  return {
    displayName: user.displayName,
    isHost: false,
    username: user.username,
    image: user.image!,
  };
};
