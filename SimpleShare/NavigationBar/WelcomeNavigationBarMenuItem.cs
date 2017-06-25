using System;
using SimpleShare.Views;
using Windows.UI.Xaml.Controls;

namespace SimpleShare.NavigationBar
{
    /// <summary>
    /// Navigation bar menu item that navigates to the
    /// <see cref="WelcomePage" />.
    /// </summary>
    public class WelcomeNavigationBarMenuItem : NavigationBarMenuItemBase,
        INavigationBarMenuItem
    {
        /// <summary>
        /// Gets the type of the destination page.
        /// </summary>
        public Type DestPage
        {
            get { return typeof(RootPage); }
        }

        /// <summary>
        /// Gets the title displayed in the navigation bar.
        /// </summary>
        public string Label
        {
            get
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                return loader.GetString("WelcomeNavigationBar_Label");
            }
        }

        /// <summary>
        /// Gets the symbol that is displayed in the navigation bar.
        /// </summary>
        /// <summary>
        /// Gets the symbol that is displayed in the navigation bar.
        /// </summary>
        public override Symbol Symbol
        {
            get { return Symbol.Help; }
        }
    }
}
