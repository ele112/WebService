import { Injectable } from "@angular/core";
import { ToastController } from '@ionic/angular';

@Injectable()
export class UtilService{

    public static data: any;
    constructor(
        private toast: ToastController,
    ){ }


    public async Toast(message: string, duration: number){
        const toast = await this.toast.create({
            message: message,
            duration: duration,
            position: 'top',
            color: 'primary'
        });

        return toast;
    }


}