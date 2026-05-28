import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BRAND_CONFIG } from 'src/app/core/constant/brand-config';
import { UserVisitCount } from 'src/app/Model/user-visit-count/user-visit-count';
import { UserVisitCountList } from 'src/app/Model/user-visit-count/user-visit-count-list';
import { UserVisitOneHour } from 'src/app/Model/user-visit-count/user-visit-one-hour';
import { UserVisitCountService } from 'src/app/Service/user-visit-count.service';

@Component({
  selector: 'static-survey',
  templateUrl: './static-survey.component.html',
  styleUrls: ['./static-survey.component.css']
})
export class StaticSurveyComponent {
public userVisitCountList: UserVisitCountList|undefined
public userVisitCoutLength:number|undefined
public userVisitCount:UserVisitCount|undefined
public uservisitCountforOneHour:Array<UserVisitOneHour>|undefined
public uservisitcountonehour:number|undefined
public totalCount:number=0;
public emp_name:any=sessionStorage.getItem('emp_name');
  brandConfig = BRAND_CONFIG;
  constructor(private userVisitCountService:UserVisitCountService,
              private router:Router)
  {
    
  }
  ngOnInit()
  {
    debugger

    this.userVisitCountService.getUserVistCount().subscribe
    (userVistCountList=>
      {this.userVisitCountList=userVistCountList
        console.log(this.userVisitCountList);
        this.userVisitCoutLength=this.userVisitCountList.data.length;
        for(let d of this.userVisitCountList.data)
        {
          this.totalCount=this.totalCount+d.LOGINCOUNT;
        }
      });
      this.userVisitCountService.getUserVisitCountForOneHour().subscribe(
        uservisitCountforOneHourList=>{
          this.uservisitCountforOneHour=uservisitCountforOneHourList.data;
          this.uservisitcountonehour=this.uservisitCountforOneHour?.length;
          console.log(this.uservisitCountforOneHour);
        }
      )
    
    //console.log(this.userVisitCountList?.data);
  }

  //logout
  logOut(){
     sessionStorage.clear();
     this.router.navigate(['/login']);
  }

  checkDOJ(){
  
    let empdoj=sessionStorage.getItem('emp_doj')??'2024-03-20'
    console.log("employe doj"+empdoj);
      if(empdoj>'2024-03-21'){
        return true;
  
    }
    else{
      return false;
    }
  }
  
}
