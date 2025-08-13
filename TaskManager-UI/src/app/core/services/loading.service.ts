import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LoadingService {
  private _loading = signal(false);
  private requestsInFlight = 0;

  // Public readonly signal
  readonly loading = this._loading.asReadonly();

  show() {
    this.requestsInFlight++;
    this._loading.set(true);
  }

  hide() {
    this.requestsInFlight = Math.max(this.requestsInFlight - 1, 0);
    if (this.requestsInFlight === 0) {
      this._loading.set(false);
    }
  }
}
