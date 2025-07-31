import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerOrder } from '../models/CustomerOrder';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class OrderService {

  constructor(private http: HttpClient) {}

  getOrdersByCustomer(
    customerId: number,
    page = 1,
    pageSize = 10,
    sortBy = 'orderid',
    sortOrder = 'asc'
  ): Observable<{ data: CustomerOrder[]; total: number }> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('sortBy', sortBy)
      .set('sortOrder', sortOrder);
  
    return this.http.get<{ data: CustomerOrder[]; total: number }>(
      `${environment.apiUrl}/Orders/${customerId}/orders`,
      { params }
    );

  }


}
