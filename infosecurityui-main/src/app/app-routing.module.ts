import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyPageComponent } from './Component/survey-page/survey-page.component';
import { authGuardGuard } from './guards/auth-guard.guard';
import { LoginPageComponent } from './Component/login-page/login-page.component';
import { ThankYouComponent } from './Component/thank-you/thank-you.component';
import { ErrorPageComponent } from './Component/error-page/error-page.component';
import { NotAuthorizedComponent } from './Component/not-authorized/not-authorized.component';

const routes: Routes = [
  {
    path:'',redirectTo:'survey',pathMatch:'full'
    
  },
  {
    path:'survey',
    component:SurveyPageComponent,
    canActivate:[authGuardGuard]
  
  },
  {
    path:'login',component:LoginPageComponent
  },
  {
    path:'thankyou',component:ThankYouComponent,
    canActivate:[authGuardGuard]
  },
  {
    path:'error',
    component:ErrorPageComponent
  },
  {
    path:'not-authorized',
    component:NotAuthorizedComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
