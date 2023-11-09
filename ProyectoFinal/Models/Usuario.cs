using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Número de documento incorrecto")]
        public string Documento { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "El nombre de usuario es incorrecto")
         , MinLength(3, ErrorMessage = "El nombre de usuaario debe ser mayor a 3 letras")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Correo incorrecto")]
        [RegularExpression(@"^[\w-_]+(\.[\w!#$%'*+\/=?\^`{|}]+)*@((([\-\w]+\.)+[a-zA-Z]{2,20})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
        ErrorMessage = "Correo Invalido")]
        public string Email { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "La contraseña no puede ser mayor a 20 letras")
         ,MinLength(3, ErrorMessage = "La contraseña debe ser mayor a 3 letras")]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; }
    }
}
