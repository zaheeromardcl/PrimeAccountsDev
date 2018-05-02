using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class ProduceGroup : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ProduceGroup()
        {
            this.ProduceGroupDepartments = new List<ProduceGroupDepartment>();
            this.Produces = new List<Produce>();
        }

        public System.Guid ProduceGroupID { get; set; }
        
        public string ProduceGroupCode { get; set; }
        public string ProduceGroupName { get; set; }

        


        [ForeignKey("ProduceGroupID")]
        public virtual ICollection<Produce> Produces { get; set; }
        [ForeignKey("ProduceGroupID")]
        public virtual ICollection<ProduceGroupDepartment> ProduceGroupDepartments { get; set; }
    }
}
