import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import { EnvironMent } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class UserSaveFeedbackService {

  constructor(private httpclient:HttpClient) { 


  }

  public  saveUserFeedbackDetail(emp_id:string):Observable<number>
  {
    
   return this.httpclient.get<any>(`${EnvironMent.localDotNetBaseUri}/api/survey/saveUserFeedbackDetails?Emp_id=`+emp_id);

  }
}
