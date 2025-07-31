import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateOrderDto, EmployeeDto, ProductDto, ShipperDto } from '../models/NewOrderRequest';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class NewOrderService {
  constructor(private http: HttpClient) {}


  getEmployees(): Observable<EmployeeDto[]> {
    return this.http.get<EmployeeDto[]>(`${environment.apiUrl}/Employees`);
  }
  
  getShippers(): Observable<ShipperDto[]> {
    return this.http.get<ShipperDto[]>(`${environment.apiUrl}/Shippers`);
  }
  
  getProducts(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${environment.apiUrl}/Products`);
  }
  
  createOrder(dto: CreateOrderDto): Observable<any> {
    console.log('Creating order with DTO:', dto);
    return this.http.post(`${environment.apiUrl}/Orders`, dto);
  }
}
