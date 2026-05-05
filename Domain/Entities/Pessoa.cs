namespace Domain.Entities
{
    /// <summary>
    /// Classe abstrata que representa uma pessoa no sistema.
    /// </summary>
    public abstract class Pessoa
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
    }
}