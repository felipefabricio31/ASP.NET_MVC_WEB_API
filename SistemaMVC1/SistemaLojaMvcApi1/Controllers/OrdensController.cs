using SistemaLojaMvcApi1.Models;
using SistemaLojaMvcApi1.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaLojaMvcApi1.Controllers
{

    public class OrdensController : Controller
    {
        private SistemaLojaMvcApi1Context db = new SistemaLojaMvcApi1Context();

        // GET: Ordens
        public ActionResult NovaOrdem()
        {
            var ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["ordemView"] = ordemView;
            
            var lista = db.Customizars.ToList();
            //Campo para inicar no formulario a opção Selecione....
            //lista.Add(new TipoDocumento { TipoDocumentoId = 0, Descricao = "[Selecione...]" });
            lista = lista.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(lista, "CustomizarId", "NomeCompleto");

            return View(ordemView);
        }

        [HttpPost]
        public ActionResult NovaOrdem(OrdemView ordemView)
        {
            ordemView = Session["ordemView"] as OrdemView;
            var customizarId = int.Parse(Request["CustomizarId"]);
            
            if (customizarId == 0)
            {
                var lista = db.Customizars.ToList();
                lista.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione...]" });
                lista = lista.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(lista, "CustomizarId", "NomeCompleto");

                //Tratamento de erro
                ViewBag.Error = "Selecione um Cliente.";

                return View(ordemView);
            }

            var cliente = db.Produtoes.Find(customizarId);

            if (cliente == null)
            {
                var lista = db.Customizars.ToList();
                lista.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione...]" });
                lista = lista.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(lista, "CustomizarId", "NomeCompleto");

                //Tratamento de erro
                ViewBag.Error = "O cliente não existe.";

                return View(ordemView);
            }

            if(ordemView.Produtos.Count == 0)
            {
                var lista = db.Customizars.ToList();
                lista.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione...]" });
                lista = lista.OrderBy(c => c.NomeCompleto).ToList();
                ViewBag.CustomizarId = new SelectList(lista, "CustomizarId", "NomeCompleto");

                return View(ordemView);
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //Salvando uma ordem
                    var ordem = new Ordem
                    {
                        CustomizarId = customizarId,
                        OrdemData = DateTime.Now,
                        OrdemStatus = OrdemStatus.Criada
                    };

                    db.Ordem.Add(ordem);
                    db.SaveChanges();
                    
                    //Salvando ordem detalhes
                    var ordemId = db.Ordem.ToList().Select(o => o.OrdemId).Max();

                    foreach (var item in ordemView.Produtos)
                    {
                        var ordemDetalhes = new OrdemDetalhe
                        {
                            ProdutoId = item.ProdutoId,
                            OrdemId = ordemId,
                            Descricao = item.Descricao,
                            Preco = item.Preco,
                            Quantidade = item.Quantidade
                        };

                        db.OrdemDetalhe.Add(ordemDetalhes);
                        db.SaveChanges();
                    }

                    //Commit para acetiar a transação
                    transaction.Commit();
                    ViewBag.Sucesso = string.Format("Ordem: {0}, foi salva com sucesso.", ordemId);
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    ViewBag.Erro = "Erro " + ex.ToString();
                }
            }


            

            var lista2 = db.Customizars.ToList();
            lista2.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione...]" });
            lista2 = lista2.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(lista2, "CustomizarId", "NomeCompleto");

            ordemView = new OrdemView();
            ordemView.Customizar = new Customizar();
            ordemView.Produtos = new List<ProdutoOrdem>();
            Session["ordemView"] = ordemView;

            return View();
        }

        public ActionResult AddProduto()
        {
            var lista = db.Produtoes.ToList();
            //Campo para inicar no formulario a opção Selecione....
            lista.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione...]" });
            lista = lista.OrderBy(c => c.Descricao).ToList();
            ViewBag.ProdutoId = new SelectList(lista, "ProdutoId", "Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduto(ProdutoOrdem produtoOrdem)
        {
            var ordemView = Session["ordemView"] as OrdemView;

            var produtoId = int.Parse(Request["ProdutoId"]);

            if(produtoId == 0)
            {
                var lista = db.Produtoes.ToList();
                lista.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione...]" });
                lista = lista.OrderBy(c => c.Descricao).ToList();
                ViewBag.ProdutoId = new SelectList(lista, "ProdutoId", "Descricao");

                //Tratamento de erro
                ViewBag.Error = "Selecione um Produto.";

                return View(produtoOrdem);
            }

            var produto = db.Produtoes.Find(produtoId);

            if (produto == null)
            {
                var lista = db.Produtoes.ToList();
                lista.Add(new ProdutoOrdem { ProdutoId = 0, Descricao = "[Selecione...]" });
                lista = lista.OrderBy(c => c.Descricao).ToList();
                ViewBag.ProdutoId = new SelectList(lista, "ProdutoId", "Descricao");

                //Tratamento de erro
                ViewBag.Error = "Não existe um produto selecionado.";

                return View(produtoOrdem);
            }

            //ID do produto já adicionado, só altera a quantidade não adiciona um novo produto
            produtoOrdem = ordemView.Produtos.Find(p => p.ProdutoId == produtoId);
            if (produtoOrdem == null)
            {
                produtoOrdem = new ProdutoOrdem
                {
                    Descricao = produto.Descricao,
                    Preco = produto.Preco,
                    ProdutoId = produto.ProdutoId,
                    Quantidade = float.Parse(Request["Quantidade"])
                };

                ordemView.Produtos.Add(produtoOrdem);
            }
            else
            {
                produtoOrdem.Quantidade += float.Parse(Request["Quantidade"]);
            }


            var listac = db.Customizars.ToList();
            //Campo para inicar no formulario a opção Selecione....
            //listac.Add(new Customizar { CustomizarId = 0, Nome = "[Selecione um cliente...]" });
            listac = listac.OrderBy(c => c.NomeCompleto).ToList();
            ViewBag.CustomizarId = new SelectList(listac, "CustomizarId", "NomeCompleto");

            return View("NovaOrdem", ordemView);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}