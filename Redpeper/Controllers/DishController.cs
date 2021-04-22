using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redpeper.Collection;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Order.Dishes;
using Newtonsoft.Json.Linq;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DishController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dish>>> GetAll()
        {
            return await _unitOfWork.DishRepository.GetAllIncludingSuppliesTask();
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
            return await _unitOfWork.DishRepository.GetByIdIncludeSuppliesTask(id);
        }

        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<Dish>> GetByDishName(string name)
        {
            return await _unitOfWork.DishRepository.GetByName(name);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Dish>> CreateDish([FromForm] DishDto dishDto)
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
                await _unitOfWork.DishRepository.InsertTask(dish);
                await _unitOfWork.Commit();

                if (dishDto.Image != null)
                {
                    var imageToRemove = await _unitOfWork.DishImageRepository.GetByDishId(dish.Id);
                    if (imageToRemove != null)
                    {
                        await _unitOfWork.DishImageRepository.DeleteTask(imageToRemove.Id);
                    }


                    var dishImage = new DishImage
                    {
                        DishId = dish.Id
                    };

                    using (var ms = new MemoryStream())
                    {
                        dishDto.Image.CopyTo(ms);
                        dishImage.Image = ms.ToArray();
                    }

                    await _unitOfWork.DishImageRepository.InsertTask(dishImage);
                    await _unitOfWork.Commit();
                }

                if (TryValidateModel(!string.IsNullOrEmpty(dishDto.DishSupplies)))
                {
                    var dishId = await _unitOfWork.DishRepository.GetMaxId();
                    var dishDetailsJson = JsonConvert.DeserializeObject<List<DishSupply>>(dishDto.DishSupplies); ;
                    var dishSupplies = dishDetailsJson.Select(x => new DishSupply
                    {
                        DishId = dishId,
                        SupplyId = x.SupplyId,
                        Comment = x.Comment,
                        Qty = x.Qty
                    }).ToList();
                    await _unitOfWork.DishSuppliesRepository.InsertRangeTask(dishSupplies);
                }
             
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
                await _unitOfWork.DishSuppliesRepository.InsertTask(dishSupply);
                await _unitOfWork.Commit();
                return dishSupply;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Dish>> UpdateDish([FromForm] DishDto dish)
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
                _unitOfWork.DishRepository.Update(dsh);
                await _unitOfWork.Commit();

                if (dish.Image != null)
                {
                    var imageToRemove = await _unitOfWork.DishImageRepository.GetByDishId(dish.Id);
                    if (imageToRemove != null)
                    {
                        await _unitOfWork.DishImageRepository.DeleteTask(imageToRemove.Id);
                    }


                    var dishImage = new DishImage
                    {
                        DishId = dish.Id
                    };

                    using (var ms = new MemoryStream())
                    {
                        dish.Image.CopyTo(ms);
                        dishImage.Image = ms.ToArray();
                    }

                    await _unitOfWork.DishImageRepository.InsertTask(dishImage);
                    await _unitOfWork.Commit();
                }

                
                var dishDetailsJson = JsonConvert.DeserializeObject<List<DishSupply>>(dish.DishSupplies); ;

                var dishDetails = dishDetailsJson.Select(x => new DishSupply
                {
                    Id = x.Id,
                    DishId = x.DishId,
                    Qty = x.Qty,
                    Comment = x.Comment,
                    SupplyId = x.SupplyId
                }).ToList();

                var dishDetailsDb = await _unitOfWork.DishSuppliesRepository.GetByDishIdNoTracking(dish.Id);

                if (dishDetailsDb.Count >= dishDetails.Count)
                {
                    var removeDetail = dishDetailsDb.Where(p => !dishDetails.Any(p2 => p2.Id == p.Id)).ToList();
                    _unitOfWork.DishSuppliesRepository.DeleteRange(removeDetail);
                    await _unitOfWork.Commit();

                }
                _unitOfWork.DishSuppliesRepository.UpdateRange(dishDetails);
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
                var dish = await _unitOfWork.DishSuppliesRepository.GetByIdTask(dishSupply.Id);

                if (dish == null)
                {
                    return NotFound();
                }
                _unitOfWork.DishSuppliesRepository.Update(dish);
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
                var dish = await _unitOfWork.DishRepository.GetByIdTask(id);
                if (dish == null)
                {
                    return NotFound();
                }

                var dishSupplies = await _unitOfWork.DishSuppliesRepository.GetByDishId(dish.Id);

                _unitOfWork.DishSuppliesRepository.DeleteRange(dishSupplies);
                await _unitOfWork.Commit();
                if (dish.DishImage !=null)
                {
                    var image = await _unitOfWork.DishImageRepository.GetByDishId(dish.Id);
                   await _unitOfWork.DishImageRepository.DeleteTask(image.Id);
                }
              
                await _unitOfWork.DishRepository.DeleteTask(id);
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
                var dishSupply = await _unitOfWork.DishSuppliesRepository.GetByIdTask(id);
                if (dishSupply == null)
                {
                    return NotFound();
                }
                await _unitOfWork.DishSuppliesRepository.DeleteTask(id);
                await _unitOfWork.Commit();
                return dishSupply;
            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
        }

        [HttpGet("[action]/{id}/image")]
        public async Task<IActionResult> GetDishImage(int id)
        {
            var dishImage = await _unitOfWork.DishImageRepository.GetByDishId(id);

            if (dishImage == null) return NotFound();

            return Ok(dishImage);

        }
    }
}
