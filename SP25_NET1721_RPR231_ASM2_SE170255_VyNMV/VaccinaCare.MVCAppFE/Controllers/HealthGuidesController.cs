using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using VaccinaCare.gRPC.Clients.Protos.HealthGuide;
using VaccinaCare.gRPC.Clients.Protos.HealthGuideCategory;
using VaccinaCare.Repositories.Models;
using HealthGuide = VaccinaCare.Repositories.Models.HealthGuide;

namespace VaccinaCare.MVCAppFE.Controllers;

public class HealthGuidesController : Controller
{
    private string APIEndPoint = "https://localhost:5050/api/";
    private readonly HealthGuideGrpc.HealthGuideGrpcClient _healthGuideClient;
    private readonly HealthGuideCategoryGrpc.HealthGuideCategoryGrpcClient _healthGuideCategoryClient;
    //private readonly NET1718_PRN231_PRJ_G2_PremaritalCounselingContext _context;
    public HealthGuidesController(HealthGuideGrpc.HealthGuideGrpcClient healthGuideClient, HealthGuideCategoryGrpc.HealthGuideCategoryGrpcClient healthGuideCategoryClient)
    {
        _healthGuideClient = healthGuideClient;
        _healthGuideCategoryClient = healthGuideCategoryClient;
    }

    // GET: HealthGuides
    public async Task<IActionResult> Index()
    {
        var healthGuides = _healthGuideClient.GetAll(new gRPC.Clients.Protos.HealthGuide.EmptyRequest()).Data;
        var opt = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        var healthGuide = System.Text.Json.JsonSerializer.Serialize(healthGuides, opt);

        var result = System.Text.Json.JsonSerializer.Deserialize<List<VaccinaCare.Repositories.Models.HealthGuide>>(healthGuide, opt);

        if (result != null)
        {
            return View(result);
        }

        return View(new List<HealthGuide>());
    }

    // GET: HealthGuides/Details/5
    public async Task<IActionResult> Details(string id)
    {
        var healthGuideIdRequest = new HealthGuideIdRequest();
        healthGuideIdRequest.HealthGuideId = int.Parse(id);
        var healthGuides = _healthGuideClient.GetById(healthGuideIdRequest);
        var opt = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        var healthGuideString = System.Text.Json.JsonSerializer.Serialize(healthGuides, opt);

        var result = System.Text.Json.JsonSerializer.Deserialize<VaccinaCare.Repositories.Models.HealthGuide>(healthGuideString, opt);
        if (result != null)
        {
            return View(result);
        }

        return View(new List<HealthGuide>());
    }

    // POST: QuizResults/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var healthGuideIdRequest = new HealthGuideIdRequest();
            healthGuideIdRequest.HealthGuideId = id;
            var result = _healthGuideClient.DeleteById(healthGuideIdRequest);
            if (result.Message == "")
            {
                TempData["ErrorMessage"] = "Failed to delete the schedule. Please try again.";
            }

        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Failed to delete the schedule. Please try again.";
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<List<Repositories.Models.HealthGuideCategory>> GetHealthGuideCategories()
    {
        var scheduleTypes = _healthGuideCategoryClient.GetAll(new gRPC.Clients.Protos.HealthGuideCategory.EmptyRequest()).Data;
        var opt = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        var scheduleTypesString = System.Text.Json.JsonSerializer.Serialize(scheduleTypes, opt);

        var result = System.Text.Json.JsonSerializer.Deserialize<List<VaccinaCare.Repositories.Models.HealthGuideCategory>>(scheduleTypesString, opt);
        if (result != null)
        {
            return result;
        }
        return new List<Repositories.Models.HealthGuideCategory>();
    }

    public async Task<IActionResult> Edit(int? id)
    {
        var healthGuideCategories = await this.GetHealthGuideCategories();

        var healthGuideIdRequest = new HealthGuideIdRequest();
        healthGuideIdRequest.HealthGuideId = (int)id;
        var healthGuides = _healthGuideClient.GetById(healthGuideIdRequest);
        var opt = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        var content = System.Text.Json.JsonSerializer.Serialize(healthGuides, opt);

        var result = JsonConvert.DeserializeObject<HealthGuide>(content);
        if (result != null)
        {
            ViewData["HealthGuideCategorieId"] = new SelectList(healthGuideCategories, "Id", "Name", result.HealthGuideCategorieId);
            return View(result);
        }
        ViewData["HealthGuideCategorieId"] = new SelectList(healthGuideCategories, "Id", "Name");
        return View(new HealthGuide());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(HealthGuide healthGuide)
    {
        var saveStatus = false;
        if (ModelState.IsValid)
        {
            try
            {
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                };
                var healthGuideString = System.Text.Json.JsonSerializer.Serialize(healthGuide, opt);
                var healthGuideAdded = System.Text.Json.JsonSerializer.Deserialize<gRPC.Clients.Protos.HealthGuide.HealthGuide>(healthGuideString, opt);
                var result = _healthGuideClient.Edit(healthGuideAdded);
                if (result.Status == 1)
                {
                    saveStatus = true;
                }
                else
                {
                    saveStatus = false;
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        if (saveStatus)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewData["HealthGuideCategorieId"] = new SelectList(await this.GetHealthGuideCategories(), "Id", "Name", healthGuide.HealthGuideCategorieId);
            return View(healthGuide);
        }
    }



    // GET: HealthGuides/Create
    public async Task<IActionResult> Create()
    {
        ViewData["HealthGuideCategorieId"] = new SelectList(await this.GetHealthGuideCategories(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HealthGuide healthGuide)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                };
                var healthGuideString = System.Text.Json.JsonSerializer.Serialize(healthGuide, opt);
                var healthGuideAdded = System.Text.Json.JsonSerializer.Deserialize<gRPC.Clients.Protos.HealthGuide.HealthGuide>(healthGuideString, opt);
                var result = _healthGuideClient.Add(healthGuideAdded);
                if (result.Status == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
        }
        ViewData["HealthGuideCategorieId"] = new SelectList(await this.GetHealthGuideCategories(), "Id", "Name");
        return View();
    }
    
    public async Task<IActionResult> HealthGuideCategoryList()
    {
        return View();    
    }
}