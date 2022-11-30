using RefactoringChallenge.Core.Entities.Interfaces;
using System.Collections.Generic;

namespace RefactoringChallenge.Core.Entities
{
    public partial class Shipper : IEntity
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
