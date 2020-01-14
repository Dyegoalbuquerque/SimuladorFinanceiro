
import {Component, OnInit, ViewChild, Inject} from '@angular/core';
import { MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource} from '@angular/material/table';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Compra } from '../../models/compra';
import { CompraService } from '../../services/compra.service';
import { Parcela } from 'src/app/models/parcela';


@Component({
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent  implements OnInit {

  constructor(private compraService: CompraService, public dialog: MatDialog) { }
  compra: Compra;
  
  displayedColumns: string[] = ['valor', 'data', 'juros', 'detalhes'];
  dataSource : MatTableDataSource<Compra>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  ngOnInit() {
    this.dataSource = new MatTableDataSource<Compra>([]);
    this.compra = new Compra();
    this.ObterCompras();
  }

  ObterCompras(){          
    this.compraService.ObterCompras().subscribe(data=>{       
       
        for(var i =0; i < data.length; i++)
        {
          var item = data[i];
          item.juros *= 100;
        }
      this.dataSource = new MatTableDataSource<Compra>(data);
      this.dataSource.paginator = this.paginator;
     });  
  } 

  Validar(item:Compra): boolean {

    return item.valorTotal && item.data  && 
           item.juros && item.quantidadeParcelas > 0;
  }

  Simular(): void{

    if(this.Validar(this.compra)){

      this.compraService.Simular(this.compra).subscribe(data=>{ 
        this.compra.juros *= 100;
        data.juros *= 100;
       
        for(var i =0; i < data.parcelas.length; i++)
        {
          var item = data.parcelas[i];
          item.juros *= 100;
        }

        this.OpenDialog(data);
      });
    }  
  }

  OpenDialog(item: Compra): void {
    const dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
      width: '350px',
      height: '600px',
      data: item
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}

@Component({
  selector: 'dialog-overview-example-dialog',
  templateUrl: 'dialog.simular.html',
})
export class DialogOverviewExampleDialog implements OnInit {

  constructor( public dialogRef: MatDialogRef<DialogOverviewExampleDialog>, 
              @Inject(MAT_DIALOG_DATA) public data: Compra,  private _snackBar: MatSnackBar,
              private compraService: CompraService) {}

  displayedColumns: string[] = ['valor', 'juros', 'vencimento'];
  dataSource : MatTableDataSource<Parcela>;
  compra: Compra;

  ngOnInit() {
    
    this.compra = this.data;
    this.dataSource = new MatTableDataSource<Parcela>(this.compra.parcelas);
  }

  CalcularMontante(): number{
    return Compra.CalcularMontante(this.compra.parcelas);
  }

  SalvarSimulacao(): void{
    this.compraService.Salvar(this.compra).subscribe(data=>{
       this.compra = data;
       this.MostrarMensagem("Salvo com sucesso !", "Compra");
       this.Fechar();
     },
     err => { 
       this.MostrarMensagem("Ocorreu um probelam !", "Compra");
    });  
  }

  Fechar(): void {
    this.dialogRef.close();
  }

  MostrarMensagem(mensagem: string, action: string) {
    this._snackBar.open(mensagem, action, {
      duration: 2000,
    });
  }
}
