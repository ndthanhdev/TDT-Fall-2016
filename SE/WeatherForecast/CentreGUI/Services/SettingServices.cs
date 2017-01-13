namespace CentreGUI.Services
{
    public class SettingServices
    {
        private static SettingServices _current;

        private double _bonus = 1;
        private int _duration = 30;
        private int _updateTime = 10;

        public double Bonus
        {
            get { return _bonus; }
            set { _bonus = value; }
        }

        public static SettingServices Current => _current ?? (_current = new SettingServices());

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public int Threshold
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }
    }
}