using Microsoft.AspNet.Identity;
using Models.DataViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Auto.Controllers
{
    public class AutoController : Controller
    {
        static HttpClient client = new HttpClient();
        string BaseURL = ConfigurationManager.AppSettings["autoService"];
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetAllPackage").Result;
                List<PackageDTO> ct = new List<PackageDTO>();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<List<PackageDTO>>(response.Content.ReadAsStringAsync().Result);
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult AddPack()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddPack(PackageDTO pack)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("მონაცემები არავალიდურია!");

                string output = JsonConvert.SerializeObject(pack);
                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync($"{BaseURL}/AddPackage", stringContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "დამატების დროს მოხდა შეცდომა. ბოდიშს გიხდით";
                return View();
            }
        }

        public ActionResult PackDetails(string packName)
        {
            try
            {
                if (packName.Equals(""))
                    throw new Exception("მონაცემები არავალიდურია!");

                HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetPackage/{packName}").Result;
                PackageDTO ct = new PackageDTO();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<PackageDTO>(response.Content.ReadAsStringAsync().Result);
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }
        public ActionResult DeletePack(string packName)
        {
            try
            {
                if (packName.Equals(""))
                    throw new Exception("მონაცემები არავალიდურია!");

                HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetPackage/{packName}").Result;
                PackageDTO ct = new PackageDTO();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<PackageDTO>(response.Content.ReadAsStringAsync().Result);
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpDelete]
        public ActionResult DeletePack(string packName, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"{BaseURL}/DeletePackage/{packName}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult UserOrder()
        {
            try
            {
                string user = User.Identity.GetUserName().Substring(0, User.Identity.GetUserName().IndexOf("@"));
                HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetOrder/{user}").Result;
                OrderDTO ct = new OrderDTO();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<OrderDTO>(response.Content.ReadAsStringAsync().Result);
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult BuyPack(string packName)
        {
            try
            {
                string user = User.Identity.GetUserName().Substring(0, User.Identity.GetUserName().IndexOf("@"));
                HttpResponseMessage response = client.GetAsync($"{BaseURL}/AddOrder/{user}/{packName}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                return RedirectToAction("UserOrder");
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult UserPacks()
        {
            string user = User.Identity.GetUserName().Substring(0, User.Identity.GetUserName().IndexOf("@"));
            HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetOrderDetails/{user}").Result;
            List<OrderDetailDTO> ct = new List<OrderDetailDTO>();
            if (response.IsSuccessStatusCode)
            {
                ct = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(response.Content.ReadAsStringAsync().Result);
            }
            return PartialView("_UserPacksPartial", ct);

        }
    }
}