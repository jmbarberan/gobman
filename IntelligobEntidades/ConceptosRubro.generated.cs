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
	public partial class ConceptosRubro
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
		
		private int? mConcepto;
		public virtual int? Concepto
		{
			get
			{
				return this.mConcepto;
			}
			set
			{
				this.mConcepto = value;
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
		
		private int? mIndice;
		public virtual int? Indice
		{
			get
			{
				return this.mIndice;
			}
			set
			{
				this.mIndice = value;
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
		public virtual Rubro RubroNav
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
		
		private Concepto mConcepto1;
		public virtual Concepto ConceptoNav
		{
			get
			{
				return this.mConcepto1;
			}
			set
			{
				this.mConcepto1 = value;
			}
		}
		
	}
}
#pragma warning restore 1591