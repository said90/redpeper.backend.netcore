﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Tables;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TableController(ITableRepository tableRepository, IUnitOfWork unitOfWork)
        {
            _tableRepository = tableRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Table>>> GetAll()
        {
            return await _tableRepository.GetAll();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Table>> GetById(int id)
        {
            return await _tableRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Table>> Create([FromBody] Table table)
        {
            try
            {
                _tableRepository.Create(table);
                await _unitOfWork.Commit();
                return table;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Table>> Update([FromBody] Table table)
        {

            try
            {
                _tableRepository.Update(table);
                await _unitOfWork.Commit();
                return table;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Table>> Remove(int id)
        {
            var table = await _tableRepository.GetById(id);
            if (table == null)
            {
                return NotFound();
            }

            _tableRepository.Remove(table);
            await _unitOfWork.Commit();
            return table;
        }
    }
}
