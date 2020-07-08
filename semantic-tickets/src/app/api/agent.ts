import axios, { AxiosResponse } from "axios";
import { ITicket, ITicketsEnvelope } from "../models/ticket";
import { history } from "../..";
import { toast } from "react-toastify";
import { IUser, IUserFormValues } from "../models/user";
import { IProfile, IPhoto } from "../models/profile";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

axios.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem("jwt");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error) => {
  if (error.message === "Network Error" && !error.response) {
    toast.error("Network error - make sure API is running!");
  }
  const { status, data, config, headers } = error.response;
  if (status === 404) {
    history.push("/notfound");
  }
  if (status === 401) {
    const bearer: string = headers["WWW-Authenticate"];
    if (bearer.indexOf("The token expired at") > 0) {
      window.localStorage.removeItem("jwt");
      history.push("/");
      toast.info("Your session has expired, please login again");
    }
  }
  if (
    status === 400 &&
    config.method === "get" &&
    data.errors.hasOwnProperty("id")
  ) {
    history.push("/notfound");
  }
  if (status === 500) {
    toast.error("Server error - check the terminal for more info!");
  }
  throw error.response;
});

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
  postForm: (url: string, file: Blob) => {
    let formData = new FormData();
    formData.append("File", file);
    return axios
      .post(url, formData, {
        headers: { "Content-type": "multipart/form-data" },
      })
      .then(responseBody);
  },
};

const Tickets = {
  list: (params: URLSearchParams): Promise<ITicketsEnvelope> =>
    axios.get("/tickets", { params: params }).then(responseBody),
  details: (id: string) => requests.get(`/tickets/${id}`),
  update: (ticket: ITicket) => requests.put(`/tickets/${ticket.id}`, ticket),
  create: (ticket: ITicket) => requests.post(`/tickets`, ticket),
  delete: (id: string) => requests.delete(`/tickets/${id}`),
  attend: (id: string) => requests.post(`/tickets/${id}/attend`, {}),
  unattend: (id: string) => requests.delete(`/tickets/${id}/attend`),
};

const User = {
  current: (): Promise<IUser> => requests.get("/user"),
  login: (user: IUserFormValues): Promise<IUser> =>
    requests.post(`/user/login`, user),
  register: (user: IUserFormValues): Promise<IUser> =>
    requests.post(`/user/register`, user),
};

const Profiles = {
  get: (username: string): Promise<IProfile> =>
    requests.get(`/profiles/${username}`),
  uploadPhoto: (photo: Blob): Promise<IPhoto> =>
    requests.postForm(`/photos`, photo),
  setMainPhoto: (id: string) => requests.post(`/photos/${id}/setMain`, {}),
  deletePhoto: (id: string) => requests.delete(`/photos/${id}`),
  follow: (username: string) =>
    requests.post(`/profiles/${username}/follow`, {}),
  unfollow: (username: string) =>
    requests.delete(`/profiles/${username}/follow`),
  listFollowings: (username: string, predicate: string) =>
    requests.get(`/profiles/${username}/follow?predicate=${predicate}`),
  listTickets: (username: string, predicate: string) =>
    requests.get(`/profiles/${username}/tickets?predicate=${predicate}`),
};

export default {
  Tickets,
  User,
  Profiles,
};
