﻿using System;
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

        public static void CreateSql()
        {

        }

        internal static Client GetClient(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public static void ReplaceSql()
        {

        }
        public static void UpdateSql()
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

        public static int EmployeeLogin(string userName, string password)
        {
            return 1;
        }

        public static int RetrieveEmployeeUser(string email, int employeeNumber)
        {
            return 1;
        }

        public static int AddUsernameAndPassword(Employee employee, string password)
        {
            return 1;
        }

        public static int CheckEmployeeUserNameExist(string username)
        {
            return 1;
        }

        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var animals = from data in database.ClientAnimalJunctions where data.approvalStatus == "pending" select data;
            return animals;
        }

        internal static IQueryable<ClientAnimalJunction> GetUserAdoptionStatus(Client client)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var adoptions = from data in database.ClientAnimalJunctions where data.client == client.ID select data;
            return adoptions;
        }

        internal static IQueryable<string> GetAnimalByID(int id)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var animal = from data in database.Animals where data.ID == id select data.name;
            return animal;
        }

        internal static void Adopt(object animal, Client client)
        {
            throw new NotImplementedException();
        }

        internal static object RetrieveClients()
        {
            throw new NotImplementedException();
        }

        internal static object GetStates()
        {
            throw new NotImplementedException();
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            throw new NotImplementedException();
        }

        internal static void updateClient(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateUsername(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateEmail(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAddress(Client client)
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
    }
}
