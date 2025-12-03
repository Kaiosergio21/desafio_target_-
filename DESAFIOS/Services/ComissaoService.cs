using Target.Models;
using System.Text.Json;

namespace Target.Services
{
    public class ComissaoService
    {
        public void Calcular()
        {
            string json = File.ReadAllText("Data/vendas.json");
            var dados = JsonSerializer.Deserialize<VendaRoot>(json);

            if (dados == null || dados.vendas == null)
            {
                Console.WriteLine("Erro ao carregar JSON.");
                return;
            }


            // ============================
            // VENDAS DETALHADAS (COM COMISSÃO)
            // ============================
     Console.WriteLine("\n===== VENDAS DETALHADAS POR VENDEDOR =====\n");
        Console.WriteLine("\n===REGRAS===\n");
           Console.WriteLine("\nVendas abaixo de R$100,00 não gera comissão ");
              Console.WriteLine("\nVendas abaixo de R$500,00 gera 1% de comissão");
                 Console.WriteLine("\nVendas a partir de R$500,00 gera 5% de comissão\n");

var grupos = dados.vendas
    .GroupBy(v => v.vendedor); // Aqui agrupamos por vendedor

foreach (var grupo in grupos)
{
    Console.WriteLine($"\n===== {grupo.Key} =====\n"); // Nome do vendedor

    double totalComissao = 0; // Acumula o total de cada vendedor

    foreach (var v in grupo)
    {
        double comissao = CalcularComissao(v.valor);
        totalComissao += comissao; // soma a comissão

        Console.WriteLine($"Valor da Venda: R${v.valor:F2} — Comissão: R${comissao:F2}");
    }

    // Exibe o total da pessoa logo abaixo do grupo
    Console.WriteLine($"\nTotal de Comissão de {grupo.Key}: R${totalComissao:F2}");
}


            // ============================
            // TOTAL DE COMISSÕES POR VENDEDOR
            // ============================
       
            Console.WriteLine("\n===========================================");
            Console.WriteLine("        PROCESSAMENTO FINALIZADO");
            Console.WriteLine("===========================================\n");
        }

        public double CalcularComissao(double valor)
        {
            if (valor < 100) return 0;
            if (valor < 500) return valor * 0.01;
            return valor * 0.05;
        }

       
    }
}
