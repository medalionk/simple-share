using SimpleShare.Views;
using System;
using Windows.UI.Xaml.Controls;

namespace SimpleShare.NavigationBar
{
    /// <summary>
    /// Navigation bar menu item that navigates to the
    /// <see cref="HomePage" />.
    /// </summary>
    class HomeNavigationBarMenuItem : NavigationBarMenuItemBase, INavigationBarMenuItem
    {
        /// <summary>
        /// Gets the type of the destination page.
        /// </summary>
        public Type DestPage
        {
            get { return typeof(HomePage); }
        }

        /// <summary>
        /// Gets the title displayed in the navigation bar.
        /// </summary>
        public string Label
        {
            get
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                return loader.GetString("HomeNavigationBar_Label");
            }
        }

        /// <summary>
        /// Gets the symbol that is displayed in the navigation bar.
        /// </summary>
        public override Symbol Symbol
        {
            get { return Symbol.Home; }
        }
    }
}
