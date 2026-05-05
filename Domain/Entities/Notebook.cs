public class Notebook : Equipamento
{
    private string Marca { get; set; }
    private string ModeloBateria { get; set; }

    public Notebook(int id, string descricao, string nomeCliente, string dataEntrada, string status, string marca, string modeloBateria)
        : base(id, descricao, nomeCliente, dataEntrada, status)
    {
        Marca = marca;
        ModeloBateria = modeloBateria;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("  Tipo:       Notebook");
        base.ExibirInfo();
        Console.WriteLine($"  Marca:      {Marca}");
        Console.WriteLine($"  Bateria:    {ModeloBateria}");
    }

    public override void RealizarManutencao()
    {
        Console.WriteLine($"[Notebook - {NomeCliente}] Realizando limpeza interna e verificação de hardware...");
    }

    public override void Diagnosticar()
    {
        Console.WriteLine($"[Notebook - {NomeCliente}] Verificando bateria {ModeloBateria}... possível desgaste, troca recomendada.");
    }

    public override string GerarRelatorio()
    {
        return $"Notebook | Cliente: {NomeCliente} | Marca: {Marca} | Bateria: {ModeloBateria} | Status: {Status}";
    }

    public override string VerificarStatus()
    {
        return $"[Notebook - ID {Id}] Status atual: {Status}";
    }
}