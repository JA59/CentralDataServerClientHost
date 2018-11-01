import { Component, Inject } from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { AuthService } from '../Services/auth.service';

@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    styleUrls: ['./login.component.css']
})

export class LoginComponent {
    title: string;
    form: FormGroup | undefined;
  currentUser: string | null;
  isAdmin: boolean;

    constructor(private router: Router,
        private fb: FormBuilder,
        private authService: AuthService,
        @Inject('BASE_URL') private baseUrl: string) {

        this.title = "Login";
        this.currentUser = authService.loggedOnUser;
        this.isAdmin = authService.isAdmin;

        // initialize the form
        this.createForm();

    }

    createForm() {
        this.form = this.fb.group({
            Username: ['', Validators.required],
            Password: ['', Validators.required]
        });


    }

    onSubmit() {
        if (this.form == undefined)
            return;
        var url = this.baseUrl + "api/token/auth";
        var username = this.form.value.Username;
        var password = this.form.value.Password;

        this.authService.login(username, password)
            .subscribe(res => {
                // login successful

                // outputs the login info through a JS alert.
                // IMPORTANT: remove this when test is done.
                //alert("Login successful! "
                //    + "USERNAME: "
                //    + username
                //    + " TOKEN: "
                //    + this.authService.getAuth()!.token
                //);

              this.currentUser = this.authService.loggedOnUser;
              this.isAdmin = this.authService.isAdmin;
                this.router.navigate(["experiments"]);
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

    doLogin(username: string, password: string) {

        var url = this.baseUrl + "api/token/auth";

        this.authService.login(username, password)
            .subscribe(res => {
                // login successful

                // outputs the login info through a JS alert.
                // IMPORTANT: remove this when test is done.
                //alert("Login successful! "
                //    + "USERNAME: "
                //    + username
                //    + " TOKEN: "
                //    + this.authService.getAuth()!.token
                //);

                this.currentUser = this.authService.loggedOnUser;
              this.isAdmin = this.authService.isAdmin;
                //this.router.navigate(["home"]);
            },
                err => {
                    // login failed
                    console.log(err)
                });

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
        this.currentUser = this.authService.loggedOnUser;
        if (this.form == undefined)
            return;
        this.form.value.Password = '';
    }
}
