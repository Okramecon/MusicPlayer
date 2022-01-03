export class Account {
  id: number;
  userName: string;
  role: Role | null;

  constructor() {
    this.id = 0;
    this.userName = "";
    this.role = null;
  }
}
export default class Role {
  id: number;
  name: string;

  constructor() {
    this.id = 0;
    this.name = "";
  }
}
