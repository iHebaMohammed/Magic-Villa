using AutoMapper;
using Demo.API.DTOs;
using Demo.API.Errors;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Demo.API.Controllers
{
    public class VillaNumberController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public VillaNumberController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
            _response = new();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ApiResponse>>> GetAllAsync()
        {
            try
            {
                var villaNumbers = await _unitOfWork.Repository<VillaNumber>().GetAllAsync();
                var mappedVillaNumbers = _mapper.Map<IReadOnlyList<VillaNumber>, IReadOnlyList<VillaNumberDTO>>(villaNumbers);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = mappedVillaNumbers;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                return BadRequest(_response);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VillaNumberDTO>> GetByIdAsync(int id)
        {
            if (id == 0)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var villaNumbers = await _unitOfWork.Repository<VillaNumber>().GetByIdAsync(id);
            if (villaNumbers == null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            var mappedVillaNumber = _mapper.Map<VillaNumber, VillaNumberDTO>(villaNumbers);
            return Ok(mappedVillaNumber);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<VillaNumberDTO>> Create(VillaNumberCreateDTO villaNumberDTO)
        {
            if (villaNumberDTO == null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));



            var mappedVillaNumber = _mapper.Map<VillaNumberCreateDTO, VillaNumber>(villaNumberDTO);

            await _unitOfWork.Repository<VillaNumber>().Create(mappedVillaNumber);
            await _unitOfWork.Complete();
            return Ok(mappedVillaNumber);
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            var villaNumber = await _unitOfWork.Repository<VillaNumber>().GetByIdAsync(id);
            if (villaNumber == null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            _unitOfWork.Repository<VillaNumber>().Delete(villaNumber);
            await _unitOfWork.Complete();
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<VillaDTO>> Update(VillaNumberUpdateDTO villaNumberDTO)
        {
            if (villaNumberDTO == null)
                return BadRequest(StatusCodes.Status400BadRequest);
            var mappedVillaNumber = _mapper.Map<VillaNumberUpdateDTO, VillaNumber>(villaNumberDTO);
            _unitOfWork.Repository<VillaNumber>().Update(mappedVillaNumber);
            await _unitOfWork.Complete();
            return Ok(villaNumberDTO);
        }
    }
}
