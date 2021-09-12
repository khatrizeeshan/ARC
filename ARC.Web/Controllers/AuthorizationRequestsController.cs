using ARC.App.Clients;
using ARC.App.AuthorizationRequests;
using ARC.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ARC.App.Engagements;

namespace ARC.Web.Controllers
{
    public class AuthorizationRequestsController : BaseController
    {
        // GET: AuthorizationRequestsController
        public async Task<ActionResult> Index([FromQuery] string filter)
        {
            var result = await Mediator.Send(new GetAuthorizationRequestsListQuery());
            return View(result);
        }

        // GET: AuthorizationRequestsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await GetAuthorizationRequestViewModel(id);
            return View(model);
        }

        // GET: AuthorizationRequestsController/Create
        public async Task<ActionResult> Create()
        {
            var model = new AuthorizationRequestUpsertViewModel();
            model.Engagements = await GetEngagements();
            return View(model);
        }

        // POST: AuthorizationRequestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind] AuthorizationRequestUpsertViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var command = Mapper.Map<CreateAuthorizationRequestCommand>(model);
                    var result = await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            model.Engagements = await GetEngagements();
            return View(model);
        }

        // GET: AuthorizationRequestsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var model = await GetAuthorizationRequestUpsertViewModel(id);
                if(model.HasSent)
                {
                    return RedirectToAction(nameof(Index));
                }
                model.Engagements = await GetEngagements();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: AuthorizationRequestsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind] AuthorizationRequestUpsertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = Mapper.Map<UpdateAuthorizationRequestCommand>(model);
                    var result = await Mediator.Send(command);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            model.Engagements = await GetEngagements();
            return View(model);
        }

        // GET: AuthorizationRequestsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await GetAuthorizationRequestViewModel(id);
            if (model.HasSent)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: AuthorizationRequestsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var command = new DeleteAuthorizationRequestCommand() { Id = id };
                var result = await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                var model = await GetAuthorizationRequestUpsertViewModel(id);
                return View(model);
            }
        }

        // GET: AuthorizationRequestsController/Delete/5
        public async Task<ActionResult> Send(int id)
        {
            var model = await GetAuthorizationRequestViewModel(id);
            if (model.HasSent)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Send(int id, IFormCollection collection)
        {
            try
            {
                var command = new SendAuthorizationRequestCommand() { Id = id };
                var result = await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                var model = await GetAuthorizationRequestViewModel(id);
                return View(model);
            }
        }

        private async Task<AuthorizationRequestUpsertViewModel> GetAuthorizationRequestUpsertViewModel(int id)
        {
            var result = await GetAuthorizationRequestDetail(id);

            var model = Mapper.Map<AuthorizationRequestUpsertViewModel>(result);
            return model;
        }

        private async Task<AuthorizationRequestViewModel> GetAuthorizationRequestViewModel(int id)
        {
            var result = await GetAuthorizationRequestDetail(id);

            var model = Mapper.Map<AuthorizationRequestViewModel>(result);
            return model;
        }

        private async Task<AuthorizationRequestDetail> GetAuthorizationRequestDetail(int id)
        {
            var query = new GetAuthorizationRequestDetailQuery() { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        private async Task<IList<EngagementLookupDto>> GetEngagements()
        {
            var query = new GetEngagementsLookupListQuery();
            var result = await Mediator.Send(query);

            return result.Engagements;
        }

    }
}
