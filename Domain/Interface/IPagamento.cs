namespace Domain.Interface
{
    public interface IPagamento
    {
        void Processar();
        string ExibirResumo();
    }
}
