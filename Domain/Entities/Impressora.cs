public class Impressora : Equipamento
{
    private string TipoImpressao { get; set; }
    private double NivelToner { get; set; }

    public Impressora(int id, string descricao, string nomeCliente, string dataEntrada, string status, string tipoImpressao, double nivelToner)
        : base(id, descricao, nomeCliente, dataEntrada, status)
    {
        TipoImpressao = tipoImpressao;
        NivelToner = nivelToner;
    }

    public override void ExibirInfo()
    {
        Console.WriteLine("  Tipo:       Impressora");
        base.ExibirInfo();
        Console.WriteLine($"  Impressão:  {TipoImpressao}");
        Console.WriteLine($"  Toner:      {NivelToner}%");
    }

    public override void RealizarManutencao()
    {
        Console.WriteLine($"[Impressora - {NomeCliente}] Limpando cabeçote e verificando nível de toner...");
    }

    public override void Diagnosticar()
    {
        string alerta = NivelToner < 20 ? "NÍVEL CRÍTICO, reposição urgente!" : "nível adequado.";
        Console.WriteLine($"[Impressora - {NomeCliente}] Toner em {NivelToner}% - {alerta}");
    }

    public override string GerarRelatorio()
    {
        return $"Impressora | Cliente: {NomeCliente} | Tipo: {TipoImpressao} | Toner: {NivelToner}% | Status: {Status}";
    }

    public override string VerificarStatus()
    {
        return $"[Impressora - ID {Id}] Toner: {NivelToner}% | Status atual: {Status}";
    }
}
