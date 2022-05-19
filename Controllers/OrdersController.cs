using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIS.Data;
using PIS.Models;

namespace PIS.Controllers
{
    public class OrdersController : BaseController
	{
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: Orders/Details/5
		[Authorize]
		public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

		// GET: Orders/Create
		[Authorize]
		public IActionResult Create()
        {
			List<SelectListItem> listItems = new List<SelectListItem>();
			foreach( var itm in _context.Address )
				listItems.Add( new SelectListItem { Text = itm.GetAddressLine(), Value = itm.Id.ToString() } );

			ViewBag.MailingAddress = _context.Address
												.Select( c => new SelectListItem() {	Text = c.GetAddressLine(), 
																						Value = c.Id.ToString() } ).ToList();

			return View();
        }

		// POST: Orders/Create
		[HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create([Bind("strOrderNumber,strOrderKey,eStatus,ePayment,strRemarks,Id,dtBeg,dtEnd,dtCreationDate")] Order order)
        {
            if (ModelState.IsValid)
            {
				string _strAddressId = HttpContext.Request.Form["MailingAddress"];
				Address _address = await _context.Address.FirstOrDefaultAsync(m => m.Id == Int64.Parse( _strAddressId ));
				if( _address == null )
					return NotFound();

				order.MailingAddress = _address;
				order.BillingAddress = _address;

				var userId =  User.FindFirstValue(ClaimTypes.Name);
				while(true)
				{
					string _strOrderKey = OrderNumberGenerator.GenerateRandomOrderNumber();
					if( _context.Order.Where( m =>	m.strOrderKey   == _strOrderKey &&
													m.User          != null         &&
													m.User.UserName == userId        ).Count() == 0 )
					{
						order.strOrderKey = _strOrderKey;
						break;
					}
				}
				order.User = await _context.Users.FirstAsync( m => m.UserName == userId );
				_context.Add(order);
                await _context.SaveChangesAsync();
				reloadOrderRefs( order );
				return View( "Index", _context.Order.Where( m =>
															m.strOrderKey   == order.strOrderKey &&
															m.User          != null              &&
															m.User.UserName == userId             ) );
			}
            return View(order);
        }

		// GET: Orders/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

			List<SelectListItem> listItems = new List<SelectListItem>();
			foreach( var itm in _context.Address )
				listItems.Add( new SelectListItem { Text = itm.GetAddressLine(), Value = itm.Id.ToString() } );

			ViewBag.MailingAddress = _context.Address.Select( c => new SelectListItem() { Text = c.GetAddressLine(), Value = c.Id.ToString() } ).ToList();

			return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(long id, string strOrderKey, [Bind("strOrderNumber,strOrderKey,eStatus,ePayment,strRemarks,Id,dtBeg,dtEnd,dtCreationDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

			if( ModelState.IsValid)
            {
                try
                {
					string _strAddressId = HttpContext.Request.Form["MailingAddress"];
					Address _address = await _context.Address.FirstOrDefaultAsync(m => m.Id == Int64.Parse( _strAddressId ));
					if( _address == null )
						return NotFound();

					order.MailingAddress = _address;
					order.BillingAddress = _address;

					_context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				var userId =  User.FindFirstValue(ClaimTypes.Name);
				reloadOrderRefs( order );

				return View( "Index", _context.Order.Where( m =>	m.strOrderKey   == strOrderKey &&
																	m.User          != null        &&
																	m.User.UserName == userId       ) );
			}
            return View(order);
        }

		// GET: Orders/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(long id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

		// GET: Orders/Check
		public IActionResult Check()
		{
			return View();
		}

		// POST: Orders/Check
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Check( string strOrderKey )
		{
			if( strOrderKey == string.Empty )
			{
				return NotFound();
			}

			var userId =  User.FindFirstValue(ClaimTypes.Name);

			var order = await _context.Order.FirstOrDefaultAsync(m =>	m.strOrderKey   == strOrderKey && 
																		m.User          != null        &&
																		m.User.UserName == userId       );

			if( order == null )
			{
				return RedirectToAction( nameof( Check ) );
			}

			reloadOrderRefs( order );

			return View( "Index", _context.Order.Where( m => 
				m.strOrderKey   == strOrderKey && 
				m.User          != null        &&
				m.User.UserName == userId       ) );
		}

		public async Task<IActionResult> CreateOrderItem()
		{
			return View();
		}

		// POST: Orders/CreateOrderItem
		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateOrderItem( OrderItem _orderitem )
		{
			if( _orderitem == null )
			{
				return NotFound();
			}

			string _strMaterialId = HttpContext.Request.Form["Material"];
			Material _material = await _context.Material.FirstOrDefaultAsync(m => m.Id == Int64.Parse( _strMaterialId ));

			long _OrderId = Int64.Parse( GetCookie( "OrderId" ) );
			Order _order = await _context.Order.FirstOrDefaultAsync(m => m.Id == _OrderId);
			if( _order == null )
			{
				return NotFound();
			}

			_orderitem.Order = _order;
			_orderitem.Material = _material;

			if( ModelState.IsValid )
			{
				_context.Add( _orderitem );
				await _context.SaveChangesAsync();

				var userId =  User.FindFirstValue(ClaimTypes.Name);

				reloadOrderRefs( _order );

				return View( "Index", _context.Order.Where( m =>	m.strOrderKey   == _order.strOrderKey && 
																	m.User          != null               &&
																	m.User.UserName == userId              ) );
			}

			return View();
		}

		// POST: Orders/AddItem
		[Authorize]
		public async Task<IActionResult> AddItem( long id )
		{
			var _order = await _context.Order.FindAsync(id);
			if( _order == null )
			{
				return NotFound();
			}

			SetCookie( "OrderId", _order.Id.ToString(), 30 );

			ViewBag.MaterialName = new SelectList( _context.Material, "Id", "strName" );

			return View( "CreateOrderItem" );
		}

		private void reloadOrderRefs( Order order )
		{
			//_context.OrderItem.Where( p => p.Order == order ).Load();
			_context.Entry( order ).Collection( s => s.listOrderItem ).Load();
			_context.Entry( order ).Reference( s => s.MailingAddress ).Load();

			foreach( var _item in order.listOrderItem )
			{
				_context.Entry( _item ).Reference( s => s.Material ).Load();
				_context.Entry( _item.Material ).Reference( s => s.DefaultUnitPrice ).Load();
				_context.Entry( _item.Material ).Reference( s => s.PrimaryUnit ).Load();
				_context.Entry( _item.Material.DefaultUnitPrice ).Reference( s => s.Currency ).Load();
			}

		}
	}
}
