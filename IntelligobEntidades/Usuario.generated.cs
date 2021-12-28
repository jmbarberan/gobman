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
	public partial class Usuario
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
		
		private string mNombres;
		public virtual string Nombres
		{
			get
			{
				return this.mNombres;
			}
			set
			{
				this.mNombres = value;
			}
		}
		
		private string mClave;
		public virtual string Clave
		{
			get
			{
				return this.mClave;
			}
			set
			{
				this.mClave = value;
			}
		}
		
		private bool mCaduca;
		public virtual bool Caduca
		{
			get
			{
				return this.mCaduca;
			}
			set
			{
				this.mCaduca = value;
			}
		}
		
		private DateTime? mCaducafecha;
		public virtual DateTime? CaducaFecha
		{
			get
			{
				return this.mCaducafecha;
			}
			set
			{
				this.mCaducafecha = value;
			}
		}
		
		private int mEstado;
		public virtual int Estado
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
		
		private IList<Autorizacion> mAutorizaciones = new List<Autorizacion>();
		public virtual IList<Autorizacion> Autorizaciones
		{
			get
			{
				return this.mAutorizaciones;
			}
		}
		
		private IList<Privilegio> mUsuariosprivilegios = new List<Privilegio>();
		public virtual IList<Privilegio> UsuariosPrivilegios
		{
			get
			{
				return this.mUsuariosprivilegios;
			}
		}
		
	}
}
#pragma warning restore 1591
