﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redpeper.Model;
using Redpeper.Repositories;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProviderController(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
        {
            _providerRepository = providerRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Provider>>> GetAll()
        {

            return await _providerRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> Get(int id)
        {
            var provider = await _providerRepository.GetById(id);

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
                _providerRepository.Create(provider);
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
                _providerRepository.Update(provider);
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
            var provider = await _providerRepository.GetById(id);
            if (provider == null)
            {
                return NotFound();
            }

            _providerRepository.Remove(provider);
            await _unitOfWork.Commit();
            return provider;
        }
    }
}