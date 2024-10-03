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
        public async Task<ActionResult<ApiResponse>> GetByIdAsync(int id)
        {
            if(id == 0)
            {
				_response.IsSuccess =false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);   
			}
			var villa = await _unitOfWork.Repository<Villa>().GetByIdAsync(id);
            if (villa == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("this villa not found!");
				return BadRequest(_response);

			}
			var mappedVilla = _mapper.Map<Villa, VillaDTO>(villa);
            _response.Result = mappedVilla;            
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<VillaDTO>> Create(VillaCreateDTO villaDTO)
        {
            if (villaDTO == null)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("There is no villa Data");
                _response.IsSuccess=false;
            }
            try
            {
                var mappedVilla = _mapper.Map<VillaCreateDTO, Villa>(villaDTO);

                await _unitOfWork.Repository<Villa>().Create(mappedVilla);
                await _unitOfWork.Complete();

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                

                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteByIdAsync(int id) 
        {
            var villa = await _unitOfWork.Repository<Villa>().GetByIdAsync(id);
            if (villa == null)
            {
                _response.StatusCode=HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("There is no villa in this id");
				return NotFound(_response);

			}
            try
            {
                _unitOfWork.Repository<Villa>().Delete(villa);
                await _unitOfWork.Complete();
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add($"{ex.Message}");
                return BadRequest(_response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<VillaDTO>> Update(VillaUpdateDTO villaDTO)
        {
            if (villaDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("There is no villa data!");
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            try
            {
				var mappedVilla = _mapper.Map<VillaUpdateDTO, Villa>(villaDTO);
				_unitOfWork.Repository<Villa>().Update(mappedVilla);
				await _unitOfWork.Complete();
                _response.IsSuccess = true;
                _response.Result = villaDTO;
				_response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
			}
			catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"{ex.Message}");
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response );
            }
        }
    }
}
