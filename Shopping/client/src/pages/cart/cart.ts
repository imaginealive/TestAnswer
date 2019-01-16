import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import { Cart, ServiceProvider, Product, CartProduct } from '../../providers/service/service';

/**
 * Generated class for the CartPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-cart',
  templateUrl: 'cart.html',
})
export class CartPage {
  cart: Cart = new Cart();
  constructor(public navCtrl: NavController, public navParams: NavParams, public svc: ServiceProvider, private toastCtrl: ToastController) {
  }

  ionViewDidLoad() {
    this.getCart();
  }

  getCart() {
    this.svc.GetCart().then(res => {
      this.cart = res;
    }, error => {
      this.presentToast(error.message);
    })
  }

  removeProduct(item: CartProduct) {
    this.svc.RemoveProductInCart(item.id).then(() => {
      this.getCart();
    }, error => {
      this.presentToast(error.message);
    })
  }

  pay() {
    this.svc.CleanCart().then(() => {
      this.presentToast("You get payment "+ this.cart.total +", Cart Reset");
      this.getCart();
    }, error => {
      this.presentToast(error.message);
    })
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
