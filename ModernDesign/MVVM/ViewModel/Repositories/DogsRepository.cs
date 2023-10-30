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

namespace ModernDesign.MVVM.ViewModel.Repositories
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
            using SqlCommand cmd = new SqlCommand("INSERT INTO hk_DOG (PedigreeNumber, Name, DOB, DadPedigreeNumber, MomPedigreeNumber, Gender, IsDead" +
                                            " (@PedigreeNumber, @Name, @DOB, @DadPedigreeNumber, @MomPedigreeNumber, @Gender, @IsDead))", con);
            
            cmd.Parameters.Add("@PedigreeNumber", SqlDbType.NVarChar).Value = entity.PedigreeNumber;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            cmd.Parameters.Add("@DOB", SqlDbType.DateTime2).Value = entity.DOB;
            cmd.Parameters.Add("@DadPedigreeNumber", SqlDbType.NVarChar).Value = entity.DadPedigreeNumber;
            cmd.Parameters.Add("@MomPedigreeNumber", SqlDbType.NVarChar).Value = entity.MomPedigreeNumber;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = entity.Gender;
            cmd.Parameters.Add("@IsDead", SqlDbType.Bit).Value = entity.IsDead;

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
            using SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM hk_DOG", con);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                return dr.Cast<IDataRecord>()
                .Select(data => new Dog(
                    data.GetString(data.GetOrdinal("PedigreeNumber")),
                    data.GetString(data.GetOrdinal("Name")),
                    data.GetDateTime(data.GetOrdinal("DOB")),
                    data.GetString(data.GetOrdinal("DadPedigreeNumber")),
                    data.GetString(data.GetOrdinal("MomPedigreeNumber")),
                    data.GetString(data.GetOrdinal("Gender")),
                    data.GetBoolean(data.GetOrdinal("IsDead"))
                ))
                .ToList();
            }
            
        }

        public Dog GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Dog entity)
        {
            throw new NotImplementedException();
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
