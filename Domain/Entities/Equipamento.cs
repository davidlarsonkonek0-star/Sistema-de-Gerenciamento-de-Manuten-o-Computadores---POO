public abstract class Equipamento : IManutensivel, IDiagnosticavel
{
    protected int Id { get; set; }
    protected string Descricao { get; set; }
    protected string NomeCliente { get; set; }
    protected string DataEntrada { get; set; }
    protected string Status { get; set; }

    public Equipamento(int id, string descricao, string nomeCliente, string dataEntrada, string status)
    {
        Id = id;
        Descricao = descricao;
        NomeCliente = nomeCliente;
        DataEntrada = dataEntrada;
        Status = status;
    }

public int GetId()
{
    return Id;
}

public void SetStatus(string status)
{
    Status = status;
}
    public virtual void ExibirInfo()
    {
        Console.WriteLine($"  ID:         {Id}");
        Console.WriteLine($"  Cliente:    {NomeCliente}");
        Console.WriteLine($"  Problema:   {Descricao}");
        Console.WriteLine($"  Status:     {Status}");
        Console.WriteLine($"  Entrada:    {DataEntrada}");
    }

    public abstract void RealizarManutencao();
    public abstract void Diagnosticar();
    public abstract string GerarRelatorio();
    public abstract string VerificarStatus();
}