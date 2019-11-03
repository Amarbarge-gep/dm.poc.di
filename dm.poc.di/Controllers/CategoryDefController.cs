using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dm.poc.iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dm.poc.di.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryDefController : ControllerBase
    {
        private ICategoryService service;
        public CategoryDefController(ICategoryService _service)
        {
            service = _service;
        }
        [Route("CategoryDefault")]
        [HttpGet]
        public string get()
        {
            return service.get();
        }
        [Route("CategorySubDefault")]
        [HttpGet]
        public string getSub()
        {
            return service.getSub();
        }
    }
}