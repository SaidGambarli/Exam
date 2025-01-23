using System.ComponentModel.DataAnnotations;

namespace Makaan.MVC.ViewModels.AgentVM;

public class AgentUpdateVM
{
    [MaxLength(64, ErrorMessage = "AD ve Soyad 64 simvoldan cox ola bilmez!"), Required]
    public string FullName { get; set; }
    [Required(ErrorMessage = "Fayli secin")]
    public IFormFile File { get; set; }
    [MaxLength(256,ErrorMessage ="Sekil 256 simvoldan cox ola bilmez!"),Required]
    public string ImageUrl { get; set; }
    public int DepartmentId { get; set; }
}
