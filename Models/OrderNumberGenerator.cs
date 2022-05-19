using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Models
{
	public class OrderNumberGenerator
	{
		private static char[] _arrChars =
		{
			'A', 'B', 'C', 'D', 'E',
			'F', 'G', 'H', 'I', 'K',
			'L', 'M', 'N', 'O', 'P',
			'Q', 'R', 'S', 'T', 'V',
			'X', 'Y', 'Z'
		};

		public static string GenerateRandomOrderNumber()
		{
			string strReturn = string.Empty;
			int hash = 0;
			try
			{
				long ticks = DateTime.Now.Ticks;
				hash = Int32.Parse( ticks.ToString().Substring( 10 ) );

				Random rnd = new Random(hash);
				for (int iPos = 0; iPos < 4; iPos++)
					strReturn += _arrChars[rnd.Next(0, _arrChars.Count())];

				for (int iPos = 0; iPos < 4; iPos++)
					strReturn += rnd.Next(0, 9);
			}
			catch( FormatException e )
			{
				Console.WriteLine( e.Message );
			}

			return strReturn;
		}
	}
}
