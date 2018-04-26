using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        // Attempting to order methods/delegates in order of CRUD 
        // The more simple the method the more likely it can be a delegate

        internal static void AddAnimal(Animal animal)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            database.Animals.InsertOnSubmit(animal);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Query.AddAnimal: " + e);
            }

        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            Client client = new Client();
            client.firstName = firstName;
            client.lastName = lastName;
            client.userName = username;
            client.pass = password;
            client.email = email;
            int address = GetClientAddress(streetAddress, zipCode, state);
            client.userAddress = address;

            database.Clients.InsertOnSubmit(client);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Query.AddNewClient: " + e);
            }
        }

        public static int AddUsernameAndPassword(Employee employee)
        {
            return 1;
        }

        internal static void Adopt(object animal, Client client)
        {
            throw new NotImplementedException();
        }

        public static bool CheckEmployeeUserNameExist(string username)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var employeeData = (from employee in database.Employees
                                where employee.userName == username
                                select employee).First();
            if (employeeData != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void CreateSql()
        {

        }

        public static void DeleteSql()
        {

        }

        // Employee stuff kept seperate from animal stuff
        public static int EmployeeLogin()
        {
            return 1;
        }

        public static Employee EmployeeLogin(string userName, string password)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var employeeData = (from employee in database.Employees
                            where employee.userName == userName
                            where employee.pass == password
                            select employee).First();
            
            return employeeData;
        }

        internal static int? EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            return 1;
        }

        internal static Animal GetAnimalByID(int id)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var animal = (from data in database.Animals where data.ID == id select data).First();
            return animal;
        }

        internal static int? GetBreed()
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var animalBreed = (from data in database.Breeds select data.breed1).First();
            return 1;
        }

        internal static Client GetClient(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public static int GetClientAddress(string streetAddress, int zipCode, int stateNumber)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            int addressNumber;
            var addressObject = from address in database.UserAddresses where address.addessLine1 == streetAddress && address.zipcode == zipCode && address.USState.ID == stateNumber select address.ID;
            if (addressObject.ToList().Count > 0)
            {
                addressNumber = addressObject.ToList()[0];
            }
            else
            {
                UserAddress address = new UserAddress();
                address.zipcode = zipCode;
                address.addessLine1 = streetAddress;
                address.USState = GetStateById(stateNumber);
                database.UserAddresses.InsertOnSubmit(address);
                database.SubmitChanges();
                var addressKey = from location in database.UserAddresses where location.addessLine1 == streetAddress && location.zipcode == zipCode && location.USState.ID == stateNumber select address.ID;
                addressNumber = addressKey.ToList()[0];
            }
            return addressNumber;
        }

        internal static int? GetDiet()
        {
            return 1;
        }

        internal static int? GetLocation()
        {
            return 1;
        }

        internal static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var animals = from data in database.ClientAnimalJunctions where data.approvalStatus == "pending" select data;
            return animals;
        }

        internal static IQueryable<AnimalShotJunction> GetShots(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<USState> GetStates()
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var states = from data in database.USStates select data;
            return states;
        }

        public static USState GetStateById(int stateNumber)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            USState newState = new USState();
            var stateObject = (from state in database.USStates where state.ID == stateNumber select state).First();
            newState.ID = stateObject.ID;
            newState.abbrev = stateObject.abbrev;
            newState.name = stateObject.name;
            return newState;
        }

        internal static IQueryable<ClientAnimalJunction> GetUserAdoptionStatus(Client client)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var adoptions = from data in database.ClientAnimalJunctions where data.client == client.ID select data;
            return adoptions;
        }
        internal static int? RemoveAnimal(Animal animal)
        {
            return 1;
        }

        public static void ReplaceSql()
        {

        }
        internal static IQueryable<Client> RetrieveClients()
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var clients = from data in database.Clients select data;
            return clients;
        }

        public static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var employeeData = (from employee in database.Employees
                                where employee.email == email
                                where employee.employeeNumber == employeeNumber
                                select employee).First();
            return employeeData;
        }

        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAddress(Client client)
        {
            throw new NotImplementedException();
        }

        internal static int? UpdateAdoption(bool isAdopted, ClientAnimalJunction animal)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateEmail(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateFirstName(Client client)
        {
            throw new NotImplementedException();
        }
        internal static void UpdateLastName(Client client)
        {
            throw new NotImplementedException();
        }

        internal static int? UpdateShot(string shot, Animal animal)
        {
            throw new NotImplementedException();
        }

        public static void UpdateSql()
        {

        }

        internal static void UpdateUsername(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
