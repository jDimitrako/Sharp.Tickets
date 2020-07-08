import React, { useEffect, useContext } from "react";
import { observer } from "mobx-react-lite";
import { Tab, Grid, Header, Card, Image, TabProps } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { IUserTicket } from "../../app/models/profile";
import { format } from "date-fns";
import { RootStoreContext } from "../../app/stores/rootStore";

const panes = [
  { menuItem: "Future Events", pane: { key: "futureEvents" } },
  { menuItem: "Past Events", pane: { key: "pastEvents" } },
  { menuItem: "Hosting", pane: { key: "hosted" } },
];

const ProfileEvents = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    loadUserTickets,
    profile,
    loadingTickets,
    userTickets,
  } = rootStore.profileStore!;

  useEffect(() => {
    loadUserTickets(profile!.username);
  }, [loadUserTickets, profile]);

  const handleTabChange = (
    e: React.MouseEvent<HTMLDivElement, MouseEvent>,
    data: TabProps
  ) => {
    let predicate;
    switch (data.activeIndex) {
      case 1:
        predicate = "past";
        break;
      case 2:
        predicate = "hosting";
        break;
      default:
        predicate = "future";
        break;
    }
    loadUserTickets(profile!.username, predicate);
  };

  return (
    <Tab.Pane loading={loadingTickets}>
      <Grid>
        <Grid.Column width={16}>
          <Header floated="left" icon="calendar" content={"Tickets"} />
        </Grid.Column>
        <Grid.Column width={16}>
          <Tab
            panes={panes}
            menu={{ secondary: true, pointing: true }}
            onTabChange={(e, data) => handleTabChange(e, data)}
          />
          <br />
          <Card.Group itemsPerRow={4}>
            {userTickets.map((ticket: IUserTicket) => (
              <Card as={Link} to={`/tickets/${ticket.id}`} key={ticket.id}>
                <Image
                  src={`/assets/categoryImages/${ticket.category}.jpg`}
                  style={{ minHeight: 100, objectFit: "cover" }}
                />
                <Card.Content>
                  <Card.Header textAlign="center">{ticket.title}</Card.Header>
                  <Card.Meta textAlign="center">
                    <div>{format(new Date(ticket.date), "do LLL")}</div>
                    <div>{format(new Date(ticket.date), "h:mm a")}</div>
                  </Card.Meta>
                </Card.Content>
              </Card>
            ))}
          </Card.Group>
        </Grid.Column>
      </Grid>
    </Tab.Pane>
  );
};

export default observer(ProfileEvents);
