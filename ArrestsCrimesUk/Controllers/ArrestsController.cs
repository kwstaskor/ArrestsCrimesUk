using ArrestsCrimesUk.Contracts;
using ArrestsCrimesUk.Contracts.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepositoryService;

namespace ArrestsCrimesUk.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [EnableCors("AllowCors")]
    public class ArrestsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArrestsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Arrests.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var arrests = await _unitOfWork.Arrests.GetAsync();
            _unitOfWork.Dispose();
            var response = _mapper.Map<List<ArrestsResponse>>(arrests);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Arrests.GetByYear)]
        //api/v1/arrests2018
        public async Task<IActionResult> Get(int? year)
        {
            if(year is null) return BadRequest();

            var arrests = await _unitOfWork.Arrests.GetByYearAsync(year.ToString());
            _unitOfWork.Dispose();
            var response = _mapper.Map<List<ArrestsResponse>>(arrests);
            return Ok(response);
        }
        
        [HttpGet(ApiRoutes.Arrests.GetBySexAndYear)]
        //api/v1/arrests/2018&male
        public async Task<IActionResult> Get(int? year , string sex)
        {
            if(year is null || string.IsNullOrWhiteSpace(sex)) return BadRequest();

            var arrests = await _unitOfWork.Arrests.GetByYearAndSexAsync(year.ToString(),sex);
            _unitOfWork.Dispose();
            var response = _mapper.Map<List<ArrestsResponse>>(arrests);
            return Ok(response);
        } 
        
        [HttpGet(ApiRoutes.Arrests.GetBySexAndYearAndTown)]
        //api/v1/arrests/year&sex&town?year=2018&sex=Male&town=Cleveland
        public async Task<IActionResult> Get(int? year ,string sex,string town)
        {
            if(year is null || string.IsNullOrWhiteSpace(sex) || string.IsNullOrWhiteSpace(town)) return BadRequest();

            var arrests = await _unitOfWork.Arrests.GetByYearAndSexAndTownAsync(year.ToString(),sex,town);
            _unitOfWork.Dispose();
            var response = _mapper.Map<List<ArrestsResponse>>(arrests);
            return Ok(response);
        }
    }
}
