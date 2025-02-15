export interface User {
  firstName: string;
  lastName: string;
  email: string;
  address: Address;
  roles: string | string[];
}

export interface Address {
  line1: string;
  line2?: string;
  city: string;
  state: string;
  country: string;
  postalCode: string;
}
