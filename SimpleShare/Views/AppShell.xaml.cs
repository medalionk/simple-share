
using SimpleShare.Controls;
using SimpleShare.NavigationBar;
using SimpleShare.Serialization;
using SimpleShare.ViewModels;
using System;
using System.Linq;
using Windows.Foundation;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SimpleShare.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public static AppShell Current;
        private Tuple<Type, object> _lastSourcePageEventArgs;
        private readonly AppShellViewModel _viewModel;

        public AppShell()
        {
            this.InitializeComponent();

            _viewModel = new AppShellViewModel();
            DataContext = _viewModel;

            Loaded += (sender, args) =>
            {
                Current = this;

                togglePaneButton.Focus(FocusState.Programmatic);

                // We need to update the initial selection because
                // OnNavigatingToPage happens before Items are loaded in
                // both navigation bars.
                UpdateSelectionState();
            };

            rootSplitView.RegisterPropertyChangedCallback(
                SplitView.DisplayModeProperty,
                (s, a) =>
                {
                    // Ensure that we update the reported size of the TogglePaneButton when the SplitView's
                    // DisplayMode changes.
                    CheckTogglePaneButtonSizeChanged();
                });

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.White;
            titleBar.ButtonHoverBackgroundColor = (Color)Application.Current.Resources["AppAccentLightColor"];
            titleBar.ButtonPressedBackgroundColor = (Color)Application.Current.Resources["AppAccentColor"];
            titleBar.ButtonForegroundColor = (Color)Application.Current.Resources["AppAccentForegroundColor"];
            titleBar.ButtonHoverForegroundColor = (Color)Application.Current.Resources["AppAccentForegroundColor"];
        }

        /// <summary>
        /// Gets the app frame.
        /// </summary>
        public Frame AppFrame
        {
            get { return frame; }
        }

        /// <summary>
        /// The rectangle of the toggle button.
        /// </summary>
        public Rect TogglePaneButtonRect { get; private set; }

        /// <summary>
        /// Default keyboard focus movement for any unhandled keyboarding
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments</param>
        private void AppShell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var direction = FocusNavigationDirection.None;
            switch (e.Key)
            {
                case VirtualKey.Left:
                case VirtualKey.GamepadDPadLeft:
                case VirtualKey.GamepadLeftThumbstickLeft:
                case VirtualKey.NavigationLeft:
                    direction = FocusNavigationDirection.Left;
                    break;
                case VirtualKey.Right:
                case VirtualKey.GamepadDPadRight:
                case VirtualKey.GamepadLeftThumbstickRight:
                case VirtualKey.NavigationRight:
                    direction = FocusNavigationDirection.Right;
                    break;

                case VirtualKey.Up:
                case VirtualKey.GamepadDPadUp:
                case VirtualKey.GamepadLeftThumbstickUp:
                case VirtualKey.NavigationUp:
                    direction = FocusNavigationDirection.Up;
                    break;

                case VirtualKey.Down:
                case VirtualKey.GamepadDPadDown:
                case VirtualKey.GamepadLeftThumbstickDown:
                case VirtualKey.NavigationDown:
                    direction = FocusNavigationDirection.Down;
                    break;
            }

            if (direction != FocusNavigationDirection.None)
            {
                var control = FocusManager.FindNextFocusableElement(direction) as Control;
                if (control != null)
                {
                    control.Focus(FocusState.Programmatic);
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Check for the conditions where the navigation pane does not occupy the space under the floating
        /// hamburger button and trigger the event.
        /// </summary>
        private void CheckTogglePaneButtonSizeChanged()
        {
            if (rootSplitView.DisplayMode == SplitViewDisplayMode.Inline ||
                rootSplitView.DisplayMode == SplitViewDisplayMode.Overlay)
            {
                var transform = togglePaneButton.TransformToVisual(this);
                var rect =
                    transform.TransformBounds(new Rect(0, 0, togglePaneButton.ActualWidth,
                        togglePaneButton.ActualHeight));
                TogglePaneButtonRect = rect;
            }
            else
            {
                TogglePaneButtonRect = new Rect();
            }

            TogglePaneButtonRectChanged?.DynamicInvoke(this, TogglePaneButtonRect);
        }

        /// <summary>
        /// Enable accessibility on each nav menu item by setting the AutomationProperties.Name on each container
        /// using the associated Label of each item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The event arguments.</param>
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item is INavigationBarMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty,
                    ((INavigationBarMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }

        /// <summary>
        /// Navigate to the Page for the selected <paramref name="listViewItem" />.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="listViewItem">The ListViewItem.</param>
        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            var item = (INavigationBarMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);

            if (item != null)
            {
                // We navigate only if current page is different to target page
                // or if navigation arguments are available.
                if ((item.DestPage != null
                    && item.DestPage != AppFrame.CurrentSourcePageType)
                    || _lastSourcePageEventArgs.Item2 != null)
                {
                    AppFrame.Navigate(item.DestPage, item.Arguments);
                }
            }
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {
            // After a successful navigation set keyboard focus to the loaded page
            if (e.Content is Page)
            {
                var control = (Page)e.Content;
                control.Loaded += Page_Loaded;
            }

            // Update the Back button depending on whether we can go Back.
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppFrame.CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;

            _lastSourcePageEventArgs = new Tuple<Type, object>(e.SourcePageType, e.Parameter);

            UpdateSelectionState();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((Page)sender).Focus(FocusState.Programmatic);
            ((Page)sender).Loaded -= Page_Loaded;
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled && AppFrame.CanGoBack)
            {
                e.Handled = true;
                AppFrame.GoBack();
            }
        }

        /// <summary>
        /// Callback when the SplitView's Pane is toggled open or close.  When the Pane is not visible
        /// then the floating hamburger may be occluding other content in the app unless it is aware.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void TogglePaneButton_Checked(object sender, RoutedEventArgs e)
        {
            CheckTogglePaneButtonSizeChanged();
        }

        /// <summary>
        /// An event to notify listeners when the hamburger button may occlude other content in the app.
        /// The custom "PageHeader" user control is using this.
        /// </summary>
        public event TypedEventHandler<AppShell, Rect> TogglePaneButtonRectChanged;

        private void UpdateSelectionState()
        {
            navMenuList.SetSelectedItem(null);
            bottomNavMenuList.SetSelectedItem(null);

            INavigationBarMenuItem itemToHighlight = null;
            ViewModelArgs viewModelArgs = null;

            var parameter = _lastSourcePageEventArgs?.Item2 as string;

            if (parameter != null)
            {
                viewModelArgs = SerializationHelper.Deserialize<ViewModelArgs>(parameter);
            }

            // We only highlight the current page in the navigation bar
            // if navigation arguments tell us to.
            if (viewModelArgs == null
                || viewModelArgs.HighlightOnNavigationBar)
            {
                itemToHighlight =
                (from p in _viewModel.NavigationBarMenuItems.Union(_viewModel.BottomNavigationBarMenuItems)
                 where p.DestPage == _lastSourcePageEventArgs?.Item1
                 select p)
                    .SingleOrDefault();
            }

            togglePaneButton.Background = itemToHighlight == null ?
                new SolidColorBrush((Color)Application.Current.Resources["AppAccentLightColor"]) :
                new SolidColorBrush(Colors.White);

            ListViewItem container;

            if (navMenuList.Items != null && navMenuList.Items.Contains(itemToHighlight))
            {
                container = (ListViewItem)navMenuList.ContainerFromItem(itemToHighlight);
            }
            else
            {
                container = (ListViewItem)bottomNavMenuList.ContainerFromItem(itemToHighlight);
            }

            // While updating the selection state of the item prevent it from taking keyboard focus. If a
            // user is invoking the back button via the keyboard causing the selected nav menu item to change
            // then focus will remain on the back button.
            if (container != null)
            {
                container.IsTabStop = false;
            }

            navMenuList.SetSelectedItem(container);
            bottomNavMenuList.SetSelectedItem(container);

            if (container != null)
            {
                container.IsTabStop = true;
            }
        }
    }
}
