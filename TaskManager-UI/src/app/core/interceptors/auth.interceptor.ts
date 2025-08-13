import { HttpInterceptorFn } from '@angular/common/http';

const BASE_API_URL = 'https://localhost:44310/api'; // Replace with your actual API base URL
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('jwt');
    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }
  
  if (/^https?:\/\//i.test(req.url)) {
    return next(req);
  }

  const apiReq = req.clone({
    url: `${BASE_API_URL}/${req.url}`,
  });

  return next(apiReq);
};
