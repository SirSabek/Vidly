using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos;

public class CustomerDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    public bool IsSubscribedToNewsLetter { get; set; }
    public byte MembershipTypeId { get; set; }
    public DateTime? BirthDate { get; set; }
}