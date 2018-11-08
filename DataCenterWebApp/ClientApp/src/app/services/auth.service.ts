import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Subject, Observable } from 'rxjs';
import { AuthData } from '../ViewModels/Authorization/AuthData';
import { TokenRequestVM } from '../ViewModels/Authorization/TokenRequestVM';
import { TokenResponseVM } from '../ViewModels/Authorization/TokenResponseVM';

@Injectable()
export class AuthService {
  private clientId: string = "iCDataCenterClientHost";                      // client identifier passed in login request
  private authSubject = new Subject<AuthData>();                            // subject for observation

  public authData: AuthData;                                                // Data structure containing current logged on user information
  public authObservable = this.authSubject.asObservable();                  // Observable for the latest AuthData

  // Constructor
  constructor(private http: HttpClient) {
    // We start out as a guest
    this.setAsGuest();
  }

  // Performs the login
  public login(username: string, password: string): Observable<boolean> {
    var url = "api/authorization/authorize";

    // build the request view model
    var loginRequest = <TokenRequestVM>{
          vm_username: username,
          vm_password: password,
          vm_client_id: this.clientId,
          vm_grant_type: "password"                 
    };

    return Observable.throw('Unauthorized');

    // send the request and process the response
    //return this.http.post<TokenResponseVM>(url, loginRequest)
    //  .map((res) => {
    //      // if the token is there, login has been successful
    //      if (res && res.vm_token) {
    //          // success, so store the response
    //        this.setAuth(res);
    //          return true;
    //      }

    //      // failed login
    //      return Observable.throw('Unauthorized');
    //  })
    //  .catch(error => {
    //      console.log("auth.service login() error ");
    //      return new Observable<any>(error);
    //  });
  }

  // Performs the login (the angular 7 way !!!)
  public login7(username: string, password: string) : boolean {
    var url = "api/authorization/authorize";

    // build the request view model
    var loginRequest = <TokenRequestVM>{
      vm_username: username,
      vm_password: password,
      vm_client_id: this.clientId,
      vm_grant_type: "password"
    };


    // send the request and process the response
    this.http.post<TokenResponseVM>(url, loginRequest)
      .subscribe((res) => {
        // if the token is there, login has been successful
        if (res && res.vm_token) {
          // success, so store the response
          this.setAuth(res);
        }
      });

    return true;
  }

  // Logout the currently logged on user
  public logout(): boolean {
    // when logging out, we return to being a guest
    this.setAsGuest();
    this.Notify();
    return true;
  }

  // Retrieves the user token object (or NULL if none) (needed by the HTTP Interceptor)
  public getAuth(): TokenResponseVM | null {
    if (this.authData.userToken) {
      return JSON.parse(this.authData.userToken);
    } else {
       return null;
    }
  }

  // Returns true if and only if the user is logged in
  public isLoggedIn(): boolean {
    return this.authData.isLoggedIn();
  }

  // Returns true if and only if the user is logged on as an admin
  public isLoggedInAsAdmin(): boolean {
    return this.authData.isLoggedInAsAdmin();
  }

  // Returns true if and only if the user is logged on as a user (not as an admin)
  public isLoggedInAsUser(): boolean {
    return this.authData.isLoggedInAsUser();
  }

  // Returns true if and only if the user is not logged in
  public isLoggedInAsGuest(): boolean {
    return this.authData.isLoggedInAsGuest();
  }

  // Private methods

  private setAuth(auth: TokenResponseVM | null): boolean {
    if (auth) {
      this.authData = new AuthData(JSON.stringify(auth), auth.vm_username, auth.vm_username, auth.vm_isadmin);
      this.Notify();
    }
    return true;
  }

  private setAsGuest() {
    this.authData = new AuthData(null, null, "Guest", false);
  }

  private Notify() {
    this.authSubject.next(this.authData);
  }
} 

