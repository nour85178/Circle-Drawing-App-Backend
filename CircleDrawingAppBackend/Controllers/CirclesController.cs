using System;
using System.Collections.Generic;
using System.Linq;
using CircleDrawingApp;
using CircleDrawingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]

[Route("api/[controller]")]
public class CirclesController : ControllerBase
{
    private DataContext _context;

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
        
     
    }


    [HttpGet("{setId}")]
    public async Task<IActionResult> GetCirclesBySetId(string setId)
    {

            try
            {
                IQueryable<Circle> query = _context.Circles;

                if (!string.IsNullOrEmpty(setId))
                {
                    query = query.Where(c => c.SetId == setId);
                }

                var circles = await query.ToListAsync();
                return Ok(circles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching circles: {ex}");
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(new List<Circle>());
    }

    [HttpGet]
    public async Task<IActionResult> GenerateNewId()
    {
        try
        {
            Console.WriteLine("GenerateNewId method called.");
            string newId = Guid.NewGuid().ToString();
            //return Ok(new { newId });
            return Ok(new { newId, contentType = "application/json" });

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating new ID: {ex}");
            return StatusCode(500, "Internal Server Error");
        }
    }
    

}

