using Domain.Entities;

namespace Domain.Interface
{
    /// <summary>
    /// Repositório para ordens de serviço.
    /// </summary>
    public interface IOrdemServicoRepository
    {
        /// <summary>Adiciona uma ordem ao repositório.</summary>
        void Add(OrdemServico ordem);

        /// <summary>Retorna uma ordem pelo ID.</summary>
        OrdemServico? GetById(int id);

        /// <summary>Atualiza os dados de uma ordem.</summary>
        void Update(OrdemServico ordem);

        /// <summary>Retorna todas as ordens.</summary>
        List<OrdemServico> GetAll();
    }
}