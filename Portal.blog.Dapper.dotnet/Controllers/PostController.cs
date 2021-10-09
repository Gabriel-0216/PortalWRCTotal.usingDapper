using Infraestrutura;
using Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.blog.Dapper.dotnet.Controllers
{
    public class PostController : Controller
    {
        private readonly IConfiguration _configuration;
        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            PostRepositorio postRepositorio = new PostRepositorio(_configuration);
            var listaPosts = new List<Post>();
            listaPosts = postRepositorio.GetAll();
            return View(listaPosts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                PostRepositorio postRepositorio = new PostRepositorio(_configuration);
                postRepositorio.Add(post);
                return RedirectToAction("index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                var post = new PostRepositorio(_configuration).Get((int)id);
                if (post == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(post);
                }
            }

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var repositorio = new PostRepositorio(_configuration);
            repositorio.Remove(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var post = new PostRepositorio(_configuration).Get((int)id);
                if(post == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(post);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                PostRepositorio postRepositorio = new PostRepositorio(_configuration);
                postRepositorio.Update(post);
                return RedirectToAction("index");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Admin()
        {
            PostRepositorio postRepositorio = new PostRepositorio(_configuration);
            var listaPosts = new List<Post>();
            listaPosts = postRepositorio.GetAll();
            return View(listaPosts);
        }
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult ReturnOne()
        {
            PostRepositorio postRepositorio = new PostRepositorio(_configuration);
            var postagem = postRepositorio.GetLast();
            return View(postagem);
        }
    }
}
