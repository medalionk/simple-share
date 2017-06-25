using SimpleShare.Extensions;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace SimpleShare.Controls
{
    /// <summary>
    /// This button is used to give gold and to visualize the
    /// number of available gold pieces.
    /// </summary>
    public class GoldButton : Button
    {
        /// <summary>
        /// Plays the moving coin animation.
        /// </summary>
        public void PlayMovingCoinAnimation()
        {
            var rootGrid = VisualTreeHelperExtensions.FindDescendants<Grid>(this)
                .FirstOrDefault(g => g.Name == "RootGrid");

            if (rootGrid != null)
            {
                var animation = (Storyboard)rootGrid.Resources["movingCoinAnimation"];
                animation.Begin();
            }
        }
    }
}
