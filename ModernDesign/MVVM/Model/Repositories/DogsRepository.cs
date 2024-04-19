using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModernDesign.MVVM.Model;
using ModernDesign.MVVM.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls.Primitives;

namespace ModernDesign.MVVM.Model.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        private readonly string ConnectionString;
        
        public ObservableCollection<Dog> dogs = new ObservableCollection<Dog>();

        private DogOwnerRepository ownerRepo = new DogOwnerRepository();

        public DogsRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Dog entity)
        {
            SetDefaultValues(entity);

            using SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            using SqlCommand cmd = new SqlCommand("INSERT INTO Dogs (PedigreeNumber, Name, DOB, DadPedigreeNumber, " +
                "MomPedigreeNumber, Gender, IsDead, ChipNumber, DKKTitles, Titles, BreedingStatus, MentalDescription, " +
                "Picture, HD, AD, HZ, SP, Color, BreedingApproval, Email) VALUES (@PedigreeNumber, @Name, @DOB, @DadPedigreeNumber, " +
                "@MomPedigreeNumber, @Gender, @IsDead, @ChipNumber, @DKKTitles, @Titles, @BreedingStatus, @MentalDescription, " +
                "@Picture, @HD, @AD, @HZ, @SP, @Color, @BreedingApproval, @Email)", con);

            cmd.Parameters.Add("@PedigreeNumber", SqlDbType.NVarChar).Value = entity.PedigreeNumber;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = entity.DOB;
            cmd.Parameters.Add("@DadPedigreeNumber", SqlDbType.NVarChar).Value = entity.DadPedigreeNumber;
            cmd.Parameters.Add("@MomPedigreeNumber", SqlDbType.NVarChar).Value = entity.MomPedigreeNumber;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = entity.Gender;
            cmd.Parameters.Add("@IsDead", SqlDbType.Bit).Value = entity.IsDead;
            cmd.Parameters.Add("@ChipNumber", SqlDbType.NVarChar).Value = entity.ChipNumber;
            cmd.Parameters.Add("@DKKTitles", SqlDbType.NVarChar).Value = entity.DKKTitles;
            cmd.Parameters.Add("@Titles", SqlDbType.NVarChar).Value = entity.Titles;
            cmd.Parameters.Add("@BreedingStatus", SqlDbType.Bit).Value = entity.BreedingStatus;
            cmd.Parameters.Add("@MentalDescription", SqlDbType.Bit).Value = entity.MentalDescription;
            cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = entity.Picture;
            cmd.Parameters.Add("@HD", SqlDbType.NVarChar).Value = entity.HD;
            cmd.Parameters.Add("@AD", SqlDbType.NVarChar).Value = entity.AD;
            cmd.Parameters.Add("@HZ", SqlDbType.NVarChar).Value = entity.HZ;
            cmd.Parameters.Add("@SP", SqlDbType.NVarChar).Value = entity.SP;
            cmd.Parameters.Add("@Color", SqlDbType.NVarChar).Value = entity.Color;
            cmd.Parameters.Add("@BreedingApproval", SqlDbType.Bit).Value = entity.BreedingApproval;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)entity.Email ?? DBNull.Value;

            cmd.ExecuteNonQuery();

            dogs.Add(entity);
        }

        public void AddRange(IEnumerable<Dog> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> Find(Expression<Func<Dog, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Dog> GetAll()
        {
            dogs = new ObservableCollection<Dog>();

            using SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            using SqlCommand cmd = new SqlCommand("SELECT * FROM DogsWithOwners", con);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Dog dog = new Dog
                    {
                        PedigreeNumber = dr["PedigreeNumber"].ToString(),
                        Name = dr["DogName"].ToString(),  // Assuming an alias 'DogName' in the view
                        DOB = Convert.ToDateTime(dr["DOB"]),
                        DadPedigreeNumber = dr["DadPedigreeNumber"].ToString(),
                        MomPedigreeNumber = dr["MomPedigreeNumber"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        IsDead = Convert.ToBoolean(dr["IsDead"]),
                        ChipNumber = dr["ChipNumber"].ToString(),
                        DKKTitles = dr["DKKTitles"].ToString(),
                        Titles = dr["Titles"].ToString(),
                        BreedingStatus = Convert.ToBoolean(dr["BreedingStatus"]),
                        MentalDescription = Convert.ToBoolean(dr["MentalDescription"]),
                        Picture = (byte[])dr["Picture"],
                        HD = dr["HD"].ToString(),
                        AD = dr["AD"].ToString(),
                        HZ = dr["HZ"].ToString(),
                        SP = dr["SP"].ToString(),
                        Color = dr["Color"].ToString(),
                        BreedingApproval = Convert.ToBoolean(dr["BreedingApproval"]),
                        Email = dr["DogEmail"].ToString(),
                        Owner = new DogOwner
                        {
                            Name = dr["OwnerName"].ToString(),
                            Address = dr["OwnerAddress"].ToString(),
                            PostalCode = dr["OwnerPostalCode"].ToString(),
                            City = dr["OwnerCity"].ToString(),
                            Phone = dr["OwnerPhone"].ToString(),
                            Email = dr["OwnerEmail"].ToString()
                        }
                    };

                    dogs.Add(dog);
                }
            }

            return dogs;
        }

        /*        public IEnumerable<Dog> GetAll()
                {
                    dogs = new ObservableCollection<Dog>();

                    using SqlConnection con = new SqlConnection(ConnectionString);
                    con.Open();
                    using SqlCommand cmd = new SqlCommand("SELECT * FROM DogsWithOwners", con);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Dog dog = new Dog(
                                dr["PedigreeNumber"].ToString(),
                                dr["Name"].ToString(),
                                Convert.ToDateTime(dr["DOB"]),
                                dr["DadPedigreeNumber"].ToString(),
                                dr["MomPedigreeNumber"].ToString(),
                                dr["Gender"].ToString(),
                                Convert.ToBoolean(dr["IsDead"]),
                                dr["ChipNumber"].ToString(),
                                dr["DKKTitles"].ToString(),
                                dr["Titles"].ToString(),
                                Convert.ToBoolean(dr["BreedingStatus"]),
                                Convert.ToBoolean(dr["MentalDescription"]),
                                (byte[])dr["Picture"],
                                dr["HD"].ToString(),
                                dr["AD"].ToString(),
                                dr["HZ"].ToString(),
                                dr["SP"].ToString(),
                                dr["Color"].ToString(),
                                Convert.ToBoolean(dr["BreedingApproval"]),
                                Convert.ToString(dr["Email"])

                            );
                            dog.Owner = new DogOwner(
                                dr["OwnerName"].ToString(),
                                dr["OwnerAddress"].ToString(),
                                dr["OwnerPostalCode"].ToString(),
                                dr["OwnerCity"].ToString(),
                                dr["OwnerPhone"].ToString(),
                                dr["OwnerEmail"].ToString()
                                );
                            *//*if (!string.IsNullOrEmpty(dog.Email))
                            {
                                DogOwner owner = ownerRepo.GetById(dog.Email);

                                if (owner != null)
                                {
                                    dog.Owner = owner;
                                }
                            }*//*

                            dogs.Add(dog);
                        }
                    }
                    return dogs;
                }*/

        public Dog GetById(string id)
        {
            Dog dog = null;

            using SqlConnection con = new SqlConnection(ConnectionString);
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Dogs WHERE PedigreeNumber = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        dog = new Dog(
                            dr["PedigreeNumber"].ToString(),
                            dr["Name"].ToString(),
                            Convert.ToDateTime(dr["DOB"]),
                            dr["DadPedigreeNumber"].ToString(),
                            dr["MomPedigreeNumber"].ToString(),
                            dr["Gender"].ToString(),
                            Convert.ToBoolean(dr["IsDead"]),
                            dr["ChipNumber"].ToString(),
                            dr["DKKTitles"].ToString(),
                            dr["Titles"].ToString(),
                            Convert.ToBoolean(dr["BreedingStatus"]),
                            Convert.ToBoolean(dr["MentalDescription"]),
                            (byte[])dr["Picture"],
                            dr["HD"].ToString(),
                            dr["AD"].ToString(),
                            dr["HZ"].ToString(),
                            dr["SP"].ToString(),
                            dr["Color"].ToString(),
                            Convert.ToBoolean(dr["BreedingApproval"]),
                            Convert.ToString(dr["Email"])
                        );
                        
                    }
                }
            }
            return dog;
        }

        public void Remove(Dog entity)
        {
            using SqlConnection con = new SqlConnection(ConnectionString);

            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Dogs WHERE PedigreeNumber = @Id", con);
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = entity.PedigreeNumber;
            cmd.ExecuteNonQuery();

            dogs.Remove(entity);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Dog entity)
        {
            using SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            using SqlCommand cmd = new SqlCommand("UPDATE Dogs SET Name = @Name, DOB = @DOB, " +
                "DadPedigreeNumber = @DadPedigreeNumber, MomPedigreeNumber = @MomPedigreeNumber, " +
                "Gender = @Gender, IsDead = @IsDead, ChipNumber = @ChipNumber, DKKTitles = @DKKTitles, " +
                "Titles = @Titles, BreedingStatus = @BreedingStatus, MentalDescription = @MentalDescription, " +
                "Picture = @Picture, HD = @HD, AD = @AD, HZ = @HZ, SP = @SP, Color = @Color, BreedingApproval = @BreedingApproval, Email = @Email " +
                "WHERE PedigreeNumber = @PedigreeNumber", con);

            cmd.Parameters.Add("@PedigreeNumber", SqlDbType.NVarChar).Value = entity.PedigreeNumber;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = entity.DOB;
            cmd.Parameters.Add("@DadPedigreeNumber", SqlDbType.NVarChar).Value = entity.DadPedigreeNumber;
            cmd.Parameters.Add("@MomPedigreeNumber", SqlDbType.NVarChar).Value = entity.MomPedigreeNumber;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = entity.Gender;
            cmd.Parameters.Add("@IsDead", SqlDbType.Bit).Value = entity.IsDead;
            cmd.Parameters.Add("@ChipNumber", SqlDbType.NVarChar).Value = entity.ChipNumber;
            cmd.Parameters.Add("@DKKTitles", SqlDbType.NVarChar).Value = entity.DKKTitles;
            cmd.Parameters.Add("@Titles", SqlDbType.NVarChar).Value = entity.Titles;
            cmd.Parameters.Add("@BreedingStatus", SqlDbType.Bit).Value = entity.BreedingStatus;
            cmd.Parameters.Add("@MentalDescription", SqlDbType.Bit).Value = entity.MentalDescription;
            cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = entity.Picture;
            cmd.Parameters.Add("@HD", SqlDbType.NVarChar).Value = entity.HD;
            cmd.Parameters.Add("@AD", SqlDbType.NVarChar).Value = entity.AD;
            cmd.Parameters.Add("@HZ", SqlDbType.NVarChar).Value = entity.HZ;
            cmd.Parameters.Add("@SP", SqlDbType.NVarChar).Value = entity.SP;
            cmd.Parameters.Add("@Color", SqlDbType.NVarChar).Value = entity.Color;
            cmd.Parameters.Add("@BreedingApproval", SqlDbType.Bit).Value = entity.BreedingApproval;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = entity.Email;

            cmd.ExecuteNonQuery();

            Dog existingDog = dogs.FirstOrDefault(d => d.PedigreeNumber == entity.PedigreeNumber);
            if (existingDog != null)
            {
                existingDog.Name = entity.Name;
                existingDog.DOB = entity.DOB;
                existingDog.DadPedigreeNumber = entity.DadPedigreeNumber;
                existingDog.MomPedigreeNumber = entity.MomPedigreeNumber;
                existingDog.Gender = entity.Gender;
                existingDog.IsDead = entity.IsDead;
                existingDog.ChipNumber = entity.ChipNumber;
                existingDog.DKKTitles = entity.DKKTitles;
                existingDog.Titles = entity.Titles;
                existingDog.BreedingStatus = entity.BreedingStatus;
                existingDog.MentalDescription = entity.MentalDescription;
                existingDog.Picture = entity.Picture;
                existingDog.HD = entity.HD;
                existingDog.AD = entity.AD;
                existingDog.HZ = entity.HZ;
                existingDog.SP = entity.SP;
                existingDog.Color = entity.Color;
                existingDog.BreedingApproval = entity.BreedingApproval;
                existingDog.Email = entity.Email;

            }
        }

        public IEnumerable<Dog> MatchTwoDogsAndShowFamilyTree(string dog1Id, string dog2Id)
        {
            Dog dog1 = GetById(dog1Id);
            Dog dog2 = GetById(dog2Id);

            List<Dog> dog1AndDog2FamilyTree = new List<Dog>();

            // Populate the dog1FamilyTree and dog2FamilyTree lists.
            void PopulateFamilyTree(Dog currentDog, List<Dog> familyTree, int depth)
            {
                if (depth > 0 && currentDog != null)
                {
                    Dog dad = GetById(currentDog.DadPedigreeNumber);
                    Dog mom = GetById(currentDog.MomPedigreeNumber);

                    if (dad != null)
                    {
                        familyTree.Add(dad);
                        PopulateFamilyTree(dad, familyTree, depth - 1);
                    }

                    if (mom != null)
                    {
                        familyTree.Add(mom);
                        PopulateFamilyTree(mom, familyTree, depth - 1);
                    }
                }
            }

            // Populate the dog1andDog2FamilyTree and limit to 3 generations.
            PopulateFamilyTree(dog1, dog1AndDog2FamilyTree, 3);
            PopulateFamilyTree(dog2, dog1AndDog2FamilyTree, 3);

            return dog1AndDog2FamilyTree;
        }




        public void GetFamilyTreeFromDatabase(string id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                // Use a query to call the function
                string query = "SELECT * FROM dbo.GetAncestorsWithInfo(@id)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access the data using reader
                            string pedigreeNumber = reader["PedigreeNumber"].ToString();
                            string name = reader["Name"].ToString();
                            // Add other columns as needed
                            // ...
                        }
                    }
                }
            }
        }



        public IEnumerable<Dog> GetFamilyTree(string dog1Id)
        {
            Dog dog1 = GetById(dog1Id);

            List<Dog> dogFamilyTree = new List<Dog>();

            void PopulateFamilyTree(Dog currentDog, List<Dog> familyTree, int depth)
            {
                if (depth > 0 && currentDog != null)
                {
                    Dog dad = GetById(currentDog.DadPedigreeNumber);
                    Dog mom = GetById(currentDog.MomPedigreeNumber);

                    if (dad != null)
                    {
                        familyTree.Add(dad);
                        currentDog.Father = dad;

                        if (dad.Parents == null)
                        {
                            dad.Parents = new List<Dog>();
                        }
                        dad.Parents.Add(currentDog);

                        PopulateFamilyTree(dad, familyTree, depth - 1);
                    }

                    if (mom != null)
                    {
                        familyTree.Add(mom);
                        currentDog.Mother = mom;

                        if (mom.Parents == null)
                        {
                            mom.Parents = new List<Dog>();
                        }
                        mom.Parents.Add(currentDog);

                        PopulateFamilyTree(mom, familyTree, depth - 1);
                    }
                }
            }

            PopulateFamilyTree(dog1, dogFamilyTree, 3);
            return dogFamilyTree;
        }



        public void SetDefaultValues(Dog dog)
        {
            if (dog.DOB < DateTime.Now.AddYears(-15))
            {
                dog.IsDead = true;
            }
            if (dog.IsDead)
            {
                dog.BreedingStatus = false;
            }
        }
    }
}
