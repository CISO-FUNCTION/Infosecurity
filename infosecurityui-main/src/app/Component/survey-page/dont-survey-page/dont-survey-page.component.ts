import { Component, ElementRef, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { DoSurveyPageService } from 'src/app/Service/do-survey-page.service';
import { DontSurveyPageService } from 'src/app/Service/dont-survey-page.service';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms'
import { UserSaveFeedbackService } from 'src/app/Service/user-save-feedback.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from 'src/app/Service/login.service';
import { SavePdfService } from 'src/app/Service/save-pdf.service';
import { BRAND_CONFIG, APP_CONSTANTS } from '../../../core/constant/brand-config';
//import * as $ from 'jquery'
@Component({
  selector: 'dont-survey-page',
  templateUrl: './dont-survey-page.component.html',
  styleUrls: ['./dont-survey-page.component.css']
})
export class DontSurveyPageComponent {
  public tab_stat: any = sessionStorage.getItem('emp_tab_Status');
  dontSurveyQuestionList: any;
  public emp_name: any;
  public emp_id: any;
  public date: any;
  public tab_Status: any;
  public getDate = new Date();
  public SubmitForm= new FormGroup({});
  public disbale: boolean = true;
  doSurveyQuestionList: any;
  public formCheckBox: any[] = [];
  public formValues: any = {};
  public isAllChecked: boolean = false
  public isFormSubmitted: number = 0
  public formSubmittedMsg: string = ''
  public showLoader:boolean=false;
  public alertText:string='';
  brandConfig = BRAND_CONFIG;
  appConstants = APP_CONSTANTS;

  @ViewChildren('checkboxes') checkboxes?: QueryList<ElementRef>;
  @ViewChild('alertmodel') alertModel?: ElementRef;
  @ViewChild('confirmmodel') confirmmodel?: ElementRef;
  @ViewChild('agree') agree?: ElementRef;

  constructor(
    private dontSurveyPageService: DontSurveyPageService,
    private doSurveyPageService: DoSurveyPageService,
    private userSaveFeedbackService: UserSaveFeedbackService,
    private loginService: LoginService,
    private savePdfService:SavePdfService,
    private router: Router,
    private route:ActivatedRoute
  ) { }

  ngOnInit() {
      debugger
      
      this.loginService.getEmployeeDetails(sessionStorage.getItem('user_name'))
               .subscribe(
                (emp_details)=>{
                   this.tab_Status= emp_details[0].tab_status
                  this.date=emp_details[0].final_submission_date
                  },
                  (eror)=>{
                    debugger
                    this.router.navigate(['/error'])
                  }

               ) 


    this.dontSurveyPageService.getDontQuestions()
      .subscribe(dontPageSurveyQuestion => {
        this.dontSurveyQuestionList = dontPageSurveyQuestion.data
        console.log('Do Not service call', this.dontSurveyQuestionList);
        for (let i = 0; i < dontPageSurveyQuestion.data.length; i++) {

          this.formCheckBox.push(
            {
              "label": 'ques' + dontPageSurveyQuestion.data[i].id
            }

          )
          //   console.log('FormCheckBoxes.....',this.formCheckBox);
        }

        // code for do api call staring
debugger
        this.doSurveyPageService.getDoQuestions()
          .subscribe(doPageSurveyQuestion => {
            this.doSurveyQuestionList = doPageSurveyQuestion.data
            //   console.log('Do Service Call',this.doSurveyQuestionList);
            for (let i = 0; i < doPageSurveyQuestion?.data?.length; i++) {
              this.formCheckBox.push(
                {
                  "label": 'ques' + doPageSurveyQuestion.data[i].id
                }
              )
              for (let j = 0; j < this.formCheckBox.length; j++) {
                this.formValues[this.formCheckBox[j].label] = new FormControl(false);

              }
              //console.log('vivrk',this.formValues);
              this.SubmitForm = new FormGroup(this.formValues);

            }

          },
          (eror)=>{
              debugger
            this.router.navigate(['/error'])
          }
          )


        // code for do api ending

        //console.log('Form Controls Value',this.formValues);
      },

      (eror)=>{
        debugger
        this.router.navigate(['/error'])
      }

      )





    this.emp_name = sessionStorage.getItem('emp_name');
    this.emp_id = sessionStorage.getItem('emp_id');
    //this.date = sessionStorage.getItem('emp_finalSubmissionDate');
    //this.tab_Status = sessionStorage.getItem('emp_tab_Status');

  }
  validateForm() {
    const a = this.SubmitForm.value;
    console.log("Pradhuman validate form");
    
    this.isAllChecked = false

    if (!this.agree?.nativeElement.checked) {
      this.alertText='Please enable the checkbox for abide the Policies'
      this.openAlertModal();
    }

    let checkFlag = 1
    this.checkboxes?.forEach((el: ElementRef) => {
      if (!el.nativeElement.checked) {
        checkFlag = 0
        el.nativeElement.closest('td').classList.add('highlight');
        this.alertText='Please respond to the pending highlighted activity.'
        this.openAlertModal();
      }
      else {
        el.nativeElement.closest('td').classList.remove('highlight');
      }
    }
    )

    if (checkFlag == 1 && this.agree?.nativeElement.checked) {
      this.isAllChecked = true
    }



    if (this.isAllChecked) {
      this.openConfirmationModel();
    }
  }

  sumitSurveyForm() {

    this.closeConfirmationModel()
    this.showLoader=true

    
      this.savePdfService.savePdf(sessionStorage.getItem('emp_id'),sessionStorage.getItem('emp_name'))
          .subscribe();
      this.showLoader=false;
      
    

    this.userSaveFeedbackService.saveUserFeedbackDetail(this.emp_id)
      .subscribe((status) => {
        if (status > 0) {
          this.isFormSubmitted = 1
          this.formSubmittedMsg = 'Form Submitted Successfully!'
          //calling pdf save api..
        } else {
          this.formSubmittedMsg = 'Somethings Went Wrong!, Pls try again later.'
          this.showLoader=false;
        }
        //alert(this.formSubmittedMsg)

        if(status>0){
          this.router.navigate(['/thankyou']);
        }
       
      }, (error) => {
        this.formSubmittedMsg = 'Internal Server Error!, Pls try again later.'
        this.showLoader=false;
        alert(this.formSubmittedMsg)
      });
  }



  openAlertModal() {
    this.alertModel?.nativeElement.classList.add('show');
  }
  closeAlertModal() {
    this.alertModel?.nativeElement.classList.remove('show');
  }
  openAgreementModal() {
    this.alertText='Please enable the checkbox for abide the Policies'
    this.alertModel?.nativeElement.classList.add('show');
  }
  closeAgreementModal() {
    this.alertModel?.nativeElement.classList.remove('show');
  }
  
  openConfirmationModel() {
    this.confirmmodel?.nativeElement.classList.add('show');
  }
  closeConfirmationModel() {
    this.confirmmodel?.nativeElement.classList.remove('show');
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
