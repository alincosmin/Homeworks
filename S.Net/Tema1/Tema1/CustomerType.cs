//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Tema1
{
    public partial class CustomerType
    {
        public CustomerType()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
