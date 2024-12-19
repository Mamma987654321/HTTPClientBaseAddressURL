using CallWebApiInMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CallWebApiInMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}

        string Baseurl = "https://localhost:7119/";
        public async Task<ActionResult> Index()
        {
            List<Employee> list = new List<Employee>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Employees/GetAll");
                if (res.IsSuccessStatusCode)
                {
                    var EmpRes = res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Employee>>(EmpRes);

                }
                return View(list);

            }
        }

    }
}