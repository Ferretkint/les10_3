using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesHelpdesk.Data;
using VivesHelpdesk.Model;

namespace VivesHelpdesk.Services
{
    public class TicketService
    {
        private readonly VivesHelpdeskDbContext _dbContext;

        public TicketService(VivesHelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Ticket> Find()
        {
            return _dbContext.Tickets.ToList();
        }

        //Get
        public Ticket? Get(int id)
        {
            return _dbContext.Tickets
                .SingleOrDefault(p => p.Id == id);
        }

        //Create
        public Ticket Create(Ticket ticket)
        {
            ticket.CreatedDate = DateTime.UtcNow;

            _dbContext.Add(ticket);
            _dbContext.SaveChanges();

            return ticket;
        }

        //Update
        public Ticket? Update(int id, Ticket ticket)
        {
            var dbTicket = _dbContext.Tickets
                .SingleOrDefault(p => p.Id == id);
            if (dbTicket == null)
            {
                return null;
            }

            dbTicket.Title = ticket.Title;
            dbTicket.Description = ticket.Description;
            dbTicket.Author = ticket.Author;
            dbTicket.AssignedToId = ticket.AssignedToId;

            _dbContext.SaveChanges();

            return ticket;
        }

        //Delete
        public void Delete(int id)
        {
            var dbTicket = _dbContext.Tickets
                .SingleOrDefault(p => p.Id == id);
            if (dbTicket == null)
            {
                return;
            }

            _dbContext.Tickets.Remove(dbTicket);
            _dbContext.SaveChanges();
        }

    }
}
