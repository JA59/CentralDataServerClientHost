// Object provided by AuthService subscription
export class AuthData {

  userToken: string;        // token for the logged on user
  loggedOnUser: string;     // user name of the logged on user, or null if not logged on
  displayName: string;      // display name for logged on user (Guest if not logged on)
  isAdmin: boolean;         // is the current user an administrator?

  constructor(
    userToken: string,
    loggedOnUser: string,
    displayName: string,
    isAdmin: boolean)
  {
    this.userToken = userToken;
    this.loggedOnUser = loggedOnUser;
    this.displayName = displayName;
    this.isAdmin = isAdmin;
  }

  public isLoggedIn(): boolean {
    return (this.loggedOnUser && this.userToken) ? true : false;
  }

  public isLoggedInAsAdmin(): boolean {
    return (this.isLoggedIn() && this.isAdmin);
  }

  public isLoggedInAsUser(): boolean {
    return (this.isLoggedIn() && !this.isAdmin);
  }

  public isLoggedInAsGuest(): boolean {
    return (!this.isLoggedIn());
  }
}
