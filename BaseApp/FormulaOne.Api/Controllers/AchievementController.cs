using AutoMapper;
using FormulaOne.Api.Models.Requests;
using FormulaOne.Api.Models.Responses;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

public class AchievementController(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    Serilog.ILogger logger) : BaseController
{

    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriverAchievements(Guid driverId)
    {
        try
        {
            var driverAchievements = await unitOfWork.AchievementRepository.GetDriverAchievement(driverId);

            if (driverAchievements is null)
            {
                return NotFound($"Driver with id: {driverId} is not found in our database!");
            }

            var result = mapper.Map<DriverAchievementResponse>(driverAchievements);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] GetDriverAchievements Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAchievement([FromBody] CreateDriverAchievementRequest request)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<Achievement>(request);

            await unitOfWork.AchievementRepository.Add(result);
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetDriverAchievements), new { driverId = result.DriverId }, result);
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] CreateAchievement Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAchievement([FromBody] UpdateDriverAchievementRequest request)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<Achievement>(request);

            await unitOfWork.AchievementRepository.Update(result);
            await unitOfWork.Complete();

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] UpdateAchievement Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }
}
