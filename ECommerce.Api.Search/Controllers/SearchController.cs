﻿using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService=searchService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            try
            {
                var result = await _searchService.SearchAsync(term.CustomerId);
                if(result.IsSuccess)
                    return Ok(result.SearchResult);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
