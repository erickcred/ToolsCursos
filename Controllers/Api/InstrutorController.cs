using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tools.Data;
using Tools.Extensions;
using Tools.Models;
using Tools.ViewModels;

namespace Tools.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class InstrutorController : ControllerBase
    {
        [HttpGet("v1/instrutores")]
        public async Task<IActionResult> GetAsync([FromServices] ToolsDataContext tdc)
        {
            try
            {
                List<Instrutor> instrutores = await tdc.Instrutores.AsNoTracking().ToListAsync();
                return Ok(new ResultViewModel<List<Instrutor>>(instrutores));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Instrutor>>("02XE0 - Falha interna no Servidor!"));
            }
        }

        [HttpGet("v1/instrutores/{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id, [FromServices] ToolsDataContext tdc)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<List<Instrutor>>(ModelState.GetErrors()));
            
            try
            {
                Instrutor instrutor = await tdc.Instrutores.FirstOrDefaultAsync(x => x.Id == id);
                if (instrutor == null)
                    return NotFound(new ResultViewModel<List<Instrutor>>("Curso não encontrado!"));

                return Ok(new ResultViewModel<Instrutor>(instrutor));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Instrutor>>("02XE1 - Falha interna no Servidor!"));
            }
        }

        [HttpPost("v1/instrutores")]
        public async Task<IActionResult> PostAsync([FromBody] Instrutor instrutorModel, [FromServices] ToolsDataContext tdc)
        {
            try
            {
                Instrutor instrutor = new Instrutor();
                instrutor = instrutorModel;

                await tdc.Instrutores.AddAsync(instrutor);
                await tdc.SaveChangesAsync();

                return Created($"v1/categories/{instrutor.Id}", new ResultViewModel<Instrutor>(instrutor));
            } catch (DbUpdateException error)
            {
                return StatusCode(500, new ResultViewModel<List<Instrutor>>("02XE2 - Não foi possível incluir o instrutor!"));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Instrutor>>("02XE3 - Falha interna no Servidor!"));
            }
        }

        [HttpPut("v1/instrutores/{id:int}")]
        public async Task<IActionResult> PutAsyc([FromRoute] int id, [FromBody] Instrutor instrutorModel, [FromServices] ToolsDataContext tdc)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<List<Curso>>(ModelState.GetErrors()));

            try
            {
                Instrutor instrutor = await tdc.Instrutores.FirstOrDefaultAsync(x => x.Id == id);
                if (instrutor == null)
                    return NotFound(new ResultViewModel<List<Instrutor>>("Curso não localizado!"));

                instrutor.Nome = instrutorModel.Nome;
                instrutor.Email = instrutorModel.Email;
                instrutor.CPF = instrutorModel.CPF;
                instrutor.Situacao = true;

                tdc.Instrutores.Update(instrutor);
                await tdc.SaveChangesAsync();

                return Ok(new ResultViewModel<Instrutor>(instrutor));
            } catch (DbUpdateException error)
            {
                return StatusCode(500, new ResultViewModel<List<Instrutor>>("02XE4 - Não foi possível alterar o Curso!"));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Instrutor>>("02XE5 - Falha interna no servidor!"));
            }
        }
    
        [HttpDelete("v1/instrutores/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ToolsDataContext tdc)
        {
            try
            {
                Instrutor instrutor = await tdc.Instrutores.FirstOrDefaultAsync(x => x.Id == id);
                if (instrutor == null)
                    return NotFound(new ResultViewModel<List<Instrutor>>("Curso não localizado!"));
                
                instrutor.Situacao = false;
                tdc.Instrutores.Update(instrutor);
                await tdc.SaveChangesAsync();

                return Ok(new ResultViewModel<Instrutor>(instrutor));
            } catch (DbUpdateException error)
            {
                return StatusCode(500, new ResultViewModel<List<Instrutor>>("02XE6 - Falha interna no servidor!"));
            }
        }

    }
}