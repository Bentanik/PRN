using Microsoft.AspNetCore.Mvc;
using VaccinaCare.SoapClients.MVC.VyNMV.SoapClients;

namespace VaccinaCare.SoapClients.MVC.VyNMV.Controllers;

public class HealthGuidesController 
    (SoapConsumer soapConsumer)
    : Controller
{
    // GET: HealthGuidesController
    public async Task<IActionResult> Index()
    {
        var result = await soapConsumer.GetHealthGuides();
        return View(result);
    }

    //// GET: HealthGuidesController/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var result  = await soapConsumer.GetHealthGuide(id);
        return View(result);
    }

    //// GET: HealthGuidesController/Create
    //public ActionResult Create()
    //{
    //    return View();
    //}

    //// POST: HealthGuidesController/Create
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create(IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}

    //// GET: HealthGuidesController/Edit/5
    //public ActionResult Edit(int id)
    //{
    //    return View();
    //}

    //// POST: HealthGuidesController/Edit/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}

    //// GET: HealthGuidesController/Delete/5
    //public ActionResult Delete(int id)
    //{
    //    return View();
    //}

    //// POST: HealthGuidesController/Delete/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Delete(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}
}
