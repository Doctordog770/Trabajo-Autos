using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Autos_tp.Clases
{
    static public class ControlCapacidad
    {
        public const int CapacidadMaxima = 15;

        static public bool HayCupoDisponible(int CantidadActual)
        {
            if (CantidadActual < CapacidadMaxima) return true;
            return false;
        }

        static public string ObtenerReporteCupo(int CantidadActual)
        {
            int libre = CapacidadMaxima - CantidadActual;
            string texto = $"Ocupacion Atual: {CantidadActual}/15 | Plazas Libres: {libre}";
            return texto;
        }
    }
}
