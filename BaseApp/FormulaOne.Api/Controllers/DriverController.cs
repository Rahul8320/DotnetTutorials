using AutoMapper;
using FormulaOne.Api.Models.Requests;
using FormulaOne.Api.Models.Responses;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

public class DriverController(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    Serilog.ILogger logger) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllDriver()
    {
        try
        {
            var driverList = await unitOfWork.DriverRepository.GetAll();

            if (driverList is null || driverList.Any() == false)
            {
                return NotFound("Does not found any driver in our database!");
            }

            var result = mapper.Map<List<GetDriverResponse>>(driverList);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] GetAllDriver Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }

    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriver(Guid driverId)
    {
        try
        {
            var driver = await unitOfWork.DriverRepository.GetById(driverId);

            if (driver is null)
            {
                return NotFound($"Driver with id: {driverId} is not found in our database!");
            }

            var result = mapper.Map<GetDriverResponse>(driver);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] GetDriver Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDriver([FromBody] CreateDriverRequest request)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<Driver>(request);

            await unitOfWork.DriverRepository.Add(result);
            await unitOfWork.Complete();

            return CreatedAtAction(nameof(GetDriver), new { driverId = result.Id }, result);
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] CreateDriver Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest request)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<Driver>(request);

            await unitOfWork.DriverRepository.Update(result);
            await unitOfWork.Complete();

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] UpdateDriver Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }

    [HttpDelete]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        try
        {
            var driver = await unitOfWork.DriverRepository.GetById(driverId);

            if (driver is null)
            {
                return NotFound($"Driver with id: {driverId} is not found in our database!");
            }

            await unitOfWork.DriverRepository.Delete(driverId);
            await unitOfWork.Complete();

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"[Controller] DeleteDriver Function Error. {ex.Message}");
            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message, title: "Server Error");
        }
    }
}
