using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiDocker.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        // Note: nullable 
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        [MaxLength(500)]
        public string Area { get; set; }
    }
}
