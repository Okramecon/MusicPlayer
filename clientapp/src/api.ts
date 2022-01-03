import { AccountResponse } from "./models/response/AccountResponse";
import axios, { AxiosInstance } from "axios";

const apiClient: AxiosInstance = axios.create({
  baseURL: "http://localhost:52455",
  headers: {
    "Content-type": "application/json",
  },
});

export function getAllAccounts(): Promise<AccountResponse[]> {
  return apiClient.get("/accounts/").then((res) => res.data);
}
