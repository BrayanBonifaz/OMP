using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPatentes.Areas.Usuarios.Models;
using WebPatentes.Library;

namespace WebPatentes.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {
        //private LUsuarios _usuarios;
        private ListObject objeto = new ListObject();

        public RegistrarModel(RoleManager<IdentityRole> roleManager, IHostingEnvironment environment)
        {
            objeto._roleManager = roleManager;
            objeto._usuarios = new LUsuarios();
            objeto._usersRole = new UsersRoles();
            objeto._environment = environment;
            objeto._image = new UploadImage();
        }


        public void OnGet()
        {
            Input = new InputModel
            {
                rolesLista = objeto._usersRole.getRoles(objeto._roleManager)
            };
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : InputModelRegistrar
        {
            [Required]
            public string Role { get; set; }

            public IFormFile AvatarImage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await GuardarAsync();
            return Page();
        }

        private async Task GuardarAsync()
        {
            try
            {
                var imageName = Input.Email + ".png";
                await objeto._image.CopiarImageAsync(Input.AvatarImage, imageName, objeto._environment, "Usuarios");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}