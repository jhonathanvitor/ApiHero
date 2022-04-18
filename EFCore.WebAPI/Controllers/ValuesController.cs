using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;
        public ValuesController(HeroiContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            //var listHeroi = _context.Herois.ToList(); //pode ser assim para ternar todos os cadastros do Herois
            var listHeroi = (from Heroi in _context.Herois select Heroi).ToList();
            return Ok(listHeroi);
        }

        // GET api/nome
        [HttpGet ("filtro/{nome}")]
        public ActionResult Getfiltro(string nome)
        {
            //pode ser assim para ternar todos os cadastros do Herois
            var listHeroi = _context.Herois
                                   .Where(h => h.Nome.Contains(nome))
                                   .ToList();
           // var listHeroi = (from Heroi in _context.Herois 
           //                  where Heroi.Nome.Contains(nome)
           //                  select Heroi).ToList();
            return Ok(listHeroi);
        }
        // GET api/values/Atualizar nome
        [HttpGet("atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            //var heroi = new Heroi { Nome = nameHero };

            //_context.Herois.Add(heroi);
            var heroi = _context.Herois.Where(h => h.Id == 3).FirstOrDefault();
            heroi.Nome = "Homem Aranha";
            _context.SaveChanges();

            return Ok();
        }

        // GET api/values/Adicionar varios nomes
        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        { 
            _context.AddRange(
                new Heroi { Nome = "Captão America"},
                new Heroi { Nome = "Doutor Estranho"},
                new Heroi { Nome = "Pantera Negra"},
                new Heroi { Nome = "Viúva Negra"},
                new Heroi { Nome = "Hulk"},
                new Heroi { Nome = "Gravião Arqueiro"},
                new Heroi { Nome = "Capitã Marvel"}
                );
            _context.SaveChanges();
            

            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpGet("Delete/{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois
                                .Where(x => x.Id == id)
                                .Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
