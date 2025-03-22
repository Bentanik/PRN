using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VaccinaCare.Repositories.Models;

namespace VaccinaCare.MVCAppFE.Controllers;

[Authorize]
public class HealthGuidesController : Controller
{
    private string APIEndPoint = "https://localhost:5050/api/";
    //private readonly NET1718_PRN231_PRJ_G2_PremaritalCounselingContext _context;
    public HealthGuidesController()
    {

    }

    // GET: HealthGuides
    public async Task<IActionResult> Index()
    {
        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion


            using (var response = await httpClient.GetAsync(APIEndPoint + "HealthGuide"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<HealthGuide>>(content);

                    if (result != null)
                    {
                        return View(result);
                    }
                }
            }
        }

        return View(new List<HealthGuide>());
    }

    // GET: HealthGuides/Details/5
    public async Task<IActionResult> Details(string id)
    {
        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion

            using (var response = await httpClient.GetAsync(APIEndPoint + "HealthGuide/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HealthGuide>(content);

                    if (result != null)
                    {
                        return View(result);
                    }
                }
            }
        }

        return View(new HealthGuide());
    }

    // POST: QuizResults/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                #region Add Token to header of Request

                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                #endregion
                using (var response = await httpClient
                    .DeleteAsync(APIEndPoint + "HealthGuide/" + id))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        TempData["ErrorMessage"] = "Failed to delete the schedule. Please try again.";
                    }
                }

            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Failed to delete the schedule. Please try again.";
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<List<HealthGuideCategory>> GetHealthGuideCategories()
    {
        var scheduleTypes = new List<HealthGuideCategory>();

        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion


            using (var response = await httpClient.GetAsync(APIEndPoint + "HealthGuideCategory"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<HealthGuideCategory>>(content);
                    scheduleTypes = result;
                }
            }
        }

        return scheduleTypes;
    }

    public async Task<IActionResult> Edit(int? id)
    {
        var healthGuidCategories = await this.GetHealthGuideCategories();

        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request
            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
            #endregion

            using (var response = await httpClient.GetAsync(APIEndPoint + "HealthGuide/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<HealthGuide>(content);
                    if (result != null)
                    {
                        ViewData["HealthGuideCategorieId"] = new SelectList(healthGuidCategories, "Id", "Name", result.HealthGuideCategorieId);
                        return View(result);
                    }
                }
            }
        }
        ViewData["HealthGuideCategorieId"] = new SelectList(healthGuidCategories, "Id", "Name");
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
                using (var httpClient = new HttpClient())
                {
                    #region Add Token to header of Request
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                    #endregion
                    using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "HealthGuide/" + healthGuide.Id, healthGuide))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<int>(content);
                            if (result > 0)
                            {
                                saveStatus = true;
                            }
                        }
                    }
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
                using (var httpClient = new HttpClient())
                {
                    #region Add Token to header of Request
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    #endregion

                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "HealthGuide/", healthGuide))
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<int>(content);

                        if (result > 0)
                        {
                            return RedirectToAction(nameof(Index));
                        }

                    }
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