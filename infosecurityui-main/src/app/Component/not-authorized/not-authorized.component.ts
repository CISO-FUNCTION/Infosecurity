import { Component } from '@angular/core';
import { BRAND_CONFIG, APP_CONSTANTS } from '../../core/constant/brand-config';

@Component({
  selector: 'app-not-authorized',
  templateUrl: './not-authorized.component.html',
  styleUrls: ['./not-authorized.component.css']
})
export class NotAuthorizedComponent {
  brandConfig = BRAND_CONFIG;
  appConstants = APP_CONSTANTS;
}
