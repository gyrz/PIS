using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Threading;

namespace PIS.Models
{
	public class LanguageService
	{
		private readonly IStringLocalizer _localizer;

		public LanguageService( IStringLocalizerFactory factory )
		{
			var type = typeof(ShareResource);
			var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
			_localizer = factory.Create( "Resource", assemblyName.Name );
		}

		public LocalizedString Getkey( string key )
		{
			return _localizer[key];
		}
	}
}