using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tools.Data;
using Tools.Models;
using Microsoft.EntityFrameworkCore;
using Tools.ViewModels;
using Tools.Extensions;

namespace Tools.Controllers.Api
{
    [ApiController]
    [Route("/api/")]
    public class CursoController : ControllerBase
    {
        [HttpGet("v1/cursos")]
        public async Task<IActionResult> GetAsync([FromServices] ToolsDataContext tdc)
        {
            try
            {
                var cursos = await tdc.Cursos.Where(x => x.Situacao == true).AsNoTracking().ToListAsync();
                return Ok(new ResultViewModel<List<Curso>>(cursos));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("01XE0 - Falha interna no Servidor!"));
            }
        }

        [HttpGet("v1/cursos/{id:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id, [FromServices] ToolsDataContext tdc)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<List<Curso>>(ModelState.GetErrors()));
            
            try
            {
                Curso curso = await tdc.Cursos.FirstOrDefaultAsync(x => x.Id == id);
                if (curso == null)
                    return NotFound(new ResultViewModel<List<Curso>>("Curso não encontrado!"));

                return Ok(new ResultViewModel<Curso>(curso));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("01XE1 - Falha interna no Servidor!"));
            }
        }

        [HttpPost("v1/cursos")]
        public async Task<IActionResult> PostAsync([FromBody] Curso cursoModel, [FromServices] ToolsDataContext tdc)
        {
            try
            {
                Curso curso = new Curso();
                curso = cursoModel;

                await tdc.Cursos.AddAsync(curso);
                await tdc.SaveChangesAsync();

                return Created($"v1/categories/{curso.Id}", new ResultViewModel<Curso>(curso));
            } catch (DbUpdateException error)
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("01XE2 - Não foi possível incluir o curso!"));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("01XE3 - Falha interna no Servidor!"));
            }
        }

        [HttpPut("v1/cursos/{id:int}")]
        public async Task<IActionResult> PutAsyc([FromRoute] int id, [FromBody] Curso cursoModel, [FromServices] ToolsDataContext tdc)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<List<Curso>>(ModelState.GetErrors()));

            try
            {
                Curso curso = await tdc.Cursos.FirstOrDefaultAsync(x => x.Id == id);
                if (curso == null)
                    return NotFound(new ResultViewModel<List<Curso>>("Curso não localizado!"));

                curso.Nome = cursoModel.Nome;
                curso.Categoria = cursoModel.Categoria;
                curso.Situacao = cursoModel.Situacao;
                curso.CodigoCRC = cursoModel.CodigoCRC;
                curso.TurmaCRC = cursoModel.TurmaCRC;

                tdc.Cursos.Update(curso);
                await tdc.SaveChangesAsync();

                return Ok(new ResultViewModel<Curso>(curso));
            } catch (DbUpdateException error)
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("01XE4 - Não foi possível alterar o Curso!"));
            } catch (Exception error)
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("01XE5 - Falha interna no servidor!"));
            }
        }
    
        [HttpDelete("v1/cursos/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] ToolsDataContext tdc)
        {
            try
            {
                Curso curso = await tdc.Cursos.FirstOrDefaultAsync(x => x.Id == id);
                if (curso == null)
                    return NotFound(new ResultViewModel<List<Curso>>("Curso não localizado!"));
                
                curso.Situacao = false;
                tdc.Cursos.Update(curso);
                await tdc.SaveChangesAsync();

                return Ok(new ResultViewModel<Curso>(curso));
            } catch (DbUpdateException error)
            {
                return StatusCode(500, new ResultViewModel<List<Curso>>("01XE6 - Falha interna no servidor!"));
            }

            
        }
    }
}