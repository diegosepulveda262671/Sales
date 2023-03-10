using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sales.Shared.Entities
{
    public class State
    {
       

       public int Id { get; set; }

        [Display(Name = "Estado/Departamento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        public Country? Country { get; set; }

        public ICollection<City>? Cities { get; set; }

        public int CitiesCount => Cities == null ? 0 : Cities.Count;

    }
}

