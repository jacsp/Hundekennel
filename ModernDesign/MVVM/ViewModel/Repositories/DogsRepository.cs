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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernDesign.MVVM.ViewModel.Repositories
{
    public class DogsRepository : IDogsRepository
    {
        private readonly string ConnectionString;

        public ObservableCollection<Dog> dogs;

        public DogsRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Dog entity)
        {
            dogs = new ObservableCollection<Dog>();

            using SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            using SqlCommand cmd = new SqlCommand("INSERT INTO hk_DOG (PedigreeNumber, Name, DOB, DadPedigreeNumber, MomPedigreeNumber, Gender, IsDead) VALUES (@PedigreeNumber, @Name, @DOB, @DadPedigreeNumber, @MomPedigreeNumber, @Gender, @IsDead)", con);

            cmd.Parameters.Add("@PedigreeNumber", SqlDbType.NVarChar).Value = entity.PedigreeNumber;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            cmd.Parameters.Add("@DOB", SqlDbType.DateTime2).Value = entity.DOB;
            cmd.Parameters.Add("@DadPedigreeNumber", SqlDbType.NVarChar).Value = entity.DadPedigreeNumber;
            cmd.Parameters.Add("@MomPedigreeNumber", SqlDbType.NVarChar).Value = entity.MomPedigreeNumber;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = entity.Gender;
            cmd.Parameters.Add("@IsDead", SqlDbType.Bit).Value = entity.IsDead;

            cmd.ExecuteNonQuery();

            dogs.Add(entity);
        }



        public void AddRange(IEnumerable<Dog> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> Find(Expression<Func<Dog, bool>> prediction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetAll()
        {
            dogs = new ObservableCollection<Dog>();

            using SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM hk_DOG", con);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Dog dog = new Dog(
                        dr["PedigreeNumber"].ToString(),
                        dr["Name"].ToString(),
                        DateTime.Parse(dr["DOB"].ToString()),
                        dr["DadPedigreeNumber"].ToString(),
                        dr["MomPedigreeNumber"].ToString(),
                        dr["Gender"].ToString(),
                        Boolean.Parse(dr["IsDead"].ToString()));

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
                SqlCommand cmd = new SqlCommand("SELECT * FROM hk_DOG WHERE PedigreeNumber = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        dog = new Dog(
                            dr["PedigreeNumber"].ToString(),
                            dr["Name"].ToString(),
                            DateTime.Parse(dr["DOB"].ToString()),
                            dr["DadPedigreeNumber"].ToString(),
                            dr["MomPedigreeNumber"].ToString(),
                            dr["Gender"].ToString(),
                            Boolean.Parse(dr["IsDead"].ToString()));
                    }
                }
            }
            return dog;
        }

        public void Remove(Dog entity)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM hk_DOG WHERE PedigreeNumber = @Id", con);
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = entity.PedigreeNumber;
                cmd.ExecuteNonQuery();
            }
            dogs.Remove(entity);
        }


        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Dog entity)
        {
            throw new NotImplementedException();
        }
    }
}
