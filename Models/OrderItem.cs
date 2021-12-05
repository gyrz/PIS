using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PIS.Properties;

namespace PIS.Models
{
	public class OrderItem : ModelBase
	{
		public Order Order { get; set; }
		[Display( Name = "Quantity", ResourceType = typeof( Resource ) )]
		public double   dQuantity  { get; set; }
		[Display( Name = "Material", ResourceType = typeof( Resource ) )]
		public Material Material   { get; set; }
		[Display( Name = "Remarks", ResourceType = typeof( Resource ) )]
		public string   strRemarks { get; set; }

		//public OrderItem()
		//{
		//	dQuantity  = 0;
		//	Material   = new Material();
		//	strRemarks = string.Empty;
		//}
	}
}
