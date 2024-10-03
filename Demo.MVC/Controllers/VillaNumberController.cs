using AutoMapper;
using Demo.API.DTOs;
using Demo.API.Helper;
using Demo.BLL.Interfaces;
using Demo.DAL;
using Demo.DAL.Entities;
using Demo.MVC.Services;
using Demo.MVC.Services.IServices;
using Demo.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Demo.MVC.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberSevices _villaNumberSevices;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(
            IVillaNumberSevices villaNumberSevices ,
            IVillaService villaService ,
            IMapper mapper
            )
        {
            _villaNumberSevices = villaNumberSevices;
            _villaService=villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            List<VillaNumberDTO> list = new();

            var response = await _villaNumberSevices.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new();
            var response = await _villaService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(villaNumberVM);
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberSevices.CreateAsync<ApiResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "villa number created succesfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                    TempData["error"] = "Error encountered.";

                }
            }

            var resp = await _villaService.GetAllAsync<ApiResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(int villaid)
        {
            VillaNumberUpdateVM villaNumberVM = new();
            var response = await _villaNumberSevices.GetAsync<ApiResponse>(villaid/*, HttpContext.Session.GetString(SD.SessionToken)*/);
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber =  _mapper.Map<VillaNumberUpdateDTO>(model);
            }

            response = await _villaService.GetAllAsync<ApiResponse>(/*HttpContext.Session.GetString(SD.SessionToken)*/);
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }


            return NotFound();
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberSevices.UpdateAsync<ApiResponse>(model.VillaNumber/*, HttpContext.Session.GetString(SD.SessionToken)*/);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "villa number updated succesfully";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                    TempData["error"] = "Error encountered.";

                }
            }

            var resp = await _villaService.GetAllAsync<ApiResponse>(/*HttpContext.Session.GetString(SD.SessionToken)*/);
            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(int villaid)
        {
            VillaNumberDeleteVM villaNumberVM = new();
            var response = await _villaNumberSevices.GetAsync<ApiResponse>(villaid/*, HttpContext.Session.GetString(SD.SessionToken)*/);
            if (response != null && response.IsSuccess)
            {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber = model;
            }

            response = await _villaService.GetAllAsync<ApiResponse>(/*HttpContext.Session.GetString(SD.SessionToken)*/);
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(villaNumberVM);
            }
            TempData["error"] = "Error encountered.";


            return NotFound();
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {

            var response = await _villaNumberSevices.DeleteAsync<ApiResponse>(model.VillaNumber.Id/*, HttpContext.Session.GetString(SD.SessionToken)*/);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "villa number deleted succesfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
    }
}
