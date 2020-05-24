using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redpeper.Collection;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Order.Dishes;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishRepository _dishRepository;
        private readonly IDishSuppliesRepository _dishSuppliesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DishController(IDishRepository dishRepository, IUnitOfWork unitOfWork, IDishSuppliesRepository dishSuppliesRepository)
        {
            _dishRepository = dishRepository;
            _unitOfWork = unitOfWork;
            _dishSuppliesRepository = dishSuppliesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dish>>> GetAll()
        {
            return await _dishRepository.GetAll();
        }

        // [HttpGet]
        // public async Task<PagedList<Dish>> GetPaginated(int page = 1, int size = 10, string sort = "")
        // {
        //     var result = await _dishRepository.GetPaginated(page, size, sort);
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
        public async Task<ActionResult<Dish>> GetDishById(int id)
        {
            return await _dishRepository.GetById(id);
        }

        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<Dish>> GetByDishName(string name)
        {
            return await _dishRepository.GetByName(name);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Dish>> CreateDish(DishDto dishDto)
        {
            try
            {
                var dish = new Dish
                {
                    Name = dishDto.Name,
                    Description = dishDto.Description,
                    DishCategoryId = dishDto.DishCategoryId,
                    Price = dishDto.Price
                };
                _dishRepository.Create(dish);
                await _unitOfWork.Commit();

                var dishId = await _dishRepository.GetMaxId();
                var dishSupplies = dishDto.DishSupplies.Select(x => new DishSupply
                {
                    DishId = dishId,
                    SupplyId = x.SupplyId,
                    Comment = x.Comment,
                    Qty = x.Qty
                }).ToList();
                _dishSuppliesRepository.CreateRange(dishSupplies);
                await _unitOfWork.Commit();

                return dish;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DishSupply>> CreateDishSupply(DishSupply dishSupply)
        {
            try
            {
                _dishSuppliesRepository.Create(dishSupply);
                await _unitOfWork.Commit();
                return dishSupply;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Dish>> UpdateDish(DishDto dish)
        {
            try
            {
                if (dish == null)
                {
                    return NotFound();
                }
                var dsh = new Dish
                {
                    Id = dish.Id,
                    Description = dish.Description,
                    Price = dish.Price,
                    Name = dish.Name,
                    DishCategoryId = dish.DishCategoryId
                };
                _dishRepository.Update(dsh);
                await _unitOfWork.Commit();

                var dishDetails = dish.DishSupplies.Select(x => new DishSupply
                {
                    Id = x.Id,
                    DishId = x.DishId,
                    Qty = x.Qty,
                    Comment = x.Comment,
                    SupplyId = x.SupplyId
                }).ToList();
                var dishDetailsDb = await _dishSuppliesRepository.GetByDishIdNoTracking(dish.Id);


                if (dishDetailsDb.Count > dishDetails.Count)
                {
                    var removeDetail = dishDetailsDb.Where(p => dishDetails.All(p2 => p2.Id != p.Id)).ToList();
                    _dishSuppliesRepository.DeleteRange(removeDetail);
                    await _unitOfWork.Commit();

                }
                _dishSuppliesRepository.UpdateRange(dishDetails);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }

            return Ok(dish);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<DishSupply>> UpdateDishSupply(DishSupply dishSupply)
        {
            try
            {
                var dish = await _dishSuppliesRepository.GetById(dishSupply.Id);

                if (dish == null)
                {
                    return NotFound();
                }
                _dishSuppliesRepository.Update(dish);
                await _unitOfWork.Commit();
                return dish;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Dish>> RemoveDish(int id)
        {
            try
            {
                var dish = await _dishRepository.GetById(id);
                if (dish == null)
                {
                    return NotFound();
                }

                var dishSupplies = await _dishSuppliesRepository.GetByDishId(dish.Id);

                _dishSuppliesRepository.DeleteRange(dishSupplies);
                await _unitOfWork.Commit();
                _dishRepository.Delete(dish);
                await _unitOfWork.Commit();
                return dish;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<DishSupply>> RemoveDishSupply(int id)
        {
            try
            {
                var dishSupply = await _dishSuppliesRepository.GetById(id);
                if (dishSupply == null)
                {
                    return NotFound();
                }
                _dishSuppliesRepository.Delete(dishSupply);
                await _unitOfWork.Commit();
                return dishSupply;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }
    }
}
