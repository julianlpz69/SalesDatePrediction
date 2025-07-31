export interface ProductDto {
  productId: number;
  productName: string;
  unitPrice: number;
}

export interface ShipperDto {
  shipperId: number;
  companyName: string;
}

export interface EmployeeDto {
  empid: number;
  firstname: string;
  lastname: string;
}

export interface CreateOrderDto {
  customerId: number;
  employeeId: number;
  shipperId: number;
  shipName: string;
  shipAddress: string;
  shipCity: string;
  shipCountry: string;
  freight: number;
  orderDate: Date;
  requiredDate: Date;
  shippedDate: Date;
  productId: number;
  unitPrice: number;
  quantity: number;
  discount: number;
}
