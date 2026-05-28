import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/Service/login.service';
import { ActivatedRoute } from '@angular/router';
import { BRAND_CONFIG, APP_CONSTANTS } from '../../core/constant/brand-config';


@Component({
  selector: 'login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
guestLogin:boolean=true;
public notValidUserMessage:any;
private isloading:boolean=false;
userData:any;
showLoader:boolean=false;
brandConfig = BRAND_CONFIG;
appConstants = APP_CONSTANTS;



constructor(private loginService:LoginService,private router:Router, private activatedRoute:ActivatedRoute)
{

}
loginSubmit(loginForm:any)
{
  this.loginService.getEmployeeDetails(loginForm.value.userName).subscribe(
    emp_Details=>
    { 
    if(emp_Details.length>0)
      {
      sessionStorage.setItem('user_name',loginForm.value.userName)
      sessionStorage.setItem('emp_id',emp_Details[0].Emp_NewID);
      sessionStorage.setItem('emp_name',emp_Details[0].EMP_NAME);
      //sessionStorage.setItem('emp_isActive',<string>emp_Details.emp_isactive);
      //sessionStorage.setItem('emp_tab_Status',(emp_Details[0].tab_status.toString()));
      sessionStorage.setItem('emp_finalSubmissionDate',emp_Details[0].final_submission_date);
      sessionStorage.setItem('emp_mailid',emp_Details[0].EMP_MAILID);
      sessionStorage.setItem('emp_doj',emp_Details[0].EMP_DATEOFJOINING);
      
      console.log(sessionStorage.getItem('emp_name'));
      console.log(emp_Details[0]);
      this.loginService.insertEmployeeRecords(emp_Details[0].Emp_NewID).subscribe(rows=>{console.log('Employee data is inserted into uservist count='+rows)

      })
      this.router.navigate(['/survey']);
     }
     else{
      this.router.navigate(['/not-authorized']);
      this.notValidUserMessage=this.brandConfig.messages.unauthorizedUser;
     }
    }
   
    
  )
//console.log(loginForm.value);

}

ngOnInit()
{    
    this.BySSOApiCall();
}

BySSOApiCall(){
  let param= this.activatedRoute.snapshot.queryParamMap;

  let getCode=param.get('code');
  if(!getCode)
  {
   this.loginService.loginWithSSO();
  }
 else{
   let param= this.activatedRoute.snapshot.queryParamMap;
   let getCode:string|null=param.get('code');
   if(getCode){
         this.SLoginOffice(getCode);
    }
 }
}


SLoginOffice(code: string) {
  //this.showLoader=true
  debugger;
  this.loginService.LoginUser(code).subscribe(res => {
  this.userData = res;
  console.log('user data ',this.userData);
  sessionStorage.setItem('token',res.access_token)
  debugger
  this.getDetailsUser();
  },
    (error) => {
      
    })
}


getDetailsUser() {
  debugger

  this.loginService.getUser().subscribe(
    (res: any) => {
      debugger
       //sessionStorage.setItem('emp_id',res.EmpNewId);
       //let userName=res.DomainId.replace('IGGLOBAL\\','')
       let userName=res.DomainId
       this.loginService.getEmployeeDetails(userName).subscribe(
        emp_Details=>
        {
         if(emp_Details.length>0)
         {
          sessionStorage.setItem('user_name',userName)
          sessionStorage.setItem('emp_id',emp_Details[0].Emp_NewID);
          sessionStorage.setItem('emp_name',emp_Details[0].EMP_NAME);
          //sessionStorage.setItem('emp_isActive',<string>emp_Details.emp_isactive);
          //sessionStorage.setItem('emp_tab_Status',(emp_Details[0].tab_status.toString()));
          sessionStorage.setItem('emp_finalSubmissionDate',emp_Details[0].final_submission_date);
          sessionStorage.setItem('emp_mailid',emp_Details[0].EMP_MAILID);
          console.log(sessionStorage.getItem('emp_name'));
          console.log(emp_Details[0]);
          this.loginService.insertEmployeeRecords(emp_Details[0].Emp_NewID).subscribe(rows=>{console.log('Employee data is inserted into uservist count='+rows)
    
          })
          this.showLoader=false;
          this.router.navigate(['/survey']);
         }
         else{
          this.showLoader=false;
          this.router.navigate(['/not-authorized']);
          this.notValidUserMessage=this.brandConfig.messages.unauthorizedUser;
         }
        }
       
        
      )
       console.log("user details : ",res);
      
    },
    (error) => {
      this.showLoader=false;
    }
  )
}







}
