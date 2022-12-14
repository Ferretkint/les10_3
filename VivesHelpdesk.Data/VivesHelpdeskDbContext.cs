using Microsoft.EntityFrameworkCore;
using VivesHelpdesk.Model;

namespace VivesHelpdesk.Data;

public class VivesHelpdeskDbContext: DbContext
{
    public VivesHelpdeskDbContext(DbContextOptions<VivesHelpdeskDbContext> options): base(options)
    {
            
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Person> People => Set<Person>();

    public void Seed()
    {
        var assignedPersonBavo = new Person
        {
            FirstName = "Bavo",
            LastName = "Ketels"
        };
        People.Add(assignedPersonBavo);

        Tickets.Add(
            new Ticket
            {
                Title = "Ticket1: This is the first ticket.",
                Description = "Description of the ticket",
                Author = "Bavo Ketels",
                CreatedDate = DateTime.UtcNow,
                AssignedTo = assignedPersonBavo
            });
        Tickets.Add(
            new Ticket
            {
                Title = "Ticket2: This is the second ticket.",
                Description = "Description of the ticket",
                Author = "Bavo Ketels",
                CreatedDate = DateTime.UtcNow,
                AssignedTo = assignedPersonBavo
            });
        Tickets.Add(
            new Ticket
            {
                Title = "Ticket3: This is the third ticket.",
                Description = "Description of the ticket",
                Author = "Bavo Ketels",
                CreatedDate = DateTime.UtcNow
            });

        SaveChanges();
    }
}