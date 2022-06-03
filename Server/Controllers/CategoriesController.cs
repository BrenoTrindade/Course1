using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
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
