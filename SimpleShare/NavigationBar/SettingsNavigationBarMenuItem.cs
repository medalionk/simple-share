using SimpleShare.Views;
using System;
using Windows.UI.Xaml.Controls;

namespace SimpleShare.NavigationBar
{
    /// <summary>
    /// Navigation bar menu item that navigates to the
    /// <see cref="SettingsPage" />.
    /// </summary>
    public class SettingsNavigationBarMenuItem : NavigationBarMenuItemBase, INavigationBarMenuItem
    {
        /// <summary>
        /// Gets the type of the destination page.
        /// </summary>
        public Type DestPage
        {
            get { return typeof(SettingsPage); }
        }

        /// <summary>
        /// Gets the title displayed in the navigation bar.
        /// </summary>
        public string Label
        {
            get
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                return loader.GetString("SettingsNavigationBar_Label");
            }
        }

        /// <summary>
        /// Gets the positions of the current item in the navigation bar.
        /// </summary>
        public override NavigationBarItemPosition Position
        {
            get { return NavigationBarItemPosition.Bottom; }
        }

        /// <summary>
        /// Gets the symbol that is displayed in the navigation bar.
        /// </summary>
        public override Symbol Symbol
        {
            get { return Symbol.Setting; }
        }
    }
}
