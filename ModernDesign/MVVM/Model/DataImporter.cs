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
using System.Diagnostics;
using DocumentFormat.OpenXml.Vml.Office;
using System.Data;

namespace ModernDesign.MVVM.Model
{
    public class DataImporter
    {
        // This file could maybe have been created as a static class library
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        string? pedigree = row.Cell(2).Value.ToString();

                        Dog dog;

                        using (var cmd = new SqlCommand("SELECT PedigreeNumber FROM Dogs WHERE PedigreeNumber = @Pedigree", con))
                        {
                            cmd.Parameters.AddWithValue("@Pedigree", pedigree);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Debug.WriteLine("Dog already exists");
                                    continue;
                                }
                                else
                                {
                                    bool checkValue(string value)
                                    {
                                        if (value == "1" || value == "AK")
                                        {
                                            return true;
                                        }
                                        else if (value == "0")
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }

                                    byte[] emptyByte = new byte[0];

                                    string? name = row.Cell(4).Value.ToString();
                                    DateTime dob = DateTime.Parse(row.Cell(12).Value.ToString());
                                    string? dad = row.Cell(6).Value.ToString();
                                    string? mom = row.Cell(7).Value.ToString();
                                    string? gender = row.Cell(19).Value.ToString();
                                    bool isDead = checkValue(row.Cell(21).Value.ToString());
                                    // I couldn't find the chipNumber in the excel file
                                    //string? chipNumber;
                                    string? dKKTitles = row.Cell(8).Value.ToString();
                                    string? titles = row.Cell(9).Value.ToString();
                                    bool breedingStatus = checkValue(row.Cell(23).Value.ToString());
                                    bool mentalDescription = checkValue(row.Cell(24).Value.ToString());
                                    // It doesn't make sense to implement this type
                                    //byte[] picture;
                                    string? hD = row.Cell(13).Value.ToString();
                                    string? aD = row.Cell(14).Value.ToString();
                                    string? hZ = row.Cell(15).Value.ToString();
                                    string? sP = row.Cell(16).Value.ToString();
                                    string? color = row.Cell(20).Value.ToString();
                                    bool breedingApproval = checkValue(row.Cell(22).Value.ToString());
                                    string? email = row.Cell(31).Value.ToString();

                                    dog = new Dog(pedigree, name, dob, dad, mom, gender,
                                        isDead, "Ukendt", dKKTitles, titles, breedingStatus, mentalDescription,
                                        emptyByte, hD, aD, hZ, sP, color, breedingApproval, email);
                                }
                            }
                        }
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Dogs (PedigreeNumber, Name, DOB, DadPedigreeNumber, " +
                            "MomPedigreeNumber, Gender, IsDead, ChipNumber, DKKTitles, Titles, BreedingStatus, MentalDescription, " +
                            "Picture, HD, AD, HZ, SP, Color, BreedingApproval, Email) VALUES (@PedigreeNumber, @Name, @DOB, @DadPedigreeNumber, " +
                            "@MomPedigreeNumber, @Gender, @IsDead, @ChipNumber, @DKKTitles, @Titles, @BreedingStatus, @MentalDescription, " +
                            "@Picture, @HD, @AD, @HZ, @SP, @Color, @BreedingApproval, @Email)", con))
                        {
                            cmd.Parameters.Add("@PedigreeNumber", SqlDbType.NVarChar).Value = dog.PedigreeNumber;
                            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = dog.Name;
                            cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = dog.DOB;
                            cmd.Parameters.Add("@DadPedigreeNumber", SqlDbType.NVarChar).Value = dog.DadPedigreeNumber;
                            cmd.Parameters.Add("@MomPedigreeNumber", SqlDbType.NVarChar).Value = dog.MomPedigreeNumber;
                            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = dog.Gender;
                            cmd.Parameters.Add("@IsDead", SqlDbType.Bit).Value = dog.IsDead;
                            cmd.Parameters.Add("@ChipNumber", SqlDbType.NVarChar).Value = dog.ChipNumber;
                            cmd.Parameters.Add("@DKKTitles", SqlDbType.NVarChar).Value = dog.DKKTitles;
                            cmd.Parameters.Add("@Titles", SqlDbType.NVarChar).Value = dog.Titles;
                            cmd.Parameters.Add("@BreedingStatus", SqlDbType.Bit).Value = dog.BreedingStatus;
                            cmd.Parameters.Add("@MentalDescription", SqlDbType.Bit).Value = dog.MentalDescription;
                            cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = dog.Picture;
                            cmd.Parameters.Add("@HD", SqlDbType.NVarChar).Value = dog.HD;
                            cmd.Parameters.Add("@AD", SqlDbType.NVarChar).Value = dog.AD;
                            cmd.Parameters.Add("@HZ", SqlDbType.NVarChar).Value = dog.HZ;
                            cmd.Parameters.Add("@SP", SqlDbType.NVarChar).Value = dog.SP;
                            cmd.Parameters.Add("@Color", SqlDbType.NVarChar).Value = dog.Color;
                            cmd.Parameters.Add("@BreedingApproval", SqlDbType.Bit).Value = dog.BreedingApproval;
                            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)dog.Email ?? DBNull.Value;

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("File could not be read");
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
