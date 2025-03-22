using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PE_PRN231_SP25_TrialTest_NguyenMaiVietVy_FE.Models;

namespace PE_PRN231_SP25_TrialTest_NguyenMaiVietVy_FE.Controllers;

public class CosmeticInformationsController : Controller
{
    private string APIEndPoint = "https://localhost:5050/api/";
    public CosmeticInformationsController()
    {
    }

    // GET: CosmeticInformations
    public async Task<IActionResult> Index()
    {
        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion


            using (var response = await httpClient.GetAsync(APIEndPoint + "CosmeticInformation"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CosmeticInformation>>(content);

                    if (result != null)
                    {
                        return View(result);
                    }
                }
            }
        }

        return View(new List<CosmeticInformation>());
    }

    public async Task<IActionResult> Search(string cosmeticName, string skinType, string cosmeticSize)
    {
        using (var httpClient = new HttpClient())
        {
            var tokenString = HttpContext.Request.Cookies["TokenString"];
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            var url = $"{APIEndPoint}CosmeticInformation/{cosmeticName}/{cosmeticSize}/{skinType}";
            using (var response = await httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CosmeticInformation>>(content);
                    return View("Index", result);
                }
            }
        }

        return View("Index", new List<CosmeticInformation>());
    }

    // GET: CosmeticInformations/Details/5
    public async Task<IActionResult> Details(string id)
    {
        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion

            using (var response = await httpClient.GetAsync(APIEndPoint + "CosmeticInformation/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CosmeticInformation>(content);

                    if (result != null)
                    {
                        return View(result);
                    }
                }
            }
        }

        return View(new CosmeticInformation());
    }

    public async Task<List<CosmeticCategory>> GetCosmeticCategories()
    {
        var cosmeticCategories = new List<CosmeticCategory>();

        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request

            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

            #endregion
            using (var response = await httpClient.GetAsync(APIEndPoint + "CosmeticCategories"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CosmeticCategory>>(content);
                    cosmeticCategories = result;
                }
            }
        }

        return cosmeticCategories;
    }

    // GET: CosmeticInformations/Create
    public async Task<IActionResult> Create()
    {
        ViewData["CategoryId"] = new SelectList(await this.GetCosmeticCategories(), "CategoryId", "CategoryName");
        return View();
    }

    // POST: CosmeticInformations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CosmeticInformation cosmeticInformation)
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
                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "CosmeticInformation/", cosmeticInformation))
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
            catch (Exception ex)
            {
                throw;
            }
        }
        ViewData["CategoryId"] = new SelectList(await this.GetCosmeticCategories(), "CategoryId", "CategoryName");
        return View();
    }
    
    public async Task<IActionResult> Edit(string id)
    {
        var items = await this.GetCosmeticCategories();

        using (var httpClient = new HttpClient())
        {
            #region Add Token to header of Request
            var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);
            #endregion

            using (var response = await httpClient.GetAsync(APIEndPoint + "CosmeticInformation/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CosmeticInformation>(content);
                    if (result != null)
                    {
                        ViewData["CategoryId"] = new SelectList(items, "CategoryId", "CategoryName", result.CategoryId);
                        return View(result);
                    }
                }
            }
        }
        ViewData["CategoryId"] = new SelectList(items, "CategoryId", "CategoryName");
        return View(new CosmeticCategory());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CosmeticInformation cosmeticInformation)
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
                    using (var response = await httpClient.PutAsJsonAsync(APIEndPoint + "CosmeticInformation/" + cosmeticInformation.CosmeticId, cosmeticInformation))
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
            catch (Exception)
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
            ViewData["HealthGuideCategorieId"] = new SelectList(await this.GetCosmeticCategories(), "CategoryId", "CategoryName", cosmeticInformation.CategoryId);
            return View(cosmeticInformation);
        }
    }

    // GET: CosmeticInformations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
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
                    .DeleteAsync(APIEndPoint + "CosmeticInformation/" + id))
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
}
