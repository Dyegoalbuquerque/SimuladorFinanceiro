import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Details } from '../components/detail/details';
import { Compra } from '../models/compra';

@Injectable({
  providedIn: 'root'
})
export class CompraService {

  ApiUrl='https://fortes-tecnologia.herokuapp.com/api/compra';    
  constructor(private httpclient: HttpClient) { }    
    
  ObterCompras():Observable<Compra[]>{    
    return this.httpclient.get<Compra[]>(this.ApiUrl);    
  }    
    
  ObterDetalhe(id:string):Observable<Compra>{    
    return this.httpclient.get<Compra>(this.ApiUrl + '/' + id);    
  }

  Simular(item:Compra):Observable<Compra>{  
    item.juros = item.juros / 100;  
    return this.httpclient.post<Compra>(this.ApiUrl + '/simular', item);    
  }
  
  Salvar(item:Compra):Observable<Compra>{ 
    item.juros = item.juros / 100;   console.log(item);
    return this.httpclient.post<Compra>(this.ApiUrl, item);    
  }
}
