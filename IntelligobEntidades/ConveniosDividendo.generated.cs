#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace Intelligob.Entidades	
{
	public partial class ConveniosDividendo
	{
		private int mId;
		public virtual int Id
		{
			get
			{
				return this.mId;
			}
			set
			{
				this.mId = value;
			}
		}
		
		private int? mConvenio;
		public virtual int? Convenio
		{
			get
			{
				return this.mConvenio;
			}
			set
			{
				this.mConvenio = value;
			}
		}
		
		private int? mDividendo;
		public virtual int? Dividendo
		{
			get
			{
				return this.mDividendo;
			}
			set
			{
				this.mDividendo = value;
			}
		}
		
		private DateTime? mVencimiento;
		public virtual DateTime? Vencimiento
		{
			get
			{
				return this.mVencimiento;
			}
			set
			{
				this.mVencimiento = value;
			}
		}
		
		private double? mValor;
		public virtual double? Valor
		{
			get
			{
				return this.mValor;
			}
			set
			{
				this.mValor = value;
			}
		}
		
		private double? mPagos;
		public virtual double? Pagos
		{
			get
			{
				return this.mPagos;
			}
			set
			{
				this.mPagos = value;
			}
		}
		
		private int? mEstado;
		public virtual int? Estado
		{
			get
			{
				return this.mEstado;
			}
			set
			{
				this.mEstado = value;
			}
		}
		
	}
}
#pragma warning restore 1591
