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
            {
                _response.IsSuccess =false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Bad Request in this id");
                return BadRequest(_response);
            }
            var villaNumbers = await _unitOfWork.Repository<VillaNumber>().GetByIdAsync(id);
            if (villaNumbers == null)
            {
                _response.IsSuccess =false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("This villa number not found");
                return NotFound(_response);
            }
            try
            {
                var mappedVillaNumber = _mapper.Map<VillaNumber, VillaNumberDTO>(villaNumbers);
                _response.IsSuccess = true;
                _response.Result = mappedVillaNumber;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess=false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add($"{ex.Message}");
                return BadRequest(_response);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<VillaNumberDTO>> Create(VillaNumberCreateDTO villaNumberDTO)
        {
            if (villaNumberDTO == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Null villa number is not allow!");
                return BadRequest(_response);
            }
            try
            {
                var mappedVillaNumber = _mapper.Map<VillaNumberCreateDTO, VillaNumber>(villaNumberDTO);

                await _unitOfWork.Repository<VillaNumber>().Create(mappedVillaNumber);
                await _unitOfWork.Complete();

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = villaNumberDTO;
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess=false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add($"{ex.Message}");
                return BadRequest(_response);
            }
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            var villaNumber = await _unitOfWork.Repository<VillaNumber>().GetByIdAsync(id);
            if (villaNumber == null)
            {
                _response.IsSuccess=false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("Not found this villa number");
                return NotFound(_response);
            }
            try
            {
                _unitOfWork.Repository<VillaNumber>().Delete(villaNumber);
                await _unitOfWork.Complete();
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Result = "Deleted";
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess=false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
                return BadRequest(_response);
            }

        }

        [HttpPut]
        public async Task<ActionResult<VillaDTO>> Update(VillaNumberUpdateDTO villaNumberDTO)
        {
            if (villaNumberDTO == null)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.IsSuccess=false;
                _response.ErrorMessages.Add("Bad request");
                return BadRequest(_response);
            }
            try
            {
                var mappedVillaNumber = _mapper.Map<VillaNumberUpdateDTO, VillaNumber>(villaNumberDTO);
                _unitOfWork.Repository<VillaNumber>().Update(mappedVillaNumber);
                await _unitOfWork.Complete();
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent ;
                _response.Result = villaNumberDTO;
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
                return BadRequest(_response);
            }
        }
    }
}
