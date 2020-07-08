import React from "react";
import { Segment, Grid, Icon } from "semantic-ui-react";
import { ITicket } from "../../../app/models/ticket";
import { format } from "date-fns";

const TicketDetailedInfo: React.FC<{ ticket: ITicket }> = ({ ticket }) => {
  return (
    <Segment.Group>
      <Segment attached="top">
        <Grid>
          <Grid.Column width={1}>
            <Icon size="large" color="teal" name="info" />
          </Grid.Column>
          <Grid.Column width={15}>
            <p>{ticket.description}</p>
          </Grid.Column>
        </Grid>
      </Segment>
      <Segment attached>
        <Grid verticalAlign="middle">
          <Grid.Column width={1}>
            <Icon name="calendar" size="large" color="teal" />
          </Grid.Column>
          <Grid.Column width={15}>
            <span>{format(ticket.dateDeadline, "eeee do MMMM")}</span>
          </Grid.Column>
        </Grid>
      </Segment>
    </Segment.Group>
  );
};

export default TicketDetailedInfo;
