using System;
using System.Collections.Generic;
using System.Linq;
using CircleDrawingApp;
using CircleDrawingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


[ApiController]
[Route("api/circles")]
public class CirclesController : ControllerBase
{
    private  DataContext _context;
    private readonly ILogger<CirclesController> _logger;

    public CirclesController(DataContext context, ILogger<CirclesController> logger)
    {
        _context = context;
        _logger = logger;

        _logger.LogInformation($"DataContext instance: {_context}");
    }

    [HttpPost]
    public async Task<IActionResult> SubmitCircle(Circle circle)
    {
        
            if (ModelState.IsValid)
            {
                circle.TimeOfSubmission = DateTime.Now;
                _context.Circles.Add(circle);
                await _context.SaveChangesAsync();
                return Ok(circle);
            }
            return BadRequest(ModelState);
        
        /*catch (Exception ex)
        {
            // Log the exception details for debugging
            Console.WriteLine($"Exception in SubmitCircle: {ex}");
            return StatusCode(500, "Internal Server Error");
        }*/
    }


    [HttpGet("{setId}")]
    public async Task<IActionResult> GetCirclesBySetId(string setId)
    {
        
            var circles = await _context.Circles.Where(c => c.SetId == setId).ToListAsync();
            return Ok(circles);
        
    }

}
