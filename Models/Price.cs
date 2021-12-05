using PIS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Models
{
	public class Price : ModelBase
	{
		public enum EPriceType
		{
			PT_ACCOUNTINGPRICE,
			PT_PURCHASEPRICE,
			PT_SELLINGPRICE,
			PT_LISTPRICE,
		}

		[Display( Name = "PriceType", ResourceType = typeof( Resource ) )]
		[EnumDataType( typeof( EPriceType ) )]
		public EPriceType ePriceType { get; set; }
		[Display( Name = "Price", ResourceType = typeof( Resource ) )]
		public decimal    nPrice     { get; set; }
		[Display( Name = "Currency", ResourceType = typeof( Resource ) )]
		public Currency   Currency   { get; set; }

		public Price()
		{
			ePriceType = EPriceType.PT_ACCOUNTINGPRICE;
			nPrice     = 0;
			Currency   = null;
		}
	}
}
