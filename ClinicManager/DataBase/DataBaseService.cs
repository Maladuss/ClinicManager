using ClinicManager.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicManager.DataBase
{
    public class DataBaseService
    {
        private SqlConnection cnn { get; set; }
        private string ServerName { get; } = @"DESKTOP-NOOS9TM\SQLEXPRESS";

        public void Connection()
        {
            string connetionString = null;
            connetionString = $"Server= {ServerName}; Database= ClinicManager; Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }
        public void Close()
        {
            cnn.Close();
        }
        public void executeSQL(string SQL)
        {
            using (SqlCommand cmd = new SqlCommand(SQL, cnn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public int AddFuntionType(string Type)
        {
            int id = 0;
            string SQL = @"INSERT INTO PersonType ([Name]) OUTPUT Inserted.PersonTypeId VALUES(@type)";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@type", Type);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        id = Convert.ToInt32(reader["PersonTypeId"]);
                    }
                    reader.Close();
                }
            }
            return id;
        }
        public bool CheckExistsPersonType(string type)
        {
            using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from PersonType where name = @type", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@type", type);
                int typeCount = (int)sqlCommand.ExecuteScalar();

                if (typeCount > 0) return true;
            }
            return false;
        }
        public int GetPersonTypeId(string type)
        {
            int id = -1;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT PersonTypeId from PersonType where name = @type", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@type", type);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        id = Convert.ToInt32(reader["PersonTypeId"]);   
                    }
                    reader.Close();
                }
            }
            return id;
        }
        public PersonType GetPersonType(int id)
        {
            PersonType personType = PersonType.NN;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT Name from PersonType where PersonTypeId = @id", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        string name = reader["Name"].ToString().Trim();
                        if (!string.IsNullOrEmpty(name))
                        {
                            personType = Enum.GetValues(typeof(PersonType)).Cast<PersonType>().Where(x => x.ToString().CompareTo(name) == 0).FirstOrDefault();
                        }
                    }
                    reader.Close();
                }
            }
            return personType;
        }
        public int AddAddress(Address address)
        {
           int id = 0;
           string SQL = @"INSERT INTO [dbo].[Address]([City],[Street],[PostNumber],[PostalCode]) OUTPUT Inserted.AddressID VALUES(@City, @Street, @PostNumber, @PostalCode)";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@City", address.City);
                sqlCommand.Parameters.AddWithValue("@Street", address.Street);
                sqlCommand.Parameters.AddWithValue("@PostNumber", address.PostNumber);
                sqlCommand.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        id = Convert.ToInt32(reader["AddressID"]);
                    }
                    reader.Close();
                }
            }
            return id;
        }
        public Address GetAddress(int id)
        {
            Address address = null;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * from Address where AddressID = @id", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        address = new Address();
                        address.City = reader["City"].ToString();
                        address.Street = reader["Street"].ToString();
                        address.PostalCode = reader["PostalCode"].ToString();
                        address.PostNumber = reader["PostNumber"].ToString();
                        address.AddressId = id;
                    }
                    reader.Close();
                }
            }
            return address;
        }
        public void UpdateAddress(Address address)
        {
            string SQL = @"UPDATE [dbo].[Address] SET City = @City, Street = @Street, PostNumber = @PostNumber, PostalCode = @PostalCode 
                            WHERE AddressID = @id";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@City", address.City);
                sqlCommand.Parameters.AddWithValue("@Street", address.Street);
                sqlCommand.Parameters.AddWithValue("@PostNumber", address.PostNumber);
                sqlCommand.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                sqlCommand.Parameters.AddWithValue("@id", address.AddressId);
                sqlCommand.ExecuteNonQuery();
            }
        }
        private void deleteAddress(Address address)
        {
            executeSQL($"DELETE FROM [dbo].[Address] WHERE AddressID = {address.AddressId}");
        }
        public void AddPerson(Person person)
        {
            int idPerson = -1;
            int addressId = AddAddress(person.Address);
            int personTypeId = GetPersonTypeId(person.PersonType.ToString());
            if(personTypeId == -1)
            {
                personTypeId = AddFuntionType(person.PersonType.ToString());
            }

            string SQL = @"INSERT INTO [dbo].[Person]
           ([SSN]
           ,[AddressId]
           ,[Name]
           ,[LastName]
           ,[PersonTypeId]) OUTPUT Inserted.PersonId  VALUES(@SSN, @AddressId, @Name, @LastName, @PersonTypeId)";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@SSN", person.SSN);
                sqlCommand.Parameters.AddWithValue("@AddressId", addressId);
                sqlCommand.Parameters.AddWithValue("@Name", person.Name);
                sqlCommand.Parameters.AddWithValue("@LastName", person.LastName);
                sqlCommand.Parameters.AddWithValue("@PersonTypeId", personTypeId);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        idPerson = Convert.ToInt32(reader["PersonId"]);
                    }
                    reader.Close();
                }
            }

            person.Address.AddressId = addressId;
            person.PersonId = idPerson;
        }
        public void UpdatePerson(Person person)
        {
            UpdateAddress(person.Address);

            string SQL = @"UPDATE [dbo].[Person] SET Name = @Name, LastName = @LastName, SSN = @SSN
                            WHERE PersonId = @PersonId";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@Name", person.Name);
                sqlCommand.Parameters.AddWithValue("@LastName", person.LastName);
                sqlCommand.Parameters.AddWithValue("@SSN", person.SSN);
                sqlCommand.Parameters.AddWithValue("@PersonId", person.PersonId);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void DeletePerson(Person person)
        {          
            executeSQL($"DELETE FROM [dbo].[Person] WHERE PersonId = {person.PersonId}");
            deleteAddress(person.Address);
        }
        public Person GetPerson(int id)
        {
            Person person = null;
            using (SqlCommand sqlCommand = new SqlCommand(
                                    @"SELECT * from Person as p  
                                    join Address as a ON p.AddressId = a.AddressId
                                    join PersonType as pt on p.PersonTypeId = pt.PersonTypeId where PersonId = @id", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        person = new Person();
                        person.PersonId = id;
                        person.Name = reader["Name"].ToString();
                        person.LastName = reader["LastNAme"].ToString();
                        person.SSN = reader["SSN"].ToString();
                        person.Address = new Address();
                        person.Address.City = reader["City"].ToString();
                        person.Address.Street = reader["Street"].ToString();
                        person.Address.PostalCode = reader["PostalCode"].ToString();
                        person.Address.PostNumber = reader["PostNumber"].ToString();
                        person.Address.AddressId = Convert.ToInt32(reader["AddressId"]);
                        string name = reader["Name"].ToString().Trim();
                        if (!string.IsNullOrEmpty(name))
                        {
                            person.PersonType = Enum.GetValues(typeof(PersonType)).Cast<PersonType>().Where(x => x.ToString().CompareTo(name) == 0).FirstOrDefault();
                        }
                    }
                    reader.Close();

                    if (person.Address == null) return null;
                }
            }
            return person;
        }  
        public List<Person> GetPersons(PersonType type)
        {
            List<Person> persons = new List<Person>();
            Person person = null;

            using (SqlCommand sqlCommand = new SqlCommand(
                                    @"SELECT *, pt.Name as PersonName from Person as p  
                                    join Address as a ON p.AddressId = a.AddressId
                                    join PersonType as pt on p.PersonTypeId = pt.PersonTypeId where pt.Name = @type", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@type", type.ToString());
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            person = new Person();
                            person.PersonId = Convert.ToInt32(reader["PersonId"]); ;
                            person.Name = reader["Name"].ToString();
                            person.LastName = reader["LastName"].ToString();
                            person.SSN = reader["SSN"].ToString();
                            person.Address = new Address();
                            person.Address.City = reader["City"].ToString();
                            person.Address.Street = reader["Street"].ToString();
                            person.Address.PostalCode = reader["PostalCode"].ToString();
                            person.Address.PostNumber = reader["PostNumber"].ToString();
                            person.Address.AddressId = Convert.ToInt32(reader["AddressId"]);
                            string name = reader["PersonName"].ToString().Trim();
                            name = name.Replace(" ", string.Empty);
                            if (!string.IsNullOrEmpty(name))
                            {
                                person.PersonType = Enum.GetValues(typeof(PersonType)).Cast<PersonType>().Where(x => x.ToString().CompareTo(name) == 0).FirstOrDefault();
                            }
                            if (person?.Address != null)
                            {
                                persons.Add(person);
                            }
                        }
                    }                    
                    reader.Close();                   
                }
            }
            return persons;
        }
        public int GetFunctionTypeId(string type)
        {
            int id = -1;
            using (SqlCommand sqlCommand = new SqlCommand("SELECT FunctionTypeId from FunctionType where Name = @type", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@type", type);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        id = Convert.ToInt32(reader["FunctionTypeId"]);
                    }
                    reader.Close();
                }
            }
            return id;
        }      
        public int AddFunction(FunctionItem function, int PersonId)
        {
            int functionId = -1;
            int functionTypeId = GetFunctionTypeId(function.FunctionType.ToString());
            
            string SQL = @"INSERT INTO [dbo].[Function]
           ([PriceStandard],[Time],[FuntionTypeId]) 
                OUTPUT Inserted.FunctionId  VALUES(@PriceStandard, @Time, @FuntionTypeId)";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@PriceStandard", function.PriceItems.Where(x =>x.PriceItemType == PriceItemType.STANDARD).FirstOrDefault().Price);
                sqlCommand.Parameters.AddWithValue("@Time", function.Time);
                sqlCommand.Parameters.AddWithValue("@FuntionTypeId", functionTypeId);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        functionId = Convert.ToInt32(reader["FunctionId"]);
                    }
                    reader.Close();
                }
            }
            function.FunctionId = functionId;
            AddPersonFunction(functionId, PersonId);

            return functionId;
        }
        public void AddPersonFunction(int functionId, int PersonId)
        {
            string SQL = @"INSERT INTO [dbo].[PersonFunction]([PersonId],[FunctionId]) VALUES(@PersonId, @FunctionId)";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@PersonId", PersonId);
                sqlCommand.Parameters.AddWithValue("@FunctionId", functionId);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    reader.Close();
                }
            }
        }
        public List<FunctionItem> GetFunctions(int Personid)
        {
            List<FunctionItem> functions = new List<FunctionItem>();
            FunctionItem function = new FunctionItem();
       
            using (SqlCommand sqlCommand = new SqlCommand(
                                    @"select fun.*, ft.[Name] from PersonFunction as pf join Person as p on pf.PersonId = p.PersonId join [Function] as fun on pf.FunctionId = fun.FunctionId 
                                        join FunctionType as ft on fun.FuntionTypeId = ft.FunctionTypeId
                                        where p.PersonId = @id", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@id", Personid);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            function = new FunctionItem();
                            function.FunctionId = Convert.ToInt32(reader["FunctionId"]);
                            string name = reader["Name"].ToString().Trim();
                            function.FunctionType = Enum.GetValues(typeof(FunctionType)).Cast<FunctionType>().Where(x => x.ToString().CompareTo(name) == 0).FirstOrDefault();
                            function.PriceItems.Add(new PriceItem(PriceItemType.STANDARD, float.Parse(reader["PriceStandard"].ToString())));
                            string time = reader["Time"].ToString();
                            function.Time = DateTime.ParseExact(time, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;
                            functions.Add(function);
                        }
                    }
                    reader.Close();          
                }
            }
            return functions;
        }
        public void DeleteFunction(int functionId)
        {
            executeSQL($"DELETE FROM [dbo].[PersonFunction] WHERE FunctionId = {functionId}");
            executeSQL($"DELETE FROM [dbo].[Function] WHERE FunctionId = {functionId}");
        }
        public int AddCalendarDay(IFunction fun, int idDoctor)
        {
            int calendarDayId = -1;
            int functionTypeId = GetFunctionTypeId(fun.FunctionType.ToString());

            string SQL = @"INSERT INTO [dbo].[CalendarDay]([FunctionTypeId],[TimeStart],[PersonPatientId])
                         OUTPUT Inserted.CalendarDayId  VALUES(@FunctionTypeId, @TimeStart, @PersonPatientId)";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@FunctionTypeId", functionTypeId);
                sqlCommand.Parameters.AddWithValue("@TimeStart", fun.DateTimeStart);
                sqlCommand.Parameters.AddWithValue("@PersonPatientId", fun.patient.PersonId);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        calendarDayId = Convert.ToInt32(reader["CalendarDayId"]);
                    }
                    reader.Close();
                }
            }

            AddPersonCalendarDay(calendarDayId, idDoctor);

            return calendarDayId;
        }
        public void AddPersonCalendarDay(int CalendarDayId, int PersonId)
        {
            string SQL = @"INSERT INTO [dbo].[PersonCalendarDay]([CalendarDayId],[PersonId]) VALUES(@CalendarDayId, @PersonId)";
            using (SqlCommand sqlCommand = new SqlCommand(SQL, cnn))
            {
                sqlCommand.Parameters.AddWithValue("@CalendarDayId", CalendarDayId);
                sqlCommand.Parameters.AddWithValue("@PersonId", PersonId);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    reader.Close();
                }
            }
        }
        public List<CalendarItem> GetCalendarDays(int Personid)
        {
            List<CalendarItem> calendarItems = new List<CalendarItem>();
            CalendarItem item = new CalendarItem();

            using (SqlCommand sqlCommand = new SqlCommand(
                                    @"  select cd.*, ft.[Name] from PersonCalendarDay as pc join Person as p on pc.PersonId = p.PersonId join CalendarDay as cd on pc.CalendarDayId = cd.CalendarDayId
                                        join FunctionType as ft on cd.FunctionTypeId = ft.FunctionTypeId
                                        where p.PersonId = @id", cnn))
            {
                sqlCommand.Parameters.AddWithValue("@id", Personid);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            item = new CalendarItem();
                            item.PersonId = Convert.ToInt32(reader["PersonPatientId"]);
                            item.FunctionType = Enum.GetValues(typeof(FunctionType)).Cast<FunctionType>().Where(x => x.ToString().CompareTo(reader["Name"].ToString().Trim()) == 0).FirstOrDefault();
                            string time = reader["TimeStart"].ToString();
                            item.DateTimeStart = DateTime.ParseExact(time, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                            calendarItems.Add(item);
                        }
                    }
                    reader.Close();
                }
            }
            return calendarItems;
        }
    }
}
