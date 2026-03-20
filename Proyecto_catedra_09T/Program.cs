using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_catedra_09T
{
    internal class Program
    {
        // **Arreglos para almacenar los datos de los estudiantes**
        // *Ocupamos static porque los métodos estáticos no pueden acceder a miembros de instancia sin crear una instancia de la clase.*
        // *Dado que el método Main es estático, los arreglos también deben serlo para que puedan ser accedidos dentro de Main sin necesidad de crear una instancia de Program.*
        static string[] carnets = new string[50]; // Arreglo para almacenar los carnets de los estudiantes
        static string[] nombres = new string[50]; // Arreglo para almacenar los nombres de los estudiantes
        static int[] edades = new int[50]; // Arreglo para almacenar las edades de los estudiantes
        static int contador = 0; // Contador para llevar el registro de la cantidad de estudiantes registrados
        static int[] notaMatematicas = new int[50];
        static int[] notaCiencias = new int[50];
        static int[] notalenguaje = new int[50];
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            Console.Title = "PROYECTO DE CÁTEDRA - Fase 1";

            int opcion = 0; // Variable para capturar la elección del usuario

            // Ciclo do-while para que el programa se repita hasta que el usuario elija salir
            do
            {

                Console.Clear(); // Limpia la consola en cada iteración del menú
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\n\t\t\t ======================================");
                Console.WriteLine("\t\t\t ||   PROYECTO DE CÁTEDRA (FASE 1)   ||");
                Console.WriteLine("\t\t\t ======================================");
                Console.WriteLine("\n\t\t\t  1. Registrar Estudiante");
                Console.WriteLine("\n\t\t\t  2. Mostrar Lista de Estudiantes");
                Console.WriteLine("\n\t\t\t  5. Salir");
                Console.WriteLine("\t\t\t ------------------------------------");
                Console.Write("\n\t\t\t  Seleccione una opción: ");

                // Validación de entrada: si intenta ingresar algo que no es un número, se asigna 0 para activar el 'default' del switch
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0; // Si la entrada no es un número, se asigna 0 para que el switch maneje la opción como inválida
                }

                // Estructura switch para dirigir el flujo según la opción elegida 
                switch (opcion)
                {
                    case 1:
                        RegistrarEstudiante(); // Llama a la función de captura de datos
                        break;
                    case 2:
                        MostrarLista(); // Llama a la función de impresión de tabla
                        break;
                    case 5:
                        Console.WriteLine("\n\t\t\t  Saliendo del sistema...");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed; // Cambia el color del texto a rojo oscuro para resaltar el error
                        Console.WriteLine("\n\t\t\t  [!] Error: Debe ingresar un número entero. Presione Enter para continuar.");
                        Console.ReadLine(); // Pausa para que el usuario lea el error
                        break;
                }

            } while (opcion != 5); // Condición de salida del programa 
        }

        // ======================================================
        // FUNCIÓN 1: RegistrarEstudiante
        // Se encarga de capturar, validar y almacenar los datos
        // ======================================================
        static void RegistrarEstudiante()
        {
            // Validación de límite, verifica que no se excedan los 50 espacios disponibles
            if (contador >= 50)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t  Error: Límite de 50 estudiantes alcanzado.");
                Console.ReadLine();
                return; // Sale de la función si se ha alcanzado el límite de estudiantes
            }
            Console.WriteLine("\n\t\t\t  **** Registro Almacenado ****");
            string nuevoCarnet; // Variable para almacenar el carnet ingresado por el usuario
            bool existe; // Variable para controlar la validación de existencia del carnet
            do
            {
                existe = false; // Reinicia la variable 'existe' en cada iteración para validar el nuevo carnet
                Console.Write("\n\t\t\t  Ingrese Carnet (8 dígitos): ");
                nuevoCarnet = Console.ReadLine();

                if (nuevoCarnet.Length != 8) // length: Propiedad que devuelve la cantidad de caracteres en la cadena, se usa para validar que el carnet tenga exactamente 8 dígitos 
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\t\t\t  Error: El carnet debe tener exactamente 8 dígitos.");
                    existe = true; // Si el carnet no tiene 8 dígitos, se marca como existente para forzar la repetición del ciclo
                }
                else
                {
                    // Algoritmo de búsqueda, este recorre los registros para revisar duplicados
                    for (int i = 0; i < contador; i++)
                    {
                        if (carnets[i] == nuevoCarnet) // Si encuentra un carnet que coincide con el nuevo carnet ingresado, se marca como existente
                        {
                            Console.WriteLine("\n\t\t\t  Error: Este carnet ya existe en el sistema.");
                            existe = true; // Si se encuentra un carnet duplicado, se marca como existente para forzar la repetición del ciclo
                            break; // Sale del ciclo for si se encuentra un carnet duplicado para evitar seguir revisando los demás registros
                        }
                    }
                }
            } while (existe); // Se repite el ciclo mientras el carnet ingresado no cumpla con la longitud requerida o ya exista en el sistema

            carnets[contador] = nuevoCarnet; // Almacena el carnet validado en el arreglo correspondiente

            // Validación de nombre: Se asegura que el nombre no esté vacío o compuesto solo por espacios, lo cual no sería un nombre válido para un estudiante
            string nombreIngresado;
            do
            {
                Console.Write("\n\t\t\t  Ingrese Nombre Completo: ");
                nombreIngresado = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nombreIngresado)) // string.IsNullOrWhiteSpace: Método que verifica si la cadena es null, está vacía o contiene solo espacios en blanco
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\t\t\t  Error: El nombre es obligatorio y no puede ser solo espacios.");
                }
            } while (string.IsNullOrWhiteSpace(nombreIngresado));
            // Se repite el ciclo mientras el nombre ingresado sea null, esté vacío o contenga solo espacios


            nombres[contador] = nombreIngresado; // Almacena el nombre validado en el arreglo correspondiente
            int edadAux; // Variable auxiliar para capturar la edad ingresada por el usuario antes de validarla y almacenarla en el arreglo de edades
            bool edadValida; // Variable para controlar la validación de la edad, se usará para repetir el ciclo hasta que se ingrese una edad válida
            do
            {
                Console.Write("\n\t\t\t  Ingrese Edad (15-100 años): ");
                edadValida = int.TryParse(Console.ReadLine(), out edadAux);
                // int.TryParse: Método que intenta convertir la entrada del usuario a un número entero, devuelve true si la conversión es exitosa y false si no lo es. La edad ingresada se almacena en la variable 'edadAux' si la conversión es exitosa.
                // Validación de rango: Además de verificar que la edad sea un número, también se asegura que esté dentro del rango permitido (15 a 100 años)
                if (!edadValida || edadAux < 15 || edadAux > 100)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n\t\t\t  Error: Ingrese una edad válida entre 15 y 100 años.");
                    edadValida = false; // Si la edad no es válida, se marca como falsa para forzar la repetición del ciclo
                }
            } while (!edadValida); // Se repite si la edad no es numérica o está fuera de rango

            edades[contador] = edadAux; // Almacena la edad validada
            contador++; // Incrementa el contador para que el siguiente estudiante se guarde en la siguiente posición

            Console.WriteLine("\n\t\t\t  ¡Estudiante guardado con éxito! Presione Enter.");
            Console.ReadLine();
        }

        // ***************************************************************
        // FUNCIÓN 2: MostrarLista 
        // Muestra los datos de los arreglos (arrays) en formato de tabla
        // ***************************************************************
        static void MostrarLista()
        {
            Console.WriteLine("\n\t\t\t  ***** Listas de Estudiantes *****");
            // Validación de existencia: Si el contador es 0, significa que no se han registrado estudiantes, por lo que se muestra un mensaje de aviso en lugar de intentar imprimir una tabla vacía
            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t  Aviso: No hay estudiantes registrados actualmente.");
            }
            else
            {
                // Encabezados de tabla con formato de alineación (números negativos alinean a la izquierda)
                Console.WriteLine("\n\t\t\t {0,-4} | {1,-10} | {2,-25} | {3,-5}", "No.", "Carnet", "Nombre", "Edad");
                Console.WriteLine("\n\t\t\t ------------------------------------------------------------");
                // Ciclo para imprimir cada estudiante registrado, se recorre desde 0 hasta el valor del contador, que indica cuántos estudiantes han sido registrados
                for (int i = 0; i < contador; i++)
                {
                    // Imprime cada fila de la tabla usando los datos de los arreglos
                    // El número de lista es (i + 1) para que el usuario no vea el número 0 y este vaya en aumento
                    // El formato de alineación asegura que cada columna tenga un ancho fijo para mantener la tabla ordenada, incluso si los datos tienen diferentes longitudes
                    Console.WriteLine("\n\t\t\t {0,-4} | {1,-10} | {2,-25} | {3,-5}", (i + 1), carnets[i], nombres[i], edades[i]);
                }
            }
            Console.WriteLine("\n\t\t\t Presione Enter para volver al menú.");
            Console.ReadLine();
        }
        static void RegistrarCalificasiones()
        {
            Console.Clear();
            Console.WriteLine("/n/t/t ****************** REGISTR0 DE CALIFICASIONES *************************");

            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("/n/t/t Error:  No Hay Estudiantes Registrados: ");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.Write("/n/t/t Ingrese El Carnet Del Estudiante: ");
            string CarnetBuscado = Console.ReadLine();

          bool Encontrado = false;

           for (int i = 0; i < contador; i++)
           {
                if (carnets[i] == CarnetBuscado)
                {
                    Encontrado = true;
                    break;
                    if (!Encontrado)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("/n/t/t Estudiante Encontrado: ");
                        Console.WriteLine("/t/t Nombre {0}", nombres[i]);
                        Console.ResetColor();
                    }


                    int nota;
                    // Matematica 
                    do
                    {
                        Console.Write("/n/t/t Ingrese Nota De Matematica (0-10): ");
                    } while (!int.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10);
                    notaMatematicas[i] = nota;
                    // Ciencia 
                    do
                    {
                        Console.Write("/n/t/t Ingrese Nota De Ciencia: ");
                    } while (!int.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10);
                    notaCiencias[i] = nota;
                    // Lenguaje 
                    do
                    {
                        Console.Write("/n/t/t Ingrese Nota De L enguaje: ");
                    } while (!int.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10);
                    notalenguaje[i] = nota;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("/n/t/t Calificasiones Registradas con exito. ");
                    Console.ResetColor();

                }
           }

        }

               
    }
}






























