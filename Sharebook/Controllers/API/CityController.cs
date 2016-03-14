using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using AutoMapper;
using Sharebook.ViewModels;
using Sharebook.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Sharebook.Controllers.API
{
    [Route("api/cities")]
    public class CityController : Controller
    {
        private ISharebookRepository _repository;

        public CityController(ISharebookRepository repository)
        {
            _repository = repository;
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]string value)
        {
            var cities = Mapper.Map<IEnumerable<CityViewModel>>(_repository.GetCities(value));
            var result = new {output= cities,
                                selected = cities.FirstOrDefault().Id};

            return Json(result);
        }

    }
}
