import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Parcelas } from '../components/detail/parcelas';

@Injectable({
  providedIn: 'root'
})
export class ParcelaService {

  ApiUrl='https://stormy-hollows-04970.herokuapp.com/api/users';    
  constructor(private httpclient: HttpClient) { }    
    
  ObterPorId(login : string):Observable<Parcelas>{    
    return this.httpclient.get<Parcelas>(this.ApiUrl + '/' + login + '/repos');    
  }      
}
