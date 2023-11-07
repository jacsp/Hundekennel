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

namespace ModernDesign.MVVM.Model.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        private readonly string ConnectionString;
        public ObservableCollection<Dog> dogs = new ObservableCollection<Dog>();

        public DogsRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Dog entity)
        {
            using SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            using SqlCommand cmd = new SqlCommand("INSERT INTO Dogs (PedigreeNumber, Name, DOB, DadPedigreeNumber, " +
                "MomPedigreeNumber, Gender, IsDead, ChipNumber, DKKTitles, Titles, BreedingStatus, MentalDescription, " +
                "Picture, HD, AD, HZ, SP, Color, BreedingApproval) VALUES (@PedigreeNumber, @Name, @DOB, @DadPedigreeNumber, " +
                "@MomPedigreeNumber, @Gender, @IsDead, @ChipNumber, @DKKTitles, @Titles, @BreedingStatus, @MentalDescription, " +
                "@Picture, @HD, @AD, @HZ, @SP, @Color, @BreedingApproval)", con);

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
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Dogs", con);
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
                        Convert.ToBoolean(dr["BreedingApproval"])
                    );

                    dogs.Add(dog);
                }
            }
            return dogs;
        }

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
                            Convert.ToBoolean(dr["BreedingApproval"])
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
                "Picture = @Picture, HD = @HD, AD = @AD, HZ = @HZ, SP = @SP, Color = @Color, BreedingApproval = @BreedingApproval " +
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

            }
        }
    }
}
