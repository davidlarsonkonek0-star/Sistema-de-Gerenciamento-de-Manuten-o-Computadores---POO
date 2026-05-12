﻿using System.IO.Compression;

using Domain.Entities;
class Program
{
    static SistemaManutencao sistema = new SistemaManutencao();

    static Cliente? clienteLogado;

    static Tecnico? tecnicoLogado;

    static void Main(string[] args)
    {
        inicializarDados();

        int opcao;

        do
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║              MENU DE ACESSO              ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine(" 1 - Acessar Cliente");
            Console.WriteLine(" 2 - Acessar Técnico");
            Console.WriteLine(" 0 - Sair");
            Console.WriteLine();
            Console.Write(" Escolha: ");

            opcao = int.Parse(Console.ReadLine()!);

            switch (opcao)
            {
                case 1:
                    MenuCliente();
                    break;
                case 2:
                    MenuTecnico();
                    break;
                case 0:
                    Console.WriteLine("\n Saindo...");
                    break;
                default:
                    Console.WriteLine("\n Opção inválida");
                    break;
            }

            if (opcao != 0)
            {
                AguardarEnter();
            }

        } while (opcao != 0);
    }


    /// <summary>
    /// Menu Do Cliente
    /// </summary>

    static void MenuCliente()
    {
        Console.Clear();
        ExibirTitulo();
        Console.WriteLine(" ╔══════════════════════════════════════════╗");
        Console.WriteLine(" ║              ACESSO CLIENTE              ║");
        Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

        Console.Write(" Nome do cliente: ");
        string nome = Console.ReadLine()!;
        Console.Write(" Telefone do cliente: ");
        string telefone = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("\n Nome inválido!");
            return;
        }

        if (string.IsNullOrWhiteSpace(telefone))
        {
            Console.WriteLine("\n Telefone inválido!");
            return;
        }

        clienteLogado = new Cliente(Guid.NewGuid().ToString(), nome, telefone);

        int opcao;

        do
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║     BEM-VINDO, {clienteLogado.Nome}!     ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine(" 1 - Cadastrar Equipamento");
            Console.WriteLine(" 2 - Ver Meus Equipamentos");
            Console.WriteLine(" 3 - Realizar Pagamento");
            Console.WriteLine(" 4 - Ver Relatório pessoal");
            Console.WriteLine(" 0 - Voltar");
            Console.WriteLine();
            Console.Write(" Escolha: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("\n Opção inválida!");
                AguardarEnter();
                continue;
            }

            switch (opcao)
            {
                case 1:
                    ClienteCadastrarEquipamento();
                    break;
                case 2:
                    ClienteVerEquipamentos();
                    break;
                case 3:
                    ClienteProcessarPagamento();
                    break;
                case 4:
                    ClienteRelatorio();
                    break;
                case 0:
                    clienteLogado = null;
                    Console.WriteLine("\n Voltando ao Menu Principal...");
                    break;
                default:
                    Console.WriteLine("\n Opção inválida!");
                    break;
            }

            if (opcao != 0)
            {
                AguardarEnter();
            }
        }while (opcao != 0);
    }

    static void ClienteCadastrarEquipamento()
    {
        Console.Clear();
        ExibirTitulo();
        Console.WriteLine(" ╔══════════════════════════════════════════╗");
        Console.WriteLine(" ║        CADASTRAR NOVO EQUIPAMENTO        ║");
        Console.WriteLine(" ╚══════════════════════════════════════════╝\n");
        Console.WriteLine();

        Console.Write("  Tipo do equipamento: ");
        Console.WriteLine(" 1 - Notebook");
        Console.WriteLine(" 2 - Desktop");
        Console.WriteLine(" 3 - Servidor");
        Console.WriteLine(" 4 - Impressora");
        Console.Write("\n Escolha: ");

        if (!int.TryParse(Console.ReadLine(), out int tipo) || tipo < 1 || tipo > 4 )
        {
            Console.WriteLine("\n Tipo inválido!");
            AguardarEnter();
            return;
        }

        Console.Write(" Descrição do problema: ");
        string descricao = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(descricao))
        {
            Console.WriteLine("\n Descrição inválida!");
            AguardarEnter();
            return;
        }

        Equipamento equipamento = tipo  switch
        {
            1 => CriarNotebook(descricao),
            2 => CriarDesktop(descricao),
            3 => CriarServidor(descricao),
            4 => CriarImpressora(descricao),
            _ => null
        };

        if (equipamento != null)
        {
            sistema.CadastrarEquipamento(equipamento);
            clienteLogado!.AdicionarEquipamento(equipamento);
            Console.WriteLine("\n Equipamento cadastrado com sucesso!");
            Console.WriteLine($" ID do equipamento: {equipamento.Id} | status: Aguardando");
        }
        else
        {
            Console.WriteLine("\n Ocorreu um erro ao cadastrar o equipamento.");
        }

        static void ClienteVerEquipamentos()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║            MEUS EQUIPAMENTOS             ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            if (clienteLogado!.Equipamentos.Count == 0)
            {
                Console.WriteLine(" Você não possui equipamentos cadastrados.");
                return;
            }

            foreach (var eq in clienteLogado!.Equipamentos)
            {
                Console.WriteLine($" ID: {eq.Id} | Tipo: {eq.GetType().Name} | Status: {eq.Status}");
                Console.WriteLine($" Cliente: {eq.NomeCliente} | Data de Entrada: {eq.DataEntrada}");
                Console.WriteLine($" Descrição: {eq.Descricao}");
                Console.WriteLine();
            }
        }

        static void ClienteProcessarPagamento()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║            REALIZAR PAGAMENTO            ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var ordensFinalizadas = sistema.Ordens
                .Where(o => o.Status == "Finalizado" && o.Pagamento == null)
                .ToList();

            if (ordensFinalizadas.Count == 0)
            {
                Console.WriteLine(" Você não Possui Ordens de Serviço Finalizadas para Pagamento.");
                return;
            }

            Console.WriteLine(" Ordens de Serviço Prontas para Pagamento:");
            for (int i = 0; i < ordensFinalizadas.Count; i++)
            {
                Console.WriteLine($" {i + 1} - Ordem: {ordensFinalizadas[i].Id} | R$: {ordensFinalizadas[i].ValorTotal.ToDecimal():F2}");
                Console.WriteLine($"     Serviço: {ordensFinalizadas[i].Servico}");
            }

            Console.Write("\n Escolha a ordem de serviço para pagamento: ");
            if (!int.TryParse(Console.ReadLine(), out int idOrdem) || idOrdem < 1 || idOrdem > ordensFinalizadas.Count)
            {
                Console.WriteLine("\n Opção inválida!");
                AguardarEnter();
                return;
            }

            var ordem = ordensFinalizadas[idOrdem - 1];

            Console.WriteLine("\n Formas de Pagamento:");
            Console.WriteLine(" 1 - PIX");
            Console.WriteLine(" 2 - Cartão");
            Console.WriteLine(" 3 - Dinheiro");
            Console.Write("\n Escolha a forma de pagamento: ");

            if (!int.TryParse(Console.ReadLine(), out int formaPagamento) || formaPagamento < 1 || formaPagamento > 3)
            {
                Console.WriteLine("\n Opção inválida!");
                AguardarEnter();
                return;
            }

            Pagamento pagamento = null!;

            try
            {
                switch (formaPagamento)
                {
                    case 1:
                        Console.Write(" Chave PIX: ");
                        string chavePix = Console.ReadLine()!;
                        pagamento = new PagamentoPix(clienteLogado.Nome, ordem.ValorTotal.ToDecimal(), chavePix);
                        break;
                    case 2:
                        Console.Write(" Número do cartão (ultimo 4 dígitos): ");
                        string numeroCartao = Console.ReadLine()!;
                        pagamento = new PagamentoCartao(clienteLogado.Nome, ordem.ValorTotal.ToDecimal(), $"****-****-****-{numeroCartao}");
                        break;
                    case 3:
                        pagamento = new PagamentoEspecie(clienteLogado.Nome, ordem.ValorTotal.ToDecimal());
                        break;
                }

                ordem.RealizarPagamento(pagamento);
                Console.WriteLine("\n Pagamento Realizado com Sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Ocorreu um Erro ao Processar o Pagamento: {ex.Message}");
            }
        }


        static void ClienteRelatorio()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║          MEU RELATÓRIO DE SERVIÇOS       ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var ordensCliente = sistema.Ordens
                .Where(o => o.Id > 0)
                .ToList();

            Console.WriteLine($" Relatório de Ordens de Serviço de {clienteLogado.Nome}:\n");
            Console.WriteLine($" Equipamentos Cadastrados: {clienteLogado.Equipamentos.Count}\n");
            Console.WriteLine();


            Console.WriteLine(" Equipamentos:");
            foreach (var eq in clienteLogado.Equipamentos)
            {
                var orden = ordensCliente.FirstOrDefault(o => o.Id == eq.Id);
                string statusOrdem = orden?.Status ?? "Aguardando Tecnico";
                Console.WriteLine($" Equipamento ID: {eq.Id}:{eq.Descricao}");
                Console.WriteLine($" Status da Ordem: {statusOrdem}");
            }

            Console.WriteLine("\n Ordens de Serviço:");
            if (ordensCliente.Count == 0)
            {
                Console.WriteLine(" Você não possui ordens de serviço.");
                return;
            }
            else
            {
                foreach (var ordem in ordensCliente)
                {
                    Console.WriteLine($" Ordem ID: {ordem.Id} | Serviço: {ordem.Servico}");
                    Console.WriteLine($" Status: {ordem.Status} | Valor Total: R$ {ordem.ValorTotal.ToDecimal():F2}");
                    if (ordem.Pagamento != null)
                    {
                        Console.WriteLine($" Pago: {ordem.Pagamento.ExibirResumo()}");
                    }
                    else
                    {
                        Console.WriteLine(" Pagamento: Pendente");
                    }
                }
            }

            var pagamentos = sistema.Pagamentos.Count;
            var valorTotal = sistema.Ordens.Where(o => o.Pagamento != null).Sum(o => o.ValorTotal.ToDecimal());
            Console.WriteLine($"\n Total de Pagamentos Realizados: {pagamentos}");
            Console.WriteLine($" Valor Total Pago: R$ {valorTotal:F2}");
        }

         /// <summary>
         /// Menu do Técnico
         /// </summary>

         static void MenuTecnico()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║              ACESSO TÉCNICO              ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");
            Console.WriteLine();

            Console.Write(" Nome do técnico: ");
            string nomeTecnico = Console.ReadLine()!;

            Console.Write(" Especialidade: ");
            string especialidade = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(nomeTecnico))
            {
                Console.WriteLine("\n Nome inválido!");
                return;
            }

            if (string.IsNullOrWhiteSpace(especialidade))
            {
                Console.WriteLine("\n Especialidade inválida!");
                return;
            }

            tecnicoLogado = new Tecnico(Guid.NewGuid().ToString(), nomeTecnico, especialidade);

            int opcao;

            do
            {
                Console.Clear();
                ExibirTitulo();
                Console.WriteLine(" ╔══════════════════════════════════════════╗");
                Console.WriteLine($"║     BEM-VINDO, {tecnicoLogado.Nome}!     ║");
                Console.WriteLine(" ╚══════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine(" 1 - Ver Equipamentos Novos para Atendimento");
                Console.WriteLine(" 2 - Abrir Ordem de Serviço e Atualizar Status");
                Console.WriteLine(" 3 - Ver Minhas Ordens de Serviço");
                Console.WriteLine(" 4 - Executar Diagnóstico");
                Console.WriteLine(" 5 - Executar Serviço");
                Console.WriteLine(" 6 - Finalizar Ordem de Serviço");
                Console.WriteLine(" 7 - Gerar Relatório Técnico");
                Console.WriteLine(" 0 - Voltar");
                Console.WriteLine();
                Console.Write(" Escolha: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("\n Opção inválida!");
                    AguardarEnter();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        TecnicoVerEquipamentosNovos();
                        break;
                    case 2:
                        TecnicoAbrirOrdem();
                        break;
                    case 3:
                        TecnicoVerOrdens();
                        break;
                    case 4:
                        TecnicoDiagnosticar();
                        break;
                    case 5:
                        TecnicoExecutarServico();
                        break;
                    case 6:
                        TecnicoFinalizarOrdem();
                        break;
                    case 7:
                        TecnicoRelatorio();
                        break;
                    case 0:
                        tecnicoLogado = null;
                        Console.WriteLine("\n Voltando ao Menu Principal...");
                        break;
                    default:
                        Console.WriteLine("\n Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    AguardarEnter();
                }
            } while (opcao != 0);
        }

        static void TecnicoVerEquipamentosNovos()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║   EQUIPAMENTOS NOVOS PARA ATENDIMENTO    ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var equipamentosSemOrdem = sistema.Equipamentos
                .Where(e => !sistema.Ordens.Any(o => o.Id == e.Id && o.Status != "Pendente"))
                .ToList();

            if (equipamentosSemOrdem.Count == 0)
            {
                Console.WriteLine(" Não há equipamentos novos para atendimento.");
                return;
            }

            foreach (var eq in equipamentosSemOrdem)
            {
                Console.WriteLine($" ID: {eq.Id} | Tipo: {eq.GetType().Name} | Cliente: {eq.NomeCliente}");
                Console.WriteLine($" Descrição: {eq.Descricao}");
                Console.WriteLine($" Data de Entrada: {eq.DataEntrada}");
                Console.WriteLine();
            }
        }

        static void TecnicoAbrirOrdem()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║          ABRIR ORDEM DE SERVIÇO          ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var equipamentosSemOrdem = sistema.Equipamentos
                .Where(e => !sistema.Ordens.Any(o => o.Id == e.Id))
                .ToList();

            if (equipamentosSemOrdem.Count == 0)
            {
                Console.WriteLine(" Não há Equipamentos Novos para Criar nova Ordem de Serviço.");
                return;
            }

            Console.WriteLine(" Equipamentos Disponíveis:");
            for (int i = 0; i < equipamentosSemOrdem.Count; i++)
            {
                Console.WriteLine($" {i + 1} - ID: {equipamentosSemOrdem[i].Id} | Tipo: {equipamentosSemOrdem[i].Descricao}");
                Console.WriteLine();
            }

            Console.Write("\n Escolha o equipamento para a ordem de serviço: ");
            if (!int.TryParse(Console.ReadLine(), out int idEquipamento) || idEquipamento < 1 || idEquipamento > equipamentosSemOrdem.Count)
            {
                Console.WriteLine("\n Opção inválida!");
                AguardarEnter();
                return;
            }

            var equipamentoSelecionado = equipamentosSemOrdem[idEquip- 1];

            Console.Write(" Descrição do serviço a ser realizado: ");
            string descricaoServico = Console.ReadLine()!;

            Console.Write(" Valor do Orçamento: (R$) ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valorOrcamento) || valorOrcamento <= 0)
            {
                Console.WriteLine("\n Valor do orçamento inválido!");
                AguardarEnter();
                return;
            }

            var ordem = new OrdemServico
            {
                Id = equipamentoSelecionado.Id,
                Servico = descricaoServico,
                ValorTotal = new Domain.ValueObjects.Money((long) Math.Round (valorOrcamento * 100), "BRL"),
                Status = "Normal"
            }

            sistema.AbrirOrdem(ordem);
            tecnicoLogado!.AtribuirOrdem(ordem);
            Console.WriteLine("\n Ordem de Serviço Aberta com Sucesso! #{ordem.Id}");
        }

        static void TecnicoVerOrdens()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║         MINHAS ORDENS DE SERVIÇO         ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var ordensTecnico = sistema.Ordens
                .Where(o => o.Id > 0)
                .ToList();

            if (ordensTecnico.Count == 0)
            {
                Console.WriteLine(" Você não possui ordens de serviço atribuídas.");
                AguardarEnter();
                return;
            }

            foreach (var ordem in tecnicoLogado.Ordens)
            {
                Console.WriteLine($" Ordem ID: {ordem.Id} | Serviço: {ordem.Servico}");
                Console.WriteLine($" Status: {ordem.Status} | Valor Total: R$ {ordem.ValorTotal.ToDecimal():F2}");
                Console.WriteLine($" DATA de Abertura: {ordem.DataEntrada}");
                if (ordem.Pagamento != null)
                {
                    Console.WriteLine($" Pago: {ordem.Pagamento.ExibirResumo()}");
                }
                else
                {
                    Console.WriteLine(" Pagamento: Pendente");
                }
                Console.WriteLine();
            }
        }

        static void TecnicoDiagnosticar()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║           DIAGNOSTICAR SERVIÇO           ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var ordensEmAndamento = tecnicoLogado!.Ordens
                .Where(o => o.Status == "Finalizada" || o.Status == "Em Execução")
                .ToList();

            if (ordensEmAndamento.Count == 0)
            {
                Console.WriteLine(" Você não possui ordens de serviço em andamento para diagnosticar.");
                AguardarEnter();
                return;
            }

            Console.WriteLine(" Ordens de Serviço para Diagnóstico:");
            for (int i = 0; i < ordensEmAndamento.Count; i++)
            {
                Console.WriteLine($" {i + 1} - Ordem ID: {ordensEmAndamento[i].Id} | Serviço: {ordensEmAndamento[i].Servico}");
                Console.WriteLine($" Status: {ordensEmAndamento[i].Status} | Valor Total: R$ {ordensEmAndamento[i].ValorTotal.ToDecimal():F2}");
                Console.WriteLine();
            }

            Console.Write("\n Escolha ama ordem de serviço para diagnosticar: ");
            if (!int.TryParse(Console.ReadLine(), out int idOrdem) || idOrdem < 1 || idOrdem > ordensEmAndamento.Count)
            {
                Console.WriteLine("\n Opção inválida!");
                AguardarEnter();
                return;
            }

            var ordem = ordensAbertas[idOrdem - 1];
            var equipamento = sistema.BuscarPorId(ordem.Id);

            if (equipamento == null)
            {
                tecnicoLogado.RealizarDiagnostico(equipamento);
                Console.WriteLine("\n Diagnóstico realizado com Sucesso!");
                return;
            }
        }

        static void TecnicoExecutarServico()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║            EXECUTAR SERVIÇO              ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var ordensAbertas = tecnicoLogado!.Ordens
                .Where(o => o.Status == "Finalizada" || o.Status == "Em Execução")
                .ToList();

            if (ordensAbertas.Count == 0)
            {
                Console.WriteLine(" Você não possui ordens de serviço em andamento para executar.");
                AguardarEnter();
                return;
            }

            Console.WriteLine(" Ordens de Serviço para Execução:");
            for (int i = 0; i < ordensAbertas.Count; i++)
            {
                Console.WriteLine($" {i + 1} - Ordem ID: {ordensAbertas[i].Id} | Serviço: {ordensAbertas[i].Servico}");
                Console.WriteLine($" Status: {ordensAbertas[i].Status} | Valor Total: R$ {ordensAbertas[i].ValorTotal.ToDecimal():F2}");
                Console.WriteLine();
            }

            Console.Write("\n Escolha uma ordem de serviço para executar: ");
            if (!int.TryParse(Console.ReadLine(), out int idOrdem) || idOrdem < 1 || idOrdem > ordensAbertas.Count)
            {
                Console.WriteLine("\n Opção inválida!");
                AguardarEnter();
                return;
            }

            var ordem = ordensAbertas[idOrdem - 1];
            ordem.Status = "Em Execução";

            Console.WriteLine($"\n Serviço em execução {ordem.Servico}...");
            System.Threading.Thread.Sleep(1500); /// Simula o tempo de execução do serviço ///
            Console.WriteLine(" Serviço executado com Sucesso!");
        }

        static void TecnicoFinalizarOrdem()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║        FINALIZAR ORDEM DE SERVIÇO        ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            var ordensEmProgresso = tecnicoLogado!.Ordens
                .Where(o => o.Status == "Finalizada" || o.Status == "Em Execução")
                .ToList();

            if (ordensEmProgresso.Count == 0)
            {
                Console.WriteLine(" Você não possui ordens de serviço em andamento para finalizar.");
                AguardarEnter();
                return;
            }

            Console.WriteLine(" Ordens de Serviço para Finalização:");
            for (int i = 0; i < ordensEmProgresso.Count; i++)
            {
                Console.WriteLine($" {i + 1} - Ordem ID: {ordensEmProgresso[i].Id} | Serviço: {ordensEmProgresso[i].Servico}");
                Console.WriteLine($" Status: {ordensEmProgresso[i].Status} | Valor Total: R$ {ordensEmProgresso[i].ValorTotal.ToDecimal():F2}");
                Console.WriteLine();
            }

            Console.Write("\n Escolha uma ordem de serviço para finalizar: ");
            if (!int.TryParse(Console.ReadLine(), out int idOrdem) || idOrdem < 1 || idOrdem > ordensEmProgresso.Count)
            {
                Console.WriteLine("\n Opção inválida!");
                AguardarEnter();
                return;
            }

            var ordem = ordensEmProgresso[idOrdem - 1];
            ordem.Finalizar();
            Console.WriteLine($"\n Ordem de Serviço #{ordem.Id} finalizada com Sucesso!");
            Console.WriteLine(" Cliente Agora Pode Realizar o Pagamento!");
        }

        static void TecnicoRelatorio()
        {
            Console.Clear();
            ExibirTitulo();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║          MEU RELATÓRIO DE SERVIÇOS       ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

            Console.WriteLine($" Relatório de Ordens de Serviço de {tecnicoLogado!.Nome}:\n");
            Console.WriteLine($" Especialidade: {tecnicoLogado.Especialidade}\n");
            Console.WriteLine($" Total de Ordens Atribuídas: {tecnicoLogado.Ordens.Count}\n");
            Console.WriteLine();

            var ordensAbertas = tecnicoLogado.Ordens.Count(o => o.Status == "Pendente");
            var ordensFinalizadas = tecnicoLogado.Ordens.Count(o => o.Status == "Finalizado");
            var ordensPagas = tecnicoLogado.Ordens.Count(o => o.Pagamento != null);

            Console.WriteLine($" Status das Ordens:");
            Console.WriteLine($" Total de Ordens em Execução: {ordensAbertas}\n");
            Console.WriteLine($" Total de Ordens Finalizadas: {ordensFinalizadas - ordensPagas}\n");
            Console.WriteLine($" Pagas: {ordensPagas}\n");
            Console.WriteLine();

            var totalserviços = tecnicoLogado.Ordens.Count;
            if (totalserviços > 0)
            {
                var percentualConclusao = (ordensFinalizadas * 100) / totalserviços;
                Console.WriteLine($" Percentual de Conclusão: {percentualConclusao}%");
            }

            Console.WriteLine(" Resumo de Valores Recebidos:");
            var valorTotal = tecnicoLogado.Ordens.Where(o => o.Pagamento != null).Sum(o => o.ValorTotal.ToDecimal());
            var valorPago = tecnicoLogado.Ordens.Where(o => o.Pagamento != null).Sum(o => o.ValorTotal.ToDecimal());
            var valorPendente = valorTotal - valorPago;

            Console.WriteLine($" Valor Total dos Serviços: R$ {valorTotal:F2}");
            Console.WriteLine($" Valor Total Recebido: R$ {valorPago:F2}");
            Console.WriteLine($" Valor Pendente: R$ {valorPendente:F2}");
        }

        /// <summary>
        /// Funçoes de Suporte
        /// </summary>

        static void ExibirTitulo()
        {
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║   SISTEMA DE MANUTENÇÃO DE COMPUTADORES  ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝");
        }

        static void AguardarEnter()
        {
            Console.WriteLine("\n Pressione Enter para continuar...");
            Console.ReadLine();
        }

        static Notebook CadastrarNotebook(string descricao)
        {
            Console.Write(" Marca do notebook: ");
            string marca = Console.ReadLine()!;

            Console.Write(" Modelo da Bateria: ");
            string modelobateria = Console.ReadLine()!;

            return new Notebook(sistema.GetProximoId(), descricao, clienteLogado!.Nome,  DateTime.Now, "Aguardando", marca, modelobateria);
        }

        static Desktop CadastrarDesktop(string descricao)
        {
            Console.Write(" Tipo do desktop (Gamer/Office): ");
            string tipo = Console.ReadLine()!;

            Console.Write(" Tem Fonte Redundante (S/N): ");
            bool temFonteRedundante = Console.ReadLine()!.ToUpper() == "S";

            return new Desktop(sistema.GetProximoId(), descricao, clienteLogado!.Nome, DateTime.Now, "Aguardando", tipo, temFonteRedundante);
        }

        static Servidor CadastrarServidor(string descricao)
        {
            Console.Write(" Marca do Servidor: ");
            string marca = Console.ReadLine()!;

            Console.Write(" Tipo (Fisico/Virtual): ");
            string tipo = Console.ReadLine()!;

            Console.Write(" Quantidade de Racks: ");
            int quantidadeRacks = int.Parse(Console.ReadLine()!);

            return new Servidor(sistema.GetProximoId(), descricao, clienteLogado!.Nome, DateTime.Now, "Aguardando", quantidadeRacks, tipo);
        }

        static Impressora CadastrarImpressora(string descricao)
        {

            Console.Write(" Tipo (Jato de Tinta): ");
            string tipo = Console.ReadLine()!;

            Console.Write(" Nivel: ");
            int nivel = int.Parse(Console.ReadLine()!);

            return new Impressora(sistema.GetProximoId(), descricao, clienteLogado!.Nome, DateTime.Now, "Aguardando", tipo, nivel);
        }

        static void inicializarDados()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

    }
}
