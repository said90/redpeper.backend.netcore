using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Order.Dishes;

namespace Redpeper.Controllers
{
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

        [HttpPost("[action]/{name}")]
        public async Task<ActionResult<Dish>> CreateDish(DishDto dishDto)
        {
            try
            {
                var dish  = new Dish
                {
                    Name = dishDto.Name,
                    Description = dishDto.Description,
                    DishCategoryId = dishDto.DishCategoryId,
                    Price = dishDto.Price
                };
                _dishRepository.Create(dish);
                await _unitOfWork.Commit();
                 
                var dishId = await _dishRepository.GetMaxId();
                var dishSupplies =  dishDto.DishSupplies.Select(x => new DishSupply
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

        [HttpPost("[action]/{name}")]
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
        public async Task<ActionResult<Dish>> UpdateDish(int id)
        {
            try
            {
                var dish = await _dishRepository.GetById(id);

                if (dish == null)
                {
                    return NotFound();
                }
                _dishRepository.Update(dish);
                await _unitOfWork.Commit();
                return dish;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<DishSupply>> UpdateDishSupply(DishSupply dishSupply)
        {
            try
            {
                var dish = await _dishSuppliesRepository.GetById(dishSupply.DishId);

                if (dish == null)
                {
                    return NotFound();
                }
                _dishSuppliesRepository.Update(dish);
                await _unitOfWork.Commit();
                return dishSupply;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpDelete]
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

        [HttpDelete("[action]")]
        public async Task<ActionResult<DishSupply>> RemoveDishSupply(int id)
        {
            try
            {
                var dish = await _dishSuppliesRepository.GetById(id);
                if (dish == null)
                {
                    return NotFound();
                }
                _dishSuppliesRepository.Delete(dish);
                await _unitOfWork.Commit();
                return dish;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }
    }
}
