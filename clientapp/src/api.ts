/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
/* eslint-disable @typescript-eslint/no-explicit-any */
import Genre from "./models/Genre";
import axios, { AxiosInstance } from "axios";
import { AccountResponse } from "./models/response/AccountResponse";
import Author from "./models/Author";
import Playlist from "./models/PlayList";
import Track from "./models/Track";

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

// Playlists
export async function GetAllPlaylists(): Promise<Playlist[]> {
  return apiClient.get("/playlists/").then((res) => res.data);
}

export async function GetPlaylist(id: number): Promise<Playlist> {
  return apiClient.get("/playlists/" + id).then((res) => res.data);
}

export async function CreatePlaylist(playlist: Playlist): Promise<Playlist> {
  return apiClient
    .post("/playlists/", JSON.stringify(playlist))
    .then((res) => res.data);
}

export async function AddTrackToPlaylist(
  playlistId: number,
  trackId: number
): Promise<Track> {
  return apiClient
    .post("/playlists/" + playlistId + "/" + trackId)
    .then((res) => res.data);
}

export async function EditPlaylist(playlist: Playlist): Promise<Playlist> {
  return apiClient
    .put("/playlists/", JSON.stringify(playlist))
    .then((res) => res.data);
}

export async function RemoveTrackFromPlaylist(
  playlistId: number,
  trackId: number
): Promise<number> {
  return apiClient
    .delete("/playlists/" + playlistId + "/" + trackId)
    .then((res) => res.data);
}

export async function DeletePlaylist(id: number): Promise<number> {
  return apiClient.delete("/playlists/" + id).then((res) => res.data);
}

// Tracks
export async function GetAllTracks(): Promise<Track[]> {
  return apiClient.get("/tracks/").then((res) => res.data);
}

// export async function CreateTrack(track: any): Promise<any> {
//   return apiClient
//     .post("/tracks/", JSON.stringify(track))
//     .then((res) => res.data);
// }

export async function EditTrack(track: Track): Promise<Track> {
  return apiClient
    .put("/tracks/", JSON.stringify(track))
    .then((res) => res.data);
}

export async function CreateTrack(track: FormData): Promise<Track> {
  return axios.post("http://localhost:52455/api/tracks/", track, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
}

export async function DeleteTrack(id: number): Promise<number> {
  return apiClient.delete("/tracks/" + id).then((res) => res.data);
}
