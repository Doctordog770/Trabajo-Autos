using System;
using System.Collections.Generic;
using System.Text;

namespace Autos_tp.Clases
{
    public class Vehiculo
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }

        public string? Patente { get; set; }

        public int anio_de_frabricacion { get; set; }

        public string? tipo_de_combustible { get; set; }

        public float preciobasepoliza { get; set; }

        public Vehiculo(string? Marca, string? Modelo, string? Patente, int anio_de_frabricacion, string? tipo_de_combustible, float preciobasepoliza)
        {
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.Patente = Patente;
            this.anio_de_frabricacion = anio_de_frabricacion;
            this.tipo_de_combustible = tipo_de_combustible;
            this.preciobasepoliza = preciobasepoliza;
        }

        public Vehiculo(string? Marca, string? Modelo, string? Patente, int anio_de_frabricacion, string? tipo_de_combustible)
        {
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.Patente = Patente;
            this.anio_de_frabricacion = anio_de_frabricacion;
            this.tipo_de_combustible = tipo_de_combustible;
            this.preciobasepoliza = 1500.0f;
        }

        public virtual float CalcularCostoPoliza()
        {
            return preciobasepoliza * 1.03f;
        }

        public virtual void MostrarInfo()
        {
            Console.WriteLine($"Marca: {Marca}");
            Console.WriteLine($"Modelo: {Modelo}");
            Console.WriteLine($"Patente: {Patente}");
            Console.WriteLine($"Año De Frabricacion: {anio_de_frabricacion}");
            Console.WriteLine($"Tipo de combustible: {tipo_de_combustible}");
            Console.WriteLine($"Precio Base Poliza: {preciobasepoliza}");
        }
    }
}
