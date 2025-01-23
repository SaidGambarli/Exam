using System.ComponentModel.DataAnnotations;

namespace Makaan.MVC.ViewModels.AuthVM;

public class LoginVM
{
    [MaxLength(128, ErrorMessage = "Email 128 simvoldan çox ola bilməz!"), Required]
    public string EmailOrUserName { get; set; }
    [MaxLength(32), DataType(DataType.Password), Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
