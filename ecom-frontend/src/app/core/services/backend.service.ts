import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class BackendService {
    private http = inject(HttpClient);
    private baseUrl = 'http://localhost:5241/api';

    get<T>(endpoint: string, params?: HttpParams): Observable<T> {
        return this.http.get<T>(`${this.baseUrl}${endpoint}`, { params }).pipe(
        catchError(this.handleError)
        );
    }

  post<T>(endpoint: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}${endpoint}`, body).pipe(
      catchError(this.handleError)
    );
  }

    private handleError(error: any) {
        console.error('API Error:', error);
        return throwError(() => error);
    }
}