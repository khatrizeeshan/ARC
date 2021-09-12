using ARC.App.Clients;
using ARC.App.Engagements;
using ARC.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ARC.Web.Controllers
{
    public class EngagementsController : BaseController
    {
        // GET: EngagementsController
        public async Task<ActionResult> Index([FromQuery] string filter)
        {
            var result = await Mediator.Send(new GetEngagementsListQuery());
            return View(result);
        }

        // GET: EngagementsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await GetEngagementViewModel(id);
            return View(model);
        }

        // GET: EngagementsController/Create
        public async Task<ActionResult> Create()
        {
            var model = new EngagementUpsertViewModel();
            model.Clients = await GetClients();
            return View(model);
        }

        // POST: EngagementsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] EngagementUpsertViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var command = Mapper.Map<CreateEngagementCommand>(model);
                    command.TeamEmailAddresses = JsonSerializer.Deserialize<string[]>(model.TeamEmailAddresses);
                    var result = await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            model.Clients = await GetClients();
            return View(model);
        }

        // GET: EngagementsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = await GetEngagementUpsertViewModel(id);
                model.Clients = await GetClients();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: EngagementsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind] EngagementUpsertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = Mapper.Map<UpdateEngagementCommand>(model);
                    command.TeamEmailAddresses = JsonSerializer.Deserialize<string[]>(model.TeamEmailAddresses);
                    var result = await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            model.Clients = await GetClients();
            return View(model);
        }

        // GET: EngagementsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await GetEngagementViewModel(id);
            return View(model);
        }

        // POST: EngagementsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var command = new DeleteEngagementCommand() { Id = id };
                var result = await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                var model = await GetEngagementUpsertViewModel(id);
                return View(model);
            }
        }

        private async Task<EngagementUpsertViewModel> GetEngagementUpsertViewModel(int id)
        {
            var result = await GetEngagementDetail(id);

            var model = Mapper.Map<EngagementUpsertViewModel>(result);
            model.TeamEmailAddresses = JsonSerializer.Serialize(result.TeamEmailAddresses);
            return model;
        }

        private async Task<EngagementViewModel> GetEngagementViewModel(int id)
        {
            var result = await GetEngagementDetail(id);

            var model = Mapper.Map<EngagementViewModel>(result);
            return model;
        }

        private async Task<EngagementDetail> GetEngagementDetail(int id)
        {
            var query = new GetEngagementDetailQuery() { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        private async Task<IList<ClientLookupDto>> GetClients()
        {
            var query = new GetClientsLookupListQuery();
            var result = await Mediator.Send(query);

            return result.Clients;
        }

    }
}
