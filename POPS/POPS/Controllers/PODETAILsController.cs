using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POPS.Models;
using POPS;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace POPS.Controllers
{
    public class PODETAILsController : Controller
    {
        // GET: PODETAILs
        string apiUrl = ConfigurationManager.AppSettings["ApiURL"];

        public ActionResult Index()
        {
            List<PoDetail> pODetails = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("PoDetails");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PoDetail>>();
                    readTask.Wait();

                    pODetails = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(pODetails);
        }

        // GET: PODETAILs/Details/5
        public ActionResult Details(string id,string iTCode)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoDetail pODETAIL =null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("PoDetails/" + id + "/" + iTCode);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PoDetail>();
                    readTask.Wait();

                    pODETAIL = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (pODETAIL == null)
            {
                return HttpNotFound();
            }
            return View(pODETAIL);
        }

        // GET: PODETAILs/Create
        public ActionResult Create()
        {
            ViewBag.ITCODE = new SelectList(GetItems(), "ITCODE", "ITDESC");
            ViewBag.PONO = new SelectList(GetPOMasters(), "PONO", "SUPLNO");
            return View();
        }

        // POST: PODETAILs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PONO,ITCODE,QTY")] PoDetail poDetail)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<PoDetail>("PODetails", poDetail);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index");
            }

            ViewBag.ITCODE = new SelectList(GetItems(), "ITCODE", "ITDESC");
            ViewBag.PONO = new SelectList(GetPOMasters(), "PONO", "SUPLNO");
            return View(poDetail);
        }

        // GET: PODETAILs/Edit/5
        public ActionResult Edit(string id,string iTCode)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoDetail pODETAIL = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("PoDetails/" + id + "/" + iTCode);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PoDetail>();
                    readTask.Wait();

                    pODETAIL = readTask.Result;
                }
            }


            if (pODETAIL == null)
            {
                return HttpNotFound();
            }
            ViewBag.ITCODE = new SelectList(GetItems(), "ITCODE", "ITDESC");
            ViewBag.PONO = new SelectList(GetPOMasters(), "PONO", "SUPLNO");
            return View(pODETAIL);
        }

        // POST: PODETAILs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PONO,ITCODE,QTY")] PoDetail poDetail)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl+ "podetails/" + poDetail.PONO);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PutAsJsonAsync(poDetail.PONO, poDetail).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.ITCODE = new SelectList(GetItems(), "ITCODE", "ITDESC");
            ViewBag.PONO = new SelectList(GetPOMasters(), "PONO", "SUPLNO");
            return View(poDetail);
        }

        // GET: PODETAILs/Delete/5
        public ActionResult Delete(string id,string iTCode)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoDetail pODETAIL = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("podetails/" + id + "/" + iTCode);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PoDetail>();
                    readTask.Wait();

                    pODETAIL = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            if (pODETAIL == null)
            {
                return HttpNotFound();
            }
            return View(pODETAIL);
        }

        // POST: PODETAILs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id,string iTCode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("podetails/" + id + "/" + iTCode);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        private List<ITEM> GetItems()
        {
            List<ITEM> items = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("Items");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ITEM>>();
                    readTask.Wait();

                    items = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return items;
        }

        private List<PoMaster> GetPOMasters()
        {
            List<PoMaster> items = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("POMasters");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PoMaster>>();
                    readTask.Wait();

                    items = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return items;
        }
    }
}
