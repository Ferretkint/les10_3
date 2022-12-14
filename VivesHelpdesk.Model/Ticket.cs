using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VivesHelpdesk.Model;

[Table(nameof(Ticket))]
public class Ticket
{
    public int Id { get; set; }

    [Required] 
    public required string Title { get; set; }

    [Required] 
    public required string Description { get; set; }

    [Required] 
    public required string Author { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? AssignedToId { get; set; }
    public Person? AssignedTo { get; set; }
}