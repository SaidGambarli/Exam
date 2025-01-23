namespace Makaan.MVC.Models;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Agent> Agents  { get; set; }
}
