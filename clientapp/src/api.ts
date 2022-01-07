import Genre from "./models/Genre";
import axios, { AxiosInstance } from "axios";
import { AccountResponse } from "./models/response/AccountResponse";

const apiClient: AxiosInstance = axios.create({
  baseURL: "http://localhost:52455/api",
  headers: {
    "Content-type": "application/json",
  },
});

export async function GetAllAccounts(): Promise<AccountResponse[]> {
  return apiClient.get("/account/").then((res) => res.data);
}

export async function GetAllGenres(): Promise<Genre[]> {
  return apiClient.get("/genres/").then((res) => res.data);
}

export async function CreateGenre(genreName: string): Promise<Genre> {
  return apiClient
    .post("/genres/", { name: genreName })
    .then((res) => res.data);
}

export async function EditGenre(editingGenre: Genre): Promise<Genre> {
  console.log(editingGenre);
  return apiClient
    .put("/genres/", JSON.stringify(editingGenre))
    .then((res) => res.data);
}

export async function DeleteGenre(deletingId: number): Promise<number> {
  return apiClient.delete("/genres/" + deletingId).then((res) => res.data);
}
