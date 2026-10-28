using System.Web.Mvc;
using Practical_8_.Net.Models;

namespace Practical_8_.Net.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryDataAccess db = new InventoryDataAccess();

        // 1. GET: Inventory
        public ActionResult Index()
        {
            var products = db.GetAllProducts();
            return View(products);
        }

        // 2. GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                if (db.InsertProduct(item))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Unable to insert product into database.");
            }
            return View(item);
        }

        // 3. GET: Inventory/Edit/5
        public ActionResult Edit(int id)
        {
            var item = db.GetProductById(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                if (db.UpdateProduct(item))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Unable to update product.");
            }
            return View(item);
        }

        // 4. GET: Inventory/Details/5
        public ActionResult Details(int id)
        {
            var item = db.GetProductById(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        // 5. GET: Inventory/Delete/5
        public ActionResult Delete(int id)
        {
            var item = db.GetProductById(id);
            if (item == null) return HttpNotFound();
            return View(item);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}