import { Pipe, PipeTransform } from '@angular/core';
import { ConfirmationToken } from '@stripe/stripe-js';
import { PaymentSummary } from '../models/order';

@Pipe({
  name: 'paymentCard',
  standalone: true,
})
export class PaymentCardPipe implements PipeTransform {
  transform(
    value?: ConfirmationToken['payment_method_preview'] | PaymentSummary
  ): string {
    // If no payment data is provided, return a default message.
    if (!value) return 'Unknown payment method';

    // payment_method_preview structure.
    if ('card' in value && value.card) {
      const { brand, last4, exp_month, exp_year } = value.card;
      return `${brand.toUpperCase()} **** **** **** ${last4}, Exp: ${exp_month}/${exp_year}`;
    }

    // PaymentSummary structure.
    else if ('last4' in value) {
      const { brand, last4, expMonth, expYear } = value;
      return `${brand.toUpperCase()} **** **** **** ${last4}, Exp: ${expMonth}/${expYear}`;
    }

    // Default message.
    else {
      return 'Unknown payment method';
    }
  }
}
