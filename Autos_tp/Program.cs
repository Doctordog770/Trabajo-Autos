using Autos_tp.Clases;
using System.Runtime.Intrinsics.X86;

static void Menu()
{
    int opcion = 0;

    int Plazas = 0;

    List<Vehiculo> listaAprobados = new List<Vehiculo>();
    List<Vehiculo> listaRechazados = new List<Vehiculo>();
    List<Vehiculo> listaTotal = new List<Vehiculo>();

    do
    {
        opcion = show_Menu_Ui(Plazas);

        switch (opcion)
        {
            case 1:
                {
                    List<object> Resultado = CargarVehiculo(Plazas, listaAprobados, listaRechazados, listaTotal);
                    Plazas = (int)Resultado[0];
                    listaAprobados = (List<Vehiculo>)Resultado[1];
                    listaRechazados = (List<Vehiculo>)Resultado[2];
                    listaTotal = (List<Vehiculo>)Resultado[3];
                    break;
                }
            case 2:
                {
                    listaAprobados = Modificar_aprobados(listaAprobados);
                    break;
                }
            case 3:
                {
                    Console.Clear();
                    int num = 0;
                    if (listaAprobados.Count == 0) { Console.WriteLine("lista vacia :("); Console.ReadKey(); break; }
                    Console.WriteLine("===============================");
                    Console.WriteLine("Lista de aprobados");
                    foreach (var item in listaAprobados)
                    {
                        Console.WriteLine($"Auto #{num = num + 1} ");
                        item.MostrarInfo();
                    }
                    Console.WriteLine("===============================");
                    Console.ReadKey();
                    break;
                }
            case 4:
                {
                    Console.Clear();
                    int num = 0;
                    if (listaRechazados.Count == 0) { Console.WriteLine("lista vacia :("); Console.ReadKey(); break; }
                    Console.WriteLine("===============================");
                    Console.WriteLine("Lista de Rechazados");
                    foreach (var item in listaRechazados)
                    {
                        Console.WriteLine($"Auto #{num = num + 1} ");
                        item.MostrarInfo();
                    }
                    Console.WriteLine("===============================");
                    Console.ReadKey();
                    break;
                }
            case 5:
                {
                    break;
                }
            default:
                {
                    Console.Clear();
                    Console.WriteLine("opcion no valida :/");
                    Console.ReadKey();
                    break;
                } 
        }
    }
    while (opcion < 5);
}

static int show_Menu_Ui(int Plazas)
{
    Console.Clear();
    string Capacidad_Texto = ControlCapacidad.ObtenerReporteCupo(Plazas);

    Console.WriteLine("=================================================");
    Console.WriteLine("SISTEMA DE CONTROL DE EMBARQUE - PUERTO SUR");
    Console.WriteLine("=================================================");
    Console.WriteLine(Capacidad_Texto);
    Console.WriteLine("=================================================");
    Console.WriteLine("1. Cargar Vehiculo (Ingreso E inspeccion): ");
    Console.WriteLine("2. Modificar Lista De Aprobados (Eliminar Por Patente): ");
    Console.WriteLine("3. Ver Lista de Vehiculos APROBADOS: ");
    Console.WriteLine("4. Ver Lista de Vehiculos RECHAZADOS: ");
    Console.WriteLine("5. Salir: ");
    Console.WriteLine("=================================================");
    Console.Write("Ingrese una opcion: ");
    string? opcion = Console.ReadLine();
    if (int.TryParse(opcion, out int result)) { return result; } else { return 0;  }
}


