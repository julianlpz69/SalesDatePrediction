import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { CustomerPrediction } from '../../models/CustomerPrediction';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrderModalComponent } from '../../components/order-modal/order-modal.component';
import { NewOrderModalComponent } from "../../components/new-order-modal/new-order-modal.component";

@Component({
  selector: 'app-customer-predictions',
  standalone: true,
  imports: [CommonModule, FormsModule, OrderModalComponent, NewOrderModalComponent],
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.scss']
})
export class CustomerPredictionsComponent implements OnInit {
  customers: CustomerPrediction[] = [];
  total = 0;
  page = 1;
  pageSize = 10;
  sortBy = 'customername';
  sortOrder = 'asc';
  search = '';

  // 👇 Modal control
  showModal = false;
  selectedCustomerId!: number;
  selectedCustomerName = '';

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    this.customerService
      .getPredictions(this.page, this.pageSize, this.sortBy, this.sortOrder, this.search)
      .subscribe(response => {
        this.customers = response.data;
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
    this.fetchData();
  }

  prevPage(): void {
    if (this.page > 1) {
      this.page--;
      this.fetchData();
    }
  }

  nextPage(): void {
    if (this.page * this.pageSize < this.total) {
      this.page++;
      this.fetchData();
    }
  }

  getEndIndex(): number {
    return Math.min(this.page * this.pageSize, this.total);
  }

  showNewOrderModal = false;


  closeNewOrderModal(): void {
    this.showNewOrderModal = false;
  }

  viewOrders(customer: CustomerPrediction): void {
    this.selectedCustomerId = customer.customerId;
    this.selectedCustomerName = customer.customerName;
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
  }

  newOrder(customer: CustomerPrediction): void {
    this.selectedCustomerId = customer.customerId;
    this.selectedCustomerName = customer.customerName;
    this.showNewOrderModal = true;
  }
  
}
