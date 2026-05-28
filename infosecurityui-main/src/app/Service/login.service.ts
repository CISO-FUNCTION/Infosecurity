import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {EmployeeDetails} from '../Model/employee-details'
import {Observable} from 'rxjs'
import { HttpHeaders } from '@angular/common/http';
import { EnvironMent } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient:HttpClient) { 

  }
  getEmployeeDetails(userName:any):Observable<EmployeeDetails[]>
{

 //const httpParams:HttpParams=new HttpParams();
 //httpParams.append('username',userName);
return this.httpClient.get<EmployeeDetails[]>(`${EnvironMent.localDotNetBaseUri}/api/survey/GetEmployeeDetails?username=`+userName
)
  }
  insertEmployeeRecords(emp_id:any):Observable<number>
  {
    return this.httpClient.get<number>(`${EnvironMent.localDotNetBaseUri}/api/survey/saveUserDetails?Emp_id=`+emp_id)
  }

  loginWithSSO(){
    debugger;
    window.location.href=`https://login.microsoftonline.com/common/oauth2/v2.0/authorize?prompt=select_account&
    client_id=${EnvironMent.clientId}
    &response_type=code
    &redirect_uri=${EnvironMent.redirectUrl}
    &response_mode=query
    &scope=openid%20profile%20email`
    }

    
  




    LoginUser(code:any=null): Observable<any> {
      let url = `${EnvironMent.localDotNetBaseUri}/token`;
      let body = new URLSearchParams();
      body.set('grant_type', 'password');
      body.set('Code', code);  
      const httpopt = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }
      
      return this.httpClient.post<any>(url, body, httpopt)
    }


    
    getUser() {
      let url: string = `${EnvironMent.localDotNetBaseUri}/api/Account/me`;
      const httpopt = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
          //'Authorization':`Bearer ${sessionStorage.getItem('token')}`
        })
      }
      return this.httpClient.get<any>(url, httpopt);
    }

}