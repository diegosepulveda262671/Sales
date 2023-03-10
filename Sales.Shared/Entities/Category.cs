using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sales.Shared.Entities
{
    public class Category
	{
        public int Id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null!;
    }
}

