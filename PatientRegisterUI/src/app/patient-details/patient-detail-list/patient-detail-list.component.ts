import { PatientDetail } from '../../shared/patient-detail.model';
import { PatientDetailService } from '../../shared/patient-detail.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-patient-detail-list',
  templateUrl: './patient-detail-list.component.html',
  styles: []
})
export class PatientDetailListComponent implements OnInit {

  constructor(private service: PatientDetailService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.service.refreshList();
  }

  populateForm(pd: PatientDetail) {
    this.service.formData = Object.assign({}, pd);
  }

  onDelete(Id) {
    if (confirm('Are you sure to delete this record ?')) {
      this.service.deletePatientDetail(Id)
        .subscribe(res => {
          debugger;
          this.service.refreshList();
          this.toastr.warning('Deleted successfully', 'Patient Detail Register');
        },
          err => {
            debugger;
            console.log(err);
          })
    }
  }

}
