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

        [Fact]
        public void Movimentar_Entrada_DeveAumentarEstoque()
        {
            // =====================
            // Arrange
            // =====================
            Directory.CreateDirectory("Data");

            // Cria arquivo JSON fake
            string jsonFake = @"{
                ""estoque"": [
                    { ""codigoProduto"": 1, ""descricaoProduto"": ""Produto Teste"", ""estoque"": 10 }
                ]
            }";
            File.WriteAllText(CaminhoJson, jsonFake);

            // Simula entrada do usuário:
            // Código do produto = 1, Tipo = E (entrada), Quantidade = 5
            var input = new StringReader("1\nE\n5\n");
            Console.SetIn(input);

            // Captura saída do console
            var output = new StringWriter();
            Console.SetOut(output);

            var service = new EstoqueService();

            // =====================
            // Act
            // =====================
            service.Movimentar();

            // =====================
            // Assert
            // =====================
            // Lê o JSON atualizado
            var dados = JsonSerializer.Deserialize<EstoqueRoot>(File.ReadAllText(CaminhoJson));
            Assert.NotNull(dados);
            Assert.NotNull(dados.estoque);

            // Verifica se o estoque foi atualizado corretamente
            Assert.Equal(15, dados.estoque[0].estoque); // 10 + 5

            // Opcional: verifica se mensagem foi exibida no console
            string saida = output.ToString();
            Assert.Contains("Movimentação registrada!", saida);
        }

        [Fact]
        public void Movimentar_Saida_DeveDiminuirEstoque()
        {
            // =====================
            // Arrange
            // =====================
            Directory.CreateDirectory("Data");

            // Cria arquivo JSON fake
            string jsonFake = @"{
                ""estoque"": [
                    { ""codigoProduto"": 1, ""descricaoProduto"": ""Produto Teste"", ""estoque"": 10 }
                ]
            }";
            File.WriteAllText(CaminhoJson, jsonFake);

            // Simula entrada do usuário:
            // Código do produto = 1, Tipo = S (saída), Quantidade = 3
            var input = new StringReader("1\nS\n3\n");
            Console.SetIn(input);

            // Captura saída do console
            var output = new StringWriter();
            Console.SetOut(output);

            var service = new EstoqueService();

            // =====================
            // Act
            // =====================
            service.Movimentar();

            // =====================
            // Assert
            // =====================
            var dados = JsonSerializer.Deserialize<EstoqueRoot>(File.ReadAllText(CaminhoJson));
            Assert.NotNull(dados);
            Assert.NotNull(dados.estoque);
            Assert.Equal(7, dados.estoque[0].estoque); // 10 - 3

            string saida = output.ToString();
            Assert.Contains("Movimentação registrada!", saida);
        }
    }
}
