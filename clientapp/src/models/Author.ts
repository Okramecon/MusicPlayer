export default class Genre {
  id: number;
  fullName: string;
  pseudonym: string;
  bio: string;

  constructor() {
    this.id = 0;
    this.fullName = "";
    this.pseudonym = "";
    this.bio = "";
  }
}
