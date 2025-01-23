namespace Makaan.MVC.Models;

public class Agent : BaseEntity
{
    public string FullName { get; set; }
    public string ImageUrl { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
