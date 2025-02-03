import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // The request object is immutable so we need to clone it to add the interceptor to it
  const clonedRequest = req.clone({
    withCredentials: true,
  });

  return next(clonedRequest);
};
