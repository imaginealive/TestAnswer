import { Component } from '@angular/core';
import { IonicPage, ToastController } from 'ionic-angular';
import { ServiceProvider, InterestData } from '../../providers/service/service';

/**
 * Generated class for the CalculatorPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-calculator',
  templateUrl: 'calculator.html',
})
export class CalculatorPage {
  principle: number;
  years: number;
  plan: InterestData[];
  constructor(public svc: ServiceProvider, private toastCtrl: ToastController) {
  }

  ionViewDidLoad() {
  }

  InterestCalculate() {
    this.svc.calculator(this.principle, this.years).then(res => {
      this.plan = res;
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
