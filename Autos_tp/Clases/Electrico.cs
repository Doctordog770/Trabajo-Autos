using System;
using System.Collections.Generic;
using System.Text;

namespace Autos_tp.Clases
{
    public class Electrico : Vehiculo
    {
        public float CapacidadKWh { get; set; }
        public float PorcentajeCarga { get; set; }

        public bool EsAptoParaEmbarque { get; set; } = false;

        public Electrico(float CapacidadKWh, float PorcentajeCarga, string? Marca, string? Modelo, string? Patente, int anio_de_frabricacion, string? tipo_de_combustible,float preciobasepoliza) : base(Marca,Modelo,Patente,anio_de_frabricacion,tipo_de_combustible,preciobasepoliza)
        {
            this.CapacidadKWh = CapacidadKWh;
            this.PorcentajeCarga = PorcentajeCarga;
            this.tipo_de_combustible = "Electrico";
        }

        public bool InspeccionarRiesgoBateria()
        {
            if (PorcentajeCarga < 30) return EsAptoParaEmbarque = true;
            return EsAptoParaEmbarque = false;
        }

        public void NuevaCargaBateria(float Value)
        {
            PorcentajeCarga = Value;
            InspeccionarRiesgoBateria();
        }

        public string InformarEstadoInspeccion()
        {
            if (EsAptoParaEmbarque) return "APROBADO PARA EMBARQUE";
            return "RECHAZADO POR RIESGO TERMICO";
        }

        public override float CalcularCostoPoliza()
        {
            return preciobasepoliza * 0.70f;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();
            Console.WriteLine($"CapacidadKWh: {CapacidadKWh}");
            Console.WriteLine($"PorcentajeCarga: {PorcentajeCarga}");
            Console.WriteLine($"EsAptoParaEmbarque: {EsAptoParaEmbarque}");
        }
    }
    
}
