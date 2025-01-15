using RegistroUsuario.Models;

namespace RegistroUsuario.Dto
{
    public class UserDto
    {
        public string name {  get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<Phone> phone { get; set; }
    }

    public class Phone
    {
        public string number { get; set; }
        public string citycode { get; set; }
        public string countrycode { get; set; }
    }
}
