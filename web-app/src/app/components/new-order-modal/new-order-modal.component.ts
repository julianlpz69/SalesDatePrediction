import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NewOrderService } from '../../services/new-order.service';
import { CreateOrderDto, EmployeeDto, ProductDto, ShipperDto } from '../../models/NewOrderRequest';

@Component({
  selector: 'app-new-order-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './new-order-modal.component.html',
  styleUrls: ['./new-order-modal.component.scss']
})
export class NewOrderModalComponent implements OnInit {
  @Input() customerId!: number;
  @Input() customerName: string = '';
  @Input() close!: () => void;

  order: CreateOrderDto = {
    customerId: 0,
    employeeId: 0,
    shipperId: 0,
    shipName: '',
    shipAddress: '',
    shipCity: '',
    shipCountry: '',
    freight: 0,
    requiredDate: new Date(),
    shippedDate: new Date(),
    orderDate: new Date(), 
    productId: 0,
    unitPrice: 0,
    quantity: 0,
    discount: 0
  };

  submitted = false;
  products: ProductDto[] = [];
  shippers: ShipperDto[] = [];
  employees: EmployeeDto[] = [];

  constructor(private orderService: NewOrderService) {}

  ngOnInit(): void {
    this.order.customerId = this.customerId;
    this.orderService.getEmployees().subscribe(data => this.employees = data);
    this.orderService.getShippers().subscribe(data => this.shippers = data);
    this.orderService.getProducts().subscribe(data => this.products = data);
  }

  save(): void {
    if (this.isValid()) {
      this.normalizeDecimalValues();
      this.orderService.createOrder(this.order).subscribe(() => {
        this.submitted = true;
        setTimeout(() => this.close(), 1500);
      });
    } else {
      alert('Please fill out all the required fields.');
    }
  }

  private normalizeDecimalValues(): void {
    const parse = (value: any): number =>
      typeof value === 'string' ? parseFloat(value.replace(',', '.')) : Number(value);

    this.order.freight = parse(this.order.freight);
    this.order.unitPrice = parse(this.order.unitPrice);
    this.order.discount = parse(this.order.discount);

    if (!isNaN(this.order.discount) && this.order.discount > 1) {
      this.order.discount = this.order.discount / 100;
    }
  }

  isValid(): boolean {
    const o = this.order;
    return (
      o.employeeId > 0 &&
      o.shipperId > 0 &&
      o.shipName.trim() !== '' &&
      o.shipAddress.trim() !== '' &&
      o.shipCity.trim() !== '' &&
      o.shipCountry.trim() !== '' &&
      !!o.orderDate &&
      !!o.requiredDate &&
      !!o.shippedDate &&
      o.freight >= 0 &&
      o.productId > 0 &&
      o.unitPrice >= 0 &&
      o.quantity > 0
    );
  }
}
