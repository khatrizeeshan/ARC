using ARC.App.Clients;
using ARC.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ARC.Web.Controllers
{

    public class ClientsController : BaseController
    {
        // GET: ClientsController
        public async Task<ActionResult> Index([FromQuery] string filter)
        {
            var result = await Mediator.Send(new GetClientsListQuery());
            return View(result);
        }

        // GET: ClientsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await GetClientViewModel(id);
            return View(model);
        }

        // GET: ClientsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] ClientViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var command = new CreateClientCommand() { Code = model.Code, Name = model.Name, Industry = model.Industry };
                    var result = await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        // GET: ClientsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = await GetClientViewModel(id);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ClientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind] ClientViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = new UpdateClientCommand() { Id = id, Code = model.Code, Name = model.Name, Industry = model.Industry };
                    var result = await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        // GET: ClientsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await GetClientViewModel(id);
            return View(model);
        }

        // POST: ClientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var command = new DeleteClientCommand() { Id = id };
                var result = await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                var model = await GetClientViewModel(id);
                return View(model);
            }
        }

        private async Task<ClientViewModel> GetClientViewModel(int id)
        {
            var command = new GetClientDetailQuery() { Id = id };
            var result = await Mediator.Send(command);

            var model = Mapper.Map<ClientViewModel>(result);
            return model;
        }
    }
}
