using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;

        public HeroiController(IEFCoreRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<HeroiController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var herois = await _repository.GetAllHerois(true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        //[HttpGet]
        //public ActionResult Get()
        //{

        //        return Ok(new Heroi());

        //}

        // GET api/<HeroiController>/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var herois = await _repository.GetHeroiById(id, true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<HeroiController>
        [HttpPost]
        public async Task<ActionResult> Post(Heroi model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangeAsync())
                {
                    return Ok("Adicionado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Não Salvou");
        }

        // PUT api/<HeroiController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Heroi model)
        {
            try
            {
                var heroi = await _repository.GetHeroiById(id);

                if (heroi != null)
                {
                    _repository.Update(model);

                    if (await _repository.SaveChangeAsync())
                    {
                        return Ok("Atualizado");
                    }

                }
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex}");
            }

            return Ok("Não Atualizado!");
        }

        // DELETE api/<HeroiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repository.GetHeroiById(id);

                if (heroi != null)
                {
                    _repository.Delete(heroi);

                    if (await _repository.SaveChangeAsync())
                    {
                        return Ok("Deletado");
                    }

                }
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex}");
            }

            return Ok("Não Deletado!");
        }
    }
}
