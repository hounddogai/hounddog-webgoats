import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Payment } from '../models/payment';
// import {environment} from "src/environments/environment";
import {getApiUrl} from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class PaymentService {


  url = getApiUrl() + '/api/payments';

  constructor(private http: HttpClient) { }

  create(payment: string) {
    return this.http.post<Payment>(this.url, payment);
  }

  findAllByBankAccountId(id: number) {
    return this.http.get<Payment[]>(this.url + '/account/' + id);
  }

  findAll() {
    return this.http.get<Payment[]>(this.url);
  }
}
