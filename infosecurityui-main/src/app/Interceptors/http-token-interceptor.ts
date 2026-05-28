import { Injectable} from '@angular/core';
import { HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from '../Service/login.service';

@Injectable()

 export class HttpTokenInterceptor{
     constructor(private loginService:LoginService){}
     intercept(req:HttpRequest<any>,next:HttpHandler):Observable<HttpEvent<any>>{
       
      if (req.headers.has(SkipInterceptor)) {
        const headers = req.headers.delete(SkipInterceptor);
        return next.handle(req.clone({ headers }));
      }
        const headersConfig = {
           'Content-Type': 'application/json',
           'Accept': 'application/json',
           'Authorization':''
          };
          
          const token = sessionStorage.getItem('token');
          if (token) {
           headersConfig['Authorization'] = `bearer ${token}`;
          }
        const request = req.clone({setHeaders:headersConfig})
        return next.handle(request);
     }
    
}

export const SkipInterceptor = "Skip Interceptor";