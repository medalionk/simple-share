namespace SimpleShare.ViewModels
{
    /// <summary>
    /// Base class for ViewModel navigation arguments.
    /// </summary>
    public class ViewModelArgs
    {
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        public ViewModelArgs()
        {
        }

        /// <summary>
        /// Gets or sets the value determining whether the target view
        /// is being highlighted on the navigation bar.
        /// <remarks>
        /// By default, this property returns true.
        /// </remarks>
        /// </summary>
        public bool HighlightOnNavigationBar { get; set; } = true;
    }
}
