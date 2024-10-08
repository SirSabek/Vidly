﻿using System.ComponentModel.DataAnnotations;

namespace Vidly.Models;

public class Customer
{
    public int Id { get; set; }

    [Required, StringLength(255)]
    public string Name { get; set; } = null!;

    [Display(Name = "  Subscribed to Newsletter?")]
    public bool IsSubscribedToNewsLetter { get; set; }
    public MembershipType MembershipType { get; set; } = null!;
    
    [Required, Display(Name = "Membership Type")]
    public byte MembershipTypeId { get; set; }

     [Display(Name = "Date of Birth")]
    [Min18YearsIfAMember]
    public DateTime? BirthDate { get; set; }
}