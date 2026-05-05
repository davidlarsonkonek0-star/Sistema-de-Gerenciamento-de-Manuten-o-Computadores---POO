using Domain.Entities;
using Domain.Interface;
using Domain.ValueObjects;

namespace Domain.Services
{
    public class OrdemServicoService
    {
        private readonly IOrdemServicoRepository _repositorio;

        public OrdemServicoService(IOrdemServicoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AbrirOrdem(OrdemServico ordem)
        {
            ordem.Status = "Aberta";
            ordem.DataEntrada = DateTime.Now.ToString("yyyy-MM-dd");
            _repositorio.Add(ordem);
        }

        public Money CalcularOrcamento(OrdemServico ordem)
        {
            return ordem.CalcularOrcamento();
        }

        public void FinalizarOrdem(int id)
        {
            var ordem = _repositorio.GetById(id);
            if (ordem != null)
            {
                ordem.Status = "Finalizada";
                _repositorio.Update(ordem);
            }
        }

        public List<OrdemServico> ListarOrdens()
        {
            return _repositorio.GetAll();
        }
    }
}
