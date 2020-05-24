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
using Redpeper.Repositories.Order.Combos;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboRepository _comboRepository;
        private readonly IComboDetailRepository _comboDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComboController(IComboRepository comboRepository, IComboDetailRepository comboDetailRepository, IUnitOfWork unitOfWork)
        {
            _comboRepository = comboRepository;
            _comboDetailRepository = comboDetailRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<List<Combo>> GetCombos()
        {
            return await _comboRepository.GetAll();
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
            return await _comboRepository.GetById(id);
        }

        [HttpGet("[action]/{name}")]
        public async Task<Combo> GetComboByName(string name)
        {
            return await _comboRepository.GetByName(name);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Combo>> CreateCombo([FromBody] Combo combo)
        {
            try
            {
                var cmbo = new Combo
                {
                    Name = combo.Name,
                    Description = combo.Description,
                    Total = combo.Total
                };
                _comboRepository.Create(cmbo);
                await _unitOfWork.Commit();

                var maxCombo = await _comboRepository.GetMaxCombo();
                var comboDetails = combo.ComboDetails.Select(x => new ComboDetail
                {
                    ComboId = maxCombo,
                    DishId = x.DishId,
                    Price = x.Price,
                    Qty = x.Qty
                }).ToList();

                _comboDetailRepository.CreateRange(comboDetails);
                await _unitOfWork.Commit();
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
                _comboDetailRepository.Create(detail);
                await _unitOfWork.Commit();
                return detail;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCombo([FromBody] Combo combo)
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
                _comboRepository.Update(cmbo);
                await _unitOfWork.Commit();

                var comboDetails = combo.ComboDetails.Select(x => new ComboDetail
                {
                    Id = x.Id,
                    ComboId = combo.Id,
                    DishId = x.DishId,
                    Price = x.Price,
                    Qty = x.Qty
                }).ToList();

                _comboDetailRepository.UpdateRange(comboDetails);
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
                _comboDetailRepository.Update(comboDetail);
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
                var combo = await _comboRepository.GetById(id);
                if (combo == null)
                {
                    return NotFound();
                }

                var comboDetails = await _comboDetailRepository.GetDetailsByCombo(id);
                _comboDetailRepository.DeleteRange(comboDetails);
                await _unitOfWork.Commit();
                _comboRepository.Remove(combo);
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
                var comboDet = await _comboDetailRepository.GetById(id);
                _comboDetailRepository.Delete(comboDet);
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
