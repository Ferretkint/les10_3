using System.ComponentModel.DataAnnotations.Schema;

namespace VivesHelpdesk.Model;

[Table(nameof(Person))]
public class Person
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public IList<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
}