import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TruncateTextPipe } from './truncate/truncate-text.pipe';
import { CustomCurrencyPipe } from './currency';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    TruncateTextPipe,
    CustomCurrencyPipe
  ],
  declarations: [
    TruncateTextPipe,
    CustomCurrencyPipe
  ]
})
export class PipeModule { }