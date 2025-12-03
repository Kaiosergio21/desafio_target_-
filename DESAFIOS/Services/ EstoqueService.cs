using Target.Models;
using System.Text.Json;

namespace Target.Services
{
    public class EstoqueService
    {
        // Método principal responsável por realizar movimentações de estoque
        public void Movimentar()
        {
            // Lê o conteúdo do arquivo JSON que armazena o estoque
            string json = File.ReadAllText("Data/estoque.json");

            // Converte o JSON em objeto C# e evita null reference
            var dados = JsonSerializer.Deserialize<EstoqueRoot>(json);

            if (dados == null || dados.estoque == null)
            {
                Console.WriteLine("Erro ao carregar arquivo de estoque.");
                return;
            }

            Console.WriteLine("\n===== MOVIMENTAÇÃO DE ESTOQUE =====");

            // Solicita ao usuário o código do produto
            Console.Write("Código do produto: ");
            string? entradaCodigo = Console.ReadLine();

            if (!int.TryParse(entradaCodigo, out int codigo))
            {
                Console.WriteLine("Código inválido!");
                return;
            }

         // Gera ID único
            string idMov = Guid.NewGuid().ToString();

            // Procura o produto no JSON pelo código informado
            var produto = dados.estoque.FirstOrDefault(p => p.codigoProduto == codigo);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                return;
            }

              // Exibe dados finais
            Console.WriteLine("\nMovimentação registrada!");
            Console.WriteLine($"ID: {idMov}");
            Console.WriteLine($"Produto: {produto.descricaoProduto}");
            Console.WriteLine($"Novo estoque: {produto.estoque}");

            // Pergunta se é entrada ou saída
            Console.Write("Entrada (E) ou Saída (S)? ");
            string? tipo = Console.ReadLine()?.ToUpper();

            if (tipo != "E" && tipo != "S")
            {
                Console.WriteLine("Tipo inválido!");
                return;
            }

            // Lê quantidade de forma segura
            Console.Write("Quantidade: ");
            string? entradaQtd = Console.ReadLine();

            if (!int.TryParse(entradaQtd, out int qtd))
            {
                Console.WriteLine("Quantidade inválida!");
                return;
            }

   

            // Aplica movimentação
            if (tipo == "E")
                produto.estoque += qtd;
            else
                produto.estoque -= qtd;

            // Exibe dados finais
            Console.WriteLine("\nMovimentação registrada!");
            Console.WriteLine($"ID: {idMov}");
            Console.WriteLine($"Produto: {produto.descricaoProduto}");
            Console.WriteLine($"Novo estoque: {produto.estoque}");

            // Salva o JSON de volta
            File.WriteAllText(
                "Data/estoque.json",
                JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true })
            );
        }
    }
}
