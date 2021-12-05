using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PIS.Models;
using PIS.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PIS.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController( ILogger<HomeController> logger )
		{
			_logger = logger;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
		public IActionResult Error()
		{
			return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
		}

		public IActionResult ChangeLanguage( string culture )
		{
			Response.Cookies.Append( CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue( new RequestCulture( culture ) ),
				new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears( 1 ) } );

			Resource.Culture = new CultureInfo( culture );

			return Redirect( Request.Headers["Referer"].ToString() );
		}
	}
}
