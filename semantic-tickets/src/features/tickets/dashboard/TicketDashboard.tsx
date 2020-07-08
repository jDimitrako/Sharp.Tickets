import React, { useContext, useEffect, useState } from "react";
import { Grid, Loader } from "semantic-ui-react";
import TicketList from "./TicketList";
import { observer } from "mobx-react-lite";
import { RootStoreContext } from "../../../app/stores/rootStore";
import InfiniteScroll from "react-infinite-scroller";
import TicketFilters from "./TicketFilters";
import TicketListItemPlaceholder from "./TicketListItemPlaceholder";

const TicketDashboard: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loadTickets,
    loadingInitial,
    setPage,
    page,
    totalPages,
  } = rootStore.ticketStore;
  const [loadingNext, setLoadingNext] = useState(false);

  const handleGetNext = () => {
    setLoadingNext(true);
    setPage(page + 1);
    loadTickets().then(() => setLoadingNext(false));
  };

  useEffect(() => {
    loadTickets();
  }, [loadTickets]);

  return (
    <Grid>
      <Grid.Column width={10}>
        {loadingInitial && page === 0 ? (
          <TicketListItemPlaceholder />
        ) : (
          <InfiniteScroll
            pageStart={0}
            loadMore={handleGetNext}
            hasMore={!loadingNext && page + 1 < totalPages}
            initialLoad={false}
          >
            <TicketList />
          </InfiniteScroll>
        )}
      </Grid.Column>
      <Grid.Column width={6}>
        <TicketFilters />
      </Grid.Column>
      <Grid.Column width={10}>
        <Loader active={loadingNext} />
      </Grid.Column>
    </Grid>
  );
};

export default observer(TicketDashboard);
