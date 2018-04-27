using System;
using System.Collections.Generic;
using System.Linq;

namespace HumaneSociety
{
    public static class Query
    {
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
            Client client = new Client
            {
                firstName = firstName,
                lastName = lastName,
                userName = username,
                pass = password,
                email = email
            };
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

        public static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var updatedEmployee = database.Employees.Where(e => e.ID == employee.ID).Select(e => e).First();
            updatedEmployee.userName = employee.userName;
            updatedEmployee.pass = employee.pass;
            database.SubmitChanges();
        }

        internal static void Adopt(object animal, Client client)
        {
            throw new NotImplementedException();
        }

        public static bool CheckEmployeeUserNameExist(string username)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            try
            {
                var employeeData = (from employee in database.Employees
                                    where employee.userName == username
                                    select employee).First();
                return true;
            }
            catch
            {
                return false;
            }
        }

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

        internal static int GetBreed(string passedInBreed, string passedInCategory)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            try
            {
                var animalBreed = (from data in database.Breeds where data.breed1 == passedInBreed select data.ID).First();
                return animalBreed;
            }
            catch
            {
                var getCategory = GetCategory(passedInCategory);
                Breed newBreed = new Breed()
                {
                    breed1 = passedInBreed,
                    catagory = getCategory
                };
                CreateBreedHelper(newBreed);
                var animalBreed = (from data in database.Breeds where data.breed1 == passedInBreed select data.ID).First();
                return animalBreed;
            }
        }

        internal static void CreateBreedHelper(Breed breedToAdd)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            database.Breeds.InsertOnSubmit(breedToAdd);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Query.CreateBreedHelper: " + e);
            }
        }

        internal static int GetCategory(string passedInCategory)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            try
            {
                var animalBreedCategory = (from data in database.Catagories where data.catagory1 == passedInCategory select data.ID).First();
                return animalBreedCategory;
            }
            catch
            {
                Catagory newCategory = new Catagory()
                {
                    catagory1 = passedInCategory
                };
                CreateCategoryHelper(newCategory);
                var animalBreedCategory = (from data in database.Catagories where data.catagory1 == passedInCategory select data.ID).First();
                return animalBreedCategory;
            }
        }

        internal static void CreateCategoryHelper(Catagory categoryToAdd)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            database.Catagories.InsertOnSubmit(categoryToAdd);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Query.CreateCategoryHelper: " + e);
            }
        }

        internal static int GetDiet(string passedInDiet, int passedInAmount)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            try
            {
                var animalDietPlan = (from data in database.DietPlans where data.food == passedInDiet select data.ID).First();
                return animalDietPlan;
            }
            catch (Exception)
            {
                DietPlan newDietPlan = new DietPlan
                {
                    food = passedInDiet,
                    amount = passedInAmount
                };
                CreateDietPlanHelper(newDietPlan);
                var animalDietPlan = (from data in database.DietPlans where data.food == passedInDiet select data.ID).First();
                return animalDietPlan;
            }
        }

        internal static void CreateDietPlanHelper(DietPlan dietPlanToAdd)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            database.DietPlans.InsertOnSubmit(dietPlanToAdd);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Query.CreateCategoryHelper: " + e);
            }
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
                UserAddress address = new UserAddress
                {
                    zipcode = zipCode,
                    addessLine1 = streetAddress,
                    USState = GetStateById(stateNumber)
                };
                database.UserAddresses.InsertOnSubmit(address);
                database.SubmitChanges();
                var addressKey = from location in database.UserAddresses where location.addessLine1 == streetAddress && location.zipcode == zipCode && location.USState.ID == stateNumber select address.ID;
                addressNumber = addressKey.ToList()[0];
            }
            return addressNumber;
        }

        internal static int? GetDiet()
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            string diet = UserInterface.GetStringData("diet", "the animal's");
            int amount = UserInterface.GetIntegerData("amount", "the animal's");
            try
            {
                var query = (from dietPlan in database.DietPlans
                             where dietPlan.food == diet
                             select dietPlan.ID).First();
                return query;
            }
            catch
            {
                DietPlan newDP = new DietPlan
                {
                    food = diet,
                    amount = amount
                };
                database.DietPlans.InsertOnSubmit(newDP);
                return newDP.ID;
            }
        }

        internal static int GetLocation(string passedInRoom, string passInBuilding)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            try
            {
                var animalRoom = (from data in database.Rooms where data.name == passedInRoom select data.ID).First();
                return animalRoom;
            }
            catch
            {
                Room newRoom = new Room()
                {
                    name = passedInRoom,
                    building = passInBuilding
                };
                CreateRoomHelper(newRoom);
                var animalRoom = (from data in database.Rooms where data.name == passedInRoom select data.ID).First();
                return animalRoom;
            }
        }

        internal static void CreateRoomHelper(Room roomToAdd)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            database.Rooms.InsertOnSubmit(roomToAdd);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Query.CreateRoomHelper: " + e);
            }
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

        internal static void RemoveAnimal(Animal animal)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            database.Animals.DeleteOnSubmit(animal);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Query.RemoveAnimal: " + e);
            }
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

        internal static void RunEmployeeQueries(Employee employee, string crud)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            switch (crud)
            {
                case "create":
                    database.Employees.InsertOnSubmit(employee);
                    try
                    {
                        database.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Query.RunEmployeeQueries(create): " + e);
                    }
                    break;

                case "read":
                    var tempEmployee = database.Employees.Where(e => e.ID == employee.ID).Select(e => e).First();
                    break;

                case "update":
                    

                    var tempEmployee = database.Employees.Where(e => e.ID == employee.ID).Select(e => e).First();
                    tempEmployee.firsttName = employee.firsttName;
                    tempEmployee.lastName = employee.lastName;
                    tempEmployee.employeeNumber = employee.employeeNumber;
                    tempEmployee.email = employee.email;
                    tempEmployee.userName = employee.userName;
                    tempEmployee.pass = employee.pass;
                    database.SubmitChanges();
                    break;

                case "delete":
                    
                    database.Employees.DeleteOnSubmit(employee);
                    try
                    {
                        database.SubmitChanges();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Query.RunEmployeeQueries(delete): " + e);
                    }
                    break;
            }
        }

        internal static void UpdateAddress(Client client)
        {
            HumaneSocietyDataContext database = new HumaneSocietyDataContext();
            var clientData = database.Clients.Where(e => e.ID == client.ID).Select(e => e);
            clientData.First().UserAddress1.zipcode = client.UserAddress1.zipcode;
            clientData.First().UserAddress1.addessLine1 = client.UserAddress1.addessLine1;
            clientData.First().UserAddress1.USState = client.UserAddress1.USState;
            database.SubmitChanges();
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

        internal static void UpdateUsername(Client client)
        {
            throw new NotImplementedException();
        }
    }
}