import { Component, Inject } from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { AuthService } from '../../Services/auth.service';
import { DemoUser } from '../login/demouser';

@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    styleUrls: ['./login.component.css']
})

export class LoginComponent {
    title: string;
    form: FormGroup | undefined;
    items: DemoUser[];


    constructor(private router: Router,
        private fb: FormBuilder,
        private authService: AuthService) {

        this.title = "Login";

        // initialize the form
        this.createForm();

        this.items = new Array(5);
        this.items[0] = new DemoUser("Guest", "");
        this.items[1] = new DemoUser("SomeUser", "USER1");
        this.items[2] = new DemoUser("SomeAdmin", "ADMIN1");
        this.items[3] = new DemoUser("Joe", "JOE");
        this.items[4] = new DemoUser("Ed", "ED");
    }

    createForm() {
        this.form = this.fb.group({
            Username: ['', Validators.required],
            Password: ['', Validators.required]});
    }

    onSubmit() {
        if (this.form == undefined)
            return;
        var username = this.form.value.Username;
        var password = this.form.value.Password;

        this.doLogin(username, password);
    }

    doLogin(username: string, password: string) {

        this.authService.login(username, password)
            .subscribe(res => {
                // login successful
                this.authService.setAuth(res);
            },
            err => {
                // login failed
                console.log(err)
                if (this.form == undefined)
                return;
                this.form.setErrors({
                    "auth": "Incorrect username or password"
                });
            });
    }

    quickLogin(username: string, password: string) {
        if (username == "Guest") {
            this.authService.logout();
        }
        else {
            this.authService.loginNoWait(username, password);
        }
    }

    onBack() {
        this.router.navigate(["login"]);
    }

    // retrieve a FormControl
    getFormControl(name: string) {
        if (this.form == undefined)
            return;
        return this.form.get(name);
    }

      // returns TRUE if the FormControl is valid
      isValid(name: string) {
          var e = this.getFormControl(name);
          return e && e.valid;
      }

      // returns TRUE if the FormControl has been changed
      isChanged(name: string) {
          var e = this.getFormControl(name);
          return e && (e.dirty || e.touched);
      }

      // returns TRUE if the FormControl is invalid after user changes
      hasError(name: string) {
          var e = this.getFormControl(name);
          return e && (e.dirty || e.touched) && !e.valid;
      }

      logout() {
        this.authService.logout();
        if (this.form == undefined)
            return;
        this.form.value.Password = '';
      }
}
