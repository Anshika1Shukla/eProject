﻿using ePizzaHub.Core.Concrete;
using ePizzaHub.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ePizzaHub.Models.ApiModels.Response;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _itemService.GetItemsAsync();
            return Ok(items);
        }
    }

}
