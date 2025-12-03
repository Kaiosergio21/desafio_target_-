namespace Target.Models
{
    // Classe que representa um produto no estoque
    public class Produto
    {
        // Identificador numérico único
        public int codigoProduto { get; set; }

        // Nome/descrição do produto
        public string descricaoProduto { get; set; } = string.Empty;


        // Quantidade atual disponível em estoque
        public int estoque { get; set; }
    }

    // Classe raiz usada para deserializar o JSON de estoque
    public class EstoqueRoot
    {
        // Lista contendo todos os produtos presentes no estoque
        public List<Produto> estoque { get; set; }  = new();
    }
}
