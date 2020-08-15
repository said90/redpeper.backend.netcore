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
        private readonly IUnitOfWork _unitOfWork;

        public DishCategoryController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<DishCategory>>> GetAll()
        {
            return await _unitOfWork.DishCategoryRepository.GetAllOrderBy();
        }

        // [HttpGet]
        // public async Task<PagedList<DishCategory>> GetPaginated(int page = 1, int size = 10, string sort = "")
        // {
        //     var result = await _dishCategoryRepository.GetPaginated(page, size, sort);
        //     var metadata = new
        //     {
        //         result.TotalCount,
        //         result.ItemPerPage,
        //         result.Page,
        //         result.TotalPages,
        //         result.HasMorePages,
        //         result.HasPrevPages,
        //         result.Sort
        //     };
        //     Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        //     return result;
        // }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DishCategory>> GetById(int id)
        {
            return await _unitOfWork.DishCategoryRepository.GetByIdTask(id);
        }

        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<DishCategory>> GetByName(string name)
        {
            return await _unitOfWork.DishCategoryRepository.GetByName(name);
        }

        [HttpPost]
        public async Task<ActionResult<DishCategory>> Create([FromBody] DishCategory dishCategory)
        {

            try
            {
                await _unitOfWork.DishCategoryRepository.InsertTask(dishCategory);
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
                _unitOfWork.DishCategoryRepository.Update(dishCategory);
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
            var dishCategory = await _unitOfWork.DishCategoryRepository.GetByIdTask(id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            await _unitOfWork.DishCategoryRepository.DeleteTask(id);
            await _unitOfWork.Commit();
            return dishCategory;
        }
    }
}
