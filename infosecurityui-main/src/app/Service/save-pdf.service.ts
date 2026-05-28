import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import { EnvironMent } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SavePdfService {

  constructor(private httpClient:HttpClient) { }

  savePdf(empId:any,empName:any):Observable<any>{
    return this.httpClient.get(`${EnvironMent.localDotNetBaseUri}/api/survey/createAndSavePdf?empName=`+empName+`&empId=`+empId);
  }


}
