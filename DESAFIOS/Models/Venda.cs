namespace Target.Models
{
    // Classe que representa uma venda individual
    public class Venda
    {
        // Nome do vendedor responsável pela venda
        public string? vendedor { get; set; }

        // Valor monetário da venda
        public double valor { get; set; }
    }

    // Classe usada para deserializar o JSON (estrutura raiz)
     
     public class VendaRoot
   {
       public List<Venda> vendas { get; set; } = new();
   }

}
