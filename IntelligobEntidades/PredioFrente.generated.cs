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
	public partial class PredioFrente
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
		
		private int? mPredio;
		public virtual int? Predio
		{
			get
			{
				return this.mPredio;
			}
			set
			{
				this.mPredio = value;
			}
		}
		
		private int? mFrente;
		public virtual int? Frente
		{
			get
			{
				return this.mFrente;
			}
			set
			{
				this.mFrente = value;
			}
		}
		
		private double? mSuperficie;
		public virtual double? Superficie
		{
			get
			{
				return this.mSuperficie;
			}
			set
			{
				this.mSuperficie = value;
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
		
		private PredioBase mPrediosbase;
		public virtual PredioBase PredioNav
		{
			get
			{
				return this.mPrediosbase;
			}
			set
			{
				this.mPrediosbase = value;
			}
		}
		
	}
}
#pragma warning restore 1591