import { Component } from '@angular/core';
import { NavController, ToastController } from 'ionic-angular';
import { ServiceProvider, Product } from '../../providers/service/service';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  products: Product[];
  constructor(public navCtrl: NavController, public svc: ServiceProvider, public toastCtrl: ToastController) {
  }

  ionViewDidLoad() {
    this.getAllProduct();
  }

  getAllProduct() {
    this.svc.getAllProdcut().then(res => {
      this.products = res;
    }, error => {
      this.presentToast(error.message);
    });
  }

  removeProduct(item: Product) { 
    this.svc.RemoveProduct(item.id).then(() => {
      this.presentToast("Removed " + item.name + " from Shop");
      this.getAllProduct();
    }, error => {
      this.presentToast(error.message);
    })
  }

  addToCart(item: Product) {
    this.svc.AddToCart(item.id, 1).then(() => {
      this.presentToast("Added Product " + item.name);
    }, error => {
      this.presentToast(error.message);
    });
  }

  presentToast(text) {
    let toast = this.toastCtrl.create({
      message: text,
      duration: 3000,
      position: 'top'
    });

    toast.onDidDismiss(() => {
      console.log('Dismissed toast');
    });

    toast.present();
  }
}
