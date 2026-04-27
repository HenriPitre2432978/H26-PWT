using MySql.Data.MySqlClient;
using ReservationWeb.Models;

namespace ReservationWeb.DataAccessLayer.Factories
{
    public class ReservationFactory
    {
        //Create from sql
        private Reservation CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string nom = mySqlDataReader["Nom"].ToString() ?? string.Empty;
            string courriel = mySqlDataReader["Courriel"].ToString() ?? string.Empty;
            int nbPersonne = (int)mySqlDataReader["NbPersonne"];
            DateTime dateReservation = (DateTime)mySqlDataReader["DateReservation"];
            int menuChoiceId = (int)mySqlDataReader["MenuChoiceId"];

            return new Reservation(id, nom, courriel, nbPersonne, dateReservation, menuChoiceId);
        }

        public Reservation CreateEmpty()
            => new Reservation(0, string.Empty, string.Empty, 0, DateTime.MinValue, 0);


        public List<Reservation> GetAll()
        {
            List<Reservation> reservations = new List<Reservation>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_reservations ORDER BY Nom";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Reservation reservation = CreateFromReader(mySqlDataReader);
                    reservations.Add(reservation);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservations;
        }


        public Reservation? Get(int id)
        {
            Reservation? reservation = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_reservations WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    reservation = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservation;
        }

        public int Insert(Reservation r)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = @"INSERT INTO tp5_reservations (Nom, Courriel, NbPersonne, DateReservation, MenuChoiceId) 
                                    VALUES (@Nom, @Courriel, @NbPersonne, @DateReservation, @MenuChoiceId)";

            //Link sql params to c# model params
            cmd.Parameters.AddWithValue("@Nom", r.Nom);
            cmd.Parameters.AddWithValue("@Courriel", r.Courriel);
            cmd.Parameters.AddWithValue("@NbPersonne", r.NbPersonne);
            cmd.Parameters.AddWithValue("@DateReservation", r.DateReservation);
            cmd.Parameters.AddWithValue("@MenuChoiceId", r.MenuChoiceId);
            cmd.ExecuteNonQuery();

            return (int)cmd.LastInsertedId;
        }

        public void Delete(int id)
        {
            using MySqlConnection cnn = new(DAL.ConnectionString);
            cnn.Open();

            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = "DELETE FROM tp5_reservations WHERE Id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
