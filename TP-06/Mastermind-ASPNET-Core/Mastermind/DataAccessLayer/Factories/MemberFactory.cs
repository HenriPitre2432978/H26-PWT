using Mastermind.DataAccessLayer;
using MySql.Data.MySqlClient;
using Mastermind.Models;

namespace Mastermind.DataAccessLayer.Factories
{
    public class MemberFactory
    {

        //Create from sql
        private Member CreateFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string fullname = reader["FullName"]?.ToString() ?? "";
            string email = reader["Email"]?.ToString() ?? "";
            string username = reader["Username"]?.ToString() ?? "";
            string password = reader["Password"]?.ToString() ?? "";
            string role = reader["Role"]?.ToString() ?? "";
            string imagePath = reader["ImagePath"]?.ToString() ?? "";

            return new Member(id, fullname, email, username, password, role, imagePath);
        }

        public Member CreateEmpty()
        {
            return new Member();
        }

        public List<Member> GetAll()
        {
            List<Member> list = new List<Member>();

            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT * FROM tp6_members ORDER BY FullName";

            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                list.Add(CreateFromReader(reader));

            return list;
        }

        public Member? Get(int id)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT * FROM tp6_members WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return CreateFromReader(reader);

            return null;
        }

        public Member? GetByUsername(string username)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();

            cmd.CommandText = @"
        SELECT *
        FROM tp6_members
        WHERE Username = @Username";

            cmd.Parameters.AddWithValue("@Username", username);

            using MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return CreateFromReader(reader);

            return null;
        }

        public bool Exists(string username)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tp6_members WHERE Username = @username";
            cmd.Parameters.AddWithValue("@username", username);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }


        //Checcks if another menu choice with the same description exists, excluding the one with the given id (used for updates)
        public bool ExistsOther(string username, int excludeId)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*) 
                FROM tp6_members
                WHERE Username = @username AND Id != @Id";

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@Id", excludeId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }


        public void Insert(Member member)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO tp6_members (FullName, Email, Username, Password, Role, ImagePath)
                VALUES (@FullName, @Email, @Username, @Password, @Role, @ImagePath)";

            cmd.Parameters.AddWithValue("@FullName", member.FullName);
            cmd.Parameters.AddWithValue("@Email", member.Email);
            cmd.Parameters.AddWithValue("@Username", member.Username);
            cmd.Parameters.AddWithValue("@Password", member.Password);
            cmd.Parameters.AddWithValue("@Role", member.Role);
            cmd.Parameters.AddWithValue("@ImagePath", member.ImagePath);

            cmd.ExecuteNonQuery();
        }


        public void Update(Member member)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"
                UPDATE tp6_members
                SET FullName = @FullName, Email = @Email, Username = @Username, Password = @Password, Role = @Role, ImagePath = @ImagePath
                WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@FullName", member.FullName);
            cmd.Parameters.AddWithValue("@Email", member.Email);
            cmd.Parameters.AddWithValue("@Username", member.Username);
            cmd.Parameters.AddWithValue("@Password", member.Password);
            cmd.Parameters.AddWithValue("@Role", member.Role);
            cmd.Parameters.AddWithValue("@ImagePath", member.ImagePath);
            cmd.Parameters.AddWithValue("@Id", member.Id);
            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand cmd = mySqlCnn.CreateCommand();
                cmd.CommandText = "DELETE FROM tp6_members WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                mySqlCnn?.Close();
            }
        }
    }
}