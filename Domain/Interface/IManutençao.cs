namespace Domain.Interface
{
    /// <summary>
    /// Interface de comportamento para manutenção de equipamentos.
    /// </summary>
    public interface IManutencao
    {
        /// <summary>Realiza a manutenção.</summary>
        void RealizarManutencao();

        /// <summary>Gera um relatório de manutenção.</summary>
        string GerarRelatorio();
    }
}