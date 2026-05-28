import { Component } from '@angular/core';
import { BRAND_CONFIG, APP_CONSTANTS } from '../../core/constant/brand-config';

@Component({
  selector: 'thank-you',
  templateUrl: './thank-you.component.html',
  styleUrls: ['./thank-you.component.css']
})
export class ThankYouComponent {
  brandConfig = BRAND_CONFIG;
  appConstants = APP_CONSTANTS;
}
