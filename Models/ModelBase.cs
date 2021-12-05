using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PIS.Properties;
using System.Resources;

namespace PIS.Models
{
	public class ModelBase
	{
		[Display( Name = "Identity", ResourceType = typeof( Resource ) )]
		public long     Id               { get; set; }
		public DateTime dtBeg            { get; set; }
		public DateTime dtEnd            { get; set; }
		public DateTime dtCreationDate   { get; set; }

		public ModelBase()
		{
			Id                = 0;
			dtBeg             = DateTime.MinValue;
			dtEnd             = DateTime.MaxValue;
			dtCreationDate    = DateTime.Now;
		}


		public string GetDisplayName( string strKey )
		{
			var rm = new ResourceManager(GetType());
			//foreach( char c in e.GetType().Name.Where( s => IsUpperCase( s ) ).ToList() )

			var resourceDisplayName = rm.GetString( strKey.ToString(), System.Globalization.CultureInfo.CurrentCulture );

			return string.IsNullOrWhiteSpace( resourceDisplayName ) ? string.Format( "[{0}]", strKey ) : resourceDisplayName;
		}
	}
}
