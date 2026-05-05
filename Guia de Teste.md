# 📖 Guia de Criação de Novos Testes

## 📐 Estrutura Padrão de um Teste

Todos os testes seguem o padrão **AAA (Arrange-Act-Assert)**:

```csharp
using Xunit;
using Domain.Entities;
using Domain.ValueObjects;

namespace Tests.Unit
{
    public class NovaClasseTests  // Nome da classe: [ClasseTestada]Tests
    {
        [Fact]  // Ou [Theory] para testes parametrizados
        public void NomeDoMetodo_DoQueEstaTestando_ResultadoEsperado()
        {
            // ARRANGE: Preparar dados
            var objeto = new MinhaClasse { Propriedade = "valor" };
            
            // ACT: Executar a ação
            var resultado = objeto.MinhaOperacao();
            
            // ASSERT: Validar o resultado
            Assert.Equal("esperado", resultado);
        }
    }
}
```

---

## 📍 Onde Colocar Novos Testes

### Testes Unitários (Unit)
**Quando:** Testar uma classe isoladamente
**Arquivo:** `Tests/Unit/[NomeClasse]Tests.cs`

```csharp
public class NotebookTests
{
    [Fact]
    public void Diagnosticar_DeveAlterarStatus()
    {
        // Testa apenas a classe Notebook, sem dependências
        var notebook = new Notebook { Descricao = "Notebook" };
        notebook.Diagnosticar();
        Assert.Equal("Diagnóstico em andamento", notebook.Status);
    }
}
```

### Testes de Integração (Integration)
**Quando:** Testar interação entre 2+ classes
**Arquivo:** `Tests/Integration/[Componentes]IntegrationTests.cs`

```csharp
public class SistemaClienteIntegrationTests
{
    [Fact]
    public void ClienteAdicionarEquipamento_DeveVincularAoSistema()
    {
        // Testa Cliente + Equipamento + SistemaManutencao juntos
        var sistema = new SistemaManutencao();
        var cliente = new Cliente { Nome = "Empresa" };
        var equipamento = new Notebook { Descricao = "NB" };
        
        sistema.CadastrarEquipamento(equipamento);
        cliente.AdicionarEquipamento(equipamento);
        
        Assert.Single(cliente.Equipamentos);
    }
}
```

### Testes de Sistema (System)
**Quando:** Testar um fluxo completo (ponta a ponta)
**Arquivo:** `Tests/System/[FluxoDescricao]SystemTests.cs`

```csharp
public class ProcessoDeManutenaoSystemTests
{
    [Fact]
    public void ProcessoCompleto_DeCadastroAteRelatorio()
    {
        // STAGE 1: Setup
        var sistema = new SistemaManutencao();
        
        // STAGE 2: Primeira ação
        var equipamento = new Notebook { Descricao = "NB1" };
        sistema.CadastrarEquipamento(equipamento);
        
        // STAGE 3: Segunda ação
        var ordem = new OrdemServico { Servico = "Manutenção" };
        sistema.AbrirOrdem(ordem);
        
        // STAGE 4: Terceira ação e validação
        sistema.GerarRelatorioGeral();
        Assert.Single(sistema.Ordens);
    }
}
```

---

## 🧩 Padrões de Assertions Úteis

### Assert Básicos
```csharp
// Igualdade
Assert.Equal("esperado", resultado);
Assert.NotEqual("não esperado", resultado);

// Null/Vazio
Assert.Null(resultado);
Assert.NotNull(resultado);
Assert.Empty(colecao);
Assert.Single(colecao);

// Booleano
Assert.True(condicao);
Assert.False(condicao);

// Coleções
Assert.Contains(item, colecao);
Assert.DoesNotContain(item, colecao);
Assert.All(colecao, item => Assert.NotNull(item));

// Exceções
Assert.Throws<ArgumentException>(() => classeTeste.MetodoQueThrow());

// Range
Assert.InRange(valor, min, max);
Assert.NotInRange(valor, min, max);

// Tipagem
Assert.IsType<Notebook>(equipamento);
Assert.IsAssignableFrom<IEquipamento>(objeto);
```

---

## 🎯 Theory Tests - Testes Parametrizados

Use `[Theory]` com `[InlineData]` para testar múltiplos valores:

```csharp
[Theory]
[InlineData(100, "BRL", 1)]          // 1º teste
[InlineData(50, "USD", 0.5)]         // 2º teste
[InlineData(25000, "EUR", 250)]      // 3º teste
public void ToDecimal_ComDiferentesValores(long centavos, string currency, decimal esperado)
{
    var money = new Money(centavos, currency);
    var resultado = money.ToDecimal();
    Assert.Equal(esperado, resultado);
}
```

---

## 🏗️ Estrutura de Teste Complexo

Para testes com múltiplas etapas (System tests):

