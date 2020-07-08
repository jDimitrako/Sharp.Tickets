import React, { useContext } from "react";
import { Segment, Item, Header, Button, Image } from "semantic-ui-react";
import { ITicket } from "../../../app/models/ticket";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { format } from "date-fns";
import { RootStoreContext } from "../../../app/stores/rootStore";

const ticketImageStyle = {
  filter: "brightness(30%)",
};

const ticketImageTextStyle = {
  position: "absolute",
  bottom: "5%",
  left: "5%",
  width: "100%",
  height: "auto",
  color: "white",
};

const manageTicketStyle = {
  background: "#ff867c",
  color: "white",
};

const TicketDetailedHeader: React.FC<{ ticket: ITicket }> = ({ ticket }) => {
  const rootStore = useContext(RootStoreContext);
  const { attendTicket, cancelAttendance, loading } = rootStore.ticketStore;
  const host = ticket.attendees.filter((x) => x.isHost)[0];
  return (
    <Segment.Group>
      <Segment basic attached="top" style={{ padding: "0" }}>
        <Image src={`/assets/placeholder.png`} fluid style={ticketImageStyle} />
        <Segment basic style={ticketImageTextStyle}>
          <Item.Group>
            <Item>
              <Item.Content>
                <Header
                  size="huge"
                  content={ticket.title}
                  style={{ color: "white" }}
                />
                <p>{format(ticket.dateFirst, "eeee do MMMM")}</p>
                <p>
                  Hosted by{" "}
                  <Link to={`/profile/${host.username}`}>
                    <strong>{host.displayName}</strong>
                  </Link>
                </p>
              </Item.Content>
            </Item>
          </Item.Group>
        </Segment>
      </Segment>
      <Segment clearing attached="bottom">
        {ticket.isHost ? (
          <Button
            as={Link}
            to={`/manage/${ticket.id}`}
            style={manageTicketStyle}
            floated="right"
          >
            Manage Ticket
          </Button>
        ) : ticket.participating ? (
          <Button loading={loading} onClick={cancelAttendance}>
            Cancel attendance
          </Button>
        ) : (
          <Button loading={loading} color="teal" onClick={attendTicket}>
            Join Ticket
          </Button>
        )}
      </Segment>
    </Segment.Group>
  );
};

export default observer(TicketDetailedHeader);
