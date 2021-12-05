﻿using System;
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

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
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
			return View();
        }

		// POST: Orders/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Create([Bind("strOrderNumber,strOrderKey,eStatus,ePayment,strRemarks,Id,dtBeg,dtEnd,dtCreationDate")] Order order)
        {
            if (ModelState.IsValid)
            {
				var userId =  User.FindFirstValue(ClaimTypes.Name);
				order.strOrderKey = OrderNumberGenerator.GenerateRandomOrderNumber();
				order.User = await _context.Users.FirstAsync( m => m.UserName == userId );
				_context.Add(order);
                await _context.SaveChangesAsync();
				ReloadOrderRefs( order );
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
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(long id, string strOrderKey, [Bind("strOrderNumber,strOrderKey,eStatus,ePayment,strRemarks,Id,dtBeg,dtEnd,dtCreationDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

			if (ModelState.IsValid)
            {
                try
                {
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

				ReloadOrderRefs( order );

				return View( "Index", _context.Order.Where( m =>
				m.strOrderKey   == strOrderKey &&
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

			var order = await _context.Order.FirstOrDefaultAsync(m => 
				m.strOrderKey   == strOrderKey && 
				m.User          != null        &&
				m.User.UserName == userId       );

			if( order == null )
			{
				return RedirectToAction( nameof( Check ) );
			}

			ReloadOrderRefs( order );

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

				ReloadOrderRefs( _order );

				return View( "Index", _context.Order.Where( m => 
				m.strOrderKey   == _order.strOrderKey && 
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

		private void ReloadOrderRefs( Order order )
		{
			_context.Entry( order ).Collection( s => s.listOrderItem ).Load();

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