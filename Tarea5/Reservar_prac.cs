using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tarea5
{
    class Reservar_prac
    {
        //Sistema de reservacion a practica, el cual permite registrase o reservar practica
        //cambiar o actualizar el dia inscrita a practica
        // Eliminar registro o reservacion 
        //Mostrar los estudiantes registrados, y el dia 

        static void Main()
        {
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = Menu();
            }
            Console.ReadKey();
        }

        private static bool Menu()
        {
            Console.WriteLine("REGISTRO A PRACTICAS PRESENCIALES ");
            Console.WriteLine("                                    ");
            Console.WriteLine("Seleccion la operación a realizar: ");
            Console.WriteLine("1. Reservar practica");
            Console.WriteLine("2. Cambiar dia de practica");
            Console.WriteLine("3. Eliminar reservacion a asistencia a practica");
            Console.WriteLine("4. Mostrar listado de estudiantes registrados a practica");
            Console.WriteLine("5. Salir");
            Console.Write("\nOpcion: ");

            switch (Console.ReadLine())
            {
                case "1":
                    registro();
                    return true;
                case "2":
                    updateData();
                    Console.ReadKey();
                    return true;
                case "3":
                    deleteData(); 

                    return true;
                case "4":

                    return true;
                case "5":
                    return false;
                default:
                    return false;
            }
        }

        //metodo para obtener la ruta del archivo
        private static string getPath()
        {
            string path = (@"C:\Tarea5\archivo.text");
            return path;
        }

        private static void registro()
        {
            Console.WriteLine("DATOS DEL ESTUDIANTE");
            Console.Write("Nombre Completo: ");
            string fullname = Console.ReadLine();
            Console.Write("Dia a asistir, Martes/Jueves: ");
            string fulllname = Console.ReadLine();

            if (search(fullname))
            {
                Console.WriteLine("El nombre ya esta registrado, ingrese otro");
            }
            else
            {
                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    sw.WriteLine("{0}; {1}", fullname, fulllname);
                    sw.Close();
                }
            }

        }

        private static Dictionary<object, object> readFile()
        {
            Dictionary<object, object> listData = new Dictionary<object, object>();

            using (var reader = new StreamReader(getPath()))
            {
                string lines;

                while ((lines = reader.ReadLine()) != null)
                {
                    string[] keyvalue = lines.Split(';');
                    if (keyvalue.Length == 2)
                    {
                        listData.Add(keyvalue[0], keyvalue[1]);
                    }
                }

            }

            return listData;
        }

        private static bool search(string name)
        {
            if (!readFile().ContainsKey(name))
            {
                return false;
            }
            return true;
        }

        private static void updateData()
        {
            Console.Write("Escriba el nombre del estudiante a actualizar: ");
            var name = Console.ReadLine();

            if (search(name))
            {
                Console.WriteLine("El registro existe!");
                Console.Write("Ingrese el nuevo dia a asistir: ");
                var newAge = Console.ReadLine();

                Dictionary<object, object> temporal = new Dictionary<object, object>();
                temporal = readFile();

                temporal[name] = newAge;
                Console.WriteLine("El registro ha sido actualizado!");
                File.Delete(getPath());

                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    foreach (KeyValuePair<object, object> values in temporal)
                    {
                        sw.WriteLine("{0}; {1}", values.Key, values.Value);
                    }
                }

            }
            else
            {
                Console.WriteLine("El registro no se encontro!");
            }
        }
        //metodo para eliminar una reservación 
        private static void deleteData()
        {
            Console.Write("Escriba el nombre del estudiante a eliminar: ");
            var name = Console.ReadLine();
            if (search(name))
            {
                Console.WriteLine("El registro existe!");
                Dictionary<object, object> temp = new Dictionary<object, object>();
                temp = readFile();

                temp.Remove(name); //eliminar elemento del diccionario

                Console.WriteLine("El registro ha sido eliminado!");
                File.Delete(getPath()); //eliminamos archivos y posteriormente lo volvemos a crear

                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    foreach (KeyValuePair<object, object> values in temp)
                    {
                        sw.WriteLine("{0}; {1}", values.Key, values.Value);
                        // sw.Close();
                    }
                }

            }
            else
            {
                Console.WriteLine("El registro no se encontro!");
            }

        }


    }
}
