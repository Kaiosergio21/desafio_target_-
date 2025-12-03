using System;
using Xunit;
using Target.Services;

namespace DESAFIOS.Tests
{
    public class ComissaoServiceTests
    {
        private readonly ComissaoService _service;

        public ComissaoServiceTests()
        {
            _service = new ComissaoService();
        }

        // ---------------------------------------------------------
        // TESTE 1 — Vendas abaixo de 100 NÃO geram comissão
        // ---------------------------------------------------------
        [Theory]
        [InlineData(0)]
        [InlineData(50)]
        [InlineData(99.99)]
        public void CalcularComissao_VendasAbaixoDe100_NaoGeraComissao(double valor)
        {
            double resultado = _service.CalcularComissao(valor);
            Assert.Equal(0, resultado);
        }

        // ---------------------------------------------------------
        // TESTE 2 — Vendas entre 100 e 500 geram 1%
        // ---------------------------------------------------------
        [Theory]
        [InlineData(100, 1)]
        [InlineData(250, 2.5)]
        [InlineData(499.99, 4.9999)]
        public void CalcularComissao_VendasAbaixoDe500_Gera1PorCento(double valor, double esperado)
        {
            double resultado = _service.CalcularComissao(valor);
            Assert.Equal(esperado, resultado, 2); // tolerância de 2 casas decimais
        }

        // ---------------------------------------------------------
        // TESTE 3 — Vendas de 500+ geram 5%
        // ---------------------------------------------------------
        [Theory]
        [InlineData(500, 25)]
        [InlineData(1000, 50)]
        [InlineData(1999.90, 99.995)]
        public void CalcularComissao_VendasAcimaDe500_Gera5PorCento(double valor, double esperado)
        {
            double resultado = _service.CalcularComissao(valor);
            Assert.Equal(esperado, resultado, 2);
        }
    }
}
