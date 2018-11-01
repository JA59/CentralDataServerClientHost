import { EventEmitter, Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { TokenRequestVM } from '../ViewModels/Authorization/TokenRequestVM';
import { TokenResponseVM } from '../ViewModels/Authorization/TokenResponseVM';
import { Observable } from "rxjs";
import 'rxjs/Rx';

@Injectable()
export class AuthService {
    clientId: string = "iCDataCenterClientHost";  // client identifier passed in login request
    userToken: string | null = null;        // token for the logged on user
    loggedOnUser: string | null = null;     // user name of the logged on user, or null if not logged on
    displayName: string | null = "Guest";   // display name for logged on user (Guest if not logged on)
    isAdmin: boolean = false;               // is the current usr an administrator


    constructor(private http: HttpClient,
        @Inject(PLATFORM_ID) private platformId: any) {
    }

    // Performs the login
    login(username: string, password: string): Observable<boolean> {
      var url = "api/token/auth";
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
        this.loggedOnUser = null;
        this.userToken = null;
        this.displayName = "Guest";
        this.isAdmin = false;
        return true;
    }

    // Store token, username and isadmin into local variables
    setAuth(auth: TokenResponseVM | null): boolean {
        if (auth) {
            this.userToken = JSON.stringify(auth);
            this.loggedOnUser = auth.vm_username;
            this.displayName = auth.vm_username;
            this.isAdmin = auth.vm_isadmin;
        } 
        return true;
    }

    // Retrieves the user token object (or NULL if none)
    getAuth(): TokenResponseVM | null {
        if (this.userToken) {
            return JSON.parse(this.userToken);
        }
        else {
            return null;
        }
    }

    // Returns true if the user is logged in, false otherwise.
    isLoggedIn(): boolean {
        return (this.loggedOnUser && this.userToken) ? true : false;
    }

    isLoggedInAsAdmin(): boolean {
        return (this.isLoggedIn() && this.isAdmin);
    }

    isLoggedInAsUser(): boolean {
        return (this.isLoggedIn() && !this.isAdmin);
    }

    isLoggedInAsGuest(): boolean {
        return (!this.isLoggedIn());
    }
} 

