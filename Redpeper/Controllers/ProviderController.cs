using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Redpeper.Collection;
using Redpeper.Model;
using Redpeper.Repositories;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Provider>>> GetAll()
        {

            return await _unitOfWork.ProviderRepository.GetAllOrderById();
        }

        // [HttpGet]
        // public async Task<PagedList<Provider>> GetPaginated(int page=1, int size = 10, string sort="")
        // {
        //     var result = await _providerRepository.GetPaginated(page, size, sort);
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
        //     Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(metadata));
        //     return result;
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> Get(int id)
        {
            var provider = await _unitOfWork.ProviderRepository.GetByIdTask(id);

            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }


        [HttpPost]
        public async Task<ActionResult<Provider>> Create(Provider provider)
        {
            try
            {
                await _unitOfWork.ProviderRepository.InsertTask(provider);
                await _unitOfWork.Commit();
                return provider;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Modify(Provider provider)
        {
            try
            {
                _unitOfWork.ProviderRepository.Update(provider);
                await _unitOfWork.Commit();
                return Ok(provider);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }

        }

        [HttpDelete]
        public async Task<ActionResult<Provider>> Remove(int id)
        {
            var provider = await _unitOfWork.ProviderRepository.GetByIdTask(id);
            if (provider == null)
            {
                return NotFound();
            }

            await _unitOfWork.ProviderRepository.DeleteTask(id);
            await _unitOfWork.Commit();
            return provider;
        }
    }
}
