using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VaccinaCare.SoapClients.MVC.VyNMV.SoapClients;
using VaccinaCareWCFReferences;

namespace VaccinaCare.SoapClients.MVC.VyNMV.Controllers
{
    public class HealthGuidesController : Controller
    {
        private readonly SoapConsumer _soapConsumer;

        public HealthGuidesController(SoapConsumer soapConsumer)
        {
            _soapConsumer = soapConsumer;
        }

        // GET: HealthGuidesController
        public async Task<IActionResult> Index()
        {
            var result = await _soapConsumer.GetHealthGuides();
            return View(result);
        }

        // GET: HealthGuidesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _soapConsumer.GetHealthGuide(id);
            return View(result);
        }

        // GET: HealthGuidesController/Create
        public async Task<IActionResult> Create()
        {
            // Fetch HealthGuideCategories and pass them to the view
            var categories = await _soapConsumer.GetHealthGuideCategories();
            ViewData["HealthGuideCategorieId"] = new SelectList(categories, "Id", "Name");
          
            return View();
        }

        // POST: HealthGuidesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HealthGuide healthGuide)
        {
            if (ModelState.IsValid)
            {
                var result = await _soapConsumer.CreateHealthGuide(healthGuide);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to Index after successful creation
                }
                ModelState.AddModelError("", "Error occurred while creating the HealthGuide.");
            }
            // Fetch categories again in case of failure and pass them back to the view
            var categories = await _soapConsumer.GetHealthGuideCategories();
            ViewBag.HealthGuideCategorieId = new SelectList(categories, "Id", "CategoryName");
            return View(healthGuide);
        }

        // GET: HealthGuidesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var healthGuide = await _soapConsumer.GetHealthGuide(id);
            if (healthGuide == null)
            {
                return NotFound();
            }
            var categories = await _soapConsumer.GetHealthGuideCategories();
            ViewData["HealthGuideCategorieId"] = new SelectList(categories, "Id", "Name");
            return View(healthGuide);
        }

        // POST: HealthGuidesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HealthGuide healthGuide)
        {
            if (id != healthGuide.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _soapConsumer.UpdateHealthGuide(healthGuide);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to Index after successful update
                }
                ModelState.AddModelError("", "Error occurred while updating the HealthGuide.");
            }
            return View(healthGuide);
        }

        // GET: HealthGuidesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var healthGuide = await _soapConsumer.GetHealthGuide(id);
            if (healthGuide == null)
            {
                return NotFound();
            }
            return View(healthGuide);
        }

        // POST: HealthGuidesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _soapConsumer.DeleteHealthGuide(id);
            if (result)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index after successful deletion
            }
            return View();
        }
    }
}
