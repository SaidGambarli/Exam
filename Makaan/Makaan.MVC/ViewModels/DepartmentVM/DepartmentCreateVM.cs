using System.ComponentModel.DataAnnotations;

namespace Makaan.MVC.ViewModels.DepartmentVM;

public class DepartmentCreateVM
{
    [MaxLength(32,ErrorMessage ="Ad 32 simvoldan çox ola bilməz")]
    [Required]
    public string Name { get; set; }
}
