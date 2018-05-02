#region

using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace PrimeActs.Infrastructure.BaseEntities
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}