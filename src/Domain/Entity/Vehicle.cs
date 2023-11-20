using System.ComponentModel.DataAnnotations;
using Domain.Common.Contracts;

namespace Domain.Entity;

public class Vehicle : BaseEntity
{
    [MaxLength(50)]
    public string Brand { get; set; }

    [MaxLength(50)]
    public string Model { get; set; }
    
    public int Year { get; set; }
    
    [MaxLength(20)]
    public string RegistrationNumber { get; set; }
}