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
using Intelligob.Entidades;

namespace Intelligob.Entidades	
{
	public partial class AguaLectura
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
		
		private int? mCuenta;
		public virtual int? Cuenta
		{
			get
			{
				return this.mCuenta;
			}
			set
			{
				this.mCuenta = value;
			}
		}
		
		private int? mAño;
		public virtual int? Año
		{
			get
			{
				return this.mAño;
			}
			set
			{
				this.mAño = value;
			}
		}
		
		private double? mMes1;
		public virtual double? Mes1
		{
			get
			{
				return this.mMes1;
			}
			set
			{
				this.mMes1 = value;
			}
		}
		
		private double? mMes2;
		public virtual double? Mes2
		{
			get
			{
				return this.mMes2;
			}
			set
			{
				this.mMes2 = value;
			}
		}
		
		private double? mMes3;
		public virtual double? Mes3
		{
			get
			{
				return this.mMes3;
			}
			set
			{
				this.mMes3 = value;
			}
		}
		
		private double? mMes4;
		public virtual double? Mes4
		{
			get
			{
				return this.mMes4;
			}
			set
			{
				this.mMes4 = value;
			}
		}
		
		private double? mMes5;
		public virtual double? Mes5
		{
			get
			{
				return this.mMes5;
			}
			set
			{
				this.mMes5 = value;
			}
		}
		
		private double? mMes6;
		public virtual double? Mes6
		{
			get
			{
				return this.mMes6;
			}
			set
			{
				this.mMes6 = value;
			}
		}
		
		private double? mMes7;
		public virtual double? Mes7
		{
			get
			{
				return this.mMes7;
			}
			set
			{
				this.mMes7 = value;
			}
		}
		
		private double? mMes8;
		public virtual double? Mes8
		{
			get
			{
				return this.mMes8;
			}
			set
			{
				this.mMes8 = value;
			}
		}
		
		private double? mMes9;
		public virtual double? Mes9
		{
			get
			{
				return this.mMes9;
			}
			set
			{
				this.mMes9 = value;
			}
		}
		
		private double? mMes10;
		public virtual double? Mes10
		{
			get
			{
				return this.mMes10;
			}
			set
			{
				this.mMes10 = value;
			}
		}
		
		private double? mMes11;
		public virtual double? Mes11
		{
			get
			{
				return this.mMes11;
			}
			set
			{
				this.mMes11 = value;
			}
		}
		
		private double? mMes12;
		public virtual double? Mes12
		{
			get
			{
				return this.mMes12;
			}
			set
			{
				this.mMes12 = value;
			}
		}
		
		private double? mEstado;
		public virtual double? Estado
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
		
		private AguaPotable mAguapotable;
		public virtual AguaPotable CuentaAguaNav
		{
			get
			{
				return this.mAguapotable;
			}
			set
			{
				this.mAguapotable = value;
			}
		}
		
	}
}
#pragma warning restore 1591