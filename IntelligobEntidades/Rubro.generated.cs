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
	public partial class Rubro
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
		
		private string mDenoninacion;
		public virtual string Denoninacion
		{
			get
			{
				return this.mDenoninacion;
			}
			set
			{
				this.mDenoninacion = value;
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
		
		private int? mTabla;
		public virtual int? Tabla
		{
			get
			{
				return this.mTabla;
			}
			set
			{
				this.mTabla = value;
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
		
		private int? mEmision;
		public virtual int? Emision
		{
			get
			{
				return this.mEmision;
			}
			set
			{
				this.mEmision = value;
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
		
		private int? mOrigen;
		public virtual int? Origen
		{
			get
			{
				return this.mOrigen;
			}
			set
			{
				this.mOrigen = value;
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
		
		private string mCalculadora;
		public virtual string Calculadora
		{
			get
			{
				return this.mCalculadora;
			}
			set
			{
				this.mCalculadora = value;
			}
		}
		
		private string mRebajascodigos;
		public virtual string RebajasCodigos
		{
			get
			{
				return this.mRebajascodigos;
			}
			set
			{
				this.mRebajascodigos = value;
			}
		}
		
		private int? mAfectante;
		public virtual int? Afectante
		{
			get
			{
				return this.mAfectante;
			}
			set
			{
				this.mAfectante = value;
			}
		}
		
		private int? mPropiedad;
		public virtual int? Propiedad
		{
			get
			{
				return this.mPropiedad;
			}
			set
			{
				this.mPropiedad = value;
			}
		}
		
		private IList<RubrosParametro> mRubrosparametros = new List<RubrosParametro>();
		public virtual IList<RubrosParametro> ParametrosNav
		{
			get
			{
				return this.mRubrosparametros;
			}
		}
		
	}
}
#pragma warning restore 1591
