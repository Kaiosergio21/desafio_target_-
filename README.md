# Desafio Target – Projetos em C#

Repositório com soluções em C# para três desafios de programação, incluindo testes automatizados em xUnit.  
O projeto está organizado para rodar tanto no Visual Studio quanto no terminal via `dotnet run`.

---

# 📑 ÍNDICE

1. **[Sobre o Projeto](#sobre-o-projeto)**  
   - [Descrição](#descrição)  
   - [Funcionalidades](#funcionalidades)  
   - [Estrutura do Repositório](#estrutura-do-repositório)

2. **[Como Executar](#como-executar)**  
   - [Clonar o Repositório](#clonar-o-repositório)  
   - [Abrir no Visual Studio](#abrir-no-visual-studio)  
   - [Executar o Projeto](#executar-o-projeto)  
   - [Executar os Testes](#executar-os-testes)

3. **[Desafios Implementados](#desafios-implementados)**  
   - [1. Cálculo de Comissões](#1-cálculo-de-comissões)  
   - [2. Movimentação de Estoque](#2-movimentação-de-estoque)  
   - [3. Cálculo de Juros](#3-cálculo-de-juros)

4. **[Exemplos de Uso](#exemplos-de-uso)**

5. **[Testes Unitários – DESAFIOS.Tests](#testes-unitários--desafiostests)**  
   - [ComissaoServiceTests](#comissaoservicetests)  
   - [JurosServiceTests](#jurosservicetests)  
   - [EstoqueServiceTests](#estoqueservicetests)

6. **[Boas Práticas e Avisos](#boas-práticas-e-avisos)**  

7. **[Créditos](#créditos)**  

8. **[Licença](#licença)**  

---

# Sobre o Projeto

## Descrição
Este projeto implementa três desafios de lógica em C#, com foco em:

- Processamento de arquivos JSON  
- Regras de negócio  
- Persistência de logs  
- Testes automatizados com xUnit  

Os desafios são:

- **Cálculo de Comissões de Venda**
- **Movimentação de Estoque**
- **Cálculo de Juros sobre Dívidas**

---

## Funcionalidades

- Leitura e gravação de arquivos JSON  
- Registro de logs (movimentações e dívidas)  
- Classes de serviço desacopladas  
- Testes unitários completos  
- Interface de texto com menu via `Program.cs`  

---

## Estrutura do Repositório

DESAFIOS/ # Código-fonte principal
DESAFIOS.Tests/ # Testes unitários
Data/ # Arquivos JSON usados pela aplicação
.gitignore
target_teste.sln # Solução Visual Studio

yaml
Copiar código

---

# Como Executar

## Clonar o Repositório

```bash
git clone https://github.com/Kaiosergio21/desafio_target_-.git
cd desafio_target_-
git checkout changes
Abrir no Visual Studio
Abra o arquivo target_teste.sln

Restaure pacotes NuGet, se solicitado

Defina DESAFIOS como Startup Project

Executar o Projeto
Rodar pela solução:

Pressione F5 ou Ctrl+F5

Rodar pelo terminal:

bash
Copiar código
cd DESAFIOS
dotnet run
Executar os Testes
Rodar todos os testes:

bash
Copiar código
dotnet test
Rodar testes específicos:

bash
Copiar código
# Classe inteira:
dotnet test --filter "FullyQualifiedName~DESAFIOS.Tests.EstoqueServiceTests"

# Método específico:
dotnet test --filter "FullyQualifiedName~DESAFIOS.Tests.EstoqueServiceTests.Movimentar_Entrada_DeveAumentarEstoque"
Desafios Implementados
1. Cálculo de Comissões
Regras:

< 100 → 0%

100 a 499.99 → 1%

>= 500 → 5%

O JSON possui vendas por vendedor e o sistema retorna a comissão total de cada um.

2. Movimentação de Estoque
Entrada e saída de produtos

Atualização automática do estoque

Registro de logs em Data/log_movimentacoes.json

Cada movimentação possui ID único, tipo (E/S), quantidade e data

3. Cálculo de Juros
Juros de 2,5% ao dia sobre valores vencidos

Formatação automática de datas (ddMMyyyy → dd/MM/yyyy)

Registro de logs em Data/log_dividas.json

Exemplos de Uso
Comissões
Entrada:

json
Copiar código
{ "vendas": [
  { "vendedor": "João Silva", "valor": 1200.50 },
  { "vendedor": "Maria Souza", "valor": 2100.40 }
]}
Saída:
Comissão total de cada vendedor.

Movimentação de Estoque
yaml
Copiar código
Produto: Caneta Azul  
Entrada: 50 unidades  
Estoque final: 200  
Cálculo de Juros
yaml
Copiar código
Valor: 1000  
Vencimento: 01/12/2025  
Juros acumulados: R$XX,XX  
Total: R$XXXX,XX  
Testes Unitários – DESAFIOS.Tests
ComissaoServiceTests
Testa comissões abaixo de 100, entre 100–499.99 e acima de 500

Usa Theory + InlineData

Verificações com tolerância decimal

JurosServiceTests
Testa formatação de datas

Testa cálculos de juros

Trata erros de entrada

EstoqueServiceTests
Testa entradas e saídas de estoque

Usa StringReader e StringWriter para simular Console

Garante isolamento dos cenários

Boas Práticas e Avisos
⚠ Arquivos JSON não devem ser compartilhados em produção.
Este projeto é apenas para estudo.
Em projetos reais, use banco de dados ou armazenamento seguro.

Créditos
Desenvolvido por Kaio Nunes
GitHub: https://github.com/Kaiosergio21

Tests: xUnit
Desafios para estudo e prática de C#
