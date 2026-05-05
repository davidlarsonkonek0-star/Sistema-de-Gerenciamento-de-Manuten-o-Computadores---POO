using Domain.Interface;

namespace Domain.Entities
{
    public abstract class Equipamento : IManutencao, IDiagnostico
    {
        public int Id { get; protected set; }
        public string Descricao { get; protected set; }
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
}