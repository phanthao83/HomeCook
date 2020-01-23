using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HC.Models;
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model.ViewModel;
using HC.Ultility;
using HomeCook.Areas.Extension;

namespace HomeCook.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
        }

        public IActionResult Index()
        {
            //Get Top 
            var homeviewMD = new HomeView();
            homeviewMD.ProductImagePath = PathConfiguration.GetProductImgStoreFolder();
            homeviewMD.AvatarPath = PathConfiguration.GetAvatarStoreFolder(); 

            homeviewMD.BestProducts = _unitOfWork.SP.ReturnList<ProductSimpleView>(SP.SelectTop4Product);
            homeviewMD.CategoryList = _unitOfWork.Category.GetAll();
            homeviewMD.NewProducts = _unitOfWork.SP.ReturnList<ProductSimpleView>(SP.SelectTop4NewProduct);
            homeviewMD.BestSuppliers = _unitOfWork.SP.ReturnList<AppUserView>(SP.SelectTop10Seller); 



            return View(homeviewMD);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
