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
	public partial class Funcion
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
		
		private string mDenominacion;
		public virtual string Denominacion
		{
			get
			{
				return this.mDenominacion;
			}
			set
			{
				this.mDenominacion = value;
			}
		}
		
		private int? mGrupo;
		public virtual int? Grupo
		{
			get
			{
				return this.mGrupo;
			}
			set
			{
				this.mGrupo = value;
			}
		}
		
		private int? mModulo;
		public virtual int? Modulo
		{
			get
			{
				return this.mModulo;
			}
			set
			{
				this.mModulo = value;
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
		
		private Modulo mModulo1;
		public virtual Modulo Modulo1
		{
			get
			{
				return this.mModulo1;
			}
			set
			{
				this.mModulo1 = value;
			}
		}
		
		private IList<Comando> mFuncionescomandos = new List<Comando>();
		public virtual IList<Comando> ComandosNav
		{
			get
			{
				return this.mFuncionescomandos;
			}
		}
		
	}
}
#pragma warning restore 1591