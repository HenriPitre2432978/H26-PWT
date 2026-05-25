using Mastermind.DataAccessLayer;
using Mastermind.Models;
using MySql.Data.MySqlClient;

namespace Mastermind.DataAccessLayer.Factories
{
    public class MemberFactory
    {
        // Build Member object from SQL row
        private Member CreateFromReader(MySqlDataReader reader) => new(
            (int)reader["Id"],
            reader["FullName"]?.ToString() ?? "",
            reader["Email"]?.ToString() ?? "",
            reader["Username"]?.ToString() ?? "",
            reader["Password"]?.ToString() ?? "",
            reader["Role"]?.ToString() ?? "",
            reader["ImagePath"]?.ToString() ?? ""
        );

        public Member CreateEmpty() => new();

        public List<Member> GetAll()
        {
            List<Member> list = new();

            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();

            cmd.CommandText =
                "SELECT * FROM tp6_members ORDER BY FullName";

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

            cmd.CommandText =
                "SELECT * FROM tp6_members WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();

            return reader.Read()
                ? CreateFromReader(reader)
                : null;
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

            return reader.Read()
                ? CreateFromReader(reader)
                : null;
        }

        public bool Exists(string username)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();

            cmd.CommandText =
                "SELECT COUNT(*) FROM tp6_members WHERE Username = @username";

            cmd.Parameters.AddWithValue("@username", username);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        // Check if another member already uses the username
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

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void Insert(Member member)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO tp6_members
                    (FullName, Email, Username, Password, Role, ImagePath)
                VALUES
                    (@FullName, @Email, @Username, @Password, @Role, @ImagePath)";

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
                SET FullName = @FullName,
                    Email = @Email,
                    Username = @Username,
                    Password = @Password,
                    Role = @Role,
                    ImagePath = @ImagePath
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

                cmd.CommandText =
                    "DELETE FROM tp6_members WHERE Id = @Id";

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