# Explicação de C# para o sistema de manutenção

Este documento traz conceitos de C# usados no seu projeto, para você poder explicar melhor o código depois.

---

## 1. Classe, instância e variável estática

No arquivo `Program.cs`, você tem:

```csharp
static SistemaManutencao sistema = new SistemaManutencao();
static Cliente? clienteLogado;
static Tecnico? tecnicoLogado;
```

- `static` significa que a variável pertence à classe `Program`, não a uma instância de `Program`.
- `SistemaManutencao sistema = new SistemaManutencao();` cria uma instância do sistema de manutenção.
- `Cliente? clienteLogado;` e `Tecnico? tecnicoLogado;` podem ficar `null` quando ninguém está logado.
- O `?` em `Cliente?` / `Tecnico?` diz que a variável pode ser nula.

### Como funciona `tecnicoLogado`

Quando o técnico informa nome e especialidade em `MenuTecnico()`, o código faz:

```csharp
tecnicoLogado = new Tecnico(Guid.NewGuid().ToString(), nomeTecnico, especialidade);
```

Isso cria uma nova instância da classe `Tecnico` e guarda na variável `tecnicoLogado`.

Enquanto o técnico estiver dentro do menu, `tecnicoLogado` contém o objeto do técnico e permite acessar métodos e propriedades como:

- `tecnicoLogado!.Nome`
- `tecnicoLogado!.Ordens`
- `tecnicoLogado!.AtribuirOrdem(ordem)`
- `tecnicoLogado!.RealizarDiagnostico(equipamento)`

Quando o técnico sai do menu, o código faz:

```csharp
tecnicoLogado = null;
```

Assim o sistema sabe que não há técnico logado.

---

## 2. Como funciona `throw` e exceções

No arquivo `Domain/Entities/OrdemServico.cs`, existe este bloco:

```csharp
public void Finalizar()
{
    if (Status != "Em Execução" && Status != "Diagnosticada")
        throw new InvalidOperationException("A ordem deve estar em status 'Em Execução' ou 'Diagnosticada' para finalizar.");
    Status = "Finalizada";
}
```

### O que isso significa

- `throw` interrompe o método imediatamente.
- `new InvalidOperationException(...)` cria uma exceção com uma mensagem.
- Essa exceção indica que a operação não é válida no estado atual.

### Quando isso é usado

A exceção é lançada se a ordem ainda não estiver pronta para finalizar.
Ou seja, só pode finalizar quando:
- `Status == "Em Execução"` ou
- `Status == "Diagnosticada"`

Se o `Status` estiver em outro valor, o método não continua e a ordem não vira "Finalizada".

### Por que isso é importante

Isso protege a lógica do sistema. Se alguém tentar finalizar sem passar pelas etapas corretas, o erro é lançado e evita que o programa siga com estado inconsistente.

---

## 3. Entendendo o fluxo de estados de `OrdemServico`

A classe `OrdemServico` usa métodos para mudar o status:

- `Abrir()` → muda para `"Aberta"`
- `Diagnosticar()` → só funciona se estiver `"Aberta"`, depois vai para `"Diagnosticada"`
- `Executar()` → só funciona se estiver `"Diagnosticada"`, depois vai para `"Em Execução"`
- `Finalizar()` → só funciona se estiver `"Em Execução"` ou `"Diagnosticada"`, depois vai para `"Finalizada"`

Esse tipo de validação é chamada de "validação de estado".

---

## 4. LINQ: `.Where()`, `.Any()` e `.ToList()`

No código `Program.cs`, você usou:

```csharp
    .Where(e => !sistema.Ordens.Any(o => o.Id == e.Id))
    .ToList();
```

### Como ler isso

- `.Where(...)` filtra uma coleção.
- `e => ...` é uma expressão lambda que representa cada item `e` da coleção.
- `sistema.Ordens.Any(o => o.Id == e.Id)` verifica se existe alguma ordem com o mesmo `Id` do equipamento.
- `!` significa "não".
- `.ToList()` transforma o resultado em uma lista.

### Em palavras simples

"Pegue todos os equipamentos `e` que não têm nenhuma ordem `o` com `o.Id == e.Id`."

Isso é usado para mostrar equipamentos que ainda não têm ordem de serviço.

---

## 5. Operadores de comparação e lógica

No método `Finalizar()` há:

```csharp
if (Status != "Em Execução" && Status != "Diagnosticada")
```

- `!=` significa "diferente de".
- `&&` significa "e".

A condição inteira só é verdadeira quando o status é diferente de ambos os valores.

---

## 6. Por que usar `tecnicoLogado!` com `!`

No `Program.cs`, você viu este uso:

```csharp
ExibirMensagemBemVindo(tecnicoLogado!.Nome);
```

O `!` é o operador "null-forgiving".
Ele diz ao compilador: "Eu sei que essa variável não é nula aqui".

Isso é usado porque `tecnicoLogado` é declarada como `Tecnico?`, então o compilador exige uma verificação de nulidade.
Se você tem certeza que o técnico já foi criado, você usa `!` para evitar aviso de compilação.

---

## 7. Funções e responsabilidades no `Program.cs`

### `MenuCliente()`
- Pede nome e telefone do cliente.
- Cria `clienteLogado`.
- Mostra um menu com opções do cliente.
- Mantém um loop até o cliente escolher voltar.

### `MenuTecnico()`
- Pede nome e especialidade do técnico.
- Cria `tecnicoLogado`.
- Mostra um menu com opções do técnico.
- Mantém um loop até voltar.

