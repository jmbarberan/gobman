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
	public partial class Mercado
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
		
		private int? mPuesto;
		public virtual int? Puesto
		{
			get
			{
				return this.mPuesto;
			}
			set
			{
				this.mPuesto = value;
			}
		}
		
		private string mActividad;
		public virtual string Actividad
		{
			get
			{
				return this.mActividad;
			}
			set
			{
				this.mActividad = value;
			}
		}
		
		private int? mContribuyente;
		public virtual int? Contribuyente
		{
			get
			{
				return this.mContribuyente;
			}
			set
			{
				this.mContribuyente = value;
			}
		}
		
		private int? mContrato;
		public virtual int? Contrato
		{
			get
			{
				return this.mContrato;
			}
			set
			{
				this.mContrato = value;
			}
		}
		
		private DateTime? mInscripcionfecha;
		public virtual DateTime? InscripcionFecha
		{
			get
			{
				return this.mInscripcionfecha;
			}
			set
			{
				this.mInscripcionfecha = value;
			}
		}
		
		private DateTime? mDesde;
		public virtual DateTime? Desde
		{
			get
			{
				return this.mDesde;
			}
			set
			{
				this.mDesde = value;
			}
		}
		
		private DateTime? mHasta;
		public virtual DateTime? Hasta
		{
			get
			{
				return this.mHasta;
			}
			set
			{
				this.mHasta = value;
			}
		}
		
		private string mObservaciones;
		public virtual string Observaciones
		{
			get
			{
				return this.mObservaciones;
			}
			set
			{
				this.mObservaciones = value;
			}
		}
		
		private string mCodigo;
		public virtual string Codigo
		{
			get
			{
				return this.mCodigo;
			}
			set
			{
				this.mCodigo = value;
			}
		}
		
		private int? mUbicacion;
		public virtual int? Ubicacion
		{
			get
			{
				return this.mUbicacion;
			}
			set
			{
				this.mUbicacion = value;
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
		
		private Contribuyente mContribuyente1;
		public virtual Contribuyente ContribuyenteNav
		{
			get
			{
				return this.mContribuyente1;
			}
			set
			{
				this.mContribuyente1 = value;
			}
		}
		
	}
}
#pragma warning restore 1591
