using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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

namespace ModernDesign.MVVM.Model.Repositories
{
    public class DogOwnerRepository : IDogOwnerRepository
    {
        private readonly string ConnectionString;
        public ObservableCollection<DogOwner> dogOwners = new ObservableCollection<DogOwner>();
        
        public DogOwnerRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }


        public void Add(DogOwner entity)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spInsertDogOwner", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = entity.Name });
                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 255) { Value = entity.Address });
                cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.NVarChar, 255) { Value = entity.PostalCode });
                cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 255) { Value = entity.City });
                cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 255) { Value = entity.Phone });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = entity.Email });

                cmd.ExecuteNonQuery();

            }
        }

        public IEnumerable<DogOwner> GetAll()
        {   
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetAllDogOwners", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DogOwner dogOwner = new DogOwner
                        {                           
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            PostalCode = reader.GetString(reader.GetOrdinal("PostalCode")),
                            City = reader.GetString(reader.GetOrdinal("City")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            Email = reader.GetString(reader.GetOrdinal("Email"))
                        };
                        dogOwners.Add(dogOwner);
                    }
                }
            }

            return dogOwners;
        }

        public void Remove(DogOwner entity)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spDeleteDogOwner", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = entity.Email});

                cmd.ExecuteNonQuery();
            }
        }      


        // Needs testing
        public void Update(DogOwner entity)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spUpdateDogOwner", con);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255) { Value = entity.Name });
                cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 255) { Value = entity.Address });
                cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.NVarChar, 255) { Value = entity.PostalCode });
                cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 255) { Value = entity.City });
                cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 255) { Value = entity.Phone });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = entity.Email });

                cmd.ExecuteNonQuery();
            }
        }

        public DogOwner GetById(string id)
        {
            throw new NotImplementedException();
        }


        public void AddRange(IEnumerable<DogOwner> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DogOwner> Find(Expression<Func<DogOwner, bool>> prediction)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
