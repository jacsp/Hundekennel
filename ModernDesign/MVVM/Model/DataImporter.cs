using Microsoft.Data.SqlClient;
using ModernDesign.MVVM.Model.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Extensions.Configuration;

namespace ModernDesign.MVVM.Model
{
    public class DataImporter
    {
        // This file could maybe have been created as a static class library
        // Also I need to have references to DogsRepository and DogOwnerRepository, I know it's bad practice
        // but I didn't want to think of a new way to do it
        DogsRepository dogsRepository = new();
        DogOwnerRepository ownerRepository = new();

        readonly string connectionString;

        public DataImporter()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void AddDogFromFile(string filePath)
        {
            string worksheetName = "Sheet1"; // Name of the worksheet

            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(worksheetName);

            try
            {
                // Check if dog with the same pedigree number already exists
                // My idea is: Check if a dog with the same pedigree number already exists
                // If it does, then skip the rest of the loop and start from the beginning again
                // If it doesn't, create a new Dog object and add it to the database using the AddDog method
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        string? pedigree = row.Cell(2).Value.ToString();

                        string sql = "SELECT * FROM Dogs WHERE PedigreeNumber = @Pedigree";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Pedigree", pedigree);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    continue;
                                }
                                else
                                {
                                    string? name = row.Cell(4).Value.ToString();
                                    DateTime dob = DateTime.Parse(row.Cell(12).Value.ToString());
                                    string? dad = row.Cell(6).Value.ToString();
                                    string? mom = row.Cell(7).Value.ToString();
                                    string? gender = row.Cell(19).Value.ToString();
                                    bool isDead = bool.Parse(row.Cell(21).Value.ToString());
                                    // I couldn't find the chipNumber in the excel file
                                    //string? chipNumber;
                                    string? dKKTitles = row.Cell(8).Value.ToString();
                                    string? titles = row.Cell(9).Value.ToString();
                                    bool breedingStatus = bool.Parse(row.Cell(23).Value.ToString());
                                    bool mentalDescription = bool.Parse(row.Cell(24).Value.ToString());
                                    // It doesn't make sense to implement this type
                                    //byte[] picture;
                                    string? hD = row.Cell(13).Value.ToString();
                                    string? aD = row.Cell(14).Value.ToString();
                                    string? hZ = row.Cell(15).Value.ToString();
                                    string? sP = row.Cell(16).Value.ToString();
                                    string? color = row.Cell(20).Value.ToString();
                                    bool breedingApproval = bool.Parse(row.Cell(22).Value.ToString());
                                    string? email = row.Cell(31).Value.ToString();

                                    dogsRepository.Add(new Dog(pedigree, name, dob, dad, mom, gender,
                                        isDead, null, dKKTitles, titles, breedingStatus, mentalDescription,
                                        null, hD, aD, hZ, sP, color, breedingApproval, email));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fi le could not be read");
                Console.WriteLine(e.Message);
            }
        }



        // This method is functionally similar to the AddDogFromFile method, just adds an owner instead
        // I still don't see a reason to add this method
        // Maybe we can combine the two methods?
        public void AddOwnerFromFile(string filePath)
        {
            string worksheetName = "Sheet1"; // Name of the worksheet

            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(worksheetName);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        string? email = row.Cell(31).Value.ToString();

                        string sql = "SELECT * FROM DogOwner WHERE Email = @Email";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Email", email);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    continue;
                                }
                                else
                                {
                                    // Remember to change "null" to the proper column in the excel file before adding it
                                    string? name = row.Cell(26).Value.ToString();
                                    string? address = row.Cell(27).Value.ToString();
                                    string? postalCode = row.Cell(28).Value.ToString();
                                    string? city = row.Cell(29).Value.ToString();
                                    string? phone = row.Cell(30).Value.ToString();

                                    ownerRepository.Add(new DogOwner(email, name, address, postalCode, city, phone));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File could not be read");
                Console.WriteLine(e.Message);
            }
        }
    }
}
