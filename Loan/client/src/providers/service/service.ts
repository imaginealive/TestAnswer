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
  baseUrl: string = "http://localhost:50959"
  constructor(public http: HttpClient) {
  }

  get() {
    let url = this.baseUrl + "/api/Loan";
    return this.http.get(url).map(res => <number>res).toPromise<number>();
  }

  set(rate: number) {
    let url = this.baseUrl + "/api/Loan/" + rate;
    return this.http.get(url).map(res => <number>res).toPromise();
  }

  calculator(principle: number,years: number){
    let url = this.baseUrl + "/api/Loan/" + principle + "/" + years;
    return this.http.get(url).map(res => <InterestData[]>res).toPromise<InterestData[]>();
  }
}

export class InterestData implements IInterestData {
  yearCount: number;
  principle: number;
  interest: number;
  total: number;

  constructor(data?: IInterestData) {
      if (data) {
          for (var property in data) {
              if (data.hasOwnProperty(property))
                  (<any>this)[property] = (<any>data)[property];
          }
      }
  }

  init(data?: any) {
      if (data) {
          this.yearCount = data["yearCount"];
          this.principle = data["principle"];
          this.interest = data["interest"];
          this.total = data["total"];
      }
  }

  static fromJS(data: any): InterestData {
      data = typeof data === 'object' ? data : {};
      let result = new InterestData();
      result.init(data);
      return result;
  }

  toJSON(data?: any) {
      data = typeof data === 'object' ? data : {};
      data["yearCount"] = this.yearCount;
      data["principle"] = this.principle;
      data["interest"] = this.interest;
      data["total"] = this.total;
      return data; 
  }
}

export interface IInterestData {
  yearCount: number;
  principle: number;
  interest: number;
  total: number;
}