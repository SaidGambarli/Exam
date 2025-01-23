using System.ComponentModel.DataAnnotations;

namespace Makaan.MVC.ViewModels.AuthVM;

public class RegisterVM
{
    [MaxLength(16,ErrorMessage ="Ad 16 simvoldan çox ola bilməz!"), Required]
    public string Name { get; set; } = null!;


    [MaxLength(16,ErrorMessage = "Soyad 16 simvoldan çox ola bilməz!"), Required]
    public string Surname { get; set; } = null!;


    [MaxLength(128,ErrorMessage = "Email 128 simvoldan çox ola bilməz!"),EmailAddress, Required]
    public string Email { get; set; } = null!;


    [MaxLength(16,ErrorMessage = "Username 16 simvoldan çox ola bilməz!"), Required]
    public string Username { get; set; } = null!;


    [MaxLength(32,ErrorMessage = "Password 32 simvoldan çox ola bilməz!"), DataType(DataType.Password), Required]
    public string Password { get; set; }


    [MaxLength(32, ErrorMessage = "Password 32 simvoldan çox ola bilməz!"), DataType(DataType.Password), Compare(nameof(Password),ErrorMessage ="Password düzgün daxil edilmədi!"),Required]
    public string RePassword { get; set; }
}
