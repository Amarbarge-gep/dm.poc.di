using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dm.poc.core;
using dm.poc.iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dm.poc.di.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService service;
        public CategoryController(IServicesProvider<ICategoryService> _service)
        {
            service = _service.GetInstance();
        }
        [Route("Category")]
        [HttpGet]
        public string getCategory()
        {
            return service.get();
        }

        [Route("CategorySub")]
        [HttpGet]
        public string getCategorySub()
        {
            return service.getSub();
        }
    }
}