using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models.ViewModel
{
    public class ViewUsuarioLogin
    {
        [Required(ErrorMessage = "Correo incorrecto")]
        [RegularExpression(@"^[\w-_]+(\.[\w!#$%'*+\/=?\^`{|}]+)*@((([\-\w]+\.)+[a-zA-Z]{2,20})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
       ErrorMessage = "Correo Invalido")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "La contraseña no puede ser mayor a 20 letras")
         , MinLength(3, ErrorMessage = "La contraseña debe ser mayor a 3 letras")]
        public string Password { get; set; }
    }
}
