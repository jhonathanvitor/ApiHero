using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;

        public BatalhaController(IEFCoreRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var herois = await _repository.GetAllBatalhas(true);
                
                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var herois = await _repository.GetBatalhaById(id, true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public async Task<ActionResult> Post(Batalha model)
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

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Batalha model)
        {

            try
            {
                var heroi = await _repository.GetBatalhaById(id);

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

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repository.GetBatalhaById(id);

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
