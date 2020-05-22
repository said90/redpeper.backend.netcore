using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redpeper.Collection;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Order.Dishes;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class DishCategoryController : ControllerBase
    {
        private readonly IDishCategoryRepository _dishCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DishCategoryController(IDishCategoryRepository dishCategoryRepository, IUnitOfWork unitOfWork)
        {
            _dishCategoryRepository = dishCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<DishCategory>>> GetAll()
        //{
        //    return await _dishCategoryRepository.GetAll();
        //}

        [HttpGet]
        public async Task<PagedList<DishCategory>> GetPaginated(int page = 1, int size = 10, string sort = "")
        {
            var result = await _dishCategoryRepository.GetPaginated(page, size, sort);
            var metadata = new
            {
                result.TotalCount,
                result.ItemPerPage,
                result.Page,
                result.TotalPages,
                result.HasMorePages,
                result.HasPrevPages,
                result.Sort
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return result;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DishCategory>> GetById(int id)
        {
            return await _dishCategoryRepository.GetById(id);
        }

        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<DishCategory>> GetByName(string name)
        {
            return await _dishCategoryRepository.GetByName(name);
        }

        [HttpPost]
        public async Task<ActionResult<DishCategory>> Create([FromBody] DishCategory dishCategory)
        {

            try
            {
                _dishCategoryRepository.Create(dishCategory);
                await _unitOfWork.Commit();
                return dishCategory;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DishCategory>> Update([FromBody] DishCategory dishCategory)
        {

            try
            {
                _dishCategoryRepository.Update(dishCategory);
                await _unitOfWork.Commit();
                return dishCategory;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DishCategory>> Remove(int id)
        {
            var dishCategory = await _dishCategoryRepository.GetById(id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            _dishCategoryRepository.Remove(dishCategory);
            await _unitOfWork.Commit();
            return dishCategory;
        }
    }
}
