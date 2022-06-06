using Microsoft.AspNetCore.Mvc;
using Repository;
using Entity;
using MongoDB.Driver;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ConfigurationsController : Controller
    {
        private readonly IMongoDBRepository<Configuration> _configurationRepo;
        private readonly IMapper _mapper;

        public ConfigurationsController(IMongoDBRepository<Configuration> configurationRepo, IMapper mapper)
        {
            _configurationRepo = configurationRepo;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index(string search)
        {
            //Sistemdeki kayıtlı konfigürasyon bilgileri getiriliyor.
            if (search != null)
            {
                //var result = await _configurationRepo.GetAll(Builders<Configuration>.Filter.Text(search));
                var result = await _configurationRepo.GetAll(Builders<Configuration>.Filter.Where(o => o.Name.ToLower().Contains(search.ToLower())));
                ViewBag.Search = search;
                return View(_mapper.Map<IEnumerable<ConfigurationViewModel>>(result));
            }
            else
            {
                var result = await _configurationRepo.GetAll(Builders<Configuration>.Filter.Empty);
                return View(_mapper.Map<IEnumerable<ConfigurationViewModel>>(result));
            }

        }

        public IActionResult AddConfig()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddConfigAsync(ConfigurationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Configuration configuration = _mapper.Map<Configuration>(model);
                    if (configuration != null)
                    {
                        await _configurationRepo.Add(configuration);
                        return RedirectToAction("Index", "Configurations");
                    }

                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Hata Oluştu";
                return View();
            }

        }

        public async Task<IActionResult> DeleteConfig(string id)
        {
            try
            {
                await _configurationRepo.Delete(Builders<Configuration>.Filter.Where(o => o.Id.Equals(id)));
                return RedirectToAction("Index", "Configurations");
            }
            catch (Exception e)
            {
                TempData["Message"] = "Silme Başarısız";
                return RedirectToAction("Index", "Configurations");
            }

        }

        public async Task<IActionResult> EditConfig(string id)
        {
            try
            {
                var result = await _configurationRepo.Get(Builders<Configuration>.Filter.Where(o => o.Id.Equals(id)));
                if (result == null)
                {
                    ViewBag.Message = "Konfigürasyon bulunamadı - Hata";
                }
                return View(_mapper.Map<ConfigurationViewModel>(result));
            }
            catch (Exception e)
            {
                ViewBag.Message = "Hata Oluştu";
                return View(null);
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditConfigAsync(ConfigurationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Configuration configuration = _mapper.Map<Configuration>(model);
                if (configuration != null)
                {
                    await _configurationRepo.Update(Builders<Configuration>.Filter.Where(o => o.Id.Equals(model.Id)), configuration);

                    return RedirectToAction("Index", "Configurations");
                }
            }
            return View(model);
        }
    }
}
