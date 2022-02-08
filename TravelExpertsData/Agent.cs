using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    public partial class Agent
    {
        public Agent()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        public int AgentId { get; set; }
        [StringLength(20)]
        public string AgtFirstName { get; set; }
        [StringLength(5)]
        public string AgtMiddleInitial { get; set; }
        [StringLength(20)]
        public string AgtLastName { get; set; }
        [StringLength(20)]
        public string AgtBusPhone { get; set; }
        [StringLength(50)]
        public string AgtEmail { get; set; }
        [StringLength(20)]
        public string AgtPosition { get; set; }
        public int? AgencyId { get; set; }

        [ForeignKey(nameof(AgencyId))]
        [InverseProperty("Agents")]
        public virtual Agency Agency { get; set; }
        [InverseProperty(nameof(Customer.Agent))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
