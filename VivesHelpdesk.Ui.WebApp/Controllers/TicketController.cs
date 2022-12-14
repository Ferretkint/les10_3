using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesHelpdesk.Model;
using VivesHelpdesk.Data;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly VivesHelpdeskDbContext _dbContext;

        public TicketController(VivesHelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index(int? assignedToId)
        {
            if (assignedToId is not null)
            {
                var assignedToPerson = _dbContext.People
                    .SingleOrDefault(p => p.Id == assignedToId);

                if (assignedToPerson is not null)
                {
                    //ViewBag.AssignedToPerson = assignedToPerson;
                    ViewData["AssignedToPerson"] = assignedToPerson;
                }
            }

            var query = _dbContext.Tickets.AsQueryable();

            if (assignedToId.HasValue)
            {
                query = query.Where(t => t.AssignedToId == assignedToId);
            }

            var tickets = query
                .Include(t => t.AssignedTo)
                .ToList();

            return View(tickets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return GetCreateEditView("Create");
        }

        [HttpPost]
        public IActionResult Create([FromForm]Ticket ticket)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return GetCreateEditView("Create", ticket);
            }

            //Execute
            ticket.CreatedDate = DateTime.UtcNow;

            _dbContext.Tickets.Add(ticket);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var ticket = _dbContext.Tickets
                .SingleOrDefault(t => t.Id == id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            return GetCreateEditView("Edit", ticket);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, [FromForm]Ticket ticket)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return GetCreateEditView(nameof(Edit), ticket);
            }

            var dbTicket = _dbContext.Tickets
                .SingleOrDefault(t => t.Id == id);

            if(dbTicket is null)
            {
                return RedirectToAction("Index");
            }

            dbTicket.Title = ticket.Title;
            dbTicket.Description = ticket.Description;
            dbTicket.Author = ticket.Author;
            dbTicket.AssignedToId = ticket.AssignedToId;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        private IActionResult GetCreateEditView(string viewName, Ticket? ticket = null)
        {
            var people = _dbContext.People.ToList();
            ViewBag.People = people;
            return View(viewName, ticket);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ticket = _dbContext.Tickets
                .SingleOrDefault(t => t.Id == id);

            return View(ticket);
        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var ticket = _dbContext.Tickets
                .SingleOrDefault(t => t.Id == id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.Tickets.Remove(ticket);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
