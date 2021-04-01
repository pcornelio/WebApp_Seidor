using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp_Seidor.Models;

namespace WebApp_Seidor.Controllers
{
    public class Cuenta_ClienteController : Controller
    {
        // GET: Cuenta_Cliente
        public async Task<ActionResult> Index()
        {
            var httpCliente = new HttpClient();
            var result = await httpCliente.GetStringAsync("http://localhost:50713/api/Cuenta_Cliente");
            var objCuentaCliente = JsonConvert.DeserializeObject<List<Cuenta_Cliente>>(result);
            return View(objCuentaCliente);
        }

        // GET: Cuenta_Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cuenta_Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cuenta_Cliente/Create
        [HttpPost]
        public async Task<ActionResult> Create(Cuenta_Cliente objeto)
        {
            try
            {
                // TODO: Add insert logic here
                var diff = DateTime.Now - objeto.FecNacimiento;
                int Edad = (int)(diff.TotalDays / 365.255);

                if (Edad < 40)
                {
                    objeto.Puntos = 1000;
                }
                else
                {
                    objeto.Puntos = 100;
                }
                objeto.Saldo = 0;
                var httpCliente = new HttpClient();
                var objJson = JsonConvert.SerializeObject(objeto);
                var objString = new StringContent(objJson, Encoding.UTF8, "application/json");
                var result =  httpCliente.PostAsync("http://localhost:50713/api/Cuenta_Cliente",objString).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
                return View(objeto);
            }
            catch
            {
                return View();
            }
        }

        // GET: Cuenta_Cliente/Edit/5
        public async  Task<ActionResult> Edit(int id)
        {
            var httpCliente = new HttpClient();
            var result = await httpCliente.GetStringAsync("http://localhost:50713/api/Cuenta_Cliente/"+id);
            var objCuentaCliente = JsonConvert.DeserializeObject<Cuenta_Cliente>(result);
            objCuentaCliente.Saldo = 0;
            return View(objCuentaCliente);
        }

        // POST: Cuenta_Cliente/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Cuenta_Cliente objeto)
        {
            try
            {
                // TODO: Add update logic here
                var httpCliente = new HttpClient();

                var result = await httpCliente.GetStringAsync("http://localhost:50713/api/Cuenta_Cliente/" + id);
                var objCuentaCliente = JsonConvert.DeserializeObject<Cuenta_Cliente>(result);

                if (objeto.Saldo >= 1000)
                {
                    objCuentaCliente.Saldo = objCuentaCliente.Saldo + objeto.Saldo;
                    objCuentaCliente.Puntos = objCuentaCliente.Puntos + 200;
                }
                else
                {
                    objCuentaCliente.Saldo = objCuentaCliente.Saldo + objeto.Saldo;
                    objCuentaCliente.Puntos = objCuentaCliente.Puntos + 50;

                }

                var objJson = JsonConvert.SerializeObject(objCuentaCliente);
                var objString = new StringContent(objJson, Encoding.UTF8, "application/json");
                var result2 = httpCliente.PutAsync("http://localhost:50713/api/Cuenta_Cliente/"+id, objString).Result;

                if (result2.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
                return View(objeto);

            }
            catch
            {
                return View();
            }
        }

        // GET: Cuenta_Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cuenta_Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
