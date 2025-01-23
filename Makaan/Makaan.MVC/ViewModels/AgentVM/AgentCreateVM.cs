using System.ComponentModel.DataAnnotations;

namespace Makaan.MVC.ViewModels.AgentVM;

public class AgentCreateVM
{
    [MaxLength(64,ErrorMessage ="Ad ve Soyad 64 simvoldan çox ola bilməz!"),Required]
    public string FullName { get; set; }
    [Required(ErrorMessage ="Faylı seçin!")]
    public IFormFile File { get; set; }
    public int DepartmentId { get; set; }
}
