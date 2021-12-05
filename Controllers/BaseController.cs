using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Controllers
{
	public class BaseController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public void SetCookie( string key, string value, int? expireTime )
		{
			CookieOptions option = new CookieOptions();

			if( expireTime.HasValue )
				option.Expires = DateTime.Now.AddMinutes( expireTime.Value );
			else
				option.Expires = DateTime.Now.AddMilliseconds( 10 );

			Response.Cookies.Append( key, value, option );
		}

		public string GetCookie( string key )
		{
			return Request.Cookies[key];
		}
	}
}
