import { Component} from "@angular/core";
import { AuthService } from '../../services/auth.service';

@Component({
    selector: "instruments-page",
  templateUrl: "./instruments-page.component.html",
  styleUrls: ['./instruments-page.component.css']
})

export class InstrumentsPageComponent {
  title = "Instruments";

    constructor(private authService: AuthService) {
  }
}
