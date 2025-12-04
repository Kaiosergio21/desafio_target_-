using Target.Models;
using System.Text.Json;

namespace Target.Services
{
    public class EstoqueService
    {
        private const string CaminhoEstoque = "Data/estoque.json";
        private const string CaminhoLog = "Data/log_movimentacoes.json";

        public void Movimentar()
        {
            // ======================
            // Valida se arquivos existem
            // ======================
            if (!File.Exists(CaminhoEstoque))
            {
                Console.WriteLine("Arquivo de estoque não encontrado!");
                return;
            }

            // ======================
            // Carrega estoque
            // ======================
            string json = File.ReadAllText(CaminhoEstoque);

            var dados = JsonSerializer.Deserialize<EstoqueRoot>(json);

            if (dados == null || dados.estoque == null)
            {
                Console.WriteLine("Erro ao carregar o estoque. JSON pode estar vazio ou inválido.");
                return;
            }

            Console.WriteLine("\n===== MOVIMENTAÇÃO DE ESTOQUE =====");
            Console.Write("Código do produto: ");
            string? entradaCodigo = Console.ReadLine();

            if (!int.TryParse(entradaCodigo, out int codigo))
            {
                Console.WriteLine("Código inválido!");
                return;
            }

            var produto = dados.estoque.FirstOrDefault(p => p.codigoProduto == codigo);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                return;
            }

            Console.Write("Entrada (E) ou Saída (S)? ");
            string? tipo = Console.ReadLine()?.ToUpper();

            if (tipo != "E" && tipo != "S")
            {
                Console.WriteLine("Tipo inválido!");
                return;
            }

            Console.Write("Quantidade: ");
            string? entradaQtd = Console.ReadLine();

            if (!int.TryParse(entradaQtd, out int qtd))
            {
                Console.WriteLine("Quantidade inválida!");
                return;
            }

            if (qtd <= 0)
            {
                Console.WriteLine("A quantidade deve ser maior que zero!");
                return;
            }

            if (tipo == "S" && qtd > produto.estoque)
            {
                Console.WriteLine("Quantidade maior que o estoque disponível!");
                return;
            }

            // ======================
            // Atualiza estoque
            // ======================
            if (tipo == "E") produto.estoque += qtd;
            else produto.estoque -= qtd;

            string idMov = Guid.NewGuid().ToString();

            Console.WriteLine("\nMovimentação registrada com sucesso!");
            Console.WriteLine($"ID Movimentação: {idMov}");
            Console.WriteLine($"Produto: {produto.descricaoProduto}");
            Console.WriteLine($"Novo estoque: {produto.estoque}");

            File.WriteAllText(CaminhoEstoque,
                JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true })
            );

            // ======================
            // Carrega ou cria log
            // ======================
            MovimentacoesRoot log;

            if (File.Exists(CaminhoLog))
            {
                string jsonLog = File.ReadAllText(CaminhoLog);
                log = JsonSerializer.Deserialize<MovimentacoesRoot>(jsonLog) ?? new MovimentacoesRoot();
            }
            else
            {
                log = new MovimentacoesRoot();
            }

            // ======================
            // Registra a movimentação
            // ======================
            log.movimentacoes.Add(new Movimentacao
            {
                Id = idMov,
                CodigoProduto = produto.codigoProduto,
                DescricaoProduto = produto.descricaoProduto,
                Tipo = tipo,
                Quantidade = qtd,
                DataHora = DateTime.Now
            });

            File.WriteAllText(CaminhoLog,
                JsonSerializer.Serialize(log, new JsonSerializerOptions { WriteIndented = true })
            );
        }
    }
}
