using LogisticsApi.DAL.Models;
using LogisticsApi.DAL.Repositories;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApi.Controllers
{
    [ApiController]
    [Route("package")]
    public class PackageController : ControllerBase
    {
        private readonly ILogger<PackageController> _logger;
        private readonly IPackageService _service;

        public PackageController(ILogger<PackageController> logger, IPackageService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Package>> Get()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, string.Empty);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Create(Package package)
        {
            try
            {
                _service.AddPackage(package);
                return Ok();
            }
            catch (PackageRepository.AlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, string.Empty);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Package> GetPackage(string id)
        {
            try
            {
                return Ok(_service.GetPackage(id));
            }
            catch (Package.KolliIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PackageRepository.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, string.Empty);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}