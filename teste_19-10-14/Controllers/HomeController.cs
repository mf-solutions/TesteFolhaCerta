using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teste_19_10_14.Models;

namespace teste_19_10_14.Controllers
{
    public class HomeController : Controller
    {
        private readonly Database1Entities _acessarBD;

        public HomeController()
        {
           _acessarBD = new Database1Entities();
        }

        public ActionResult Index()
        {
            var idPrimeiroFuncionario = 1;

            var dbFunc = _acessarBD.funcionario.SingleOrDefault(x => x.Id == idPrimeiroFuncionario);
            var dbCargo = _acessarBD.cargo.SingleOrDefault(x => x.ID_CARGO == dbFunc.Id);

            var funcView = new FuncionarioViewModel
            {
                Id = dbFunc.Id,
                NOME = dbFunc.NOME,
                ID_CARGO = dbFunc.ID_CARGO,
                DESC_CARGO = dbCargo.DESCRICAO,
                DATA_ENTRADA = dbFunc.DATA_ENTRADA,
                SALARIO = dbFunc.SALARIO,
            };

            PopularDadosSelectBox();

            return View(funcView);
        }

        [HttpPost]
        public ActionResult Index(FuncionarioViewModel func)
        {
            var novoFuncionario = new funcionario
            {
                Id = func.Id,
                NOME = func.NOME,
                ID_CARGO = func.ID_CARGO,
                DATA_ENTRADA = func.DATA_ENTRADA,
                SALARIO = func.SALARIO
            };
            
            _acessarBD.funcionario.Add(novoFuncionario);

            func.DESC_CARGO = _acessarBD.cargo.Where(x => x.ID_CARGO == func.ID_CARGO).SingleOrDefault().DESCRICAO;

            PopularDadosSelectBox();

            return View(func);
        }

        public ActionResult Deletar(int? id)
        {
            var funcToRemove = _acessarBD.funcionario.SingleOrDefault(x => x.Id == id);

            if(funcToRemove != null)
             _acessarBD.funcionario.Remove(funcToRemove);

            PopularDadosSelectBox();

            return View(new FuncionarioViewModel());
        }

        private void PopularDadosSelectBox()
        {
            var tipoAcao = new List<dynamic>
            {
                new {Value ="01", Text ="Visualizar"},
                new {Value ="02", Text ="Inserir Novo"},
            };
            var cargos = _acessarBD.cargo.ToList();

            ViewBag.TipoAcao = new SelectList(tipoAcao, "Value", "Text");
            ViewBag.Cargos = new SelectList(cargos, "ID_CARGO", "DESCRICAO");
        }
    }
}