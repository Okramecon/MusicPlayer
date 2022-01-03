export class AccountResponse {
  id: number;
  userName: string;
  roleId: number;
  roleName: string;

  constructor() {
    this.id = 0;
    this.userName = "";
    this.roleId = 0;
    this.roleName = "";
  }
}
