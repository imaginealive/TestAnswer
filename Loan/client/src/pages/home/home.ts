import { Component } from '@angular/core';
import { ToastController } from 'ionic-angular';
import { ServiceProvider } from '../../providers/service/service';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  rateDisplay: number;
  rateInput: number;
  constructor(public svc: ServiceProvider, private toastCtrl: ToastController) {
  }

  ionViewDidLoad() {
    this.getInterest();
  }

  setInterest() {
    this.svc.set(this.rateInput).then(() => {
      this.getInterest();
    });
  }

  getInterest() {
    this.svc.get().then(res => {
      this.rateDisplay = res;
      this.rateInput = res;
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
