using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;

namespace PrimeActs.Domain
{
    public partial class SetupLocal : AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public string SetupName { get; set; }
        public byte SetupValueType { get; set; }
        public Nullable<int> SetupValueInt { get; set; }
        public Nullable<decimal> SetupValueNumeric { get; set; }
        public Nullable<bool> SetupValueBit { get; set; }
        public string SetupValueNvarchar { get; set; }
                                            }
}
