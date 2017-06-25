using SimpleShare.NavigationBar;
using System.Collections.Generic;

namespace SimpleShare.ViewModels
{
    /// <summary>
    /// The ViewModel for the AppShell.
    /// </summary>
    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel()
        {
            NavigationBarMenuItems = new List<INavigationBarMenuItem>()
            {
                new WelcomeNavigationBarMenuItem(),
                new HomeNavigationBarMenuItem()
            };

            BottomNavigationBarMenuItems = new List<INavigationBarMenuItem>()
            {
                new SettingsNavigationBarMenuItem()
            };
        }

        /// <summary>
        /// The navigation bar items at the bottom.
        /// </summary>
        public List<INavigationBarMenuItem> BottomNavigationBarMenuItems { get; }

        /// <summary>
        /// The navigation bar items at the top.
        /// </summary>
        public List<INavigationBarMenuItem> NavigationBarMenuItems { get; private set; }
    }
}
