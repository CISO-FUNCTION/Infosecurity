import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SurveyPageComponent } from './Component/survey-page/survey-page.component';
import { StaticSurveyComponent } from './Component/survey-page/static-survey/static-survey.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { DontSurveyPageComponent } from './Component/survey-page/dont-survey-page/dont-survey-page.component';

import { LoginPageComponent } from './Component/login-page/login-page.component'
import { FormsModule,  ReactiveFormsModule } from '@angular/forms';
import { ThankYouComponent } from './Component/thank-you/thank-you.component';
import { ErrorPageComponent } from './Component/error-page/error-page.component';
import { HttpTokenInterceptor } from './Interceptors/http-token-interceptor';
import { NotAuthorizedComponent } from './Component/not-authorized/not-authorized.component';

@NgModule({
  declarations: [
    AppComponent,
    SurveyPageComponent,
    StaticSurveyComponent,
    DontSurveyPageComponent,
    LoginPageComponent,
    ThankYouComponent,
    ErrorPageComponent,
    NotAuthorizedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule 
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpTokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
