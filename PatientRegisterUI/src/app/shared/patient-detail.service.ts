import { PatientDetail } from './patient-detail.model';
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { mapChildrenIntoArray } from '@angular/router/src/url_tree';

@Injectable({
  providedIn: 'root'
})
export class PatientDetailService {
  formData: PatientDetail= {
    Address: null,
    Name: null,
    DOB: null,
    Id: null
  };
  
  readonly rootURL = 'http://localhost:59035/api';
  list : PatientDetail[];

  constructor(private http: HttpClient) { }

  postPatientDetail() {
    return this.http.post(this.rootURL + '/patient', this.formData);
  }
  putPatientDetail() {
    return this.http.put(this.rootURL + '/patient/'+ this.formData.Id, this.formData);
  }
  deletePatientDetail(id) {
    return this.http.delete(this.rootURL + '/patient/'+ id);
  }

  refreshList(){
    this.http.get(this.rootURL + '/patient')
    .toPromise()
    .then(res => this.list = res as PatientDetail[]);
  }
}
