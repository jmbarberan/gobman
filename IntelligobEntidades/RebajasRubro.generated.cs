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
	public partial class RebajasRubro
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
		
		private int? mRebaja;
		public virtual int? Rebaja
		{
			get
			{
				return this.mRebaja;
			}
			set
			{
				this.mRebaja = value;
			}
		}
		
		private int? mRubro;
		public virtual int? Rubro
		{
			get
			{
				return this.mRubro;
			}
			set
			{
				this.mRubro = value;
			}
		}
		
		private int? mTipo;
		public virtual int? Tipo
		{
			get
			{
				return this.mTipo;
			}
			set
			{
				this.mTipo = value;
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
		
		private double? mDenominador;
		public virtual double? Denominador
		{
			get
			{
				return this.mDenominador;
			}
			set
			{
				this.mDenominador = value;
			}
		}
		
		private int? mPersonal;
		public virtual int? Personal
		{
			get
			{
				return this.mPersonal;
			}
			set
			{
				this.mPersonal = value;
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
		
		private Rubro mRubro1;
		public virtual Rubro NavRubro
		{
			get
			{
				return this.mRubro1;
			}
			set
			{
				this.mRubro1 = value;
			}
		}
		
	}
}
#pragma warning restore 1591