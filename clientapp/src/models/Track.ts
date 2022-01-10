export default class Track {
  id: number;
  name: string;
  summary: string;
  text: string;
  coverUrl: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  upload: any;
  musicUrl: string;
  authorId: number;
  howl: unknown | null;

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
