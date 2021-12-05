using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using PIS.Properties;

namespace PIS.Models
{
	public static class EnumResourceManager
	{
		public static string GetDisplayName( this Enum e )
		{
			var rm = new ResourceManager(typeof (Resource));
			//foreach( char c in e.GetType().Name.Where( s => IsUpperCase( s ) ).ToList() )

			var resourceDisplayName = rm.GetString( e.ToString(), Resource.Culture );

			return string.IsNullOrWhiteSpace( resourceDisplayName ) ? string.Format( "[{0}]", e ) : resourceDisplayName;
		}

		public static bool IsUpperCase( char c )
		{
			return char.IsUpper( c ) || c == '_';
		}
	}
}
