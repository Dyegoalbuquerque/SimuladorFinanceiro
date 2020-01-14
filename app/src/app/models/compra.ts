import { Parcela } from './parcela';

export class Compra{    
    id: number;      
    valorTotal: number;  
    data: Date; 
    juros: number; 
    quantidadeParcelas: number;
    parcelas: Parcela[];

    constructor(){}
}