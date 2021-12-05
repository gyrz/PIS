using PIS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Models
{
	public class Material : ModelBase
	{
		public enum EMaterialType
		{
			MT_PRODUCT,
			MT_SERVICE,
			MT_EQUIPMENT
		}

		[Display( Name = "Name", ResourceType = typeof( Resource ) )]
		public string        strName               { get; set; }
		[Display( Name = "Description", ResourceType = typeof( Resource ) )]
		public string        strDescription        { get; set; }
		[Display( Name = "PrimaryUnit", ResourceType = typeof( Resource ) )]
		public Unit          PrimaryUnit           { get; set; }
		[Display( Name = "AuxiliaryUnit", ResourceType = typeof( Resource ) )]
		public List<Unit>    listAuxiliaryUnit     { get; set; }
		[Display( Name = "MaterialType", ResourceType = typeof( Resource ) )]
		[EnumDataType( typeof( EMaterialType ) )]
		public EMaterialType eMaterialType         { get; set; }
		[Display( Name = "DefaultUnitPrice", ResourceType = typeof( Resource ) )]
		public Price         DefaultUnitPrice      { get; set; }
		public List<Price>   listDefaultUnitPrice  { get; set; }

		public Material()
		{
			strName              = string.Empty;
			strDescription       = string.Empty;
			PrimaryUnit          = null;
			listAuxiliaryUnit    = new List<Unit>();
			eMaterialType        = EMaterialType.MT_PRODUCT;
			listDefaultUnitPrice = new List<Price>();
		}
	}
}
