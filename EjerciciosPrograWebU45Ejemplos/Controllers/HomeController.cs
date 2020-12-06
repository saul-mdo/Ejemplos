using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using EjerciciosPrograWebU45Ejemplos.Helpers;

namespace EjerciciosPrograWebU45Ejemplos.Controllers
{
    public class HomeController : Controller
    {
        public IWebHostEnvironment Environment { get; set; }
        public HomeController(IWebHostEnvironment env)
        {
            Environment = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarCorreo(string email, string texto)
        {
            MailMessage message = new MailMessage();
            // ESPECIFICAR DE QUÉ CORREO VA A SALIR EL MENSAJE. EL SEGUNDO PARAMETRO ES EL NOMBRE QUE VA A SALIR EN EL CORREO.
            message.From = new MailAddress("noreply@sistemas171.com", "Cuenta Automatizada Sistemas171");
            // A QUE CORREO SE VA A ENVIAR. PUEDE ENVIARSE A VARIOS A LA VEZ.
            message.To.Add(email);
            // EL ASUNTO NO PUEDE DEJARSE EN NULO.
            message.Subject = "Ejemplo de Correo";

            // ATTACHMENTS SIRVE PARA ENVIAR ARCHIVOS. 
            //message.Attachments.Add(new Attachment(Environment.WebRootPath + "/htmlpage.html"));
            
            // BODY ES EL CUERPO DEL MENSAJE.
            //1. SE ENVÍA EL TEXTO DEL TEXTBOX.
            //message.Body = texto;

            //2. PODEMOS ENVÍAR UN TEXTO POR DEFAULT CON FORMATO EN HTML. SOLO TENEMOS QUE AGREGAR EL ISBODYHTML EN TRUE.
            //message.Body = "¡Bienvenido a Sistemas171!<br/> Eres libre de navegar por el sitio.<br/>" + "<mark>" + DateTime.Now + "</mark>";
            //message.IsBodyHtml = true;

            // 3. SE PUEDE ENVIAR UNA PAGINA HTML.
            string text = System.IO.File.ReadAllText(Environment.WebRootPath + "/htmlpage.html");
            message.Body = text.Replace("{##correo##}",email);
            message.IsBodyHtml = true;

            // NECESITA DOS PARAMETROS, EL HOST (DESDE DONDE SE VA A ENVIAR EL CORREO) Y EL PUERTO.
            SmtpClient client = new SmtpClient("mail.sistemas171.com",2525);
            // LAS CREDENCIALES SON LOS DATOS DE AUTENTICACION DEL USUARIO.
            client.UseDefaultCredentials = false;
            // AQUI SON LOS DATOS DE AUTENTICACIÓN DEL CORREO POR EL CUAL SE VA A MANDAR EL CORREO. SE USA EL CORREO Y LA CONTRASEÑA.
            client.Credentials = new NetworkCredential("noreply@sistemas171.com", "##ITESRC2020");
            client.Send(message);

            return RedirectToAction("Index");
        }

        // CODIFICACIÓN: Se usa para almacenar elementos que no pueden ser guardados en una base de datos, como imagenes o documentos. Transforma el archivo en una cadena de string. mostrar un dato para que se vea diferente y no sea legible para una persona. Tambien sirve para transferirlo o guardarlo en un archivo. NO ES SEGURO. Podemos almacenar archivos dentro de la BD. Una vez codificado se puede encriptar.
        // ENCRIPTACIÓN:  es un procedimiento mediante el cual los archivos, o cualquier tipo de documento, se vuelven completamente ilegibles gracias a un algoritmo que desordena sus componentes.
        // HASHING: toma un objeto y lo reduce a un arreglo de bits, para obtener un identificador unico de ese objeto. No se puede regresarlo a su estado original. Se usa para diferenciar un objeto de otro objeto. Permite reducir objetos de tamaño muy grande a un arreglo.

        public IActionResult VerImagen()
        {
            return View();
        }

        public IActionResult Checksum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checksum(string cadena)
        {
            return View((object)HashingHelper.GetHash(cadena));
        }
    }
}
