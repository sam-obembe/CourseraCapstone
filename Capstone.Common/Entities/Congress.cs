using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Common.Entities;

public class Congress:Entity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Number { get; init; }
    public string? Name { get; set; }
    public string? EndYear { get; set; }
    public string? StartYear { get; set; }
}