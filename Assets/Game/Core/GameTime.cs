namespace Game.Core
{
    using UnityEngine;

    public static class GameTime
    {
        public static float MinutesPerRealSecond { get; set; } = 1f;
        public static int MinutesSinceMidnight => _minutes;
        public static int Hour => _minutes / 60;
        public static int Minute => _minutes % 60;

        public static int CurrentTime => Hour * 100 + Minute;

        private static int _minutes = 8 * 60;
        private static float _accum;

        public static void Update(float deltaTime)
        {
            _accum += deltaTime * MinutesPerRealSecond;

            while (_accum >= 1f)
            {
                _accum -= 1f;
                _minutes = (_minutes + 1) % 1440;
            }
        }

        public static void SetTimeHHMM(int hhmm)
        {
            int h = Mathf.Clamp(hhmm / 100, 0, 23);
            int m = Mathf.Clamp(hhmm % 100, 0, 59);
            _minutes = h * 60 + m;
        }

        public static void SetTimeHM(int hour, int minute)
        {
            int h = Mathf.Clamp(hour, 0, 23);
            int m = Mathf.Clamp(minute, 0, 59);
            _minutes = h * 60 + m;
        }

        public static void AdvanceMinutes(int minutes)
        {
            _minutes = ((_minutes + minutes) % 1440 + 1440) % 1440;
        }

        public static string GetFormattedTime() => $"{Hour:00}:{Minute:00}";
    }

}

