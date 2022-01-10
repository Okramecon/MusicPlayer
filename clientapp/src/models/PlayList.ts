import Track from "./Track";

export default class Playlist {
  id: number;
  name: string;
  summary: string;
  coverUrl: string;
  tracks: Track[];

  constructor() {
    this.id = 0;
    this.name = "";
    this.summary = "";
    this.coverUrl = "";
    this.tracks = [];
  }
}
