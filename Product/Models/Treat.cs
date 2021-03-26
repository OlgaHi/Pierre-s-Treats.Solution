using System.Collections.Generic;

namespace Product.Models
{
  public class Treat
  {
    public Item()
    {
      this.Flavors = new HashSet<FlavorTreat>();
    }

    public int TreatId { get; set; }
    public string ProductName { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<FlavorTreat> Flavors { get;}
  }
}