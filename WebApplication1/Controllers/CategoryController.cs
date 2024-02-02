using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyProjectBE.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        public IConfiguration Configuration;

        public CategoryController(ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }
        public string UserIdentity
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return HttpContext.Session.GetString("JWTUname");
                }
                else { return ""; }
            }
        }
        public string path { get { return Configuration["UserConfig:PathAPI"]; } }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            ViewData["UserIdentity"] = UserIdentity;
            List<CategoryModel> model = new List<CategoryModel>();
            model = JsonConvert.DeserializeObject<List<CategoryModel>>(await Helper.GetDataAsync("/api/category", path));
            return View(model);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int CategoryID)
        {
            List<CategoryModel> model = new List<CategoryModel>();
            model = JsonConvert.DeserializeObject<List<CategoryModel>>(await Helper.GetDataAsync(CategoryID, path + "/api/category/"));

            return View(model[0]);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel collection)
        {
            ResultModel model = new ResultModel();
            model.data = collection;
            try
            {
                if (ModelState.IsValid)
                {
                    model = JsonConvert.DeserializeObject<ResultModel>(await Helper.PostDataAsync(collection, path + "/api/category"));
                    model.data = collection;
                    if (model.status == "200")
                    {
                        model.data = collection;

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        model.data = collection;
                        return View("Create", model);
                    }
                }
                else
                {
                    return View("Create", model);
                }
            }
            catch
            {
                return View("Create", model);
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int CategoryID)
        {
            List<CategoryModel> model = new List<CategoryModel>();
            model = JsonConvert.DeserializeObject<List<CategoryModel>>(await Helper.GetDataAsync(CategoryID, path + "/api/category/"));

            return View(model[0]);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CategoryID, IFormCollection collection)
        {
            try
            {
                CategoryModel model = new CategoryModel();
                model.CategoryID = CategoryID;
                model.CategoryName = collection["CategoryName"].ToString();
                var res_ = JsonConvert.DeserializeObject<ResultModel>(await Helper.PutDataAsync(model, path + "/api/category/" + CategoryID));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int CategoryID)
        {
            ResultModel model = new ResultModel();
            model = JsonConvert.DeserializeObject<ResultModel>(await Helper.DeleteDataAsync(CategoryID, path + "/api/category/"));
            if (model.status == "200")
            {
                return Ok(model);
            }
            else
            {
                return Ok(model);
            }
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int CategoryID, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
