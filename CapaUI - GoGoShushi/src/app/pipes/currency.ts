import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe } from '@angular/common';

@Pipe({ name: 'CustomeCurrency' })
export class CustomCurrencyPipe implements PipeTransform {

    public transform(value: any) {
        return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");;
    }
}