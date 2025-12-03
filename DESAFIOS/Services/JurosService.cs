using System.Globalization;

namespace Target.Services
{
    public class JurosService
    {
        // ===========================================
        //  MÉTODO 1 — Formata datas digitadas
        // ===========================================
        public string FormatarData(string entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada))
                throw new ArgumentException("Data inválida");

            string numeros = new string(entrada.Where(char.IsDigit).ToArray());

            if (numeros.Length != 8)
                throw new ArgumentException("Data deve conter 8 dígitos");

            return $"{numeros.Substring(0, 2)}/{numeros.Substring(2, 2)}/{numeros.Substring(4, 4)}";
        }

        // ===========================================
        //  MÉTODO 2 — Calcula juros de atraso (USADO NO TESTE)
        // ===========================================
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

        // ===========================================
        //  SEU MÉTODO ORIGINAL (USO NO CONSOLE)
        // ===========================================
        public void CalcularJuros()
        {
            Console.Write("Valor da conta: ");
            string? entrada = Console.ReadLine();

            if (!double.TryParse(entrada, out double valor))
            {
                Console.WriteLine("Valor inválido!");
                return;
            }

            Console.Write("=== exemplos de formatos válidos ===\n\n01012025\n01-01-2025\n01 01 2025\n01/01/2025\n\n");
            Console.Write("Data de vencimento (DD/MM/AAAA): ");
            string? entradaData = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entradaData))
            {
                Console.WriteLine("Data inválida!");
                return;
            }

            // Remove tudo que não é número
            string apenasNumeros = new string(entradaData.Where(char.IsDigit).ToArray());

            // Formatação automática
            if (apenasNumeros.Length == 8)
            {
                entradaData =
                    $"{apenasNumeros.Substring(0, 2)}/" +
                    $"{apenasNumeros.Substring(2, 2)}/" +
                    $"{apenasNumeros.Substring(4, 4)}";
            }

            // Converte para DateTime
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
                return;
            }

            Console.WriteLine($"\nDias em atraso: {resultado.dias}");
            Console.WriteLine($"Juros: R${resultado.juros:F2}");
            Console.WriteLine($"Total atualizado: R${resultado.total:F2}");
        }
    }
}
