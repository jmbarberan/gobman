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
	public partial class Patente
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
		
		private string mNombrecomercial;
		public virtual string NombreComercial
		{
			get
			{
				return this.mNombrecomercial;
			}
			set
			{
				this.mNombrecomercial = value;
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
		
		private bool? mArtesano;
		public virtual bool? Artesano
		{
			get
			{
				return this.mArtesano;
			}
			set
			{
				this.mArtesano = value;
			}
		}
		
		private bool? mContabilidadrequerida;
		public virtual bool? ContabilidadRequerida
		{
			get
			{
				return this.mContabilidadrequerida;
			}
			set
			{
				this.mContabilidadrequerida = value;
			}
		}
		
		private string mDireccion;
		public virtual string Direccion
		{
			get
			{
				return this.mDireccion;
			}
			set
			{
				this.mDireccion = value;
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
		
		private Contribuyente mContribuyenteNav;
		public virtual Contribuyente ContribuyenteNav
		{
			get
			{
				return this.mContribuyenteNav;
			}
			set
			{
				this.mContribuyenteNav = value;
			}
		}
		
		private IList<PatentesComponente> mComponentesNav = new List<PatentesComponente>();
		public virtual IList<PatentesComponente> ComponentesNav
		{
			get
			{
				return this.mComponentesNav;
			}
		}
		
	}
}
#pragma warning restore 1591
