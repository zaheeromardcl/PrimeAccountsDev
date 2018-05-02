#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class ItemViewModel
    {
        public string Id { get; set; }

        public string label { get; set; }

        public string value { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }

    public class dropdownlistModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class dropdownlistModelTwo
    {
        public Guid Id { get; set; }
        public Guid Id1 { get; set; }
        public Guid Id2 { get; set; }
    }

    public class dropdownlistModelThree
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
    }
}