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
	public partial class PredioTerreno
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
		
		private string mLinderonortenombres;
		public virtual string LinderoNorteNombres
		{
			get
			{
				return this.mLinderonortenombres;
			}
			set
			{
				this.mLinderonortenombres = value;
			}
		}
		
		private int? mLinderonortepredio;
		public virtual int? LinderoNortePredio
		{
			get
			{
				return this.mLinderonortepredio;
			}
			set
			{
				this.mLinderonortepredio = value;
			}
		}
		
		private double? mLinderonorteextension;
		public virtual double? LinderoNorteExtension
		{
			get
			{
				return this.mLinderonorteextension;
			}
			set
			{
				this.mLinderonorteextension = value;
			}
		}
		
		private string mLinderosurnombres;
		public virtual string LinderoSurNombres
		{
			get
			{
				return this.mLinderosurnombres;
			}
			set
			{
				this.mLinderosurnombres = value;
			}
		}
		
		private int? mLinderosurpredio;
		public virtual int? LinderoSurPredio
		{
			get
			{
				return this.mLinderosurpredio;
			}
			set
			{
				this.mLinderosurpredio = value;
			}
		}
		
		private double? mLinderosurextension;
		public virtual double? LinderoSurExtension
		{
			get
			{
				return this.mLinderosurextension;
			}
			set
			{
				this.mLinderosurextension = value;
			}
		}
		
		private string mLinderoestenombres;
		public virtual string LinderoEsteNombres
		{
			get
			{
				return this.mLinderoestenombres;
			}
			set
			{
				this.mLinderoestenombres = value;
			}
		}
		
		private int? mLinderoestepredio;
		public virtual int? LinderoEstePredio
		{
			get
			{
				return this.mLinderoestepredio;
			}
			set
			{
				this.mLinderoestepredio = value;
			}
		}
		
		private double? mLinderoesteextension;
		public virtual double? LinderoEsteExtension
		{
			get
			{
				return this.mLinderoesteextension;
			}
			set
			{
				this.mLinderoesteextension = value;
			}
		}
		
		private string mLinderooestenombres;
		public virtual string LinderoOesteNombres
		{
			get
			{
				return this.mLinderooestenombres;
			}
			set
			{
				this.mLinderooestenombres = value;
			}
		}
		
		private int? mLinderooestepredio;
		public virtual int? LinderoOestePredio
		{
			get
			{
				return this.mLinderooestepredio;
			}
			set
			{
				this.mLinderooestepredio = value;
			}
		}
		
		private double? mLinderooesteextension;
		public virtual double? LinderoOesteExtension
		{
			get
			{
				return this.mLinderooesteextension;
			}
			set
			{
				this.mLinderooesteextension = value;
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
		
		private int? mNumerolados;
		public virtual int? NumeroLados
		{
			get
			{
				return this.mNumerolados;
			}
			set
			{
				this.mNumerolados = value;
			}
		}
		
		private int? mNumeroangulosrectos;
		public virtual int? NumeroAngulosRectos
		{
			get
			{
				return this.mNumeroangulosrectos;
			}
			set
			{
				this.mNumeroangulosrectos = value;
			}
		}
		
		private double? mPerimetro;
		public virtual double? Perimetro
		{
			get
			{
				return this.mPerimetro;
			}
			set
			{
				this.mPerimetro = value;
			}
		}
		
		private int? mCalidadsuelo;
		public virtual int? CalidadSuelo
		{
			get
			{
				return this.mCalidadsuelo;
			}
			set
			{
				this.mCalidadsuelo = value;
			}
		}
		
		private int? mLocalizacionmanzana;
		public virtual int? LocalizacionManzana
		{
			get
			{
				return this.mLocalizacionmanzana;
			}
			set
			{
				this.mLocalizacionmanzana = value;
			}
		}
		
		private int? mNivelrazante;
		public virtual int? NivelRazante
		{
			get
			{
				return this.mNivelrazante;
			}
			set
			{
				this.mNivelrazante = value;
			}
		}
		
		private int? mFuenteagua;
		public virtual int? FuenteAgua
		{
			get
			{
				return this.mFuenteagua;
			}
			set
			{
				this.mFuenteagua = value;
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
		
		private double? mFrente;
		public virtual double? Frente
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
		
		private double? mFondo;
		public virtual double? Fondo
		{
			get
			{
				return this.mFondo;
			}
			set
			{
				this.mFondo = value;
			}
		}
		
		private int? mZonaHomogenea;
		public virtual int? ZonaHomogenea
		{
			get
			{
				return this.mZonaHomogenea;
			}
			set
			{
				this.mZonaHomogenea = value;
			}
		}
		
		private int? mClaseTierra;
		public virtual int? ClaseTierra
		{
			get
			{
				return this.mClaseTierra;
			}
			set
			{
				this.mClaseTierra = value;
			}
		}
		
		private double? mAvaluo;
		public virtual double? Avaluo
		{
			get
			{
				return this.mAvaluo;
			}
			set
			{
				this.mAvaluo = value;
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
		
		private TablaClave mCalidadSueloNav;
		public virtual TablaClave CalidadSueloNav
		{
			get
			{
				return this.mCalidadSueloNav;
			}
			set
			{
				this.mCalidadSueloNav = value;
			}
		}
		
		private TablaClave mRazanteNav;
		public virtual TablaClave RazanteNav
		{
			get
			{
				return this.mRazanteNav;
			}
			set
			{
				this.mRazanteNav = value;
			}
		}
		
		private TablaClave mTablaclave2;
		public virtual TablaClave LocManzanaNav
		{
			get
			{
				return this.mTablaclave2;
			}
			set
			{
				this.mTablaclave2 = value;
			}
		}
		
	}
}
#pragma warning restore 1591
