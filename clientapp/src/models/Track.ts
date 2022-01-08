export default class Track {
  id: number;
  name: string;
  summary: string;
  text: string;
  coverUrl: string;
  musicUrl: string;
  authorId: number;

  constructor() {
    this.id = 0;
    this.name = "";
    this.summary = "";
    this.text = "";
    this.coverUrl = "";
    this.musicUrl = "";
    this.authorId = 0;
  }
}
