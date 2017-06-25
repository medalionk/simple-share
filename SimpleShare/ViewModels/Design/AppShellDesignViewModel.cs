using SimpleShare.NavigationBar;
using System.Collections.Generic;

namespace SimpleShare.ViewModels.Design
{
    /// <summary>
    /// The design-time ViewModel for the AppShell.
    /// </summary>
    public class AppShellDesignViewModel
    {
        public AppShellDesignViewModel()
        {
            NavigationBarMenuItems = new List<INavigationBarMenuItem>();

            BottomNavigationBarMenuItems = new List<INavigationBarMenuItem>
            {
                new SettingsNavigationBarMenuItem()
            };
        }

        public List<INavigationBarMenuItem> BottomNavigationBarMenuItems { get; }

        public List<INavigationBarMenuItem> NavigationBarMenuItems { get; private set; }
    }
}