static List<object> CargarVehiculo(int Plazas,List<Vehiculo> listaAprobados, List<Vehiculo> listaRechazados, List<Vehiculo> listaTotal)
{
    bool cupo = ControlCapacidad.HayCupoDisponible(Plazas);
    List<object> resultado = [];

    if (cupo)
    {
        while (true)
        {
            Console.Clear();
            float Capacidad_de_bateria = 0f;
            float PorcentajeCarga = 0f;
            string? Marca = "";
            string? Modelo = "";
            string? Patente = "";
            int anio_de_fabricacion = 67;
            string? tipo_de_combustible = "";
            float Precio_base_de_poliza = 0f;
            bool electrico = false;

            Console.Write($"¿El vehiculo es electrico? (S/N):");
            string? Res = Console.ReadLine();
            if (Res != "S" && Res != "N") { continue; }

            if (Res == "S")
            {
                electrico = true;
                Console.Write($"Capacidad de bateria (kWh): ");
                Res = Console.ReadLine();
                if (!float.TryParse(Res, out Capacidad_de_bateria)) 
                {
                    Console.Clear();
                    Console.WriteLine($"Dato no valido ");
                    Console.ReadKey();
                    continue;
                }

                Console.Write($"Porcentaje de carga actual (%): ");
                Res = Console.ReadLine();
                if(!float.TryParse(Res, out PorcentajeCarga))
                {
                    Console.Clear();
                    Console.Write($"Dato no valido ");
                    Console.ReadKey();
                    continue;
                }
            }

            Console.Write($"Ingrese la Marca: ");
            Marca = Console.ReadLine();

            Console.Write($"ingrese el modelo: ");
            Modelo = Console.ReadLine();

            Console.Write($"ingrese la patente: ");
            Patente = Console.ReadLine();
            
            foreach(var item in listaTotal)
            {
                if(Patente == item.Patente)
                {
                    Console.Clear();
                    Console.WriteLine($"esta patente ya existe ");
                    Console.ReadKey();
                    continue;
                }
            }

            Console.Write("ingrese el año del auto: ");
            Res = Console.ReadLine();

            if(!int.TryParse(Res, out anio_de_fabricacion))
            {
                Console.Clear();
                Console.WriteLine($"dato no valido ");
                Console.ReadKey();
                continue;
            }

            if (!electrico)
            {
                Console.Write($"ingrese el tipo de combustible: ");
                tipo_de_combustible = Console.ReadLine();
            }

            Console.Write($"ingrese el precio base de la poliza: ");
            Res = Console.ReadLine();
            if (!float.TryParse(Res, out Precio_base_de_poliza))
            {
                Console.Clear();
                Console.WriteLine($"dato no valido ");
                Console.ReadKey();
                continue;
            }



            Plazas += 1;

            if (electrico)
            {
                Electrico auto = new Electrico(Capacidad_de_bateria, PorcentajeCarga,Marca,Modelo,Patente,anio_de_fabricacion,tipo_de_combustible,Precio_base_de_poliza);
                bool estado = auto.InspeccionarRiesgoBateria();
                if (!estado)
                {
                    Console.WriteLine(">> DICTAMEN: RECHAZADO POR RIESGO TERMICO");
                    Console.WriteLine(">> El vehiculo queda retenido en bodega.");
                    Console.ReadKey();
                    listaTotal.Add(auto);
                    listaRechazados.Add(auto);
                    resultado = [Plazas,listaAprobados,listaRechazados,listaTotal];
                    return resultado;
                }
                Console.WriteLine(">> Auto Electrico correcto.");
                Console.ReadKey();
                listaTotal.Add(auto);
                listaAprobados.Add(auto);
                resultado = [Plazas, listaAprobados, listaRechazados, listaTotal];
                return resultado;
            }

            if(Precio_base_de_poliza <0 || Precio_base_de_poliza == 0)
            {
                Vehiculo auto3 = new Vehiculo(Marca,Modelo,Patente,anio_de_fabricacion,tipo_de_combustible);
                Console.WriteLine(">> Auto agregado con exito :3");
                Console.ReadKey();
                listaTotal.Add(auto3);
                listaAprobados.Add(auto3);
                resultado = [Plazas, listaAprobados, listaRechazados, listaTotal];
                return resultado;
            }

            Vehiculo auto2 = new Vehiculo(Marca,Modelo,Patente, anio_de_fabricacion, tipo_de_combustible, Precio_base_de_poliza);
            auto2.preciobasepoliza = auto2.CalcularCostoPoliza();
            Console.WriteLine(">> Auto agregado con exito :3");
            Console.ReadKey();
            listaTotal.Add(auto2);
            listaAprobados.Add(auto2);
            resultado = [Plazas, listaAprobados, listaRechazados, listaTotal];
            return resultado;
        }
    }

    Console.WriteLine("el cupo esta lleno :(");
    resultado = [Plazas, listaAprobados, listaRechazados, listaTotal];
    return resultado;
}

static List<Vehiculo> Modificar_aprobados(List<Vehiculo> listaAprobados)
{
    Console.Clear();
    if(listaAprobados.Count == 0)
    {
        Console.WriteLine("lista vacia :/");
        Console.ReadKey();
        return listaAprobados;
    }
    Console.Write("ingrese la patente para eliminar el Vehiculo: ");
    string? patente = Console.ReadLine();
    foreach (var item in listaAprobados)
    {
        if(patente == item.Patente)
        {
            listaAprobados.Remove(item);
            Console.WriteLine(">> Vehiculo eliminado ");
            Console.ReadKey();
            return listaAprobados;
        }
    }
    Console.WriteLine(">>Patente no encontrada ");
    Console.ReadKey();
    return listaAprobados;
}

Menu();