using Mastermind.Models;
using MySql.Data.MySqlClient;

namespace Mastermind.DataAccessLayer.Factories
{
    public class MemberStatsFactory
    {
        private MemberStats CreateFromReader(MySqlDataReader reader)
        {
            return new MemberStats(
                Convert.ToInt32(reader["MemberId"]),
                Convert.ToInt32(reader["GamesWon"]),
                Convert.ToInt32(reader["GamesLost"]),
                reader["BestPerformance"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(reader["BestPerformance"])
            );
        }

        public MemberStats? GetByMemberId(int memberId)
        {
            using MySqlConnection cnn =
                new(DAL.ConnectionString);

            cnn.Open();

            using MySqlCommand cmd =
                cnn.CreateCommand();

            cmd.CommandText = @"
                SELECT *
                FROM tp6_member_stats
                WHERE MemberId = @MemberId";

            cmd.Parameters.AddWithValue(
                "@MemberId",
                memberId
            );

            using MySqlDataReader reader =
                cmd.ExecuteReader();

            if (reader.Read())
                return CreateFromReader(reader);

            return null;
        }

        public void CreateForMember(int memberId)
        {
            using MySqlConnection cnn =
                new(DAL.ConnectionString);

            cnn.Open();

            using MySqlCommand cmd =
                cnn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO tp6_member_stats
                (
                    MemberId,
                    GamesWon,
                    GamesLost,
                    BestPerformance
                )
                VALUES
                (
                    @MemberId,
                    0,
                    0,
                    NULL
                )";

            cmd.Parameters.AddWithValue(
                "@MemberId",
                memberId
            );

            cmd.ExecuteNonQuery();
        }

        public void RegisterWin(
            int memberId,
            int attemptsUsed
        )
        {
            using MySqlConnection cnn =
                new(DAL.ConnectionString);

            cnn.Open();

            using MySqlCommand cmd =
                cnn.CreateCommand();

            cmd.CommandText = @"
                UPDATE tp6_member_stats
                SET
                    GamesWon = GamesWon + 1,

                    BestPerformance =
                        CASE
                            WHEN BestPerformance IS NULL
                                THEN @AttemptsUsed

                            WHEN @AttemptsUsed < BestPerformance
                                THEN @AttemptsUsed

                            ELSE BestPerformance
                        END
                WHERE MemberId = @MemberId";

            cmd.Parameters.AddWithValue(
                "@AttemptsUsed",
                attemptsUsed
            );

            cmd.Parameters.AddWithValue(
                "@MemberId",
                memberId
            );

            cmd.ExecuteNonQuery();
        }

        public void RegisterLoss(int memberId)
        {
            using MySqlConnection cnn =
                new(DAL.ConnectionString);

            cnn.Open();

            using MySqlCommand cmd =
                cnn.CreateCommand();

            cmd.CommandText = @"
                UPDATE tp6_member_stats
                SET GamesLost = GamesLost + 1
                WHERE MemberId = @MemberId";

            cmd.Parameters.AddWithValue(
                "@MemberId",
                memberId
            );

            cmd.ExecuteNonQuery();
        }

        public int GetTotalWins()
        {
            using MySqlConnection cnn =
                new(DAL.ConnectionString);

            cnn.Open();

            using MySqlCommand cmd =
                cnn.CreateCommand();

            cmd.CommandText =
                "SELECT SUM(GamesWon) FROM tp6_member_stats";

            object? result =
                cmd.ExecuteScalar();

            return result == DBNull.Value
                ? 0
                : Convert.ToInt32(result);
        }

        public int GetTotalLosses()
        {
            using MySqlConnection cnn =
                new(DAL.ConnectionString);

            cnn.Open();

            using MySqlCommand cmd =
                cnn.CreateCommand();

            cmd.CommandText =
                "SELECT SUM(GamesLost) FROM tp6_member_stats";

            object? result =
                cmd.ExecuteScalar();

            return result == DBNull.Value
                ? 0
                : Convert.ToInt32(result);
        }
    }
}