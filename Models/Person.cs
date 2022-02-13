using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Minimal_Swagger_EF.Models
{
    public partial class Person
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public short Age { get; set; }
        public string? JobPosition { get; set; }
    }
}
