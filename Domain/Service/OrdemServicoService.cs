using Domain.Entities;
using Domain.Interface;
using Domain.ValueObjects;

namespace Domain.Services
{
    /// <summary>
    /// Serviço para gerenciar ordens de serviço.
    /// </summary>
    public class OrdemServicoService
    {
        private readonly IOrdemServicoRepository _repositorio;

        /// <summary>Inicializa o serviço com um repositório de ordens.</summary>
        public OrdemServicoService(IOrdemServicoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>Abre uma nova ordem de serviço.</summary>
        public void AbrirOrdem(OrdemServico ordem)
        {
            ordem.Status = "Aberta";
            ordem.DataEntrada = DateTime.Now.ToString("yyyy-MM-dd");
            _repositorio.Add(ordem);
        }

        /// <summary>Calcula o orçamento de uma ordem.</summary>
        public Money CalcularOrcamento(OrdemServico ordem)
        {
            return ordem.CalcularOrcamento();
        }

        /// <summary>Finaliza uma ordem existente.</summary>
        public void FinalizarOrdem(int id)
        {
            var ordem = _repositorio.GetById(id);
            if (ordem != null)
            {
                ordem.Status = "Finalizada";
                _repositorio.Update(ordem);
            }
        }

        /// <summary>Retorna todas as ordens de serviço.</summary>
        public List<OrdemServico> ListarOrdens()
        {
            return _repositorio.GetAll();
        }
    }
}
