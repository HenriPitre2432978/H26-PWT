using ReservationWeb.DataAccessLayer.Factories;

namespace ReservationWeb.DataAccessLayer
{
    public static class DAL
    {
        private static ReservationFactory? _reservationFact = null;
        private static MenuChoiceFactory? _menuChoiceFact = null;

        public static string? ConnectionString { get; set; }

        public static ReservationFactory ReservationFact
        {
            get
            {
                if (_reservationFact == null)
                {
                    _reservationFact = new ReservationFactory();
                }

                return _reservationFact;
            }
        }

        public static MenuChoiceFactory MenuChoiceFact
        {
            get
            {
                if (_menuChoiceFact == null)
                {
                    _menuChoiceFact = new MenuChoiceFactory();
                }

                return _menuChoiceFact;
            }
        }
    }
}
