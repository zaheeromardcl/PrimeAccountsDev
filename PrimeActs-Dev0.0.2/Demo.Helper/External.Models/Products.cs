using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; //Add  
namespace PrimeActs.Helper.External.Models
{
    /// <summary>  
    /// This class is being serialized to XML.  
    /// </summary>  
    [Serializable]
    [XmlRoot("Products"), XmlType("Products")]
    public class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCost { get; set; }
    }
}