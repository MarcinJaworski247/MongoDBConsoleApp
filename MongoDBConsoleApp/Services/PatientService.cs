using MongoDB.Driver;
using MongoDBConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBConsoleApp.Services
{
    public class PatientService
    {
        private readonly IMongoCollection<Patient> _patients;
        public PatientService()
        {
            var client = new MongoClient(@"mongodb://localhost:27017");
            var database = client.GetDatabase("MongoDBConsoleApp");
            _patients = database.GetCollection<Patient>("Patients");
        }
        public void ShowPatients()
        {
            List<Patient> patients = _patients.Find(x => true).ToList();
            foreach (var patient in patients)
            {
                patient.WriteData();
            }
        }
        public void AddPatient()
        {
            string firstName, lastName;

            Console.WriteLine("Podaj dane pacjenta");
            Console.WriteLine("Imię");
            firstName = Console.ReadLine();
            Console.WriteLine("Nazwisko");
            lastName = Console.ReadLine();

            Patient patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName
            };

            _patients.InsertOne(patient);
        }
        public void EditPatient()
        {
            Console.WriteLine("Podaj ID pacjenta do edycji");
            string id = Console.ReadLine();
            Patient patient = _patients.Find(x => x.Id.Equals(id)).FirstOrDefault();
            if (patient == null)
            {
                Console.WriteLine("Nie znaleziono pacjenta o podanym ID");
                return;
            }
            else
            {
                string firstName, lastName;

                Console.WriteLine("Podaj nowe dane pacjenta");
                Console.WriteLine("Imię");
                firstName = Console.ReadLine();
                Console.WriteLine("Nazwisko");
                lastName = Console.ReadLine();

                patient.FirstName = firstName;
                patient.LastName = lastName;

                _patients.ReplaceOne(x => x.Id.Equals(patient.Id), patient);
            }
        }
        public void FindPatientById()
        {
            Console.WriteLine("Podaj ID");
            string id = Console.ReadLine();
            Patient patient = _patients.Find(x => x.Id.Equals(id)).FirstOrDefault();
            if (patient == null)
            {
                Console.WriteLine("Nie znaleziono pacjenta o podanym ID");
                return;
            }
            else
            {
                patient.WriteData();
            }
        }
        public void FindPatientByName()
        {
            Console.WriteLine("Podaj imię");
            string firstName = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko");
            string lastName = Console.ReadLine();

            Patient patient = _patients.Find(x => x.FirstName.Equals(firstName) && x.LastName.Equals(lastName)).FirstOrDefault();
            if (patient == null)
            {
                Console.WriteLine("Nie znaleziono pacjenta");
                return;
            }
            else
            {
                patient.WriteData();
            }
        }
        public void RemovePatient()
        {
            Console.WriteLine("Podaj ID pacjenta do usunięcia");
            string id = Console.ReadLine();
            Patient patient = _patients.Find(x => x.Id.Equals(id)).FirstOrDefault();
            if (patient == null)
            {
                Console.WriteLine("Nie znaleziono pacjenta o podanym ID");
                return;
            }
            else
            {
                _patients.DeleteOne(x => x.Id.Equals(id));
            }
        }
        public void CountPatients()
        {
            long count = _patients.Count(x => true);
            Console.WriteLine("Ilość pacjentów w bazie danych: {0}", count);
        }
    }
}