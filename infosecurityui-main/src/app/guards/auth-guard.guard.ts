import { CanActivateFn, Router } from '@angular/router';
import {EmployeeDetails} from '../Model/employee-details';
import {inject} from '@angular/core';

export const authGuardGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
 const emp_id=sessionStorage.getItem("emp_id");
 if(emp_id==='undefined' || emp_id===null)
 {
  router.navigate(['/login']);
  return false;
 
 }
 else
 {
  return true;
 }
  
};