### `ExibirMensagemBemVindo(string nome)`
- Centraliza o texto de boas-vindas em uma caixa.
- Evita problemas de alinhamento quando o nome muda de tamanho.

---

## 8. Padrões que podem causar confusão

### 8.1 `TryParse()` com operador ternário `?:`

No `Program.cs` linha 30, você tem:

```csharp
opcao = int.TryParse(Console.ReadLine()!, out int result) ? result : -1;
```

- `int.TryParse(...)` tenta converter uma string para inteiro.
- Se conseguir, retorna `true` e coloca o valor em `result`.
- Se não conseguir, retorna `false`.
- O `?` aqui é o operador **ternário** (não é sobre null).
- A sintaxe é: `condição ? valor_se_verdade : valor_se_falso`

Em outras palavras: "Se conseguir parsear, use `result`. Senão, use `-1`."

Outro exemplo da mesma coisa (linha 159):

```csharp
if (!int.TryParse(Console.ReadLine(), out int tipo) || tipo < 1 || tipo > 4)
```

- `!int.TryParse(...)` significa "se não conseguir parsear".
- `||` significa "ou".
- A condição inteira é verdadeira se: não parsear OU tipo < 1 OU tipo > 4.

### 8.2 `FirstOrDefault()`

No `Program.cs` linha 319, você tem:

```csharp
var orden = ordensCliente.FirstOrDefault(o => o.Id == eq.Id);
```

- `.FirstOrDefault(...)` procura o **primeiro** item que atende a condição.
- Se achar, retorna o item.
- Se não achar, retorna `null`.

Isso é diferente de `.First()` que lança erro se não achar nada.

### 8.3 Operador `?.` (null-conditional)

No `Program.cs` linha 866, você tem:

```csharp
Console.WriteLine($" Cliente: {sistema.Equipamentos.FirstOrDefault(e => e.Id == ordem.Id)?.NomeCliente}");
```

- `?.` só executa o que vem depois se o item não for nulo.
- Se `FirstOrDefault()` retornar `null`, `?.NomeCliente` retorna `null`.
- Se `FirstOrDefault()` encontrar algo, `?.NomeCliente` pega o nome do cliente.

Isso evita erro de tentativa de acessar propriedade em `null`.

Comparação:
- `objeto.propriedade` → erro se `objeto` for nulo.
- `objeto?.propriedade` → seguro, retorna `null` se `objeto` for nulo.

### 8.4 `try-catch` blocks

No `Program.cs` linhas 271-296, você tem:

```csharp
try
{
    switch (formaPagamento)
    {
        case 1:
            // ... código que pode dar erro ...
            pagamento = new PagamentoPix(...);
            break;
        // ... mais cases ...
    }
    ordem.RegistrarPagamento(pagamento);
    Console.WriteLine("\n Pagamento Realizado com Sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine($"\n Ocorreu um Erro ao Processar o Pagamento: {ex.Message}");
}
```

- `try` contém o código que **pode** gerar erro.
- Se um erro for lançado (com `throw`), o programa não quebra.
- Em vez disso, o bloco `catch` captura o erro.
- `Exception ex` é o objeto do erro.
- `ex.Message` é a mensagem do erro.

Por que usar? Evita que um erro inesperado derrube o programa inteiro.

### 8.5 Expressão lambda com `out`

No `Program.cs` linha 105, você tem:

```csharp
if (!int.TryParse(Console.ReadLine(), out opcao))
```

- `out opcao` significa que `opcao` vai receber o resultado da conversão.
- Você não precisa declarar `int opcao` antes, o `out` faz isso.
- O `out` é um parâmetro especial que permite que o método **modifique** a variável.

### 8.6 Comparação de string com `??`

No seu código não há, mas é comum ver:

```csharp
string nome = cliente?.Nome ?? "Desconhecido";
```

- `??` é o operador "null coalescing".
- Se o lado esquerdo for nulo, use o lado direito.
- Então: "Se `cliente.Nome` for nulo, use `'Desconhecido'`."

---

## 9. Como explicar o código para outra pessoa

Quando você for falar do código, siga esta estrutura:

1. Explique o papel do programa:
   - É um sistema de manutenção de equipamentos.
   - Tem fluxo de cliente e técnico.

2. Mostre como o usuário entra no sistema:
   - `MenuCliente()` cria um cliente e mostra opções.
   - `MenuTecnico()` cria um técnico e mostra opções.

3. Fale sobre os objetos principais:
   - `SistemaManutencao` guarda equipamentos, ordens e pagamentos.
   - `Cliente` guarda equipamentos do cliente.
   - `Tecnico` recebe ordens e executa serviços.

4. Detalhe uma regra importante:
   - `OrdemServico.Finalizar()` só funciona se o status estiver correto.
   - Essa regra é feita com `throw` para evitar erros de lógica.

5. Explique o uso de LINQ:
   - `.Where(...)` filtra coleções.
   - `.Any(...)` verifica existência.
   - `.ToList()` converte para lista.

---

## 9. Perguntas para você responder enquanto estuda

- Você entende a diferença entre uma classe e uma instância?
- Você sabe por que `tecnicoLogado` pode ser nulo antes do login?
- Você consegue explicar por que `throw` é usado em vez de apenas mudar o status?
- Você consegue ler esta expressão e dizer o que ela retorna?
  ```csharp
  sistema.Equipamentos.Where(e => !sistema.Ordens.Any(o => o.Id == e.Id)).ToList();
  ```
- Você entende o que o `!` faz em `tecnicoLogado!.Nome`?

Se quiser, posso transformar essas perguntas em um pequeno roteiro de explicação passo a passo para você usar depois.
