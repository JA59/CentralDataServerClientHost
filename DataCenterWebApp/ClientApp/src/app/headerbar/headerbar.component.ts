import { Component, Inject } from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { AuthService } from '../Services/auth.service';

@Component({
    selector: "header-bar",
    templateUrl: "./headerbar.component.html",
    styleUrls: ['./headerbar.component.css']
})

export class HeaderBarComponent {
    title: string;

    constructor(private router: Router,
        private fb: FormBuilder,
        public authService: AuthService,
        @Inject('BASE_URL') private baseUrl: string) {

        this.title = "Header Bar";
    }
}
