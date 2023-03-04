using Microsoft.AspNetCore.Mvc;
using SCEAPI.Models;
using SCEAPI.Models.DTOs;
using SCEAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCEAPI.Repository.IRepository;
using Swashbuckle.AspNetCore.Annotations;

namespace SCEAPI.Controllers
{

    [Route("api/events")]
    [ApiController]
    [Produces("application/json")]

    public class SCEAPIController : ControllerBase
    {
        public readonly IEventRepository _eventRepo;
        public readonly IMapper _mapper;
        public readonly ILogger<SCEAPIController> _logger;

        public readonly IConfiguration _config;

        public SCEAPIController(IEventRepository eventRepo, IMapper mapper, ILogger<SCEAPIController> logger, IConfiguration config)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
            _logger = logger;
            _config = config;
        }

        [SwaggerOperation(
            Summary = "Lists all Events",
            Description = "Gets the list of all Events that are stored in events.json."
        )]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the Events.", Type = typeof(List<EventDTO>))]
        public async Task<IActionResult> GetEvents()
        {
            var objList = await _eventRepo.GetAllAsync();
            var objDto = new List<EventDTO>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<EventDTO>(obj));
            }
            return Ok(objDto);
        }

        [SwaggerOperation(
            Summary = "Gets a specific event by Id.",
            Description = "Gets a specific Event by its Id."
        )]
        [HttpGet("{eventId:int}", Name = "GetEvent")]
        [SwaggerResponse(StatusCodes.Status200OK, "The found Event.", Type = typeof(EventDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The Event was not found.")]
        public async Task<IActionResult> GetEvent([SwaggerParameter("The event ID to look up")] int eventId)
        {
            var obj = await _eventRepo.GetAsync(v => v.Id == eventId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<EventDTO>(obj);
            return Ok(objDto);
        }

        [SwaggerOperation(
            Summary = "Gets an Event by display name.",
            Description = "Gets a specific Event by its Display Name. Will return the first matching event found. \n\nNote: This may stop working as expected the API is updated as the display name calculator is expected to change as more events are added."
        )]
        [HttpGet("{displayName}", Name = "GetEventByDisplayName")]
        [SwaggerResponse(StatusCodes.Status200OK, "The found Event.", Type = typeof(EventDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The Event was not found.")]
        public async Task<IActionResult> GetEventByDisplayName([SwaggerParameter("Display Name of the Event")] string displayName)
        {
            var obj = await _eventRepo.GetByDisplayNameAsync(displayName: displayName);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<EventDTO>(obj);
            return Ok(objDto);
        }

        [SwaggerOperation(
            Summary = "Creates a new Event.",
            Description = "Creates a new event, only if the API is in local development. \n\nWill check that no duplicate display names will occur. If you have a display name conflict, the display name generator may need updated"
        )]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The Event was created.", Type = typeof(EventDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The Request was not good.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The endpoint was called when in read only mode.")]
        public async Task<IActionResult> CreateEvent([FromBody, SwaggerRequestBody("The Create Event Payload", Required = true)] EventCreateDTO eventDTO)
        {
            if (_config.GetValue<bool>("ReadOnly"))
            {
                _logger.LogCritical("Attempted to write readonly api");
                return NotFound("The API is in read-only mode. No changes can be made.");
            }
            if (eventDTO == null)
            {
                return BadRequest(ModelState);
            }

            var displayName = Event.GenerateDisplayName(eventDTO.Name, eventDTO.StartDateTime, eventDTO.EndDateTime);

            if (await _eventRepo.GetByDisplayNameAsync(displayName: displayName) != null)
            {
                ModelState.AddModelError("Name", $"Event: {displayName} already exists. Try updating the year, or event name.");
                return ValidationProblem(ModelState);
            }


            var eventObj = _mapper.Map<Event>(eventDTO);

            eventObj.Id = (await _eventRepo.GetAllAsync(tracked: false, orderBy: v => v.OrderBy(v => v.Id))).LastOrDefault(defaultValue: new Event() { Id = 0 }).Id + 1;

            await _eventRepo.CreateAsync(eventObj);

            return Created($"/api/events/{eventObj.Id}", eventObj);
        }
    }

    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [SwaggerOperation("Returns 200 if the API is up.", Description = "If this does not return 200 OK, the API should be considered down.")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The API is up.")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}