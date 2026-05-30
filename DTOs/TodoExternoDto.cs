namespace ApiInteligenteTareas.DTOs
{
    public class TodoExternoDto
    {
        public int ExternalId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public bool Completado { get; set; }
    }
}
