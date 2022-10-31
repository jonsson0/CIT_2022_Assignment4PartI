using AutoMapper;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IMapper _mapper;

        public CategoriesController(IDataService dataService, LinkGenerator generator, IMapper mapper)
        {
            _dataService = dataService;
            _generator = generator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = 
                _dataService.GetCategories().Select(x => CreateCategoryModel(x));
            return Ok(categories);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = CreateCategoryModel(category);

            return Ok(model);

        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateModel model)
        {
            var category = _mapper.Map<Category>(model);

            _dataService.CreateCategory(category);

            return CreatedAtRoute(null, CreateCategoryModel(category));
          //  return CreatedAtRoute(null, category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var deleted = _dataService.DeleteCategory(id);

            if (!deleted)
            {
               return  NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}", Name = nameof(UpdateCategory))]
        public IActionResult UpdateCategory(int id, CategoryCreateModel model)
        {
           // var category = _dataService.GetCategory(Int32.Parse(categoryModel.Url.Substring(categoryModel.Url.Length - 4)));
       
           
           if (_dataService.GetCategory(id) == null)
           {
               return NotFound();
           }

           var category = _mapper.Map<Category>(model);

           var categoryactual = _dataService.GetCategory(id);


           _dataService.UpdateCategory(categoryactual.Id, category.Name, category.Description);

           return Ok();
        }


        private CategoryModel CreateCategoryModel(Category category)
        {
            var model = _mapper.Map<CategoryModel> (category);
            model.Url = _generator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
            return model;
        }
    }
}