```csharp
[Fact]
public void ProcessoCompleto_ComMultiplasEtapas()
{
    // ===== SETUP =====
    var sistema = new SistemaManutencao();
    
    // ===== ETAPA 1: Preparação Initial =====
    var equipamentos = new[] { 
        new Notebook { Descricao = "NB1" },
        new Desktop { Descricao = "DT1" }
    };
    foreach(var eq in equipamentos)
        sistema.CadastrarEquipamento(eq);
    
    Assert.Equal(2, sistema.Equipamentos.Count);
    
    // ===== ETAPA 2: Processamento =====
    var ordem = new OrdemServico { Servico = "Manutenção" };
    sistema.AbrirOrdem(ordem);
    
    Assert.Single(sistema.Ordens);
    
    // ===== ETAPA 3: Conclusão =====
    var pagamento = new PagamentoPix 
    { 
        Pagador = "Cliente",
        Valor = new Money(5000, "BRL")
    };
    sistema.RegistrarPagamento(pagamento, 1);
    
    // ===== VALIDAÇÕES FINAIS =====
    Assert.Equal("Pago", sistema.GetOrdemById(1)?.Status);
}
```

---

## ✅ Checklist para Criar um Novo Teste

- [ ] Arquivo criado na pasta correta (Unit/Integration/System)
- [ ] Classe nomeada como `[ClasseTestada]Tests`
- [ ] Método nomeado como `Metodo_DoQueFaz_ResultadoEsperado`
- [ ] Padrão AAA seguido (Arrange, Act, Assert)
- [ ] Teste isolado (não depende de outros testes)
- [ ] Assertions claros e específicos
- [ ] Teste passa ✓
- [ ] Teste falha quando o código está errado ✓

---

## 🚀 Executar Novo Teste

Depois de criar um novo teste:

```bash
# Compilar
dotnet build

# Rodar apenas este teste
dotnet test --filter "FullyQualifiedName=Tests.Unit.MeuTesteTests.MeuNovoTeste"

# Rodar todos os testes
dotnet test
```

---

## 🐛 Debugar um Teste

### No Visual Studio
1. Clique no teste
2. Pressione `Ctrl+F5` para rodar sem debug
3. Ou `F5` para rodar com debug e breakpoints

### Via Terminal
```bash
dotnet test --logger "console;verbosity=detailed"
```

---

## 📊 Boas Práticas de Teste

### ✅ FAÇA
```csharp
[Fact]
public void CadastroEquipamento_ComDadosValidos_DeveAdicionarComSucesso()
{
    // Teste bem nomeado e específico
    var sistema = new SistemaManutencao();
    var notebook = new Notebook { Descricao = "NB" };
    
    sistema.CadastrarEquipamento(notebook);
    
    Assert.Single(sistema.Equipamentos);
    Assert.Equal("NB", sistema.Equipamentos[0].Descricao);
}
```

### ❌ NÃO FAÇA
```csharp
[Fact]
public void Test1() // Nomes genéricos não dizem nada
{
    var s = new SistemaManutencao();
    var e = new Notebook { Descricao = "x" };
    s.CadastrarEquipamento(e);
    // Sem asserts!
}
```

---

## 🔄 Manutenção de Testes Existentes

Se você modificar uma classe, atualize seus testes:

```csharp
// Se você alterar a assinatura:
// De:  public void Diagnosticar()
// Para: public void Diagnosticar(bool verboso)

// Seu teste deve ser atualizado:
notebook.Diagnosticar(true);  // Passou o novo parâmetro
```

---

## 📚 Recursos Úteis

### xUnit Documentation
- [xUnit.net Official Docs](https://xunit.net/)
- [Assert Methods](https://xunit.net/docs/assert)

### Padrões de Teste
- [AAA Pattern](https://www.freecodecamp.org/news/arrange-act-assert-pattern/)
- [Dado-Quando-Então (BDD)](https://cucumber.io/docs/bdd/)

---

## 🎓 Exemplo Completo: Adicionando um Novo Teste

### Cenário: Adicionar teste para um novo método em `Cliente`

1. **Criar o teste:**
```csharp
// Tests/Unit/ClienteTests.cs
using Xunit;
using Domain.Entities;

namespace Tests.Unit
{
    public class ClienteTests
    {
        [Fact]
        public void Cliente_DeveHerdarDePresoa()
        {
            // Arrange
            var cliente = new Cliente { 
                Nome = "Empresa ABC", 
                Telefone = "1133334444" 
            };
            
            // Act & Assert
            Assert.NotNull(cliente.Nome);
            Assert.Equal("1133334444", cliente.Telefone);
        }
        
        [Theory]
        [InlineData("11999999999")]
        [InlineData("2188888888")]
        public void Telefone_DeveArmazenarValoresValidos(string telefone)
        {
            var cliente = new Cliente { Telefone = telefone };
            Assert.Equal(telefone, cliente.Telefone);
        }
    }
}
```

2. **Compilar:**
```bash
dotnet build
```

3. **Rodar os testes:**
```bash
dotnet test --filter "FullyQualifiedName~Tests.Unit.ClienteTests"
```

4. **Ver resultado:**
```
✓ Tests.Unit.ClienteTests.Cliente_DeveHerdarDePresoa
✓ Tests.Unit.ClienteTests.Telefone_DeveArmazenarValoresValidos [11999999999]
✓ Tests.Unit.ClienteTests.Telefone_DeveArmazenarValoresValidos [2188888888]
```

Pronto! Teste criado e funcionando! 🎉
