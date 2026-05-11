namespace Domain.Entities
{
    /// <summary>
    /// Classe abstrata que representa uma pessoa no sistema.
    /// </summary>
    public abstract class Pessoa
    {
        public string Id { get; private set; } 
        public string Nome { get; private set; }

        public Pessoa(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

    }
}
