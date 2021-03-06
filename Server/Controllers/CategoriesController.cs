using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoriesController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }
        #region CRUD operations

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = await _dataContext.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpGet("withposts")]
        public async Task<IActionResult> GetWithPosts()
        {
            List<Category> categories = await _dataContext.Categories
                .Include(category => category.Posts)
                .ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Category category = await GetCategoryByCategoryId(id, false);

            return Ok(category);
        }

        [HttpGet("withpost/{id}")]
        public async Task<ActionResult> GetWithPosts(int id)
        {
            Category category = await GetCategoryByCategoryId(id, true);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category categoryToCreate)
        {
            try
            {
                if(categoryToCreate == null)
                {
                    return BadRequest(ModelState);
                }

                if(ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                await _dataContext.Categories.AddAsync(categoryToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if(changesPersistedToDatabase == false)
                {
                    return StatusCode(500, $"Something went wrong ou our side. Please contact the administrator.");
                }
                else
                {
                    return Created("Create", categoryToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong ou our side. Please contact the administrator. Error message:{e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updatedCategory)
        {
            try
            {
                if (id < 1 || updatedCategory == null || id != updatedCategory.categoryId)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _dataContext.Categories.AnyAsync(category => category.categoryId == id);

                if(exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                _dataContext.Categories.Update(updatedCategory);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, $"Something went wrong ou our side. Please contact the administrator.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong ou our side. Please contact the administrator. Error message:{e.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _dataContext.Categories.AnyAsync(category => category.categoryId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Category categoryToDelete = await GetCategoryByCategoryId(id, false);

                if(categoryToDelete.ThumbnailImagePath != "uploads/placeholder.jpg")
                {
                    string fileName = categoryToDelete.ThumbnailImagePath.Split('/').Last();

                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }

                _dataContext.Categories.Remove(categoryToDelete);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, $"Something went wrong ou our side. Please contact the administrator.");
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong ou our side. Please contact the administrator. Error message:{e.Message}");
            }
        }



        #endregion

        #region Utility methods

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangesToDatabase()
        {
            int amountOfChanges = await _dataContext.SaveChangesAsync();

            return amountOfChanges > 0;
        }


        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Category> GetCategoryByCategoryId(int categoryId, bool withPosts)
        {
            Category categoryToGet = null;

            if (withPosts == true)
            {
                categoryToGet = await _dataContext.Categories
                    .Include(category => category.Posts)
                    .FirstAsync(category => category.categoryId == categoryId);
            }
            else
            {
                categoryToGet = await _dataContext.Categories
                    .FirstAsync(category => category.categoryId == categoryId);
            }
            return categoryToGet;
        }
        #endregion
    }
}
