using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;

namespace BYRClient.Utils
{
    public class Settings
    {
        public static IsolatedStorageSettings settings;
        public static Settings instance = null;
        public const string PREFIX = "settings_";

        public Settings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        public static Settings GetAppSettings()
        {
            if (instance == null)
            {
                instance = new Settings();
            }

            return instance;
        }

        public bool GetSetting(string key)
        {
            bool result = false;

            if (settings.Contains(PREFIX + key))
            {
                result = (bool)settings[PREFIX + key];
            }
            else
            {
                settings.Add(PREFIX + key, false);
                settings.Save();
            }

            return result;
        }

        public void SetSetting(string key, bool value)
        {
            if (settings.Contains(PREFIX + key))
            {
                settings[PREFIX + key] = value;
            }
            else
            {
                settings.Add(PREFIX + key, value);                
            }

            settings.Save();
        }
    }
}
