using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var owners = _repository.Owner.GetAllOwners();
                _logger.LogInfo($"Returned all owners from database. ");

                var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);

                return Ok(ownersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside {nameof(GetAllOwners)} action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOwnerById(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);

                if(owner == null)
                {
                    _logger.LogError($"Could not find owner with id {id} in the database");

                    return NotFound();
                }

                _logger.LogInfo($"Returned owner with id: {id}");

                var ownerResult = _mapper.Map<OwnerDto>(owner);
                return Ok(ownerResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside {nameof(GetOwnerById)} action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
