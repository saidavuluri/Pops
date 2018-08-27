using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using POPS;
using POPS.Models;

namespace POPS.Controllers
{
    public class POMASTERsController : Controller
    {
        // GET: POMASTERs
        public ActionResult Index()
        {
            List<PoMaster> pOMASTERs = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57888/Api/");
                //HTTP GET
                var responseTask = client.GetAsync("POMASTERs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PoMaster>>();
                    readTask.Wait();

                    pOMASTERs = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(pOMASTERs.ToList());
        }

        // GET: POMASTERs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoMaster pOMASTER =null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57888/Api/");
                //HTTP GET
                var responseTask = client.GetAsync("POMASTERs/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PoMaster>();
                    readTask.Wait();

                    pOMASTER = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            if (pOMASTER == null)
            {
                return HttpNotFound();
            }
            return View(pOMASTER);
        }

        // GET: POMASTERs/Create
        public ActionResult Create()
        {
            ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME");
            return View();
        }

        // POST: POMASTERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PONO,PODATE,SUPLNO")] PoMaster pOMASTER)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57888/Api/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<PoMaster>("POMASTERs", pOMASTER);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME", pOMASTER.SUPLNO);
            return View(pOMASTER);
        }

        // GET: POMASTERs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoMaster pOMASTER = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57888/Api/");
                //HTTP GET
                var responseTask = client.GetAsync("POMASTERs/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PoMaster>();
                    readTask.Wait();

                    pOMASTER = readTask.Result;
                }
            }
            if (pOMASTER == null)
            {
                return HttpNotFound();
            }


            ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME", pOMASTER.SUPLNO);
            return View(pOMASTER);
        }

        // POST: POMASTERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PONO,PODATE,SUPLNO")] PoMaster pOMASTER)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:57888/Api/POMASTERs/" + pOMASTER.PONO);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PutAsJsonAsync(pOMASTER.PONO, pOMASTER).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.SUPLNO = new SelectList(GetSuppliers(), "SUPLNO", "SUPLNAME", pOMASTER.SUPLNO);
            return View(pOMASTER);
        }

        // GET: POMASTERs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoMaster pOMASTER = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57888/Api/");
                //HTTP GET
                var responseTask = client.GetAsync("POMASTERs/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PoMaster>();
                    readTask.Wait();

                    pOMASTER = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (pOMASTER == null)
            {
                return HttpNotFound();
            }
            return View(pOMASTER);
        }

        // POST: POMASTERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57888/Api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("POMASTERs/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57888/Api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.GetAsync("suppliers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Supplier>>();
                    readTask.Wait();

                    suppliers = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return suppliers;
        }

      
    }
}
