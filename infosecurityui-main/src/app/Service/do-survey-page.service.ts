import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import { EnvironMent } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DoSurveyPageService {

  constructor(private httpclient:HttpClient) { 

  }

  public  getDoQuestions():Observable<any>
  {
    
   return this.httpclient.get<any>(`${EnvironMent.localDotNetBaseUri}/api/survey/GetDoPageQuestions`);

  }

}
