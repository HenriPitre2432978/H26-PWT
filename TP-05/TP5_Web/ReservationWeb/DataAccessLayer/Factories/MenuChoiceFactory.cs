using MySql.Data.MySqlClient;
using ReservationWeb.Models;

namespace ReservationWeb.DataAccessLayer.Factories
{
    public class MenuChoiceFactory
    {

        //Create from sql
        private MenuChoice CreateFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["Id"];
            string description = reader["Description"]?.ToString() ?? "";

            return new MenuChoice(id, description);
        }

        public MenuChoice CreateEmpty()
        {
            return new MenuChoice(0, "");
        }

        public List<MenuChoice> GetAll()
        {
            List<MenuChoice> list = [];

            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT * FROM tp5_menuchoices ORDER BY Description";

            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                list.Add(CreateFromReader(reader));

            return list;
        }

        public MenuChoice? Get(int id)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT * FROM tp5_menuchoices WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return CreateFromReader(reader);

            return null;
        }

        public bool Exists(string desc)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tp5_menuchoices WHERE Description = @Desc";
            cmd.Parameters.AddWithValue("@Desc", desc);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }


        //Checcks if another menu choice with the same description exists, excluding the one with the given id (used for updates)
        public bool ExistsOther(string desc, int excludeId)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*) 
                FROM tp5_menuchoices 
                WHERE Description = @Desc AND Id != @Id";

            cmd.Parameters.AddWithValue("@Desc", desc);
            cmd.Parameters.AddWithValue("@Id", excludeId);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }


        public void Insert(MenuChoice menuChoice)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO tp5_menuchoices (Description)
                VALUES (@Desc)";

            cmd.Parameters.AddWithValue("@Desc", menuChoice.Description);

            cmd.ExecuteNonQuery();
        }


        public void Update(MenuChoice menuChoice)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            using MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"
                UPDATE tp5_menuchoices 
                SET Description = @Desc
                WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Desc", menuChoice.Description);
            cmd.Parameters.AddWithValue("@Id", menuChoice.Id);

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
                cmd.CommandText = "DELETE FROM tp5_menuchoices WHERE Id = @Id";
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