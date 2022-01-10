/* eslint-disable @typescript-eslint/no-explicit-any */
export default class Track {
  id: number;
  name: string;
  summary: string;
  text: string;
  coverUrl: string;
  upload: any;
  musicUrl: string;
  authorId: number;
  howl: any | null;

  constructor() {
    this.id = 0;
    this.name = "";
    this.summary = "";
    this.text = "";
    this.coverUrl = "";
    this.upload = {};
    this.musicUrl = "";
    this.authorId = 0;
    this.howl = null;
  }
}
