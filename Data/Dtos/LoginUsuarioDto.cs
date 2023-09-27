using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
    public class LoginUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
