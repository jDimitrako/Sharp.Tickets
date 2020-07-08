import React, { useContext } from "react";
import { Item, Label, Button, Icon, Segment } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { ITicket } from "../../../app/models/ticket";
import { format } from "date-fns";
import { RootStoreContext } from "../../../app/stores/rootStore";
import TicketListItemAttendees from "./TicketListItemAttendees";

const TicketsListItem: React.FC<{ ticket: ITicket }> = ({ ticket }) => {
  const rootStore = useContext(RootStoreContext);
  const { deleteTicket, submitting, target } = rootStore.ticketStore;
  const host = ticket.attendees.filter((x) => x.isHost)[0];
  return (
    <Segment.Group>
      <Segment>
        <Item.Group>
          <Item>
            <Item.Image
              size="tiny"
              circular
              src={host.image || "/assets/user.png"}
              style={{ marginBottom: 3 }}
            />
            <Item.Content>
              <Item.Header as={Link} to={`/tickets/${ticket.id}`}>
                {ticket.title}
              </Item.Header>
              <Item.Description>
                <div>
                  Hosted by
                  <Link to={`/profile/${host.username}`}>
                    {host.displayName}
                  </Link>
                </div>
                <Label basic content="Category" />
              </Item.Description>
              {ticket.isHost && (
                <Item.Description>
                  <Label
                    basic
                    color="orange"
                    content="You are hosting this ticket"
                  />
                </Item.Description>
              )}
              {ticket.participating && !ticket.isHost && (
                <Item.Description>
                  <Label
                    basic
                    color="green"
                    content="You are participating in this ticket"
                  />
                </Item.Description>
              )}
            </Item.Content>
          </Item>
        </Item.Group>
      </Segment>
      <Segment>
        <Label>
          Date Created
          <Label.Detail>{format(ticket.dateFirst, "h:mm a")}</Label.Detail>
        </Label>
        <Label>
          Date Modified
          <Label.Detail>{format(ticket.dateModified, "h:mm a")}</Label.Detail>
        </Label>
        <Label>
          Deadline
          <Label.Detail>{format(ticket.dateDeadline, "h:mm a")}</Label.Detail>
        </Label>
      </Segment>
      <Segment secondary>
        <TicketListItemAttendees attendees={ticket.attendees} />
      </Segment>
      <Segment clearing>
        <span>{ticket.description}</span>
        <Button
          as={Link}
          to={`/tickets/${ticket.id}`}
          floated="right"
          color="teal"
          animated
        >
          <Button.Content visible>View</Button.Content>
          <Button.Content hidden>
            <Icon name="arrow right" />
          </Button.Content>
        </Button>
        <Button
          name={ticket.id}
          onClick={(e) => deleteTicket(e, ticket.id)}
          loading={target === ticket.id && submitting}
          floated="right"
          color="red"
          content="Delete"
        />
      </Segment>
    </Segment.Group>
  );
};

export default TicketsListItem;
