import React, { useContext, useEffect } from "react";
import { Grid } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { RouteComponentProps } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TicketDetailedHeader from "./TicketDetailedHeader";
import TicketDetailedInfo from "./TicketDetailedInfo";
import TicketDetailedChat from "./TicketDetailedChat";
import TicketDetailedSidebar from "./TicketDetailedSidebar";
import { RootStoreContext } from "../../../app/stores/rootStore";

interface DetailsParams {
  id: string;
}

const TicketDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
}) => {
  const rootStore = useContext(RootStoreContext);
  const { ticket, loadTicket, loadingInitial } = rootStore.ticketStore;

  useEffect(() => {
    loadTicket(match.params.id);
  }, [loadTicket, match.params.id]);

  if (loadingInitial) return <LoadingComponent content="Loading ticket" />;

  if (!ticket) return <h2>Ticket not found</h2>;

  return (
    <Grid>
      <Grid.Column width={10}>
        <TicketDetailedHeader ticket={ticket} />
        <TicketDetailedInfo ticket={ticket} />
        <TicketDetailedChat ticket={ticket} />
      </Grid.Column>
      <Grid.Column width={6}>
        <TicketDetailedSidebar attendees={ticket.attendees} />
      </Grid.Column>
    </Grid>
  );
};

export default observer(TicketDetails);
