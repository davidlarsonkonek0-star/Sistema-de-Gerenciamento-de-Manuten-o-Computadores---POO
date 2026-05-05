using Domain.Entities;
using Domain.Interface;
using Domain.Services;
using Domain.ValueObjects;

using Xunit;

namespace Tests.Integration
{
    public class OrdemServicoRepositoryIntegrationTests
    {
        private readonly IOrdemServicoRepository _repository;
        private readonly OrdemServicoService _service;

        public OrdemServicoRepositoryIntegrationTests()
        {
            _repository = new InMemoryOrdemServicoRepository();
            _service = new OrdemServicoService(_repository);
        }

        [Fact]
        public void AbrirOrdem_DeveAdicionarNoRepositorio()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Id = 1,
                Servico = "Limpeza",
                ValorTotal = new Money(3000, "BRL")
            };

            // Act
            _service.AbrirOrdem(ordem);

            // Assert
            var recuperada = _repository.GetById(1);
            Assert.NotNull(recuperada);
            Assert.Equal("Aberta", recuperada.Status);
        }

        [Fact]
        public void CalcularOrcamento_DeveRetornarValorCorreto()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Servico = "Troca de componentes",
                ValorTotal = new Money(25000, "BRL")
            };
            _repository.Add(ordem);

            // Act
            var orcamento = _service.CalcularOrcamento(ordem);

            // Assert
            Assert.Equal(25000, orcamento.AmountMinor);
        }

        [Fact]
        public void FinalizarOrdem_DeveAlterarStatus()
        {
            // Arrange
            var ordem = new OrdemServico
            {
                Id = 1,
                Servico = "Backup",
                Status = "Aberta",
                ValorTotal = new Money(5000, "BRL")
            };
            _repository.Add(ordem);

            // Act
            _service.FinalizarOrdem(1);

            // Assert
            var finalizada = _repository.GetById(1);
            Assert.Equal("Finalizada", finalizada?.Status);
        }

        [Fact]
        public void ListarOrdens_DeveRetornarTodasAsOrdens()
        {
            // Arrange
            var ordem1 = new OrdemServico { Id = 1, Servico = "Serviço 1" };
            var ordem2 = new OrdemServico { Id = 2, Servico = "Serviço 2" };
            _repository.Add(ordem1);
            _repository.Add(ordem2);

            // Act
            var ordens = _service.ListarOrdens();

            // Assert
            Assert.Equal(2, ordens.Count);
        }
    }
}
