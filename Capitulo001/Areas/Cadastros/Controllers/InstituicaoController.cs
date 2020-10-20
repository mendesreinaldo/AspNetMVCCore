using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capitulo001.Data;
using Capitulo001.Data.Dal.Cadastro;
using Capitulo001.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using Microsoft.AspNetCore.Authorization;

namespace Capitulo001.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    [Authorize]
    public class InstituicaoController : Controller
    {
        //private static IList<Instituicao> instituicoes = new List<Instituicao>()
        //{
        //    new Instituicao(){
        //        InstituicaoID = 1,
        //        Nome = "UniParaná",
        //        Endereco = "Paraná"
        //    },
        //    new Instituicao(){
        //        InstituicaoID = 2,
        //        Nome = "UniSanta",
        //        Endereco = "Santa Catarina"
        //    },
        //    new Instituicao(){
        //        InstituicaoID = 3,
        //        Nome = "UniSãoPaulo",
        //        Endereco = "São Paulo"
        //    },
        //    new Instituicao(){
        //        InstituicaoID = 4,
        //        Nome = "UniSulgrandense",
        //        Endereco = "Rio Grande do Sul"
        //    },
        //    new Instituicao(){
        //        InstituicaoID = 5,
        //        Nome = "UniCarioca",
        //        Endereco = "Rio de Janeiro"
        //    },
        //};

        private readonly IESContext _context;
        private readonly InstituicaoDAL instituicaoDAL;

        public InstituicaoController(IESContext context)
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
        }

        //Definição de uam action chamada index

        /*Index Get*/
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Instituicoes.OrderBy(d => d.Nome).ToListAsync());
            return View(await instituicaoDAL.ObterInstituicoesClassificadaPorNome().ToListAsync());
        }

        /*Create Get*/
        public ActionResult Create()
        {
            return View();
        }

        /*Create Post*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Endereco")] Instituicao instituicao)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //_context.Add(instituicao);
                    //await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));

                    await instituicaoDAL.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {

                ModelState.AddModelError("", "Não foi possível inserir os dados");
            }            

            return View(instituicao);
        }


        /*Edit Get*/
        public async Task<IActionResult> Edit(long? id)
        {
            //if (id == null)
            //    return NotFound();

            //var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(d => d.InstituicaoID == id);

            //if (instituicao == null)
            //    return NotFound();

            //return View(instituicao);

            return await obterVisaoInstituicaoPorId(id);
        }


        /*Edit  Post*/
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID, Nome,Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(instituicao);
                    //await _context.SaveChangesAsync();
                    await instituicaoDAL.GravarInstituicao(instituicao);

                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!await InstituicaoExists(instituicao.InstituicaoID))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(instituicao);
        }
       

        public async Task<IActionResult> Details(long? id)
        {
            //if (id == null)
            //    return NotFound();

            ////var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(d => d.InstituicaoID == id);

            //var instituicao = await _context.Instituicoes.Include(d => d.Departamentos).SingleOrDefaultAsync(m => m.InstituicaoID == id);

            //if (instituicao == null)
            //    return NotFound();

            //return View(instituicao);

            return await obterVisaoInstituicaoPorId(id);
        }

        /*Delete Get*/
        public async Task<IActionResult> Delete(long? id)
        {
            //if (id == null)
            //    return NotFound();

            //var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(d => d.InstituicaoID == id);

            //if (instituicao == null)
            //    return NotFound();

            //return View(instituicao);

            return await obterVisaoInstituicaoPorId(id);
        }

        /*Delete Post*/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(long? id)
        {
            //var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(d => d.InstituicaoID == id);
            //_context.Instituicoes.Remove(instituicao);
            //await _context.SaveChangesAsync();
            //TempData["Message"] = "Instituição" + instituicao.Nome.ToUpper() + "foi removida";
            //return RedirectToAction(nameof(Index));

            var instituicao = await instituicaoDAL.EliminarInstituicaoPorId((long)id);
            TempData["Message"] = "Instituição" + instituicao.Nome.ToUpper() + "foi removida";
            return RedirectToAction(nameof(Index));


        }

        private async Task<IActionResult> obterVisaoInstituicaoPorId(long? id)
        {
            if (id == null)
                return NotFound();

            var instituicao = await instituicaoDAL.ObterInstituicaoPorId((long)id);

            if (instituicao == null)
                return NotFound();

            return View(instituicao);
        }


        //private bool InstituicaoExists(long? id)
        //{
        //    return _context.Instituicoes.Any(i => i.InstituicaoID == id);
        //}

        private async Task<bool> InstituicaoExists(long? id)
        {
            return await instituicaoDAL.ObterInstituicaoPorId((long)id) != null;
        }


    }
}
