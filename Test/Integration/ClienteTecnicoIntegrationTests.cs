using Domain.Entities;
using Domain.ValueObjects;
using Xunit;

namespace Tests.Integration
{
    public class ClienteTecnicoIntegrationTests
    {
        [Fact]
        public void Cliente_DeveReferenciarPessoa()
        {
            // Arrange
            var cliente = new Cliente { Id = "C001", Nome = "João Silva", Telefone = "11999999999" };

            // Act & Assert
            Assert.Equal("C001", cliente.Id);
            Assert.Equal("João Silva", cliente.Nome);
            Assert.Equal("11999999999", cliente.Telefone);
        }

        [Fact]
        public void Cliente_DeveAdicionarEquipamentos()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Maria Santos" };
            var notebook = new Notebook { Descricao = "Notebook Dell", Marca = "Dell" };
            var desktop = new Desktop { Descricao = "Desktop HP", TipoGabinete = "ATX" };

            // Act
            cliente.AdicionarEquipamento(notebook);
            cliente.AdicionarEquipamento(desktop);

            // Assert
            Assert.Equal(2, cliente.Equipamentos.Count);
        }

        [Fact]
        public void Tecnico_DeveReferenciarPessoa()
        {
            // Arrange
            var tecnico = new Tecnico { Id = "T001", Nome = "Carlos Oliveira", Especialidade = "Hardware" };

            // Act & Assert
            Assert.Equal("T001", tecnico.Id);
            Assert.Equal("Carlos Oliveira", tecnico.Nome);
            Assert.Equal("Hardware", tecnico.Especialidade);
        }

        [Fact]
        public void Tecnico_DeveRealizarServicosEmEquipamentos()
        {
            // Arrange
            var tecnico = new Tecnico { Nome = "Pedro Expert", Especialidade = "Redes" };
            var notebook = new Notebook { Id = 1, Descricao = "Notebook Dell", Marca = "Dell" };

            // Act
            tecnico.RealizarServico(notebook);

            // Assert
            Assert.Equal("Manutenção concluída", notebook.Status);
        }

        [Fact]
        public void Tecnico_DeveListarOrdens()
        {
            // Arrange
            var tecnico = new Tecnico { Nome = "Ana Expert" };
            var ordem1 = new OrdemServico { Id = 1, Servico = "Diagnóstico" };
            var ordem2 = new OrdemServico { Id = 2, Servico = "Manutenção" };
            tecnico.Ordens.Add(ordem1);
            tecnico.Ordens.Add(ordem2);

            // Act & Assert
            Assert.Equal(2, tecnico.Ordens.Count);
        }

        [Fact]
        public void ClienteTecnicoFluxo_DeveInteragirCorretamente()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Empresa ABC", Telefone = "1133334444" };
            var tecnico = new Tecnico { Nome = "Técnico Principal", Especialidade = "Suporte" };

            var equipamento = new Impressora { Id = 1, Descricao = "Impressora Xerox", NivelToner = 0.3 };
            cliente.AdicionarEquipamento(equipamento);

            var ordem = new OrdemServico 
            { 
                Id = 1, 
                Servico = "Troca de toner", 
                ValorTotal = new Money(2000, "BRL") 
            };
            tecnico.Ordens.Add(ordem);

            // Act
            tecnico.RealizarServico(equipamento);

            // Assert
            Assert.Single(cliente.Equipamentos);
            Assert.Single(tecnico.Ordens);
            Assert.Equal("Manutenção concluída", equipamento.Status);
        }
    }
}
