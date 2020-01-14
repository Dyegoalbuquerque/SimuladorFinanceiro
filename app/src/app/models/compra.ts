import { Parcela } from './parcela';

export class Compra{    
    id: number;      
    valorTotal: number;  
    data: Date; 
    juros: number; 
    quantidadeParcelas: number;
    parcelas: Parcela[];

    constructor(){}

    static CalcularMontante(parcelas: Parcela[]): number {
        let soma = 0;

        if(parcelas == undefined){
            return 0;
        }
        parcelas.forEach(p => {
            soma += p.valor;
        });
        return soma;
    }
}