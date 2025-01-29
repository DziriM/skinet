import { HttpInterceptorFn } from '@angular/common/http';
import { delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';
import { inject } from '@angular/core';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);

  // Start the loading indicator
  busyService.busy();

  return next(req).pipe(
    delay(600),
    finalize(() => busyService.idle())
  );
};
