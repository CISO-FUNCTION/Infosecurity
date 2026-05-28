import { Component } from '@angular/core';
import { BRAND_CONFIG, APP_CONSTANTS } from '../../core/constant/brand-config';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent {
  brandConfig = BRAND_CONFIG;
  appConstants = APP_CONSTANTS;
}
