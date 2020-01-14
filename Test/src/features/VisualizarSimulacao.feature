#language: pt-br

Funcionalidade: Visualizar simulações de compras salvas
        Como comprador
        Quero visualizar minhas simulações salvas
        Para acessar suas informações

Cenario: Visualizar simulações de compras persistidas anteriormente
        Dado simulações persistidas anteriormente
        Quando sistema visualizar as simulações de compra
        Entao o sistema apresenta simulações de compras persistidas
        E o sistema apresenta detalhes de simulações como quantidade de parcelas, valor total, valor do juros e data da compra