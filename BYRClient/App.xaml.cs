using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BYRClient.Models;
using System.IO.IsolatedStorage;

namespace BYRClient
{
    public partial class App : Application
    {
        public static ByrApi api;
        public static UIArticleItem CurrentThread;
        public static bool IsRecovered = false;

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            string user;
            string pass;
            settings.TryGetValue("username", out user);
            settings.TryGetValue("password", out pass);
            if (user == null) user = "guest";
            if (pass == null) pass = "";
            api = new ByrApi(user, pass);
            LoadSectionCache();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            App.IsRecovered = true;
            if (e.IsApplicationInstancePreserved)
            {
                PhoneApplicationService.Current.State.Clear();
            }
            else
            {
                //ViewModelLocator.NavigationService.RecoveredFromTombstoning = true;
            }
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            //PhoneApplicationService.Current.State["CurrentPage"] = App.CurrentPage;            
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("username"))
            {
                settings.Remove("username");
            }
            settings.Add("username", App.api.getUserId());
            if (settings.Contains("password"))
            {
                settings.Remove("password");
            }
            settings.Add("password", App.api.getPassword());
            settings.Save();

            SaveSectionCache();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;            

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new TransitionFrame();//PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Save cache of the sections to ioslated drive;
        private void SaveSectionCache()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            foreach (string key in Models.Section.cache.Keys)
            {
                string iso_key = "section_cache_" + key;
                if (!settings.Contains(iso_key))
                {
                    settings.Add(iso_key, Models.Section.cache[key]);
                }/*
                else
                {
                    settings.Remove(iso_key);
                }*/
            }
            settings.Save();
        }

        private void LoadSectionCache()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            Dictionary<string, Models.Section> cache = Models.Section.cache;
            foreach (string iso_key in settings.Keys)
            {
                if (iso_key.StartsWith("section_cache_"))
                {
                    cache.Add(iso_key.Replace("section_cache_", ""), (Models.Section)settings[iso_key]);
                }
            }
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}