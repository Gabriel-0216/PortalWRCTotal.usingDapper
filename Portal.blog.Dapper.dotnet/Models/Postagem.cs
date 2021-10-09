using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portal.blog.Dapper.dotnet.Models
{
    public class Postagem
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="É NECESSÁRIO DIGITAR O ASSUNTO DO TEXTO")]
        public string Assunto { get; set; }
        [Required(ErrorMessage ="É necessário colocar o corpo do texto")]
        [AllowHtml]
        public string Texto { get; set; }


    }
}
