using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_Eco
{
    public class CombosModel : RespuestaGeneral
    {
        //public List<TipoDocumento> ListaTipoDocumento { get; set; }
        //public List<Extension> ListaExtension { get; set; }
        //public List<Genero> ListaGenero { get; set; }
        //public List<CiudadResidencia> ListaCiudadResidencia { get; set; }
        //public List<NivelEducacion> ListaNivelEducacion { get; set; }
        //public List<Profesion> ListaProfesion { get; set; }
        //public List<TipoDireccion> ListaTipoDireccion { get; set; }
        //public List<TipoVivienda> ListaTipoVivienda { get; set; }
        //public List<Relacion> ListaRelacion { get; set; }
        //public List<PaisResidencia> ListaPaisResidencia { get; set; }
    }

    public class ComboCredito : RespuestaGeneral
    {
        public List<TipoOperacionCB> ListaTipoOperacion { get; set; }
        public List<EstrategiaCB> ListaEstrategia { get; set; }
        public List<DestinoCB> ListaDestino { get; set; }
        public List<ObjetoOperacionCB> ListaObjetoOperacion { get; set; }
        public List<ClienteCPOPCB> ListaClienteCPOP { get; set; }
        public List<AntiguedadClienteCB> ListaAntiguedadCliente { get; set; }
        public List<MonedaCB> ListaMoneda { get; set; }
        public List<TipoTasaNegocioCB> ListaTipoTasaNegocio { get; set; }

    }
    public class ComboDocumentoPersonal : RespuestaGeneral
    {
        public List<TipoDocumentoCB> ListaTipoDocumento { get; set; }
        public List<ExtensionCB> ListaExtension { get; set; }
        public List<EstadoCivilCB> ListaEstadoCivil { get; set; }
        public List<NacionalidadCB> ListaNacionalidad { get; set; }
        public List<PaisCB> ListaPais { get; set; }
    }

    public class ComboPersona : RespuestaGeneral
    {
        public List<GeneroCB> ListaGenero { get; set; }
        public List<NivelEducacionCB> ListaNivelEducacion { get; set; }
        public List<ProfesionCB> ListaProfesion { get; set; }
        public List<EstadoCivilCB> ListaEstadoCivil { get; set; }
    }
    public class ComboDireccion : RespuestaGeneral
    {
        public List<PaisResidenciaCB> ListaPaisResidencia { get; set; }
        public List<CiudadResidenciaCB> ListaCiudadResidencia { get; set; }
        public List<DepartamentoCB> ListaDepartamento { get; set; }
        public List<LocalidadCB> ListaLocalidad { get; set; }
        public List<TipoViviendaCB> ListaTipoVivienda { get; set; }
        public List<TipoDireccionCB> ListaTipoDireccion { get; set; }
    }

    public class ComboActividadEconomica : RespuestaGeneral
    {
        public List<NivelLaboralCB> ListaNivelLaboral { get; set; }
        public List<IngresosMensualesCB> ListaIngresosMensuales { get; set; }
        public List<EstadoActividadCB> ListaEstadoActividad { get; set; }
        public List<SectorEconomicoCB> ListaSectorEconomico { get; set; }

    }

    public class ComboReferencia : RespuestaGeneral
    {
        public List<RelacionCB> ListaRelacion { get; set; }
        public List<CalificacionCB> ListaCalificacion { get; set; }
    }

    public class ComboTelefono : RespuestaGeneral
    {
        public List<TipoTelefonoCB> ListaTipoTelefono { get; set; }
    }
    public class ComboCaedec : RespuestaGeneral
    {
        public List<CaedecCB> ListaCaedec { get; set; }
    }

    public class ComboAutorizacionesEspeciales : RespuestaGeneral
    {
        public List<AutorizacionesEspecialesCB> ListaAutorizacionesEspeciales { get; set; }
    }

    public class ComboTipoFechaAM : RespuestaGeneral
    {
        public List<TipoFechaAMCB> ListaTipoFechaAM { get; set; }
    }

    public class ComboDocumentosEntregados : RespuestaGeneral
    {
        public List<DocumentosEntregadosCB> ListaDocumentosEntregados { get; set; }
    }

    public class ComboScoring : RespuestaGeneral
    {
        public List<CriterioCB> ListaCriterio { get; set; }
    }

    public class ComboEntrevista : RespuestaGeneral
    {
        public List<VerificacionClienteCB> ListaVerificacionCliente { get; set; }
        public List<ScriptEvaluacionCB> ListaScriptEvaluacion { get; set; }
        public List<InstitucionCB> ListaInstitucion { get; set; }
        public List<SeguroDesgravameCB> ListaSeguroDesgravame { get; set; }
    }


    public class AntiguedadClienteCB
    {
        public int ID_ANTIGUEDAD_CLIENTE { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class AutorizacionesEspecialesCB
    {
        public int ID_AUTORIZACIONES_ESPECIALES { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class CaedecCB
    {
        public int ID_ACTIVIDAD_CAEDEC { get; set; }
        public string COD_CAEDEC { get; set; }
        public string ACTIVIDAD_CAEDEC { get; set; }
        public string COD_GRUPO_CAEDEC { get; set; }
        public string GRUPO_CAEDEC { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class CalificacionCB
    {
        public int ID_CALIFICACION { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class CiudadResidenciaCB
    {
        public int ID_CIUDAD_RESIDENCIA { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class DepartamentoCB
    {
        public int ID_DEPARTAMENTO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class DestinoCB
    {
        public int ID_DESTINO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class DocumentosEntregadosCB
    {
        public int ID_DOCUMENTOS_ENTREGADOS { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class EstadoCivilCB
    {
        public int ID_ESTADO_CIVIL { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class EstrategiaCB
    {
        public int ID_ESTRATEGIA { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class ExtensionCB
    {
        public int ID_EXTENSION { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class GeneroCB
    {
        public int ID_GENERO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class IngresosMensualesCB
    {
        public int ID_INGRESOS_MENSUALES { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class LocalidadCB
    {
        public int ID_LOCALIDAD { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class MonedaCB
    {
        public int ID_MONEDA { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class NacionalidadCB
    {
        public int ID_NACIONALIDAD { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class NivelEducacionCB
    {
        public int ID_NIVEL_EDUCACION { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class ObjetoOperacionCB
    {
        public int ID_OBJETO_OPERACION { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class PaisCB
    {
        public int ID_PAIS { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class PaisResidenciaCB
    {
        public int ID_PAIS_RESIDENCIA { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class ParentescoCB
    {
        public int ID_PARENTESCO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class ProfesionCB
    {
        public int ID_PROFESION { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class RelacionCB
    {
        public int ID_RELACION { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class SeleccionCPOPCB
    {
        public int ID_SELECCION_CPOP { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class TipoDireccionCB
    {
        public int ID_TIPO_DIRECCION { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class TipoDocumentoCB
    {
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class TipoOperacionCB
    {
        public int ID_TIPO_OPERACION { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class TipoTasaNegocioCB
    {
        public int ID_TIPO_TASA_NEGOCIACION { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class TipoTelefonoCB
    {
        public int ID_TIPO_TELEFONO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class TipoViviendaCB
    {
        public int ID_TIPO_VIVIENDA { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class TipoFechaAMCB
    {
        public int ID_TIPO_FECHA_A_M { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class NivelLaboralCB
    {
        public int ID_NIVEL_LABORAL { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class EstadoActividadCB
    {
        public int ID_ESTADO_ACTIVIDAD { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class ClienteCPOPCB
    {
        public int ID_CLIENTE_CPOP { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class SectorEconomicoCB
    {
        public int ID_SECTOR_ECONOMICO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class CriterioCB
    {
        public int ID_CRITERIO { get; set; }
        public string AREA { get; set; }
        public string ID_AREA { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class InstitucionCB
    {
        public int ID_INSTITUCION { get; set; }
        public string CODIGO_INSTITUCION { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class ScriptEvaluacionCB
    {
        public int ID_SCRIPT_EVALUACION { get; set; }
        public string CODIGO_SCRIPT_EVALUACION { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class SeguroDesgravameCB
    {
        public int ID_SEGURO_DESGRAVAME { get; set; }
        public string CODIGO_SEGURO_DESGRAVAME { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class VerificacionClienteCB
    {
        public int ID_VERIFICACION_CLIENTE { get; set; }
        public string CODIGO_VERIFICACION_CLIENTE { get; set; }
        public string DESCRIPCION { get; set; }
    }

}
