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

namespace Intelligob.Entidades	
{
	public partial class CarpetaCatastralCorteItem
	{
		private int? _id;
		public virtual int? id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}
		
		private string _nombres;
		public virtual string nombres
		{
			get
			{
				return this._nombres;
			}
			set
			{
				this._nombres = value;
			}
		}
		
		private int? _concepto_Id;
		public virtual int? concepto_id
		{
			get
			{
				return this._concepto_Id;
			}
			set
			{
				this._concepto_Id = value;
			}
		}
		
		private string _codigo;
		public virtual string codigo
		{
			get
			{
				return this._codigo;
			}
			set
			{
				this._codigo = value;
			}
		}
		
		private int? _año;
		public virtual int? año
		{
			get
			{
				return this._año;
			}
			set
			{
				this._año = value;
			}
		}
		
		private string _rubro;
		public virtual string rubro
		{
			get
			{
				return this._rubro;
			}
			set
			{
				this._rubro = value;
			}
		}
		
		private double? _valor;
		public virtual double? valor
		{
			get
			{
				return this._valor;
			}
			set
			{
				this._valor = value;
			}
		}
		
		private double? _base_Imponible;
		public virtual double? base_imponible
		{
			get
			{
				return this._base_Imponible;
			}
			set
			{
				this._base_Imponible = value;
			}
		}
		
	}
}
#pragma warning restore 1591