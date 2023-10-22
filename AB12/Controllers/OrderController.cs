using AB12.Application.Orders.Results;
using AB12.Services.Components;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AB12.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly OrderService _service;

    public OrderController(ILogger<OrderController> logger, OrderService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] OrderResult order)
    {
        try
        {
            var result = await _service.Create(order);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating order");
            throw;
        }
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize, bool includeSoftDeleted = false)
    {
        try
        {
            var result = await _service.GetAllWithPagingAsync(pageNumber, pageSize, includeSoftDeleted);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting all the orders");
            throw;
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([Required] string id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while getting order by id = {id}");
            throw;
        }
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] OrderResult order)
    {
        try
        {
            var result = await _service.Update(order);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating the order");
            throw;
        }
    }

    [HttpDelete("DeleteById")]
    public async Task<IActionResult> DeleteById([Required] string id)
    {
        try
        {
            var result = await _service.DeleteById(id, true);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting the order id = {id}");
            throw;
        }
    }
}
