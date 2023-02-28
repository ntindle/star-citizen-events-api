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

        public SCEAPIController(IEventRepository eventRepo, IMapper mapper, ILogger<SCEAPIController> logger)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventDTO))]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateDTO eventDTO)
        {
            if (eventDTO == null)
            {
                return BadRequest(ModelState);
            }

            // if(await _eventRepo.GetAsync(v=> v.Name.ToLower()==eventDTO.Name.ToLower()) != null)
            // {
            //     ModelState.AddModelError("Name", "Event already exists. Try adding the year instead");
            //     return ValidationProblem(ModelState);
            // }

            var eventObj = _mapper.Map<Event>(eventDTO);

            eventObj.Id =  (await _eventRepo.GetAllAsync(tracked: false, orderBy: v => v.OrderBy(v => v.Id))).LastOrDefault(defaultValue: new Event(){Id = 0}).Id + 1;

            await _eventRepo.CreateAsync(eventObj);

            return Created($"/api/events/{eventObj.Id}", eventObj);
        }
    }
}