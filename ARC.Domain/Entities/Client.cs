namespace ARC.Domain
{
    public class Client : Entity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
    }
}
