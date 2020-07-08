import React, { useContext, Fragment } from "react";
import { Item, Label } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import TicketsListItem from "./TicketsListItem";
import { RootStoreContext } from "../../../app/stores/rootStore";

const TicketList: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { ticketsByDate } = rootStore.ticketStore;
  return (
    <Fragment>
      {ticketsByDate.map(([group, tickets]) => (
        <Fragment key={group}>
          <Label size="large" color="teal">
            {group}
          </Label>
          <Item.Group divided>
            {tickets.map((ticket) => (
              <TicketsListItem key={ticket.id} ticket={ticket} />
            ))}
          </Item.Group>
        </Fragment>
      ))}
    </Fragment>
  );
};

export default observer(TicketList);
