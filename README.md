# Sistema de Gerenciamento de Manutenção de Computadores

## 📋 Visão Geral

Um sistema desktop desenvolvido em **C# .NET 10.0** que gerencia ordens de manutenção de equipamentos de TI (computadores, notebooks, impressoras e servidores). O projeto utiliza **Programação Orientada a Objetos (POO)** com arquitetura em camadas, implementando padrões como **Domain-Driven Design** e **Repository Pattern**.

## 🎯 Problema a Resolver

Empresas com múltiplos equipamentos de TI enfrentam dificuldades em:
- Registrar e rastrear ordens de manutenção
- Gerenciar clientes e técnicos responsáveis
- Diagnosticar problemas em equipamentos
- Processar pagamentos de forma organizada
- Gerar relatórios de manutenção

## ✨ Funcionalidades Principais

- **Cadastro de Equipamentos**: Registre computadores, notebooks, impressoras e servidores
- **Gerenciamento de Ordens de Serviço**: Abra, monitore e finalize ordens de manutenção
- **Diagnóstico de Equipamentos**: Realize diagnósticos e gere relatórios técnicos
- **Processamento de Pagamentos**: Suporte a diferentes métodos (Cartão e Pix)
- **Gerenciamento de Clientes e Técnicos**: Organize contatos e responsáveis
- **Relatórios do Sistema**: Visualize resumos gerais de ordens e equipamentos

## 🏗️ Arquitetura

```
Domain/
  ├── Entities/          # Modelos de negócio
  ├── Interface/         # Contratos
  ├── Service/           # Lógica de serviço
  └── ValueObjects/      # Objetos de valor
```

## 🚀 Como Usar

1. **Build do projeto**:
   ```bash
   dotnet build
   ```

2. **Executar a aplicação**:
   ```bash
   dotnet run --project Sistema-de-Gerenciamento-de-Manuten-o-Computadores---POO.csproj
   ```

## 💾 Tecnologias

- **Linguagem**: C# 10.0+
- **Framework**: .NET 10.0
- **Padrões**: DDD, Repository, Strategy, Inheritance, Polymorphism

---

**Desenvolvido como projeto de demonstração em Programação Orientada a Objetos**
