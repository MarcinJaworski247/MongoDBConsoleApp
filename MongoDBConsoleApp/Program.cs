using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDBConsoleApp.Models;
using MongoDBConsoleApp.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MongoDBConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            bool isRunning = true;
            int operation;

            PatientService patientService = new PatientService();

            try
            {
                while (isRunning)
                {
                    Console.WriteLine("Wybierz operację");
                    Console.WriteLine("1 - lista pacjentów");
                    Console.WriteLine("2 - dodaj pacjenta");
                    Console.WriteLine("3 - edytuj pacjenta");
                    Console.WriteLine("4 - usuń pacjenta");
                    Console.WriteLine("5 - wyszukaj pacjenta po ID");
                    Console.WriteLine("6 - wyszukaj pacjenta po imieniu i nazwisku");
                    Console.WriteLine("7 - ilość pacjentów");
                    Console.WriteLine("0 - wyjście");

                    operation = Convert.ToInt32(Console.ReadLine());

                    switch (operation)
                    {
                        case 1:
                            patientService.ShowPatients();
                            break;
                        case 2:
                            patientService.AddPatient();
                            break;
                        case 3:
                            patientService.EditPatient();
                            break;
                        case 4:
                            patientService.RemovePatient();
                            break;
                        case 5:
                            patientService.FindPatientById();
                            break;
                        case 6:
                            patientService.FindPatientByName();
                            break;
                        case 7:
                            patientService.CountPatients();
                            break;
                        case 0:
                            isRunning = false;
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
