using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP3_AspNet.Models;

namespace TP3_AspNet.Controllers
{
    public class PessoaController : Controller
    {
        public string Nome { get; private set; }


        #region Create Pessoa

        private static int ContaId = 1;

        public static int AddId()
        {
            ContaId += 1;
            return ContaId;
        }

        public static int GetId()
        {
            return ContaId;
        }

        private static List<Pessoa> pessoaList = new List<Pessoa>
        {
            new Pessoa()
            {
            PessoaId = 0,
            Idade = 22,
            Nome = "Lucas",
            Sobrenome = "Samel"
            }     
        };

        public static void AddPessoa(Pessoa pessoa)
        {
            pessoaList.Add(pessoa);
        }
        #endregion

        public static List<Pessoa> GetpessoaList()
        {
            return pessoaList;
        }

        public static Pessoa BuscarId(int id)
        {
            Pessoa resultado = new Pessoa();
            foreach(Pessoa a in pessoaList)
            {
                if(a.PessoaId == id)
                {
                    resultado.PessoaId = a.PessoaId;
                    resultado.Nome = a.Nome;
                    resultado.Sobrenome = a.Sobrenome;
                    resultado.Idade = a.Idade;
                    break;
                }
            }
            return resultado;
        }

        public static void EditPessoa (int id , Pessoa pessoaUpdate)
        {
            foreach(Pessoa a in pessoaList)
            {
                if(a.PessoaId == id)
                {
                    a.Nome = pessoaUpdate.Nome;
                    a.Sobrenome = pessoaUpdate.Sobrenome;
                    a.Idade = pessoaUpdate.Idade;
                    break;
                }
            }
        }

        public static void DeletePessoa(int id)
        {
            foreach (Pessoa a in pessoaList)
            {
                if (a.PessoaId == id)
                {
                    pessoaList.Remove(a);
                    break;
                }
            }
        }

        public static List<Pessoa> BuscarPessoa(string pesquisa)
        {
            List<Pessoa> resultados = new List<Pessoa>();
            foreach (Pessoa a in pessoaList)
            {
                if (a.Nome.Contains(pesquisa))
                {
                    resultados.Add(a);
                }
            }
            return resultados;
        }

        // GET: Pessoa
        public ActionResult Index()
        {
            return View(GetpessoaList());
        }

        #region Details
        // GET: Pessoa/Details/5
        public ActionResult Details(int id)
        {


            return View(BuscarId(id));
        }
        #endregion

        #region Create
        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }        

        // POST: Pessoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Pessoa pessoa = new Pessoa();
            pessoa.PessoaId = GetId();
            pessoa.Nome = collection["Nome"];
            pessoa.Sobrenome = collection["Sobrenome"];
            pessoa.Idade = Int32.Parse(collection["Idade"]);

            AddId();
            AddPessoa(pessoa);

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            return View(BuscarId(id));
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Nome = collection["Nome"];
                pessoa.Sobrenome = collection["Sobrenome"];
                pessoa.Idade = Int32.Parse(collection["Idade"]);

                EditPessoa(id, pessoa);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Delete
        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            return View(BuscarId(id));
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                DeletePessoa(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion

        public ActionResult Buscar()
        {
            string pesquisa = "";

            return View(BuscarPessoa(pesquisa));
        }

        [HttpPost]
        public ActionResult Buscar(string pesquisa)
        {
            try
            {
                return View(BuscarPessoa(pesquisa));
            }
            catch
            {

            }

            return View();

        }

    }



}