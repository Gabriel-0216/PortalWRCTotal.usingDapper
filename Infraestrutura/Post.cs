using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infraestrutura
{
    public class Post
    {
        public int Id { get; set; }
        public string Assunto { get; set; }

        [AllowHtml]
        public string Texto { get; set; }
    }
}
