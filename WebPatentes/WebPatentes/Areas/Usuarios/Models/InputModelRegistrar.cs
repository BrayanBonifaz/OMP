using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPatentes.Areas.Usuarios.Models
{
    public class InputModelRegistrar
    {
        [Required(ErrorMessage = "<font color='red'>El Campo nombre es obligatorio.</font>")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "<font color='red'>El Campo apellido es obligatorio.</font>")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "<font color='red'>El Campo nid es obligatorio.</font>")]
        public string NID { get; set; }

        [Required(ErrorMessage = "<font color='red'>El Campo teléfono es obligatorio.</font>")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "<font color='red'>El Campo correo es obligatorio.</font>")]
        [EmailAddress(ErrorMessage = "<font color='red'>El Campo correo no es una dirección de correo eletrónico válida.</font>")]
        public string Email { get; set; }

        [Required(ErrorMessage = "<font color='red'>El Campo contraseña es obligatorio.</font>")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "<font color='red'>El número de caracteres de {0} de ser al menos {2}.</font>", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
