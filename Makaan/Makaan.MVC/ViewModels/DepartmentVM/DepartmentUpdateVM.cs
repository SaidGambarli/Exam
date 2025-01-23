using System.ComponentModel.DataAnnotations;

namespace Makaan.MVC.ViewModels.DepartmentVM;

public class DepartmentUpdateVM
{
    [MaxLength(32, ErrorMessage = "Ad 32 simvoldan cox ola bilmez"), Required]
    public string Name { get; set; }
}
