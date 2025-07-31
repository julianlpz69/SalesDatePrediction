import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerOrder } from '../../models/CustomerOrder';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-order-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './order-modal.component.html',
  styleUrls: ['./order-modal.component.scss']
})
export class OrderModalComponent implements OnInit {
  @Input() customerId!: number;
  @Input() customerName: string = '';
  @Input() close!: () => void;

  orders: CustomerOrder[] = [];
  total = 0;
  page = 1;
  pageSize = 10;
  sortBy = 'orderid';
  sortOrder = 'asc';

  constructor(private customerService: OrderService) {}

  ngOnInit(): void {
    this.fetchOrders();
  }

  fetchOrders(): void {
    this.customerService
      .getOrdersByCustomer(this.customerId, this.page, this.pageSize, this.sortBy, this.sortOrder)
      .subscribe(response => {
        this.orders = response.data;
        this.total = response.total;
      });
  }

  sort(column: string): void {
    if (this.sortBy === column) {
      this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = column;
      this.sortOrder = 'asc';
    }
    this.fetchOrders();
  }

  prevPage(): void {
    if (this.page > 1) {
      this.page--;
      this.fetchOrders();
    }
  }

  nextPage(): void {
    if (this.page * this.pageSize < this.total) {
      this.page++;
      this.fetchOrders();
    }
  }

  getEndIndex(): number {
    return Math.min(this.page * this.pageSize, this.total);
  }
}
