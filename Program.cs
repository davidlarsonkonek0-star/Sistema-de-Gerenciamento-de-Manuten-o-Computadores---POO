﻿using Domain.Entities;
class Program
{
    static SistemaManutencao sistema = new SistemaManutencao();

    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine(" ╔══════════════════════════════════════════╗");
            Console.WriteLine(" ║ SISTEMA DE MANUTENÇÃO DE COMPUTADORES ║");
            Console.WriteLine(" ╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine(" 1 - Cadastrar Equipamento");
            Console.WriteLine(" 2 - Listar Equipamentos");
            Console.WriteLine(" 3 - Buscar por ID");
            Console.WriteLine(" 4 - Alterar Status");
            Console.WriteLine(" 5 - Remover Equipamento");
            Console.WriteLine(" 6 - Diagnosticar Todos");
            Console.WriteLine(" 7 - Gerar Relatório Geral");
            Console.WriteLine(" 0 - Sair");
            Console.WriteLine();
            Console.Write(" Escolha: ");

            opcao = int.Parse(Console.ReadLine()!);

            switch (opcao)
            {
                case 1:
                    CadastrarEquipamento();
                    break;
                case 2:
                    sistema.ListarEquipamentos();
                    break;
                case 3:
                    BuscarPorId();
                    break;
                case 4:
                    AlterarStatus();
                    break;
                case 5:
                    RemoverEquipamento();
                    break;
                case 6:
                    sistema.DiagnosticarTodos();
                    break;
                case 7:
                    sistema.GerarRelatorioGeral();
                    break;
                case 0:
                    Console.WriteLine("\n Saindo... Até logo!");
                    break;
                default:
                    Console.WriteLine("\n Opção inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\n Pressione Enter para continuar...");
                Console.ReadLine();
            }

        } while (opcao != 0);
    }

    static void CadastrarEquipamento()
    {
        Console.Clear();
        Console.WriteLine(" ╔══════════════════════════════════════════╗");
        Console.WriteLine(" ║ CADASTRAR EQUIPAMENTO ║");
        Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

        Console.WriteLine(" Tipo:");
        Console.WriteLine(" 1 - Notebook");
        Console.WriteLine(" 2 - Desktop");
        Console.WriteLine(" 3 - Servidor");
        Console.WriteLine(" 4 - Impressora");
        Console.Write("\n Escolha o tipo: ");
        int tipo = int.Parse(Console.ReadLine()!);

        Console.Write(" Nome do cliente: ");
        string nomeCliente = Console.ReadLine()!;

        Console.Write(" Descrição do problema: ");
        string descricao = Console.ReadLine()!;

        string dataEntrada = DateTime.Now.ToString("dd/MM/yyyy");
        string status = "Aguardando";
        int id = sistema.GetProximoId();

        switch (tipo)
        {
            case 1:
                Console.Write(" Marca: ");
                string marca = Console.ReadLine()!;
                Console.Write(" Modelo da bateria: ");
                string bateria = Console.ReadLine()!;
                sistema.CadastrarEquipamento(new Notebook(id, descricao, nomeCliente, dataEntrada, status, marca, bateria));
                break;
            case 2:
                Console.Write(" Tipo de gabinete: ");
                string tipoGabinete = Console.ReadLine()!;
                Console.Write(" Tem fonte redundante? (s/n): ");
                bool temFonteRedundante = Console.ReadLine()!.ToLower() == "s";
                sistema.CadastrarEquipamento(new Desktop(id, descricao, nomeCliente, dataEntrada, status, tipoGabinete, temFonteRedundante));
                break;
            case 3:
                Console.Write(" Número de racks: ");
                int racks = int.Parse(Console.ReadLine()!);
                Console.Write(" Criticidade (Alta/Média/Baixa): ");
                string criticidade = Console.ReadLine()!;
                sistema.CadastrarEquipamento(new Servidor(id, descricao, nomeCliente, dataEntrada, status, racks, criticidade));
                break;
            case 4:
                Console.Write(" Tipo de impressão (Laser/Jato de Tinta): ");
                string tipoImpressao = Console.ReadLine()!;
                Console.Write(" Nível de toner (%): ");
                double toner = double.Parse(Console.ReadLine()!);
                sistema.CadastrarEquipamento(new Impressora(id, descricao, nomeCliente, dataEntrada, status, tipoImpressao, toner));
                break;
            default:
                Console.WriteLine("\n Tipo inválido!");
                break;
        }
    }

    static void BuscarPorId()
    {
        Console.Clear();
        Console.WriteLine(" ╔══════════════════════════════════════════╗");
        Console.WriteLine(" ║ BUSCAR POR ID ║");
        Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

        Console.Write(" ID do equipamento: ");
        int id = int.Parse(Console.ReadLine()!);

        var equipamento = sistema.BuscarPorId(id);
        if (equipamento != null)
        {
            Console.WriteLine();
            equipamento.ExibirInfo();
        }
        else
        {
            Console.WriteLine("\n Equipamento não encontrado.");
        }
    }

    static void AlterarStatus()
    {
        Console.Clear();
        Console.WriteLine(" ╔══════════════════════════════════════════╗");
        Console.WriteLine(" ║ ALTERAR STATUS ║");
        Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

        Console.Write(" ID do equipamento: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.WriteLine("\n Status:");
        Console.WriteLine(" 1 - Aguardando");
        Console.WriteLine(" 2 - Em manutenção");
        Console.WriteLine(" 3 - Concluído");
        Console.Write("\n Escolha: ");
        int opcao = int.Parse(Console.ReadLine()!);

        string? novoStatus = opcao switch
        {
            1 => "Aguardando",
            2 => "Em manutenção",
            3 => "Concluído",
            _ => null
        };

        if (novoStatus != null)
            sistema.AlterarStatus(id, novoStatus);
        else
            Console.WriteLine("\n Opção inválida!");
    }

    static void RemoverEquipamento()
    {
        Console.Clear();
        Console.WriteLine(" ╔══════════════════════════════════════════╗");
        Console.WriteLine(" ║ REMOVER EQUIPAMENTO ║");
        Console.WriteLine(" ╚══════════════════════════════════════════╝\n");

        Console.Write(" ID do equipamento: ");
        int id = int.Parse(Console.ReadLine()!);

        sistema.RemoverEquipamento(id);
    }
}