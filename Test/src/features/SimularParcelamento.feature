#language: pt-br

Funcionalidade: Simular parcelamento de uma compra
        Como comprador
        Quero simular os valores de uma compra cujo pagamento será parcelado
        Para saber se serei capaz de pagar o montante conforme meu orçamento mensal
 
Cenario: Simular parcelamento informando dados da compra
        Dado um comprador que informou os dados de uma compra
        Quando sistema simular a compra
        Entao o sistema apresenta uma compra com as informações de quantidade de parcelas, valor total, valor do juros e data da compra
        E um montante do valor total que sera pago
        E os valores do juros das parcelas podem ser representados em ate quatro casas decimais
        E os valores da compra como o valor das parcelas e montante devem conter duas casas decimais
        E o valor do montante deve corresponder a soma dos valores das parcelas 
        E a diferença de valores do montante e da soma das parcelas deve ser inferior a um centavo



