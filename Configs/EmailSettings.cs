namespace Project.Configs

{
    public class EmailSettings
    {
        public required string Email { get; set; }
        public string? DisplayName { get; set; }
        public required string Password { get; set; }
        public required string Host { get; set; }
        public int Port { get; set; }
    }
}
