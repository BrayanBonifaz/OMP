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

        public RegistrarModel(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager, IHostingEnvironment environment)
        {
            objeto._roleManager = roleManager;
            objeto._userManager = userManager;
            objeto._usuarios = new LUsuarios();
            objeto._usersRole = new UsersRoles();
            objeto._environment = environment;
            objeto._image = new UploadImage();
            objeto._userRoles = new List<SelectListItem>();
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
            [TempData]
            public string ErrorMessage { get; set; }
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
                objeto._userRoles.Add(new SelectListItem {
                    Text = Input.Role
                });
                var userList = objeto._userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if (userList.Count == 0)
                {
                    var imageName = Input.Email + ".png";
                    var user = new IdentityUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        PhoneNumber = Input.Telefono
                    };
                    var result = await objeto._userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        await objeto._image.CopiarImageAsync(Input.AvatarImage, imageName, objeto._environment, "Usuarios");

                        Input = new InputModel
                        {
                            ErrorMessage = string.Concat("El ", Input.Email, " fue registrado correctamente"),
                            rolesLista = objeto._userRoles
                        };
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            Input = new InputModel
                            {
                                ErrorMessage = item.Description,
                                rolesLista = objeto._userRoles
                            };
                        }

                    }
                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMessage = string.Concat("El ", Input.Email, " ya esta registrado"),
                        rolesLista = objeto._userRoles
                    };
                }

            
            }
            catch (Exception ex)
            {
                Input = new InputModel
                {
                    ErrorMessage = ex.Message,
                    rolesLista = objeto._userRoles
                };
            }
        }
    }
}