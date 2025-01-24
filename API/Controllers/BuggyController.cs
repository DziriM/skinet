using API.DTOs;
using API.Errors;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController(StoreContext context) : BaseApiController
{
    private readonly StoreContext _context = context;

    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }
    
    [HttpGet("badrequest")]
    public IActionResult GetBadRequest()
    {
        return BadRequest();
    }
    
    [HttpGet("notfound")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }
    
    [HttpGet("internalerror")]
    public IActionResult GetInternalError()
    {
        throw new Exception("This is a test exception");
    }
    
    [HttpPost("validationerror")]
    public IActionResult GetValidationError(CreateProductDTto productDto)
    {
        return Ok();
    }
}