import {Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { CompraService } from '../../services/compra.service';
import { Compra } from 'src/app/models/compra';
import { Parcela } from '../../models/parcela';

@Component({
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent  implements OnInit {


  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  compra: Compra;
  parcelas: Parcela[];
  id: string;
  displayedColumns: string[] = ['valor', 'juros', 'vencimento'];
  dataSource : MatTableDataSource<Parcela>;

  constructor(private route: ActivatedRoute, private compraService: CompraService) { 
    this.route.params.subscribe(res => {this.id = res.id; });
    this.compra = new Compra();
  }

  ngOnInit() {

    this.dataSource = new MatTableDataSource<Parcela>([]);
    this.ObterCompra(this.id);
  }

  ObterCompra(id : string){          
    this.compraService.ObterDetalhe(id).subscribe(data=>{
      this.compra = data;
      this.compra.juros = this.compra.juros * 100;
     
      for(var i =0; i < this.compra.parcelas.length; i++)
      {
        var item = this.compra.parcelas[i];
        item.juros = item.juros * 100;
      }

      this.dataSource = new MatTableDataSource<Parcela>(this.compra.parcelas);
      this.dataSource.paginator = this.paginator;
     }); 
  } 
}