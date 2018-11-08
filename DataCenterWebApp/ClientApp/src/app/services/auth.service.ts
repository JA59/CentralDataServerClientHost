import { EventEmitter, Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { Subject } from 'rxjs/Subject';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from "@angular/common/http";
import { TokenRequestVM } from '../ViewModels/Authorization/TokenRequestVM';
import { TokenResponseVM } from '../ViewModels/Authorization/TokenResponseVM';
import { AuthData } from '../ViewModels/Authorization/AuthData';
import { Observable } from "rxjs";
import 'rxjs/Rx';

@Injectable()
export class AuthService {
    clientId: string = "iCDataCenterClientHost";  // client identifier passed in login request

    authData: AuthData;
  private authSubject = new Subject<AuthData>();                            // subject for observation
  public authObservable = this.authSubject.asObservable();                  // Observable IInstrumentVM[] stream




    constructor(private http: HttpClient,
      @Inject(PLATFORM_ID) private platformId: any) {
      this.authData = new AuthData(null, null, null, false);
    }

    // Performs the login
    login(username: string, password: string): Observable<boolean> {
      var url = "api/authorization/authorize";
      var loginRequest = <TokenRequestVM>{
            vm_username: username,
            vm_password: password,
            vm_client_id: this.clientId,
            vm_grant_type: "password"                 
        };

        return this.http.post<TokenResponseVM>(url, loginRequest)
            .map((res) => {
                // if the token is there, login has been successful
                if (res && res.vm_token) {
                    // success, so store the response
                  this.setAuth(res);
                    return true;
                }

                // failed login
                return Observable.throw('Unauthorized');
            })
            .catch(error => {
                console.log("auth.service login() error ");
                return new Observable<any>(error);
            });
    }

    // Performs the logout
    logout(): boolean {
        // Clear fields, and set display name to Guest (not logged in)
      this.authData = new AuthData(null, null, "Guest", false);
      this.Notify();
      return true;
    }

    // Store token, username and isadmin into local variables
    setAuth(auth: TokenResponseVM | null): boolean {
      if (auth) {
        this.authData = new AuthData(JSON.stringify(auth), auth.vm_username, auth.vm_username, auth.vm_isadmin);
        this.Notify();
      } 
      return true;
    }

    // Retrieves the user token object (or NULL if none)
    getAuth(): TokenResponseVM | null {
      if (this.authData.userToken) {
        return JSON.parse(this.authData.userToken);
        }
        else {
            return null;
        }
    }

    // Returns true if the user is logged in, false otherwise.
    isLoggedIn(): boolean {
      return (this.authData.loggedOnUser && this.authData.userToken) ? true : false;
    }

    isLoggedInAsAdmin(): boolean {
      return (this.isLoggedIn() && this.authData.isAdmin);
    }

    isLoggedInAsUser(): boolean {
      return (this.isLoggedIn() && !this.authData.isAdmin);
    }

    isLoggedInAsGuest(): boolean {
        return (!this.isLoggedIn());
  }

  private Notify() {
    this.authSubject.next(this.authData);
  }
} 

