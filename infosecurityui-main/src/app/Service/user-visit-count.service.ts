import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import { UserVisitCountList } from '../Model/user-visit-count/user-visit-count-list';
import { EnvironMent } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'

})
export class UserVisitCountService {
//public y:any;
  constructor(private httpclient:HttpClient) {

   }
   public  getUserVistCount():Observable<UserVisitCountList>
  {
    

     return this.httpclient.get<UserVisitCountList>(`${EnvironMent.localDotNetBaseUri}/api/survey/GetRegularUsersDetails`);

  }
  public getUserVisitCountForOneHour():Observable<any>
  {
    return this.httpclient.get<any>(`${EnvironMent.localDotNetBaseUri}/api/survey/GetRegularUsersDetailsWithInOneHour`);
  }
}
