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
	public partial class PredioBase
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
		
		private int? mFormatocodigo;
		public virtual int? FormatoCodigo
		{
			get
			{
				return this.mFormatocodigo;
			}
			set
			{
				this.mFormatocodigo = value;
			}
		}
		
		private int? mModopropiedad;
		public virtual int? ModoPropiedad
		{
			get
			{
				return this.mModopropiedad;
			}
			set
			{
				this.mModopropiedad = value;
			}
		}
		
		private int? mTipopropiedad;
		public virtual int? TipoPropiedad
		{
			get
			{
				return this.mTipopropiedad;
			}
			set
			{
				this.mTipopropiedad = value;
			}
		}
		
		private bool? mEscritura;
		public virtual bool? Escritura
		{
			get
			{
				return this.mEscritura;
			}
			set
			{
				this.mEscritura = value;
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
		
		private int? mDominio;
		public virtual int? Dominio
		{
			get
			{
				return this.mDominio;
			}
			set
			{
				this.mDominio = value;
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
		
		private string mUbicacion;
		public virtual string Ubicacion
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
		
		private string mNombreinmueble;
		public virtual string NombreInmueble
		{
			get
			{
				return this.mNombreinmueble;
			}
			set
			{
				this.mNombreinmueble = value;
			}
		}
		
		private int? mViaaccesibilidad;
		public virtual int? ViaAccesibilidad
		{
			get
			{
				return this.mViaaccesibilidad;
			}
			set
			{
				this.mViaaccesibilidad = value;
			}
		}
		
		private int? mViamaterial;
		public virtual int? ViaMaterial
		{
			get
			{
				return this.mViamaterial;
			}
			set
			{
				this.mViamaterial = value;
			}
		}
		
		private bool? mViaaceras;
		public virtual bool? ViaAceras
		{
			get
			{
				return this.mViaaceras;
			}
			set
			{
				this.mViaaceras = value;
			}
		}
		
		private bool? mViaalcantarillado;
		public virtual bool? ViaAlcantarillado
		{
			get
			{
				return this.mViaalcantarillado;
			}
			set
			{
				this.mViaalcantarillado = value;
			}
		}
		
		private bool? mViaalumbrado;
		public virtual bool? ViaAlumbrado
		{
			get
			{
				return this.mViaalumbrado;
			}
			set
			{
				this.mViaalumbrado = value;
			}
		}
		
		private int? mPredioagua;
		public virtual int? PredioAgua
		{
			get
			{
				return this.mPredioagua;
			}
			set
			{
				this.mPredioagua = value;
			}
		}
		
		private bool? mPredioelectricidad;
		public virtual bool? PredioElectricidad
		{
			get
			{
				return this.mPredioelectricidad;
			}
			set
			{
				this.mPredioelectricidad = value;
			}
		}
		
		private int? mPredioalcantarillado;
		public virtual int? PredioAlcantarillado
		{
			get
			{
				return this.mPredioalcantarillado;
			}
			set
			{
				this.mPredioalcantarillado = value;
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
		
		private double? mValorpropiedad;
		public virtual double? ValorPropiedad
		{
			get
			{
				return this.mValorpropiedad;
			}
			set
			{
				this.mValorpropiedad = value;
			}
		}
		
		private int? mRegistro;
		public virtual int? Registro
		{
			get
			{
				return this.mRegistro;
			}
			set
			{
				this.mRegistro = value;
			}
		}
		
		private double? mAvaluoterreno;
		public virtual double? AvaluoTerreno
		{
			get
			{
				return this.mAvaluoterreno;
			}
			set
			{
				this.mAvaluoterreno = value;
			}
		}
		
		private double? mAvaluoconstruccion;
		public virtual double? AvaluoConstruccion
		{
			get
			{
				return this.mAvaluoconstruccion;
			}
			set
			{
				this.mAvaluoconstruccion = value;
			}
		}
		
		private TablaClave mViaMaterialNav;
		public virtual TablaClave ViaMaterialNav
		{
			get
			{
				return this.mViaMaterialNav;
			}
			set
			{
				this.mViaMaterialNav = value;
			}
		}
		
		private TablaClave mDominioNav;
		public virtual TablaClave DominioNav
		{
			get
			{
				return this.mDominioNav;
			}
			set
			{
				this.mDominioNav = value;
			}
		}
		
		private TablaClave mModoPropiedadNav;
		public virtual TablaClave ModoPropiedadNav
		{
			get
			{
				return this.mModoPropiedadNav;
			}
			set
			{
				this.mModoPropiedadNav = value;
			}
		}
		
		private TablaClave mTipoPropiedadNav;
		public virtual TablaClave TipoPropiedadNav
		{
			get
			{
				return this.mTipoPropiedadNav;
			}
			set
			{
				this.mTipoPropiedadNav = value;
			}
		}
		
		private TablaClave mPreAguaNav;
		public virtual TablaClave PreAguaNav
		{
			get
			{
				return this.mPreAguaNav;
			}
			set
			{
				this.mPreAguaNav = value;
			}
		}
		
		private TablaClave mPreAlcantarilladoNav;
		public virtual TablaClave PreAlcantarilladoNav
		{
			get
			{
				return this.mPreAlcantarilladoNav;
			}
			set
			{
				this.mPreAlcantarilladoNav = value;
			}
		}
		
		private IList<PredioTerreno> mTerrenosNav = new List<PredioTerreno>();
		public virtual IList<PredioTerreno> TerrenosNav
		{
			get
			{
				return this.mTerrenosNav;
			}
		}
		
		private IList<PredioTabla> mTablasNav = new List<PredioTabla>();
		public virtual IList<PredioTabla> TablasNav
		{
			get
			{
				return this.mTablasNav;
			}
		}
		
		private IList<PredioPropietario> mPropietariosNav = new List<PredioPropietario>();
		public virtual IList<PredioPropietario> PropietariosNav
		{
			get
			{
				return this.mPropietariosNav;
			}
		}
		
		private IList<PredioFrente> mFrentesNav = new List<PredioFrente>();
		public virtual IList<PredioFrente> FrentesNav
		{
			get
			{
				return this.mFrentesNav;
			}
		}
		
		private IList<PredioBloque> mBloquesNav = new List<PredioBloque>();
		public virtual IList<PredioBloque> BloquesNav
		{
			get
			{
				return this.mBloquesNav;
			}
		}
		
		private IList<PredioFoto> mFotosNav = new List<PredioFoto>();
		public virtual IList<PredioFoto> FotosNav
		{
			get
			{
				return this.mFotosNav;
			}
		}
		
	}
}
#pragma warning restore 1591
