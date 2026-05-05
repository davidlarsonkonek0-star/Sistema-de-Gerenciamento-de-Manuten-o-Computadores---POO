using Domain.Entities;

namespace Domain.Interface
{
    public interface IOrdemServicoRepository
    {
        void Add(OrdemServico ordem);
        OrdemServico? GetById(int id);
        void Update(OrdemServico ordem);
        List<OrdemServico> GetAll();
    }
}
