using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace LD50ByButterfly
{
    class WallClock
    {
        public int Hours = 0;
        public int Minutes = 0;
        public int HighscoreHours = 0;
        public int HighscoreMinutes = 0;


        public string GetHighscore()
        {
            return HighscoreHours + ":" + HighscoreMinutes;
        }

        public string GetScore()
        {
            return Hours + ":" + Minutes;
        }

        public float GetHourRotation()
        {
            return MathHelper.ToRadians( Hours * 30);
        }

        public float GetMinuteRotation()
        {
            return MathHelper.ToRadians(Minutes * 6);
        }

        public void AddTime()
        {
            Minutes += 5;
            if (Minutes >= 60)
            {
                Hours++;
                Minutes = 0;
            }
        }
        public void ResetTime()
        {
            Hours = 0;
            Minutes = 0;
        }

        public bool HigherThanHighscore()
        {
            if (Hours > HighscoreHours)
            {
                return true;
            }
            else if (Hours >= HighscoreHours && Minutes > HighscoreMinutes)
            {
                return true;
            }
            
            return false;
        }

    }
}
