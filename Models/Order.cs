using Microsoft.AspNetCore.Identity;
using PIS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PIS.Models
{
	public class Order : ModelBase
	{
		public enum EOrderStatus
		{
			WfPU,
			PU,
			ID,
			OD,
			DD
		}

		public enum EPaymentType
		{
			PT_CREDITCARD,
			PT_TRANSFER,
			PT_CASH
		}

		[Display( Name = "OrderNumber", ResourceType = typeof( Resource ) )]
		public string          strOrderNumber { get; set; }
		[Display( Name = "User", ResourceType = typeof( Resource ) )]
		public IdentityUser    User           { get; set; }
		[Display( Name = "BillingAddress", ResourceType = typeof( Resource ) )]
		public Address         BillingAddress { get; set; }
		[Display( Name = "MailingAddress", ResourceType = typeof( Resource ) )]
		public Address         MailingAddress { get; set; }
		[Display( Name = "OrderKey", ResourceType = typeof( Resource ) )]
		[RegularExpression( "^[A-Z]{4}[0-9]{4}$", ErrorMessageResourceName = "OrderKey_Error_Format", ErrorMessageResourceType = typeof( Resource ))]
		public string          strOrderKey    { get; set; }
		[Display( Name = "Status", ResourceType = typeof( Resource ) )]
		[EnumDataType( typeof( EOrderStatus ) )]
		public EOrderStatus    eStatus        { get; set; }
		[Display( Name = "Payment", ResourceType = typeof( Resource ) )]
		[EnumDataType( typeof( EPaymentType ) )]
		public EPaymentType    ePayment       { get; set; }
		[Display( Name = "OrderItem", ResourceType = typeof( Resource ) )]
		public List<OrderItem> listOrderItem  { get; set; }
		[Display( Name = "Remarks", ResourceType = typeof( Resource ) )]
		public string          strRemarks     { get; set; }

		public Order()
		{
			strOrderNumber = string.Empty;
			User           = null;
			BillingAddress = null;
			MailingAddress = null;
			strOrderKey    = string.Empty;
			eStatus        = EOrderStatus.WfPU;
			ePayment       = EPaymentType.PT_CREDITCARD;
			listOrderItem  = new List<OrderItem>();
			strRemarks     = string.Empty;
		}
	}
}
