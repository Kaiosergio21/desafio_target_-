﻿using Target.Services;

class Program
{
    static void Main()
    {
        // Loop principal do menu
        while (true)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1 - Calcular Comissão");
            Console.WriteLine("2 - Movimentar Estoque");
            Console.WriteLine("3 - Calcular Juros por Atraso");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string? op = Console.ReadLine();

            // Direciona para o serviço escolhido
            switch (op)
            {
                case "1":
                    new ComissaoService().Calcular(); // Processa comissões
                    break;

                case "2":
                    new EstoqueService().Movimentar(); // Entrada ou saída de estoque
                    break;

                case "3":
                    new JurosService().CalcularJuros(); // Cálculo de juros por atraso
                    break;

                case "0":
                    return; // Encerra o programa

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
}
