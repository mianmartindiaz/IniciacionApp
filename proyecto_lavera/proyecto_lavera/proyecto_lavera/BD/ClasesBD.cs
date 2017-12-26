using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace proyecto_lavera.BD
{ 

    public class BuscarImagen
    {

        public int Id { get; set; }
        public string Url { get; set; }
        public string Nombre { get; set; }
        public string Slug_Poblacion { get; set; }
        public String Servicio { get; set; }

    }

    public class PoblacionDetail
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Habitantes { get; set; }
        public string Extension { get; set; }
        public string Coordenadas { get; set; }
        public int Cp { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Slug { get; set; }
        public DateTime Modificado { get; set; }
        public DateTime Creado { get; set; }
        public object Eliminado { get; set; }
    }

    public class ListServicioPoblacion
    {
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string Direccion { get; set; }
        public List<Poblaciones> Poblacion { get; set; }
        public string Tipo_Servicio { get; set; }
    }

    public class ClaseLaVera
    {
        public string Titulo { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Imagen { get; set; }
        public string Imagen_Movil { get; set; }
    }

    public class RestauranteDetail
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_Comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public string Telefono_uno { get; set; }
        public string Telefono_dos { get; set; }
        public string Categoria { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public string Tipo_comida { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClaseActividadOcio
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_larga { get; set; }
        public string Puntuacion_comentarios { get; set; }
        public string Puntuacion_publicidad { get; set; }
        public string Puntuacion_total { get; set; }
        public bool Permitir_publicidad { get; set; }
        public string Telefono_uno { get; set; }
        public string Telefono_dos { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public string Tipo_Actividad { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClaseAlojamiento
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_Comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public string Telefono_uno { get; set; }
        public string Telefono_dos { get; set; }
        public string Categoria { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public string Tipo_Alojamiento { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClaseEmpresaTuristica
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_Comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public string Telefono_uno { get; set; }
        public string Telefono_dos { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public string Tipo_Empresa { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClasePatrimonioCultura
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_Comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public string Telefono_Uno { get; set; }
        public string Telefono_Dos { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public Object Precio { get; set; }
        public bool Visita { get; set; }
        public string Horario { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }


    public class ClaseRuta
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_Comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public string Duracion { get; set; }
        public string Distancia { get; set; }
        public string Mapa { get; set; }
        public string Dificultad { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClaseTransporte
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_larga { get; set; }
        public string Puntuacion_comentarios { get; set; }
        public string Puntuacion_publicidad { get; set; }
        public string Puntuacion_total { get; set; }
        public bool Permitir_publicidad { get; set; }
        public string Telefono_Uno { get; set; }
        public string Telefono_Dos { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClaseZonaBano
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_Comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public bool Accesible_Coche { get; set; }
        public bool Parking { get; set; }
        public bool Zona_Juegos { get; set; }
        public bool Zona_Merendero { get; set; }
        public bool Entrada_Animales { get; set; }
        public bool Restaurantes { get; set; }
        public bool Zona_Acampada { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClaseFiestaEvento
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public string Fecha { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }


    public class ClasePuntoInteres
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public List<Poblaciones> Poblaciones { get; set; }
        public string Direccion { get; set; }
        public string Coordenadas { get; set; }
        public string Descripcion_Corta { get; set; }
        public string Descripcion_Larga { get; set; }
        public string Puntuacion_Comentarios { get; set; }
        public string Puntuacion_Publicidad { get; set; }
        public string Puntuacion_Total { get; set; }
        public bool Permitir_Publicidad { get; set; }
        public string Tipo_Punto { get; set; }
        public string Horario { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class UsuarioDetail
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public int Cp { get; set; }
        public string Telefono_Uno { get; set; }
        public string Telefono_Dos { get; set; }
        public string Email { get; set; }
        public String Fecha_Nacimiento { get; set; }
        public string Sexo { get; set; }
    }

    public class ComentarioListPoblacion
    {
        public string Titulo { get; set; }
        public string Comentario { get; set; }
        public String Puntuacion { get; set; }
        public string User { get; set; }
        public string Fecha { get; set; }
    }

    public class ComentarioListServicio
    {
        public string Titulo { get; set; }
        public string Comentario { get; set; }
        public String Puntuacion { get; set; }
        public string User { get; set; }
        public string Fecha { get; set; }
    }

    public class ListaDeseosList
    {
        public string Lugar_Actividad { get; set; }
        public string Slug { get; set; }
        public List<Poblaciones> Poblacion { get; set; }
        public string Tipo_Lugar { get; set; }
        public List<Imagenes> Imagenes { get; set; }
    }

    public class ClaseDeseos
    {
        public string Lugar_Actividad { get; set; }
        public string Slug { get; set; }
        public string Poblacion { get; set; }
        public string Tipo_Lugar { get; set; }
        public string Url { get; set; }
    }

    public class PlanningList
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public int Usuario { get; set; }
    }

    public class ClasePlanningDetail
    {
        public string Fecha { get; set; }
        public string Lugar { get; set; }
        public string Slug { get; set; }
        public string Tipo_Servicio { get; set; }
        public string Hora_Comienzo { get; set; }
        public string Hora_Finalizacion { get; set; }
    }

    public class Poblaciones
    {
        public string Nombre { get; set; }
    }

    public class Imagenes
    {
        public string Tipo_foto { get; set; }
        public string Url { get; set; }
    }

    public class ClaseUrl
    {
        public string Url { get; set; }
    }
    public class ClasesBD{}
}
