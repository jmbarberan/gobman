#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace IntelligobMovil.Services
{
	using Telerik.OpenAccess;
	using System.Linq.Dynamic;
	using Intelligob.Entidades;
	using IntelligobMovil.Dto;
	using IntelligobMovil.Assemblers;
	using IntelligobMovil.Repositories;
	using IntelligobMovil.Converters;
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	
	public partial interface IService<TDto, TEntity>
	    where TEntity : class
		where TDto : IDtoWithKey
	{
	    IAssembler<TDto, TEntity> Assembler { get; }
	    IRepository<TEntity> Repository { get; }
	
	    IEnumerable<TDto> Find(Expression<Func<TEntity, bool>> filter);
	    IEnumerable<TDto> GetAll();
		
		IEnumerable<TDto> Find(int startRowIndex, int maximumRows);
	    IEnumerable<TDto> Find(string sortExpression, string filterExpression);
	    IEnumerable<TDto> Find(int? startRowIndex, int? maximumRows, string sortExpression, string filterExpression);
	    
		int Count();
	    int Count(string filterExpression);
		
		TDto GetByKey(string dtoKey);
	    string Add(TDto dto);
		void Update(TDto dto);
	    void Delete(TDto dto);	
	}
	
	public abstract partial class Service<TDto, TEntity> : IService<TDto, TEntity>
	    where TEntity : class
		where TDto : IDtoWithKey
	{
	    IAssembler<TDto, TEntity> assembler;
	    IRepository<TEntity> repository;
	
	    public Service(IAssembler<TDto, TEntity> assembler,
	        IRepository<TEntity> repository)
	    {
	        this.assembler = assembler;
	        this.repository = repository;
	    }
	
	    public IAssembler<TDto, TEntity> Assembler 
	    { 
	        get 
	        {
	            return this.assembler; 
	        } 
	    }
	
	    public IRepository<TEntity> Repository 
	    { 
	        get 
	        {
	            return this.repository; 
	        }
	    }
		
		public virtual IEnumerable<TDto> GetAll()
	    {
	        return this.assembler.Assemble(this.Repository.GetAll());
	    }
	
	    public virtual IEnumerable<TDto> Find(Expression<Func<TEntity, bool>> filter)
	    {
	        return this.Assembler.Assemble(this.Repository.Find(filter));
	    }
	
	    public virtual IEnumerable<TDto> Find(int startRowIndex, int maximumRows)
	    {
	        return this.Find(startRowIndex, maximumRows, null, null);
	    }
	
	    public virtual IEnumerable<TDto> Find(string sortExpression, string filterExpression)
	    {
	        return this.Find(null, null, sortExpression, filterExpression);
	    }
	
	    public virtual IEnumerable<TDto> Find(int? startRowIndex, int? maximumRows, string sortExpression, string filterExpression)
	    {
	        IQueryable<TEntity> query = this.Repository.GetAll();
	
	        if (!string.IsNullOrEmpty(filterExpression))
	        {
	            query = query.Where(filterExpression);
	        }
	        if (!string.IsNullOrEmpty(sortExpression))
	        {
	            query = query.OrderBy(sortExpression);
	        }
	        if (startRowIndex.HasValue)
	        {
	            query = query.Skip(startRowIndex.Value);
	        }
	        if (maximumRows.HasValue)
	        {
	            query = query.Take(maximumRows.Value);
	        }
	
	        return this.Assembler.Assemble(query);
	    }
	
	    public virtual int Count()
	    {
	        return this.Count(string.Empty);
	    }
	
	    public virtual int Count(string filterExpression)
	    {
	        IQueryable<TEntity> query = this.Repository.GetAll();
	
	        if (!string.IsNullOrEmpty(filterExpression))
	        {
	            query = query.Where(filterExpression);    
	        }
	
	        return query.Count();
	    }
	
	    
	    public virtual TDto GetByKey(string dtoKey)
	    {
	        ObjectKey key = KeyUtility.Instance.Convert<TEntity>(dtoKey);
			
	        return this.assembler.Assemble(this.Repository.Get(key));
	    }
	
	    public virtual string Add(TDto dto)
	    {
	        TEntity entity = this.assembler.Assemble(null, dto);
	
	        this.repository.Add(entity);
	
	        ObjectKey key = KeyUtility.Instance.Create(entity);
	
	        return KeyUtility.Instance.Convert(key);
	    }
	
	    public virtual void Update(TDto dto)
	    {
	        ObjectKey key = KeyUtility.Instance.Convert<TEntity>(dto.DtoKey);
	        TEntity entity = this.repository.Get(key);
	
	        this.assembler.Assemble(entity, dto);
	    }
	
	    public virtual void Delete(TDto dto)
	    {
			ObjectKey key = KeyUtility.Instance.Convert<TEntity>(dto.DtoKey);
	        TEntity entity = this.repository.Get(key);
	
	        this.Repository.Remove(entity);
	    }
	}
	
	public partial interface ITablaClaveService : IService<TablaClaveDto, TablaClave>
	{
	
	}
	
	public partial class TablaClaveService : Service<TablaClaveDto, TablaClave>, ITablaClaveService
	{
	    public TablaClaveService(ITablaClaveAssembler assembler, ITablaClaveRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ITablaService : IService<TablaDto, Tabla>
	{
	
	}
	
	public partial class TablaService : Service<TablaDto, Tabla>, ITablaService
	{
	    public TablaService(ITablaAssembler assembler, ITablaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IContribuyenteService : IService<ContribuyenteDto, Contribuyente>
	{
	
	}
	
	public partial class ContribuyenteService : Service<ContribuyenteDto, Contribuyente>, IContribuyenteService
	{
	    public ContribuyenteService(IContribuyenteAssembler assembler, IContribuyenteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRubroService : IService<RubroDto, Rubro>
	{
	
	}
	
	public partial class RubroService : Service<RubroDto, Rubro>, IRubroService
	{
	    public RubroService(IRubroAssembler assembler, IRubroRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRebajasRubroService : IService<RebajasRubroDto, RebajasRubro>
	{
	
	}
	
	public partial class RebajasRubroService : Service<RebajasRubroDto, RebajasRubro>, IRebajasRubroService
	{
	    public RebajasRubroService(IRebajasRubroAssembler assembler, IRebajasRubroRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRebajaService : IService<RebajaDto, Rebaja>
	{
	
	}
	
	public partial class RebajaService : Service<RebajaDto, Rebaja>, IRebajaService
	{
	    public RebajaService(IRebajaAssembler assembler, IRebajaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IContribuyentesRebajaService : IService<ContribuyentesRebajaDto, ContribuyentesRebaja>
	{
	
	}
	
	public partial class ContribuyentesRebajaService : Service<ContribuyentesRebajaDto, ContribuyentesRebaja>, IContribuyentesRebajaService
	{
	    public ContribuyentesRebajaService(IContribuyentesRebajaAssembler assembler, IContribuyentesRebajaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioTerrenoService : IService<PredioTerrenoDto, PredioTerreno>
	{
	
	}
	
	public partial class PredioTerrenoService : Service<PredioTerrenoDto, PredioTerreno>, IPredioTerrenoService
	{
	    public PredioTerrenoService(IPredioTerrenoAssembler assembler, IPredioTerrenoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioTablaService : IService<PredioTablaDto, PredioTabla>
	{
	
	}
	
	public partial class PredioTablaService : Service<PredioTablaDto, PredioTabla>, IPredioTablaService
	{
	    public PredioTablaService(IPredioTablaAssembler assembler, IPredioTablaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioPropietarioService : IService<PredioPropietarioDto, PredioPropietario>
	{
	
	}
	
	public partial class PredioPropietarioService : Service<PredioPropietarioDto, PredioPropietario>, IPredioPropietarioService
	{
	    public PredioPropietarioService(IPredioPropietarioAssembler assembler, IPredioPropietarioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioPisoService : IService<PredioPisoDto, PredioPiso>
	{
	
	}
	
	public partial class PredioPisoService : Service<PredioPisoDto, PredioPiso>, IPredioPisoService
	{
	    public PredioPisoService(IPredioPisoAssembler assembler, IPredioPisoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioFrenteService : IService<PredioFrenteDto, PredioFrente>
	{
	
	}
	
	public partial class PredioFrenteService : Service<PredioFrenteDto, PredioFrente>, IPredioFrenteService
	{
	    public PredioFrenteService(IPredioFrenteAssembler assembler, IPredioFrenteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioFotoService : IService<PredioFotoDto, PredioFoto>
	{
	
	}
	
	public partial class PredioFotoService : Service<PredioFotoDto, PredioFoto>, IPredioFotoService
	{
	    public PredioFotoService(IPredioFotoAssembler assembler, IPredioFotoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioConstruccionService : IService<PredioConstruccionDto, PredioConstruccion>
	{
	
	}
	
	public partial class PredioConstruccionService : Service<PredioConstruccionDto, PredioConstruccion>, IPredioConstruccionService
	{
	    public PredioConstruccionService(IPredioConstruccionAssembler assembler, IPredioConstruccionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioBloqueService : IService<PredioBloqueDto, PredioBloque>
	{
	
	}
	
	public partial class PredioBloqueService : Service<PredioBloqueDto, PredioBloque>, IPredioBloqueService
	{
	    public PredioBloqueService(IPredioBloqueAssembler assembler, IPredioBloqueRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPredioBaseService : IService<PredioBaseDto, PredioBase>
	{
	
	}
	
	public partial class PredioBaseService : Service<PredioBaseDto, PredioBase>, IPredioBaseService
	{
	    public PredioBaseService(IPredioBaseAssembler assembler, IPredioBaseRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IServicioService : IService<ServicioDto, Servicio>
	{
	
	}
	
	public partial class ServicioService : Service<ServicioDto, Servicio>, IServicioService
	{
	    public ServicioService(IServicioAssembler assembler, IServicioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPatenteService : IService<PatenteDto, Patente>
	{
	
	}
	
	public partial class PatenteService : Service<PatenteDto, Patente>, IPatenteService
	{
	    public PatenteService(IPatenteAssembler assembler, IPatenteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IAguaPotableService : IService<AguaPotableDto, AguaPotable>
	{
	
	}
	
	public partial class AguaPotableService : Service<AguaPotableDto, AguaPotable>, IAguaPotableService
	{
	    public AguaPotableService(IAguaPotableAssembler assembler, IAguaPotableRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IAguaLecturaService : IService<AguaLecturaDto, AguaLectura>
	{
	
	}
	
	public partial class AguaLecturaService : Service<AguaLecturaDto, AguaLectura>, IAguaLecturaService
	{
	    public AguaLecturaService(IAguaLecturaAssembler assembler, IAguaLecturaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPatentesComponenteService : IService<PatentesComponenteDto, PatentesComponente>
	{
	
	}
	
	public partial class PatentesComponenteService : Service<PatentesComponenteDto, PatentesComponente>, IPatentesComponenteService
	{
	    public PatentesComponenteService(IPatentesComponenteAssembler assembler, IPatentesComponenteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IConceptoService : IService<ConceptoDto, Concepto>
	{
	
	}
	
	public partial class ConceptoService : Service<ConceptoDto, Concepto>, IConceptoService
	{
	    public ConceptoService(IConceptoAssembler assembler, IConceptoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICoeficienteService : IService<CoeficienteDto, Coeficiente>
	{
	
	}
	
	public partial class CoeficienteService : Service<CoeficienteDto, Coeficiente>, ICoeficienteService
	{
	    public CoeficienteService(ICoeficienteAssembler assembler, ICoeficienteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICoeficienteElementoService : IService<CoeficienteElementoDto, CoeficienteElemento>
	{
	
	}
	
	public partial class CoeficienteElementoService : Service<CoeficienteElementoDto, CoeficienteElemento>, ICoeficienteElementoService
	{
	    public CoeficienteElementoService(ICoeficienteElementoAssembler assembler, ICoeficienteElementoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ISupervisionService : IService<SupervisionDto, Supervision>
	{
	
	}
	
	public partial class SupervisionService : Service<SupervisionDto, Supervision>, ISupervisionService
	{
	    public SupervisionService(ISupervisionAssembler assembler, ISupervisionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPrivilegioService : IService<PrivilegioDto, Privilegio>
	{
	
	}
	
	public partial class PrivilegioService : Service<PrivilegioDto, Privilegio>, IPrivilegioService
	{
	    public PrivilegioService(IPrivilegioAssembler assembler, IPrivilegioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IUsuarioService : IService<UsuarioDto, Usuario>
	{
	
	}
	
	public partial class UsuarioService : Service<UsuarioDto, Usuario>, IUsuarioService
	{
	    public UsuarioService(IUsuarioAssembler assembler, IUsuarioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IComandoService : IService<ComandoDto, Comando>
	{
	
	}
	
	public partial class ComandoService : Service<ComandoDto, Comando>, IComandoService
	{
	    public ComandoService(IComandoAssembler assembler, IComandoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IFuncionService : IService<FuncionDto, Funcion>
	{
	
	}
	
	public partial class FuncionService : Service<FuncionDto, Funcion>, IFuncionService
	{
	    public FuncionService(IFuncionAssembler assembler, IFuncionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IAutorizacionService : IService<AutorizacionDto, Autorizacion>
	{
	
	}
	
	public partial class AutorizacionService : Service<AutorizacionDto, Autorizacion>, IAutorizacionService
	{
	    public AutorizacionService(IAutorizacionAssembler assembler, IAutorizacionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IModuloUsuarioService : IService<ModuloUsuarioDto, ModuloUsuario>
	{
	
	}
	
	public partial class ModuloUsuarioService : Service<ModuloUsuarioDto, ModuloUsuario>, IModuloUsuarioService
	{
	    public ModuloUsuarioService(IModuloUsuarioAssembler assembler, IModuloUsuarioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ISeguimientoService : IService<SeguimientoDto, Seguimiento>
	{
	
	}
	
	public partial class SeguimientoService : Service<SeguimientoDto, Seguimiento>, ISeguimientoService
	{
	    public SeguimientoService(ISeguimientoAssembler assembler, ISeguimientoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRubrosParametroService : IService<RubrosParametroDto, RubrosParametro>
	{
	
	}
	
	public partial class RubrosParametroService : Service<RubrosParametroDto, RubrosParametro>, IRubrosParametroService
	{
	    public RubrosParametroService(IRubrosParametroAssembler assembler, IRubrosParametroRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IConceptosRubroService : IService<ConceptosRubroDto, ConceptosRubro>
	{
	
	}
	
	public partial class ConceptosRubroService : Service<ConceptosRubroDto, ConceptosRubro>, IConceptosRubroService
	{
	    public ConceptosRubroService(IConceptosRubroAssembler assembler, IConceptosRubroRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IConceptosDocumentoService : IService<ConceptosDocumentoDto, ConceptosDocumento>
	{
	
	}
	
	public partial class ConceptosDocumentoService : Service<ConceptosDocumentoDto, ConceptosDocumento>, IConceptosDocumentoService
	{
	    public ConceptosDocumentoService(IConceptosDocumentoAssembler assembler, IConceptosDocumentoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IAguaServicioService : IService<AguaServicioDto, AguaServicio>
	{
	
	}
	
	public partial class AguaServicioService : Service<AguaServicioDto, AguaServicio>, IAguaServicioService
	{
	    public AguaServicioService(IAguaServicioAssembler assembler, IAguaServicioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPlanillaSustitucionService : IService<PlanillaSustitucionDto, PlanillaSustitucion>
	{
	
	}
	
	public partial class PlanillaSustitucionService : Service<PlanillaSustitucionDto, PlanillaSustitucion>, IPlanillaSustitucionService
	{
	    public PlanillaSustitucionService(IPlanillaSustitucionAssembler assembler, IPlanillaSustitucionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPlanillaRubroService : IService<PlanillaRubroDto, PlanillaRubro>
	{
	
	}
	
	public partial class PlanillaRubroService : Service<PlanillaRubroDto, PlanillaRubro>, IPlanillaRubroService
	{
	    public PlanillaRubroService(IPlanillaRubroAssembler assembler, IPlanillaRubroRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPlanillaAtributoService : IService<PlanillaAtributoDto, PlanillaAtributo>
	{
	
	}
	
	public partial class PlanillaAtributoService : Service<PlanillaAtributoDto, PlanillaAtributo>, IPlanillaAtributoService
	{
	    public PlanillaAtributoService(IPlanillaAtributoAssembler assembler, IPlanillaAtributoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPlanillaService : IService<PlanillaDto, Planilla>
	{
	
	}
	
	public partial class PlanillaService : Service<PlanillaDto, Planilla>, IPlanillaService
	{
	    public PlanillaService(IPlanillaAssembler assembler, IPlanillaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IConvenioTransaccionService : IService<ConvenioTransaccionDto, ConvenioTransaccion>
	{
	
	}
	
	public partial class ConvenioTransaccionService : Service<ConvenioTransaccionDto, ConvenioTransaccion>, IConvenioTransaccionService
	{
	    public ConvenioTransaccionService(IConvenioTransaccionAssembler assembler, IConvenioTransaccionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IConvenioService : IService<ConvenioDto, Convenio>
	{
	
	}
	
	public partial class ConvenioService : Service<ConvenioDto, Convenio>, IConvenioService
	{
	    public ConvenioService(IConvenioAssembler assembler, IConvenioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICobroTransaccionService : IService<CobroTransaccionDto, CobroTransaccion>
	{
	
	}
	
	public partial class CobroTransaccionService : Service<CobroTransaccionDto, CobroTransaccion>, ICobroTransaccionService
	{
	    public CobroTransaccionService(ICobroTransaccionAssembler assembler, ICobroTransaccionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICobroService : IService<CobroDto, Cobro>
	{
	
	}
	
	public partial class CobroService : Service<CobroDto, Cobro>, ICobroService
	{
	    public CobroService(ICobroAssembler assembler, ICobroRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICobrosElementoService : IService<CobrosElementoDto, CobrosElemento>
	{
	
	}
	
	public partial class CobrosElementoService : Service<CobrosElementoDto, CobrosElemento>, ICobrosElementoService
	{
	    public CobrosElementoService(ICobrosElementoAssembler assembler, ICobrosElementoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICobrosRubroService : IService<CobrosRubroDto, CobrosRubro>
	{
	
	}
	
	public partial class CobrosRubroService : Service<CobrosRubroDto, CobrosRubro>, ICobrosRubroService
	{
	    public CobrosRubroService(ICobrosRubroAssembler assembler, ICobrosRubroRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRepRecaudacionesFechaService : IService<RepRecaudacionesFechaDto, RepRecaudacionesFecha>
	{
	
	}
	
	public partial class RepRecaudacionesFechaService : Service<RepRecaudacionesFechaDto, RepRecaudacionesFecha>, IRepRecaudacionesFechaService
	{
	    public RepRecaudacionesFechaService(IRepRecaudacionesFechaAssembler assembler, IRepRecaudacionesFechaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRepEmisionesSaldoService : IService<RepEmisionesSaldoDto, RepEmisionesSaldo>
	{
	
	}
	
	public partial class RepEmisionesSaldoService : Service<RepEmisionesSaldoDto, RepEmisionesSaldo>, IRepEmisionesSaldoService
	{
	    public RepEmisionesSaldoService(IRepEmisionesSaldoAssembler assembler, IRepEmisionesSaldoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IConceptosEmisionService : IService<ConceptosEmisionDto, ConceptosEmision>
	{
	
	}
	
	public partial class ConceptosEmisionService : Service<ConceptosEmisionDto, ConceptosEmision>, IConceptosEmisionService
	{
	    public ConceptosEmisionService(IConceptosEmisionAssembler assembler, IConceptosEmisionRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IModuloService : IService<ModuloDto, Modulo>
	{
	
	}
	
	public partial class ModuloService : Service<ModuloDto, Modulo>, IModuloService
	{
	    public ModuloService(IModuloAssembler assembler, IModuloRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IReporteService : IService<ReporteDto, Reporte>
	{
	
	}
	
	public partial class ReporteService : Service<ReporteDto, Reporte>, IReporteService
	{
	    public ReporteService(IReporteAssembler assembler, IReporteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRepCuentaCorrienteResumenService : IService<RepCuentaCorrienteResumenDto, RepCuentaCorrienteResumen>
	{
	
	}
	
	public partial class RepCuentaCorrienteResumenService : Service<RepCuentaCorrienteResumenDto, RepCuentaCorrienteResumen>, IRepCuentaCorrienteResumenService
	{
	    public RepCuentaCorrienteResumenService(IRepCuentaCorrienteResumenAssembler assembler, IRepCuentaCorrienteResumenRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRepCuentaCorrienteDetalleService : IService<RepCuentaCorrienteDetalleDto, RepCuentaCorrienteDetalle>
	{
	
	}
	
	public partial class RepCuentaCorrienteDetalleService : Service<RepCuentaCorrienteDetalleDto, RepCuentaCorrienteDetalle>, IRepCuentaCorrienteDetalleService
	{
	    public RepCuentaCorrienteDetalleService(IRepCuentaCorrienteDetalleAssembler assembler, IRepCuentaCorrienteDetalleRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICajaService : IService<CajaDto, Caja>
	{
	
	}
	
	public partial class CajaService : Service<CajaDto, Caja>, ICajaService
	{
	    public CajaService(ICajaAssembler assembler, ICajaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICajasUsuarioService : IService<CajasUsuarioDto, CajasUsuario>
	{
	
	}
	
	public partial class CajasUsuarioService : Service<CajasUsuarioDto, CajasUsuario>, ICajasUsuarioService
	{
	    public CajasUsuarioService(ICajasUsuarioAssembler assembler, ICajasUsuarioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRepPredioService : IService<RepPredioDto, RepPredio>
	{
	
	}
	
	public partial class RepPredioService : Service<RepPredioDto, RepPredio>, IRepPredioService
	{
	    public RepPredioService(IRepPredioAssembler assembler, IRepPredioRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICuentaBancariaService : IService<CuentaBancariaDto, CuentaBancaria>
	{
	
	}
	
	public partial class CuentaBancariaService : Service<CuentaBancariaDto, CuentaBancaria>, ICuentaBancariaService
	{
	    public CuentaBancariaService(ICuentaBancariaAssembler assembler, ICuentaBancariaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICajaElementoService : IService<CajaElementoDto, CajaElemento>
	{
	
	}
	
	public partial class CajaElementoService : Service<CajaElementoDto, CajaElemento>, ICajaElementoService
	{
	    public CajaElementoService(ICajaElementoAssembler assembler, ICajaElementoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICajaComprobanteService : IService<CajaComprobanteDto, CajaComprobante>
	{
	
	}
	
	public partial class CajaComprobanteService : Service<CajaComprobanteDto, CajaComprobante>, ICajaComprobanteService
	{
	    public CajaComprobanteService(ICajaComprobanteAssembler assembler, ICajaComprobanteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICarpetaCatastralAnualService : IService<CarpetaCatastralAnualDto, CarpetaCatastralAnual>
	{
	
	}
	
	public partial class CarpetaCatastralAnualService : Service<CarpetaCatastralAnualDto, CarpetaCatastralAnual>, ICarpetaCatastralAnualService
	{
	    public CarpetaCatastralAnualService(ICarpetaCatastralAnualAssembler assembler, ICarpetaCatastralAnualRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ISoporteService : IService<SoporteDto, Soporte>
	{
	
	}
	
	public partial class SoporteService : Service<SoporteDto, Soporte>, ISoporteService
	{
	    public SoporteService(ISoporteAssembler assembler, ISoporteRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ISoporteMovimientoService : IService<SoporteMovimientoDto, SoporteMovimiento>
	{
	
	}
	
	public partial class SoporteMovimientoService : Service<SoporteMovimientoDto, SoporteMovimiento>, ISoporteMovimientoService
	{
	    public SoporteMovimientoService(ISoporteMovimientoAssembler assembler, ISoporteMovimientoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IMejoraElementoService : IService<MejoraElementoDto, MejoraElemento>
	{
	
	}
	
	public partial class MejoraElementoService : Service<MejoraElementoDto, MejoraElemento>, IMejoraElementoService
	{
	    public MejoraElementoService(IMejoraElementoAssembler assembler, IMejoraElementoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IMejoraService : IService<MejoraDto, Mejora>
	{
	
	}
	
	public partial class MejoraService : Service<MejoraDto, Mejora>, IMejoraService
	{
	    public MejoraService(IMejoraAssembler assembler, IMejoraRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRepRecaudacionesCompDetalleService : IService<RepRecaudacionesCompDetalleDto, RepRecaudacionesCompDetalle>
	{
	
	}
	
	public partial class RepRecaudacionesCompDetalleService : Service<RepRecaudacionesCompDetalleDto, RepRecaudacionesCompDetalle>, IRepRecaudacionesCompDetalleService
	{
	    public RepRecaudacionesCompDetalleService(IRepRecaudacionesCompDetalleAssembler assembler, IRepRecaudacionesCompDetalleRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICobrosNotasElementoService : IService<CobrosNotasElementoDto, CobrosNotasElemento>
	{
	
	}
	
	public partial class CobrosNotasElementoService : Service<CobrosNotasElementoDto, CobrosNotasElemento>, ICobrosNotasElementoService
	{
	    public CobrosNotasElementoService(ICobrosNotasElementoAssembler assembler, ICobrosNotasElementoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface ICobrosNotaService : IService<CobrosNotaDto, CobrosNota>
	{
	
	}
	
	public partial class CobrosNotaService : Service<CobrosNotaDto, CobrosNota>, ICobrosNotaService
	{
	    public CobrosNotaService(ICobrosNotaAssembler assembler, ICobrosNotaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IRepReversionesFechaService : IService<RepReversionesFechaDto, RepReversionesFecha>
	{
	
	}
	
	public partial class RepReversionesFechaService : Service<RepReversionesFechaDto, RepReversionesFecha>, IRepReversionesFechaService
	{
	    public RepReversionesFechaService(IRepReversionesFechaAssembler assembler, IRepReversionesFechaRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IConveniosDividendoService : IService<ConveniosDividendoDto, ConveniosDividendo>
	{
	
	}
	
	public partial class ConveniosDividendoService : Service<ConveniosDividendoDto, ConveniosDividendo>, IConveniosDividendoService
	{
	    public ConveniosDividendoService(IConveniosDividendoAssembler assembler, IConveniosDividendoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IPlanillaMesService : IService<PlanillaMesDto, PlanillaMes>
	{
	
	}
	
	public partial class PlanillaMesService : Service<PlanillaMesDto, PlanillaMes>, IPlanillaMesService
	{
	    public PlanillaMesService(IPlanillaMesAssembler assembler, IPlanillaMesRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
	
	public partial interface IMercadoService : IService<MercadoDto, Mercado>
	{
	
	}
	
	public partial class MercadoService : Service<MercadoDto, Mercado>, IMercadoService
	{
	    public MercadoService(IMercadoAssembler assembler, IMercadoRepository repository)
	        : base(assembler, repository)
	    {
	
	    }
	}
}
#pragma warning restore 1591
