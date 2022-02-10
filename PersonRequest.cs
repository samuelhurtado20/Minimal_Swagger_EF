namespace Minimal_Swagger_EF
{
    public class PersonRequest
    {
        public string Name { get; set; } = null!;
        public short Age { get; set; }
        public string? JobPosition { get; set; }
    }
}
