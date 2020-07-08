import React, { useState, useContext, useEffect } from "react";
import { Segment, Form, Button, Grid } from "semantic-ui-react";
import { TicketFormValues } from "../../../app/models/ticket";
import { v4 as uuid } from "uuid";
import { observer } from "mobx-react-lite";
import { RouteComponentProps } from "react-router-dom";
import { Form as FinalForm, Field } from "react-final-form";
import TextInput from "../../../app/common/form/TextInput";
import TextAreaInput from "../../../app/common/form/TextAreaInput";
import SelectInput from "../../../app/common/form/SelectInput";
import { category } from "../../../app/common/options/categoryOptions";
import DateInput from "../../../app/common/form/DateInput";
import { combineDateAndTime } from "../../../app/common/util/util";
import {
  combineValidators,
  isRequired,
  composeValidators,
  hasLengthGreaterThan,
} from "revalidate";
import { RootStoreContext } from "../../../app/stores/rootStore";

const validate = combineValidators({
  title: isRequired("Title"),
  category: isRequired("Category"),
  description: composeValidators(
    isRequired("Description"),
    hasLengthGreaterThan(4)({
      message: "Description needs to be at least 5 characters",
    })
  )(),
  date: isRequired("Date"),
  time: isRequired("Time"),
});

interface DetailsParams {
  id: string;
}

const TicketForm: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
    createTicket,
    editTicket,
    submitting,
    loadTicket,
  } = rootStore.ticketStore;

  const [ticket, setTicket] = useState(new TicketFormValues());
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (match.params.id) {
      setLoading(true);
      loadTicket(match.params.id)
        .then((ticket) => setTicket(new TicketFormValues(ticket)))
        .finally(() => setLoading(false));
    }
  }, [loadTicket, match.params.id]);

  // const handleSubmit = () => {
  //   if (ticket.id.length === 0) {
  //     let newTicket = {
  //       ...ticket,
  //       id: uuid(),
  //     };
  //     createTicket(newTicket).then(() =>
  //       history.push(`/tickets/${newTicket.id}`)
  //     );
  //   } else {
  //     editTicket(ticket).then(() => history.push(`/tickets/${ticket.id}`));
  //   }
  // };

  const handleFinalFormSubmit = (values: any) => {
    const dateAndTime = combineDateAndTime(values.date, values.time);
    const { date, time, ...ticket } = values;
    ticket.dateDeadline = dateAndTime;
    if (!ticket.id) {
      let newTicket = {
        ...ticket,
        id: uuid(),
      };
      createTicket(newTicket).then(() =>
        history.push(`/tickets/${newTicket.id}`)
      );
    } else {
      editTicket(ticket).then(() => history.push(`/tickets/${ticket.id}`));
    }
    console.log(ticket);
  };

  return (
    <Grid>
      <Grid.Column width={10}>
        <Segment clearing>
          <FinalForm
            validate={validate}
            initialValues={ticket}
            onSubmit={handleFinalFormSubmit}
            render={({ handleSubmit, invalid, pristine }) => (
              <Form onSubmit={handleSubmit} loading={loading}>
                <label>Title</label>
                <Field
                  name="title"
                  placeholder="Title"
                  value={ticket.title}
                  component={TextInput}
                />
                <label>Description</label>
                <Field
                  name="description"
                  placeholder="Description"
                  rows={3}
                  value={ticket.description}
                  component={TextAreaInput}
                />
                <label>Priority</label>
                <Field
                  component={TextInput}
                  name="priority"
                  placeholder="Priority"
                  value={ticket.priority}
                />
                <label>Category</label>
                <Field
                  component={SelectInput}
                  options={category}
                  name="category"
                  placeholder="Category"
                  value={ticket.category}
                />
                <label>Deadline</label>
                <Form.Group widths="equal">
                  <Field
                    component={DateInput}
                    name="date"
                    date={true}
                    placeholder="Deadline"
                    value={ticket.dateDeadline}
                  />
                  <Field
                    component={DateInput}
                    name="time"
                    time={true}
                    placeholder="Deadline"
                    value={ticket.time}
                  />
                </Form.Group>

                <Button
                  loading={submitting}
                  disabled={loading || invalid || pristine}
                  floated="right"
                  color="teal"
                  type="submit"
                  content="Submit"
                />
                <Button
                  onClick={
                    ticket.id
                      ? () => history.push(`/tickets/${ticket.id}`)
                      : () => history.push("/tickets")
                  }
                  disabled={loading || invalid || pristine}
                  floated="right"
                  content="Cancel"
                />
              </Form>
            )}
          />
        </Segment>
      </Grid.Column>
    </Grid>
  );
};

export default observer(TicketForm);
