using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Representa um pagamento realizado via PIX.
    /// </summary>
    public class PagamentoEspecie : Pagamento
    {
        public string ValorMonetario { get; set; } = string.Empty;
                                                                                                              /// <summary>
                                                                                                              /// mudar para o cash para valore em especie
                                                                                                              /// </summary>
                                                                                                              /// <param name="Cash"></param>
                                                                                                    
        public PagamentoEspecie (string pagador, decimal valor, string valorMonetario) : base (pagador, new Money((long) Math.Round (valor * 100), "BRL"))
        {
            ValorMonetario = valorMonetario;
        }

        public override void Processar()
        {
            Console.WriteLine($"Processando pagamento em Espécie para {Pagador}.");
        }

        public override string ExibirResumo()
        {
            return $"Pagador: {Pagador} - Valor: {Valor}";
        }
    }
}