import { Pipe, PipeTransform } from '@angular/core';
import { ConfirmationToken } from '@stripe/stripe-js';
import { ShippingAddress } from '../models/order';

@Pipe({
  name: 'address',
  standalone: true,
})
export class AddressPipe implements PipeTransform {
  transform(value?: ConfirmationToken['shipping'] | ShippingAddress): string {
    // If no address is provided, return a default message.
    if (!value) return 'Unknown address';

    // Init
    let name = '';
    let line1 = '';
    let line2 = '';
    let city = '';
    let state = '';
    let country = '';
    let postalCode = '';

    // Handle Stripe shipping case (address is inside `value.address`).
    if ('address' in value && value.address) {
      name = value.name ?? '';
      line1 = value.address.line1 ?? '';
      line2 = value.address.line2 ?? '';
      city = value.address.city ?? '';
      state = value.address.state ?? '';
      country = value.address.country ?? '';
      postalCode = value.address.postal_code ?? '';
    }
    // Handle ShippingAddress case (address fields are directly inside `value`).
    else if ('line1' in value) {
      name = value.name ?? '';
      line1 = value.line1 ?? '';
      line2 = value.line2 ?? '';
      city = value.city ?? '';
      state = value.state ?? '';
      country = value.country ?? '';
      postalCode = value.postalCode ?? '';
    }
    // Additional cases can be added here if needed.
    else {
      return 'Unknown address';
    }

    // Format and return the address.
    return `${name ? name + ', ' : ''}${line1}${
      line2 ? ', ' + line2 : ''
    }, ${city}, ${state}, ${postalCode}, ${country}`;
  }
}
