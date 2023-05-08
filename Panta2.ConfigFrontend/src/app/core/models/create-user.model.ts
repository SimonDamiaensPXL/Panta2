export class UserCreation {
    userName: string;
    firstName: string;
    lastName: string;
    password: string;
    confirmpassword: string;
    companyId: number;

    constructor(userName: string, firstName: string, lastName: string, password: string, confirmpassword: string, companyId: number) {
      this.userName = userName;
      this.firstName = firstName;
      this.lastName = lastName;
      this.password = password;
      this.confirmpassword = confirmpassword;
      this.companyId = companyId;
    }
  }