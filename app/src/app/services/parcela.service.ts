import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Parcela } from '../models/parcela';

@Injectable({
  providedIn: 'root'
})
export class ParcelaService {

  ApiUrl='https://fortes-tecnologia.herokuapp.com/api/parcela';    
  constructor(private httpclient: HttpClient) { }    

  
  ObterParcelas():Observable<Parcela[]>{    
    return this.httpclient.get<Parcela[]>(this.ApiUrl);    
  }
}
