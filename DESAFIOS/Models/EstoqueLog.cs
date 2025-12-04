namespace Target.Models
{
    public class Movimentacao
    {
        public string Id { get; set; } = string.Empty;
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // "E" ou "S"
        public int Quantidade { get; set; }
        public DateTime DataHora { get; set; }
    }

    public class MovimentacoesRoot
    {
        public List<Movimentacao> movimentacoes { get; set; } = new List<Movimentacao>();
    }
}
