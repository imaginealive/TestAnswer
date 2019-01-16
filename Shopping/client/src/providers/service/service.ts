import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';

/*
  Generated class for the ServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class ServiceProvider {
  baseUrl: string = "http://localhost:55426"
  constructor(public http: HttpClient) {
  }

  getAllProdcut() {
    let url = this.baseUrl + "/api/shopmanage";
    return this.http.get(url).map(res => <Product[]>res).toPromise<Product[]>();
  }

  AddNewProduct(name: string, price: number){
    let url = this.baseUrl + "/api/shopmanage/add/" + name + "/" + price;
    return this.http.get<Product>(url).toPromise();
  }

  RemoveProduct(id: string){
    let url = this.baseUrl + "/api/shopmanage/remove/"+ id;
    return this.http.get(url).toPromise();
  }

  GetProduct(id: string){
    let url = this.baseUrl + "/api/shopmanage/product/"+ id;
    return this.http.get(url).map(res => <Product>res).toPromise<Product>();
  }

  GetCart(){
    let url = this.baseUrl + "/api/shopmanage/cart";
    return this.http.get(url).map(res => <Cart>res).toPromise<Cart>();
  }

  AddToCart(id: string, amount: number){
    let url = this.baseUrl + "/api/shopmanage/cart/add/" + id + "/" + amount;
    return this.http.get(url).toPromise();
  }

  RemoveProductInCart(id: string){
    let url = this.baseUrl + "/api/shopmanage/cart/remove/" + id;
    return this.http.get(url).toPromise();
  }

  CleanCart(){
    let url = this.baseUrl + "/api/shopmanage/cart/clean";
    return this.http.get(url).toPromise();
  }
}

export class Product implements IProduct {
  id?: string | undefined;
  name?: string | undefined;
  price: number;

  constructor(data?: IProduct) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(data?: any) {
    if (data) {
      this.id = data["id"];
      this.name = data["name"];
      this.price = data["price"];
    }
  }

  static fromJS(data: any): Product {
    data = typeof data === 'object' ? data : {};
    let result = new Product();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["id"] = this.id;
    data["name"] = this.name;
    data["price"] = this.price;
    return data;
  }
}

export interface IProduct {
  id?: string | undefined;
  name?: string | undefined;
  price: number;
}

export class Cart implements ICart {
  product?: CartProduct[] | undefined;
  discount: number;
  totalBeforeDiscount: number;
  total: number;

  constructor(data?: ICart) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(data?: any) {
    if (data) {
      if (data["product"] && data["product"].constructor === Array) {
        this.product = [];
        for (let item of data["product"])
          this.product.push(CartProduct.fromJS(item));
      }
      this.discount = data["discount"];
      this.totalBeforeDiscount = data["totalBeforeDiscount"];
      this.total = data["total"];
    }
  }

  static fromJS(data: any): Cart {
    data = typeof data === 'object' ? data : {};
    let result = new Cart();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    if (this.product && this.product.constructor === Array) {
      data["product"] = [];
      for (let item of this.product)
        data["product"].push(item.toJSON());
    }
    data["discount"] = this.discount;
    data["totalBeforeDiscount"] = this.totalBeforeDiscount;
    data["total"] = this.total;
    return data;
  }
}

export interface ICart {
  product?: CartProduct[] | undefined;
  discount: number;
  totalBeforeDiscount: number;
  total: number;
}

export class CartProduct extends Product implements ICartProduct {
  amount: number;
  totalBeforeDiscount: number;
  discount: number;

  constructor(data?: ICartProduct) {
    super(data);
  }

  init(data?: any) {
    super.init(data);
    if (data) {
      this.amount = data["amount"];
      this.totalBeforeDiscount = data["totalBeforeDiscount"];
      this.discount = data["discount"];
    }
  }

  static fromJS(data: any): CartProduct {
    data = typeof data === 'object' ? data : {};
    let result = new CartProduct();
    result.init(data);
    return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["amount"] = this.amount;
    data["totalBeforeDiscount"] = this.totalBeforeDiscount;
    data["discount"] = this.discount;
    super.toJSON(data);
    return data;
  }
}

export interface ICartProduct extends IProduct {
  amount: number;
  totalBeforeDiscount: number;
  discount: number;
}

