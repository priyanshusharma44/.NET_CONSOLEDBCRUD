using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleWithDbCrud
{
    public class DataAccess
    {
        private string conString = "Server=PRIYANSHUEFX; Database=TestDb; Integrated Security=SSPI";
        private SqlConnection con;
        private SqlCommand cmd;

        public DataAccess()
        {
            con = new SqlConnection(conString);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        public bool AddStudent(Student s)
        {
            cmd.CommandText = "INSERT INTO Student(Id, Name, Address, Gender, DoB) VALUES(@Id, @Name, @Address, @Gender, @DoB)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", s.Id);
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Address", s.Address);
            cmd.Parameters.AddWithValue("@Gender", s.Gender);
            cmd.Parameters.AddWithValue("@DoB", s.DoB.ToShortDateString());

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con.State != ConnectionState.Closed)
                    con.Close();
                return false;
            }
            return true;
        }

        public List<Student> GetStudentList()
        {
            List<Student> list = new List<Student>();
            cmd.CommandText = "SELECT Id, Name, Address, Gender, DoB FROM Student";
            cmd.Parameters.Clear();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student s = new Student();
                    s.Id = Convert.ToInt32(reader["Id"]);
                    s.Name = Convert.ToString(reader["Name"]);
                    s.Address = Convert.ToString(reader["Address"]);
                    s.Gender = Convert.ToBoolean(reader["Gender"]);
                    s.DoB = DateOnly.FromDateTime(Convert.ToDateTime(reader["DoB"]));
                    list.Add(s);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return list;
        }

        public bool UpdateStudent(Student s, int oldId)
        {
            cmd.CommandText = "UPDATE Student SET Id=@Id, Name=@Name, Address=@Address, Gender=@Gender, DoB=@DoB WHERE Id=@OldId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", s.Id);
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Address", s.Address);
            cmd.Parameters.AddWithValue("@Gender", s.Gender);
            cmd.Parameters.AddWithValue("@DoB", s.DoB.ToShortDateString());
            cmd.Parameters.AddWithValue("@OldId", oldId);

            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                con.Close();

                return count >= 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con.State != ConnectionState.Closed)
                    con.Close();
                return false;
            }
        }

        public int DeleteStudent(int Id)
        {
            cmd.CommandText = "DELETE FROM Student WHERE Id=@Id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", Id);

            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                con.Close();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con.State != ConnectionState.Closed)
                    con.Close();
                return 0;
            }
        }
    }
}
