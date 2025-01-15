namespace RegistroUsuario.Models
{
    public class Users
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<Phone> phones { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public DateTime last_login { get; set; }
        public string token { get; set; }
        public bool is_active { get; set; }
    }
}
