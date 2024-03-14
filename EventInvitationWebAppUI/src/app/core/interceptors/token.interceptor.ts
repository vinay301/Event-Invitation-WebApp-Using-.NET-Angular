// import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
// import { AuthService } from '../../modules/auth/services/auth.service';
// import { Router } from '@angular/router';
// import { NgToastService } from 'ng-angular-popup';
// import { Observable, catchError, switchMap, throwError } from 'rxjs';
// import { TokenApiModel } from '../models/token.api.model';

// export class TokenInterceptor implements HttpInterceptor {
//   constructor(private service : AuthService, private router : Router, private toast : NgToastService) { }
//   intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
//     const myToken = this.service.getToken();
//       if(myToken){
//         req = req.clone({
//           setHeaders: {Authorization : `Bearer ${myToken}`}
//         })
//       }
//       return next.handle(req)
//       .pipe(
//         catchError((err:any)=>{
//           console.log(err);
//           if(err instanceof HttpErrorResponse)
//           {
//             if(err.status === 401)
//             {
//               //this.toast.warning({detail:"WARN",summary:'Token is Expired,Please Login Again',duration:5000});
//               //this.router.navigate(['login']);

//               //handle request
//               return this.handleUnAuthorizedError(req,next);
//             }
//           }
//           return throwError(()=>new Error("Some Other Error Occurred!"))
//         })
//       );
//   }
//   // handleUnAuthorizedError(req : HttpRequest<any>, next : HttpHandler){
//   //   const tokenApiModel = new TokenApiModel();
//   //   tokenApiModel.accessToken =  this.service.getToken()!;
//   //   tokenApiModel.refreshToken = this.service.getRefreshToken()!;

//   //   return this.service.renewToken(tokenApiModel).pipe(
//   //     switchMap((data:TokenApiModel) => {
//   //       this.service.storeRefreshToken(data.refreshToken);
//   //       this.service.storeToken(data.accessToken);
//   //       req = req.clone({
//   //         setHeaders: {Authorization : `Bearer ${data.accessToken}`}
//   //       })
//   //       return next.handle(req)
//   //     }),
//   //     catchError((err)=>{
//   //       return throwError(()=>{
//   //         this.toast.warning({detail:"WARN",summary:'Token is Expired,Please Login Again',duration:5000});
//   //         this.router.navigate(['login']);

//   //       })
//   //     })
//   //   );
//   // }
// }

