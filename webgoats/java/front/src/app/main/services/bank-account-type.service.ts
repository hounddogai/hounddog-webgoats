import { Injectable } from '@angular/core';
import { BankAccType } from '../models/bank-acc-type';
import { HttpClient } from '@angular/common/http';
// import {environment} from "src/environments/environment";
import {getApiUrl} from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class BankAccountTypeService {


  url = getApiUrl() + '/api/bankaccounttypes';

  constructor(private http: HttpClient) { }

  findAll() {
    return this.http.get<BankAccType[]>(this.url);
  }

  findById(id: any) {
    return this.http.get<BankAccType>(this.url + '/' + id);
  }

  update(id: any, result: any) {
    return this.http.put<BankAccType>(this.url + '/' + id, result);
  }

}
