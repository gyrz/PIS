using PIS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Models
{
	public class Address : ModelBase
	{
		// TODO add more types 
		public enum EPublicPlace
		{
			PS_STREET
		}

		[Display( Name = "Country", ResourceType = typeof( Resource ) )]
		public string       strCountry     { get; set; }
		[Display( Name = "PostCode", ResourceType = typeof( Resource ) )]
		public string       strPostCode    { get; set; }
		[Display( Name = "City", ResourceType = typeof( Resource ) )]
		public string       strCity        { get; set; }
		[Display( Name = "PublicPlace", ResourceType = typeof( Resource ) )]
		[EnumDataType( typeof( EPublicPlace ) )]
		public EPublicPlace ePublicPlace   { get; set; }
		[Display( Name = "PlaceName", ResourceType = typeof( Resource ) )]
		public string       strPlaceName   { get; set; }
		[Display( Name = "HouseNumber", ResourceType = typeof( Resource ) )]
		public string       strHouseNumber { get; set; }

		public Address()
		{
			strCountry     = string.Empty;
			strPostCode    = string.Empty;
			strCity        = string.Empty;
			ePublicPlace   = EPublicPlace.PS_STREET;
			strPlaceName   = string.Empty;
			strHouseNumber = string.Empty;
		}

	}
}
