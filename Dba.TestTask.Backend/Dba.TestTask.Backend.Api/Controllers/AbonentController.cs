using Dba.TestTask.Backend.Application.Exceptions;
using Dba.TestTask.Backend.Application.Requests.Abonents;
using Dba.TestTask.Backend.Application.Services.Interfaces;
using Dba.TestTask.Backend.Application.ViewModels.Abonents;
using Dba.TestTask.Backend.Application.ViewModels.StreetStatistics;
using Microsoft.AspNetCore.Mvc;

namespace Dba.TestTask.Backend.Api.Controllers;

[ApiController]
[Route("Abonent")]
public class AbonentController : ControllerBase
{
   private readonly IAbonentService _service;

   public AbonentController(IAbonentService service) => _service = service;

   [HttpPost("Add")]
   public async Task<ActionResult<int>> PostAbonentAsync(AddAbonentRequest request)
   {
       try
       {
           var abonentId = await _service.AddAbonentAsync(request);
           return Ok(abonentId);
       }
       catch (Exception e)
       {
           return BadRequest();;
       }
   }

   [HttpGet]
   public async Task<ActionResult<AbonentViewModelCollection>> GetAbonentsAsync([FromQuery] string? phoneNumber)
   {
       try
       {
           var abonents = (phoneNumber is null)
               ? await _service.GetAbonentsAsync()
               : await _service.GetAbonentsByPhoneAsync(phoneNumber);
           return Ok(abonents);
       }
       catch (Exception e)
       {
           return BadRequest();
       }
   }

   [HttpDelete("Remove/{id:int}")]
   public async Task<IActionResult> RemoveAbonentAsync(int id)
   {
       try
       {
           await _service.RemoveAbonentAsync(id);
           return Ok();
       }
       catch (Exception e)
       {
           return BadRequest();
       }
   }

   [HttpPut("Update")]
   public async Task<IActionResult> UpdateAbonentAsync(UpdateAbonentRequest request)
   {
       try
       {
           await _service.UpdateAbonentAsync(request);
           return Ok();
       }
       catch (Exception e)
       {
           return BadRequest();
       }
   }

   [HttpGet("StreetStatistics")]
   public async Task<ActionResult<StreetStatisticViewModelCollection>> GetStreetStatistics()
   {
       try
       {
           var streetStatistics = await _service.GetStreetStatisticsAsync();
           return Ok(streetStatistics);
       }
       catch (Exception e)
       {
           return BadRequest();
       }
   }
}