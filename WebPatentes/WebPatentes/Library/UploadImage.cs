using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebPatentes.Library
{
    public class UploadImage
    {
        public async Task CopiarImageAsync(IFormFile avatarImage, string fileName, IHostingEnvironment environment,string carpeta)
        {
            if (avatarImage == null)
            {
                var archivoOrigen = environment.ContentRootPath + "/wwwroot/images/fotos/" + carpeta + "/default.png";
                var destFileName = environment.ContentRootPath + "/wwwroot/images/fotos/" + carpeta + "/" + fileName;
                File.Copy(archivoOrigen, destFileName, true);
            }
            else
            {
                var filePath = Path.Combine(environment.ContentRootPath, "wwwroot/images/fotos/" + carpeta, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatarImage.CopyToAsync(stream);
                }
            }

        }
    }
}
