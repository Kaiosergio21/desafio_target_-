using System.Globalization;
using System.Text.Json;
using Target.Models;

namespace Target.Services
{
    public class JurosService
    {
        private const string CaminhoLog = "Data/log_dividas.json";

        // Método usado no console
        public void CalcularJuros()
        {
            Console.Write("Valor da conta: ");
            string? entrada = Console.ReadLine();

            if (!double.TryParse(entrada, out double valor))
            {
                Console.WriteLine("Valor inválido!");
                return;
            }

            Console.Write("Data de vencimento (DD/MM/AAAA): ");
            string? entradaData = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entradaData))
            {
                Console.WriteLine("Data inválida!");
                return;
            }

            // Remove tudo que não é número
            string apenasNumeros = new string(entradaData.Where(char.IsDigit).ToArray());
            if (apenasNumeros.Length == 8)
                entradaData = $"{apenasNumeros.Substring(0, 2)}/{apenasNumeros.Substring(2, 2)}/{apenasNumeros.Substring(4, 4)}";

            if (!DateTime.TryParseExact(
                    entradaData,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime vencimento))
            {
                Console.WriteLine("Data inválida! Use DD/MM/AAAA.");
                return;
            }

            Console.WriteLine($"Data formatada: {entradaData}");

            var resultado = CalcularJurosAtraso(valor, vencimento);

            if (resultado.dias == 0)
            {
                Console.WriteLine("Conta dentro do prazo, sem juros.");
            }
            else
            {
                Console.WriteLine($"\nDias em atraso: {resultado.dias}");
                Console.WriteLine($"Juros: R${resultado.juros:F2}");
                Console.WriteLine($"Total atualizado: R${resultado.total:F2}");
            }

            // ======================
            // Registrar log
            // ======================
            DividasRoot log;

            if (File.Exists(CaminhoLog))
            {
                string jsonLog = File.ReadAllText(CaminhoLog);
                log = JsonSerializer.Deserialize<DividasRoot>(jsonLog) ?? new DividasRoot();
            }
            else
            {
                log = new DividasRoot();
            }

            log.Dividas.Add(new DividaLog
            {
                Id = Guid.NewGuid().ToString(),
                ValorOriginal = valor,
                DataVencimento = entradaData,
                DiasAtraso = resultado.dias,
                Juros = resultado.juros,
                TotalAtualizado = resultado.total,
                DataHoraCalculo = DateTime.Now
            });

            // 🔥 Importante: garantir que a pasta exista
            Directory.CreateDirectory("Data");

            // Salvar log
            File.WriteAllText(
                CaminhoLog,
                JsonSerializer.Serialize(log, new JsonSerializerOptions { WriteIndented = true })
            );

            Console.WriteLine("Dívida registrada no log com sucesso!");
        }

        // Método de cálculo
        public (int dias, double juros, double total) CalcularJurosAtraso(double valor, DateTime venc)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero");

            int diasAtraso = (DateTime.Today - venc).Days;

            if (diasAtraso <= 0)
                return (0, 0, valor);

            double juros = valor * 0.025 * diasAtraso;
            double total = valor + juros;

            return (diasAtraso, juros, total);
        }
    }
}
