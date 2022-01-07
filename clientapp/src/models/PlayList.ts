export default class Playlist {
  id: number;
  name: string;
  summary: string;
  coverUrl: string;

  constructor() {
    this.id = 0;
    this.name = "";
    this.summary = "";
    this.coverUrl = "";
  }
}
