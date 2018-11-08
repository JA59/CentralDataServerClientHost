import { Injectable, Injector } from "@angular/core";
import { HttpHandler, HttpEvent, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { AuthService } from "./auth.service";
import { Observable } from "rxjs";

// Injects a JWT token (for the logged in user if any) into the header of any http request
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private injector: Injector) { }

  // Intercept http requests, and add the JWT token (if any) to the header
  intercept(
      request: HttpRequest<any>,
      next: HttpHandler): Observable<HttpEvent<any>>
    {
      var auth = this.injector.get(AuthService);
      var token = (auth.isLoggedIn()) ? auth.getAuth()!.vm_token : null;
      if (token) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        });
      }
      return next.handle(request);
    }
}
