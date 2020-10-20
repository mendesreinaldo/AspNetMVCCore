using Capitulo001.Data;
using Capitulo001.Data.Dal.Cadastro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo001.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;
        private readonly DepartamentoDAL departamentoDAL;
        private readonly InstituicaoDAL instituicaoDAL;

        public DepartamentoController(IESContext context)
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
        }



        public async Task<IActionResult> Index()
        {
            //return View(await _context.Departamentos.OrderBy(d => d.Nome).ToListAsync());            
            //return View(await _context.Departamentos.Include(i => i.Instituicao).OrderBy(c => c.Nome).ToListAsync());
            return View(await departamentoDAL.ObterDepartamentoClassificadosPorNome().ToListAsync());
        }

        // GET: Departamento/Create
        public IActionResult Create()
        {
            //var instituicoes = _context.Instituicoes.OrderBy(i => i.Nome).ToList();
            //instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione a instituição" });
            //ViewBag.Instituicoes = instituicoes;
            //return View();

            var instituicoes = instituicaoDAL.ObterInstituicoesClassificadaPorNome().ToList();
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione a instituição" });
            ViewBag.Instituicoes = instituicoes;
            return View();
        }

        // POST: Departamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, InstituicaoID")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Add(departamento);
                    //await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));

                    await departamentoDAL.GravaDepartamento(departamento);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados");                
            }

            return View(departamento);
        }

        //GET: Departamento/Edit/5

        public async Task<IActionResult> Edit(long? id)
        {
            //if(id== null)
            //    return NotFound();         

            //var departamento = await _context.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);

            //if (departamento == null)
            //    return NotFound();

            //ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID);

            //return View(departamento);

            ViewResult visaoDepartamento = (ViewResult)await ObterVisaoDepartamentoPorId(id);
            Departamento departamento = (Departamento)visaoDepartamento.Model;
            ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID);
            return visaoDepartamento;
        }

        // POST:  Departamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID, Nome,InstituicaoID")] Departamento departamento)
        { 
            if( id != departamento.DepartamentoID)            
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(departamento);
                    //await _context.SaveChangesAsync();

                    await departamentoDAL.GravaDepartamento(departamento);
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!await DepartamentoExists(departamento.DepartamentoID))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewBag.Instituicoes = new SelectList(_context.Instituicoes.OrderBy(b => b.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID);

            return View(departamento);
        }

        private async Task<bool> DepartamentoExists(long? id)
        {
            //return _context.Departamentos.Any(d => d.DepartamentoID == id);

            return await departamentoDAL.ObterDepartamentoPorId((long)id) != null;
        }


        //GET: Departamento/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            //if (id == null)
            //    return NotFound();

            //var departamento = await _context.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);

            //_context.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();

            //if (departamento == null)
            //    return NotFound();

            //return View(departamento);

            return await ObterVisaoDepartamentoPorId(id);
        }

        //GET: Departamento/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            //if (id == null)         
            //    return NotFound();

            //var departamento = await _context.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);

            //_context.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();            

            //if (departamento == null)
            //    return NotFound();

            //return View(departamento);

            return await ObterVisaoDepartamentoPorId(id);
        }

        //POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(long? id)
        {
            //var departamento = await _context.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);
            //_context.Departamentos.Remove(departamento);
            //TempData["Message"] = "Departamento" + departamento.Nome.ToUpper() + "foi removido";
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            var departamento = await departamentoDAL.EliminarDepartamentoPorId((long)id);
            TempData["Message"] = "Departamento" + departamento.Nome.ToUpper() + "foi removido";
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ObterVisaoDepartamentoPorId(long? id)
        {
            if (id == null)
                return NotFound();

            var departamento = await departamentoDAL.ObterDepartamentoPorId((long)id);

            if (departamento == null)
                return NotFound();

            return View(departamento);
        }
    }
}
