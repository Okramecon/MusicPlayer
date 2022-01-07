import Genre from "./models/Genre";
import axios, { AxiosInstance } from "axios";
import { AccountResponse } from "./models/response/AccountResponse";
import Author from "./models/Author";

const apiClient: AxiosInstance = axios.create({
  baseURL: "http://localhost:52455/api",
  headers: {
    "Content-type": "application/json",
  },
});

// Accounts
export async function GetAllAccounts(): Promise<AccountResponse[]> {
  return apiClient.get("/accounts/").then((res) => res.data);
}

// Genres
export async function GetAllGenres(): Promise<Genre[]> {
  return apiClient.get("/genres/").then((res) => res.data);
}

export async function CreateGenre(genreName: string): Promise<Genre> {
  return apiClient
    .post("/genres/", { name: genreName })
    .then((res) => res.data);
}

export async function EditGenre(editingGenre: Genre): Promise<Genre> {
  return apiClient
    .put("/genres/", JSON.stringify(editingGenre))
    .then((res) => res.data);
}

export async function DeleteGenre(deletingId: number): Promise<number> {
  return apiClient.delete("/genres/" + deletingId).then((res) => res.data);
}

// Authors
export async function GetAllAuthors(): Promise<Author[]> {
  return apiClient.get("/authors/").then((res) => res.data);
}

export async function CreateAuthor(author: Author): Promise<Author> {
  return apiClient
    .post("/authors/", JSON.stringify(author))
    .then((res) => res.data);
}

export async function EditAuthor(author: Author): Promise<Author> {
  return apiClient
    .put("/authors/", JSON.stringify(author))
    .then((res) => res.data);
}

export async function DeleteAuthor(id: number): Promise<number> {
  return apiClient.delete("/authors/" + id).then((res) => res.data);
}
