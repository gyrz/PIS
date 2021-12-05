using PIS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Models
{
	public class Currency : ModelBase
	{
		[Display( Name = "Name", ResourceType = typeof( Resource ) )]
		public string strName   { get; set; }
		[Display( Name = "Abbrev", ResourceType = typeof( Resource ) )]
		public string strAbbrev { get; set; }

		public Currency()
		{
			strName   = string.Empty;
			strAbbrev = string.Empty;
		}
	}
}
