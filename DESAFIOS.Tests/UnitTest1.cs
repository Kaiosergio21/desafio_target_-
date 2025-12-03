using Xunit;   // <- obrigatório para usar [Fact]

namespace DESAFIOS.Tests;

public class UnitTest1
{
    [Fact]  // <- sem isso, o compilador não reconhece
    public void TesteSimples()
    {
        Assert.True(true);
    }
}
