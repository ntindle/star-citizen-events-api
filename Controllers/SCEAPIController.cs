using Microsoft.AspNetCore.Mvc;
using SCEAPI.Models;
using SCEAPI.Models.DTOs;
using SCEAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCEAPI.Repository.IRepository;

namespace SCEAPI.Controllers
{

    [Route("api/events")]
    [ApiController]
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

        [HttpGet("{eventId:int}", Name = "GetEvent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            var obj = await _eventRepo.GetAsync(v => v.Id == eventId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<EventDTO>(obj);
            return Ok(objDto);
        }

        [HttpGet("{displayName}", Name = "GetEventByDisplayName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEventByDisplayName(string displayName)
        {
            var obj = await _eventRepo.GetByDisplayNameAsync(displayName: displayName);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<EventDTO>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDTO eventDTO)
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}