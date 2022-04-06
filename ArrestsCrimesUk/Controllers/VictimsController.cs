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
    public class VictimsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VictimsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Victims.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var victims = await _unitOfWork.Victims.GetAsync();
            _unitOfWork.Dispose();
            var response = _mapper.Map<List<VictimsResponse>>(victims);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Victims.GetByYear)]
        //api/v1/victims2018
        public async Task<IActionResult> Get(int? year)
        {
            if (year is null) return BadRequest();

            var victims = await _unitOfWork.Victims.GetByYearAsync(year.ToString());
            _unitOfWork.Dispose();
            var response = _mapper.Map<List<VictimsResponse>>(victims);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Victims.GetBySexAndYear)]
        //api/v1/victims/2018&male
        public async Task<IActionResult> Get(int? year, string sex)
        {
            if (year is null || string.IsNullOrWhiteSpace(sex)) return BadRequest();

            var victims = await _unitOfWork.Victims.GetByYearAndSexAsync(year.ToString(), sex);
            _unitOfWork.Dispose();
            var response = _mapper.Map<List<VictimsResponse>>(victims);
            return Ok(response);
        }
    }
}
