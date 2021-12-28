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
	public partial class Concepto
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
		
		private int? mPeriodo;
		public virtual int? Periodo
		{
			get
			{
				return this.mPeriodo;
			}
			set
			{
				this.mPeriodo = value;
			}
		}
		
		private int? mConsecutivo;
		public virtual int? Consecutivo
		{
			get
			{
				return this.mConsecutivo;
			}
			set
			{
				this.mConsecutivo = value;
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
		
		private string mReportedefinicion;
		public virtual string ReporteDefinicion
		{
			get
			{
				return this.mReportedefinicion;
			}
			set
			{
				this.mReportedefinicion = value;
			}
		}
		
		private string mReportecomprobarte;
		public virtual string ReporteComprobarte
		{
			get
			{
				return this.mReportecomprobarte;
			}
			set
			{
				this.mReportecomprobarte = value;
			}
		}
		
		private int? mMenuemisiones;
		public virtual int? MenuEmisiones
		{
			get
			{
				return this.mMenuemisiones;
			}
			set
			{
				this.mMenuemisiones = value;
			}
		}
		
		private string mRecargoscodigos;
		public virtual string RecargosCodigos
		{
			get
			{
				return this.mRecargoscodigos;
			}
			set
			{
				this.mRecargoscodigos = value;
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
		
		private bool? mPagosparciales;
		public virtual bool? PagosParciales
		{
			get
			{
				return this.mPagosparciales;
			}
			set
			{
				this.mPagosparciales = value;
			}
		}
		
		private int? mConveniohabilitado;
		public virtual int? ConvenioHabilitado
		{
			get
			{
				return this.mConveniohabilitado;
			}
			set
			{
				this.mConveniohabilitado = value;
			}
		}
		
		private int? mValidar;
		public virtual int? Validar
		{
			get
			{
				return this.mValidar;
			}
			set
			{
				this.mValidar = value;
			}
		}
		
		private IList<ConceptosRubro> mConceptosrubros = new List<ConceptosRubro>();
		public virtual IList<ConceptosRubro> RubrosNav
		{
			get
			{
				return this.mConceptosrubros;
			}
		}
		
		private IList<ConceptosEmision> mConceptosemisions = new List<ConceptosEmision>();
		public virtual IList<ConceptosEmision> EmisionParametros
		{
			get
			{
				return this.mConceptosemisions;
			}
		}
		
	}
}
#pragma warning restore 1591
