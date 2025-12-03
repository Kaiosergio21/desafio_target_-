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
        //  MÉTODO 2 — Calcula juros de atraso
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

        // ---------------------------------------------------------
        // SEU MÉTODO ORIGINAL (continua usando Console normalmente)
        // ---------------------------------------------------------
        public void CalcularJuros()
        {
            // ... (SEU CÓDIGO AQUI, SEM ALTERAR)
        }
    }
}
