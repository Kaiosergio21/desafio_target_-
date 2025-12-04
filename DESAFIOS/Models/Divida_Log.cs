namespace Target.Models
{
    public class DividaLog
    {
        public string Id { get; set; } = string.Empty;
        public double ValorOriginal { get; set; }
        public string DataVencimento { get; set; } = string.Empty; // Formato DD/MM/YYYY
        public int DiasAtraso { get; set; }
        public double Juros { get; set; }
        public double TotalAtualizado { get; set; }
        public DateTime DataHoraCalculo { get; set; }
    }

    public class DividasRoot
    {
        public List<DividaLog> Dividas { get; set; } = new List<DividaLog>();
    }
}
