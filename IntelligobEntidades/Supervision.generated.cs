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
	public partial class Supervision
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
		
		private int? mUsuario;
		public virtual int? Usuario
		{
			get
			{
				return this.mUsuario;
			}
			set
			{
				this.mUsuario = value;
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
		
		private Usuario mUsuario1;
		public virtual Usuario UsuarioNav
		{
			get
			{
				return this.mUsuario1;
			}
			set
			{
				this.mUsuario1 = value;
			}
		}
		
	}
}
#pragma warning restore 1591