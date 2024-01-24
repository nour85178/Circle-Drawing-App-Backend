using System;
using System.Collections.Generic;
using System.Linq;
using CircleDrawingApp;
using CircleDrawingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

//An attribute indicating that this class is an API controller, and it should follow certain conventions for handling requests and responses.
[ApiController]

//Specifies the route template for the controller. In this case, it sets the base route to "api/Circles," where "Circles" is derived from the controller name.
[Route("api/[controller]")]
public class CirclesController : ControllerBase
{
    // a class representing the database context, allowing the controller to interact with the database.
    private DataContext _context;

    // capturing various events, messages, and data points during the runtime of an application and storing them in a structured format, typically for analysis, debugging, and monitoring purposes. 
    //is used for logging. The logger instance is stored in the private field _logger
    private readonly ILogger<CirclesController> _logger;

    //a constructor that receives instances of DataContext and ILogger<CirclesController> through dependency injection.
    public CirclesController(DataContext context, ILogger<CirclesController> logger)
    {
        _context = context;
        _logger = logger;

        _logger.LogInformation($"DataContext instance: {_context}");
    }

    [HttpPost]
    //ensures that the application remains responsive to user input during these operations.
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

            // Return an empty list explicitly
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

