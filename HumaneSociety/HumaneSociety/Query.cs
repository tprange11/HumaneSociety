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

        public delegate string AddAnimal(string derp);
        public delegate string UpdateAdoption(string derp);
        public delegate string UpdateShot(string derp);
        public delegate string EnterUpdate(string derp);
        public delegate string RemoveAnimal(string derp);

        public delegate string GetPendingAdoptions(string derp);
        public delegate string GetShots(string derp);
        public delegate string GetBreed(string derp);
        public delegate string GetDiet(string derp);
        public delegate string GetLocation(string derp);

        public static void CreateSql()
        {

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

        // Simple queries done with this method
        public static void QuerySql()
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
    }
}
