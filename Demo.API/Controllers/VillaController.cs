using AutoMapper;
using Demo.API.DTOs;
using Demo.API.Errors;
using Demo.BLL.Interfaces;
using Demo.DAL;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Demo.API.Controllers
{
    public class VillaController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public VillaController(IUnitOfWork unitOfWork , IMapper mapper)
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
                var villas = await _unitOfWork.Repository<Villa>().GetAllAsync();
                var mappedVillas = _mapper.Map<IReadOnlyList<Villa>, IReadOnlyList<VillaDTO>>(villas);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = mappedVillas;
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
        public async Task<ActionResult<VillaDTO>> GetByIdAsync(int id)
        {
            if(id == 0)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var villa = await _unitOfWork.Repository<Villa>().GetByIdAsync(id);
            if (villa == null) 
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            var mappedVilla = _mapper.Map<Villa, VillaDTO>(villa);
            return Ok(mappedVilla);
        }

        [HttpPost]
        public async Task<ActionResult<VillaDTO>> Create(VillaCreateDTO villaDTO)
        {
            if (villaDTO == null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            

            var mappedVilla = _mapper.Map<VillaCreateDTO, Villa>(villaDTO);

            await _unitOfWork.Repository<Villa>().Create(mappedVilla);
            await _unitOfWork.Complete();
            return Ok(mappedVilla);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteByIdAsync(int id) 
        {
            var villa = await _unitOfWork.Repository<Villa>().GetByIdAsync(id);
            if (villa == null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            _unitOfWork.Repository<Villa>().Delete(villa);
            await _unitOfWork.Complete();
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<VillaDTO>> Update(VillaUpdateDTO villaDTO)
        {
            if (villaDTO == null)
                return BadRequest(StatusCodes.Status400BadRequest);
            var mappedVilla = _mapper.Map<VillaUpdateDTO, Villa>(villaDTO);
            _unitOfWork.Repository<Villa>().Update(mappedVilla);
            await _unitOfWork.Complete();
            return Ok(villaDTO);
        }
    }
}
