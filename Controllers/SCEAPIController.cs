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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDTO>))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            Description = "Gets a specific Event by its Display Name. \n\nNote: This may stop working as expected the API is updated as the display name calculator is expected to change as more events are added."
        )]
        [HttpGet("{displayName}", Name = "GetEventByDisplayName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}