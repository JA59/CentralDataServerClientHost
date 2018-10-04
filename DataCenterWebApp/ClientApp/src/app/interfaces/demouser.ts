export class DemoUser  {
  Name: string;
  Password: string;
  Enabled: boolean;

  constructor(name: string, password: string) {
    this.Name = name;
    this.Password = password;
    this.Enabled = password.length > 0;
  }
}
