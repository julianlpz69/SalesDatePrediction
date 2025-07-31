import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerPrediction } from '../models/CustomerPrediction';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  getOrdersByCustomer(customerId: number, page: number, pageSize: number, sortBy: string, sortOrder: string) {
    throw new Error('Method not implemented.');
  }

  constructor(private http: HttpClient) {}

  getPredictions(
    page = 1,
    pageSize = 10,
    sortBy = 'customername',
    sortOrder = 'asc',
    search: string = ''
  ): Observable<{ data: CustomerPrediction[]; total: number }> {
    let params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('sortBy', sortBy)
      .set('sortOrder', sortOrder)
      .set('search', search);

    return this.http.get<{ data: CustomerPrediction[]; total: number }>(
      `${environment.apiUrl}/Customers/predictions`,
      { params }
    );
  }
}
