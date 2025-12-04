using System;
using System.IO;
using System.Text.Json;
using Xunit;
using Target.Services;
using Target.Models;

namespace DESAFIOS.Tests
{
    public class EstoqueServiceTests
    {
        private const string CaminhoJson = "Data/estoque.json";
        private const string CaminhoLog = "Data/log_movimentacoes.json";

        private void PrepararAmbiente()
        {
            Directory.CreateDirectory("Data");
            File.WriteAllText(CaminhoLog, "{}");   // <--- evita erro de JSON vazio
        }

        [Fact]
        public void Movimentar_Entrada_DeveAumentarEstoque()
        {
            PrepararAmbiente();

            string jsonFake = @"{
                ""estoque"": [
                    { ""codigoProduto"": 1, ""descricaoProduto"": ""Produto Teste"", ""estoque"": 10 }
                ]
            }";
            File.WriteAllText(CaminhoJson, jsonFake);

            var input = new StringReader("1\nE\n5\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var service = new EstoqueService();

            service.Movimentar();

            var dados = JsonSerializer.Deserialize<EstoqueRoot>(File.ReadAllText(CaminhoJson));

            Assert.NotNull(dados);
            Assert.Equal(15, dados.estoque[0].estoque);

            Assert.Contains("Movimentação registrada", output.ToString());
        }

        [Fact]
        public void Movimentar_Saida_DeveDiminuirEstoque()
        {
            PrepararAmbiente();

            string jsonFake = @"{
                ""estoque"": [
                    { ""codigoProduto"": 1, ""descricaoProduto"": ""Produto Teste"", ""estoque"": 10 }
                ]
            }";
            File.WriteAllText(CaminhoJson, jsonFake);

            var input = new StringReader("1\nS\n3\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var service = new EstoqueService();

            service.Movimentar();

            var dados = JsonSerializer.Deserialize<EstoqueRoot>(File.ReadAllText(CaminhoJson));

            Assert.NotNull(dados);
            Assert.Equal(7, dados.estoque[0].estoque);

            Assert.Contains("Movimentação registrada", output.ToString());
        }
    }
}
