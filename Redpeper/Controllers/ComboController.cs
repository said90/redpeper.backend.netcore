using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redpeper.Collection;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Order.Combos;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComboController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<List<Combo>> GetCombos()
        {
            return await _unitOfWork.ComboRepository.GetAllInludeDetails();
        }
        // [HttpGet]
        // public async Task<PagedList<Combo>> GetPaginated(int page = 1, int size = 10, string sort = "")
        // {
        //     var result = await _comboRepository.GetPaginated(page, size, sort);
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
        public async Task<Combo> GetComboById(int id)
        {
            return await _unitOfWork.ComboRepository.GetByIdTask(id);
        }

        [HttpGet("[action]/{name}")]
        public async Task<Combo> GetComboByName(string name)
        {
            return await _unitOfWork.ComboRepository.GetByName(name);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Combo>> CreateCombo([FromForm] ComboDto combo)
        {
            try
            {
                var cmbo = new Combo
                {
                    Name = combo.Name,
                    Description = combo.Description,
                    Total = combo.Total
                };
                await _unitOfWork.ComboRepository.InsertTask(cmbo);
                await _unitOfWork.Commit();


                if (combo.Image !=null)
                {
                    var imageToRemove = await _unitOfWork.ComboImageRepository.GetByComboId(combo.Id);
                    if (imageToRemove !=null)
                    {
                        await _unitOfWork.ComboImageRepository.DeleteTask(imageToRemove.Id);
                    }

                    var comboImage = new ComboImage
                    {
                        ComboId = cmbo.Id
                    };

                    using (var ms = new MemoryStream())
                    {
                        combo.Image.CopyTo(ms);
                        comboImage.Image = ms.ToArray();
                    }

                    await _unitOfWork.ComboImageRepository.InsertTask(comboImage);
                    await _unitOfWork.Commit();
                }


                if (!string.IsNullOrEmpty(combo.ComboDetails))
                {
                    var comboDetailsJson = JsonConvert.DeserializeObject<List<ComboDetail>>(combo.ComboDetails); ;

                    var comboDetails = comboDetailsJson.Select(x => new ComboDetail
                    {
                        ComboId = cmbo.Id,
                        DishId = x.DishId,
                        Price = x.Price,
                        Qty = x.Qty
                    }).ToList();

                    await _unitOfWork.ComboDetailRepository.InsertRangeTask(comboDetails);
                    await _unitOfWork.Commit();
                }
               
                return cmbo;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ComboDetail>> CreateComboDetail(ComboDetail detail)
        {
            try
            {
                // var cmbo = new ComboDetail()
                // {
                //     ComboId = detail.ComboId,
                //     DishId = detail.DishId,
                //     Price = detail.Price,
                //     Qty = detail.Qty
                // };
                await _unitOfWork.ComboDetailRepository.InsertTask(detail);
                await _unitOfWork.Commit();
                return detail;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCombo([FromForm] ComboDto combo)
        {
            try
            {
                var cmbo = new Combo
                {
                    Id = combo.Id,
                    Name = combo.Name,
                    Description = combo.Description,
                    Total = combo.Total
                };

                _unitOfWork.ComboRepository.Update(cmbo);
                await _unitOfWork.Commit();

                if (combo.Image != null)
                {
                    var imageToRemove = await _unitOfWork.ComboImageRepository.GetByComboId(combo.Id);
                    if (imageToRemove != null)
                    {
                        await _unitOfWork.ComboImageRepository.DeleteTask(imageToRemove.Id);
                    }

                    var comboImage = new ComboImage
                    {
                        ComboId = cmbo.Id
                    };

                    using (var ms = new MemoryStream())
                    {
                        combo.Image.CopyTo(ms);
                        comboImage.Image = ms.ToArray();
                    }

                    await _unitOfWork.ComboImageRepository.InsertTask(comboImage);
                    await _unitOfWork.Commit();
                }

                if (!string.IsNullOrEmpty(combo.ComboDetails))
                {
                    var comboDetailsJson = JsonConvert.DeserializeObject<List<ComboDetail>>(combo.ComboDetails); ;

                    var comboDetails = comboDetailsJson.Select(x => new ComboDetail
                    {
                        ComboId = cmbo.Id,
                        DishId = x.DishId,
                        Price = x.Price,
                        Qty = x.Qty
                    }).ToList();

                    await _unitOfWork.ComboDetailRepository.InsertRangeTask(comboDetails);
                    await _unitOfWork.Commit();

                    var comboDetailsDb = await _unitOfWork.ComboDetailRepository.GetDetailsByComboNoTracking(combo.Id);

                    var ids = comboDetails.Select(x => x.Id);

                    if (comboDetailsDb.Count >= comboDetails.Count)
                    {
                        var removeDetail = comboDetailsDb.Where(p => !comboDetails.Any(p2 => p2.Id == p.Id)).ToList();
                        _unitOfWork.ComboDetailRepository.DeleteRange(removeDetail);
                        await _unitOfWork.Commit();

                    }

                    _unitOfWork.ComboDetailRepository.UpdateRange(comboDetails);
                }

             

                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(combo);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateComboDetail([FromBody] ComboDetail comboDetail)
        {
            try
            {
                _unitOfWork.ComboDetailRepository.Update(comboDetail);
                await _unitOfWork.Commit();
                return Ok(comboDetail);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> RemoveCombo(int id)
        {
            try
            {
                var combo = await _unitOfWork.ComboRepository.GetByIdTask(id);
                if (combo == null)
                {
                    return NotFound();
                }

                var comboDetails = await _unitOfWork.ComboDetailRepository.GetDetailsByCombo(id);
                _unitOfWork.ComboDetailRepository.DeleteRange(comboDetails);
                await _unitOfWork.Commit();
                await _unitOfWork.ComboRepository.DeleteTask(id);
                await _unitOfWork.Commit();
                return Ok(combo);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> RemoveComboDetail(int id)
        {
            try
            {
                var comboDet = await _unitOfWork.ComboDetailRepository.GetByIdTask(id);
                await _unitOfWork.ComboDetailRepository.DeleteTask(id);
                await _unitOfWork.Commit();
                return Ok(comboDet);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
