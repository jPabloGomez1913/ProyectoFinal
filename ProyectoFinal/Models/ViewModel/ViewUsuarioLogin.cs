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
        [RegularExpression("^[0-9]*$", ErrorMessage = "Número de documento incorrecto")]
        public string Documento { get; set; }
    }
}
