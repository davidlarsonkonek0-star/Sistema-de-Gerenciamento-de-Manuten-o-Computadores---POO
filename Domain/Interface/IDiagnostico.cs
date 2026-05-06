namespace Domain.Interface
{
    /// <summary>
    /// Interface de comportamento para diagnóstico de equipamentos.
    /// </summary>
    public interface IDiagnostico
    {
        /// <summary>Executa o diagnóstico.</summary>
        void Diagnosticar();

        /// <summary>Retorna o status após o diagnóstico.</summary>
        string VerificarStatus();
    }
}