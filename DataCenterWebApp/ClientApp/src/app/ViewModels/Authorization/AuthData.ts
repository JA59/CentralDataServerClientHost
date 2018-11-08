export class AuthData {

  userToken: string;        // token for the logged on user
  loggedOnUser: string;     // user name of the logged on user, or null if not logged on
  displayName: string;   // display name for logged on user (Guest if not logged on)
  isAdmin: boolean;               // is the current usr an administrator

  constructor(
    userToken: string,
    loggedOnUser: string,
    displayName: string,
    isAdmin: boolean,
  ) {
    this.userToken = userToken;
    this.loggedOnUser = loggedOnUser;
    this.displayName = displayName;
    this.isAdmin = isAdmin;
  }
}
