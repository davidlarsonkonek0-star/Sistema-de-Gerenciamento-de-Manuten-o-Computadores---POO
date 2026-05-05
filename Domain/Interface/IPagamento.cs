namespace Domain.Interface
{
    /// <summary>
    /// Interface de comportamento para pagamentos.
    /// </summary>
    public interface IPagamento
    {
        /// <summary>Processa o pagamento.</summary>
        void Processar();

        /// <summary>Retorna um resumo do pagamento.</summary>
        string ExibirResumo();
    }
}
