using MongoDB.Driver.Core.Configuration;
using Onion.Domain.Entities;
using Onion.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Onion.Infrastructure.SqlDbService
{
    public class SqlDbService : IStudentDbService

     {

        string connectionString = "Data Source=db-mssql;Initial Catalog=s15480;Integrated Security=True";
        public bool EnrollStudent(Student newStudent, int semestr)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Student (FirstName, LastName) VALUES (" + newStudent.FirstName + "," + newStudent.LastName + ")", con))
                using (SqlDataReader reader = command.ExecuteReader()) { };

            }
            return true;
        }

        public IEnumerable<Student> GetStudents()
        {
            List<Student> StudentsList = new List<Student>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT IdStudent, FirstName, LastName from Student", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student { IdStudent = Convert.ToInt32(reader["IdStudent"]), FirstName = reader["FirstName"].ToString(), LastName = reader["LastName"].ToString() };
                        StudentsList.Add(student);
                    }
                }
            }
            return StudentsList;
        }
    }
}
