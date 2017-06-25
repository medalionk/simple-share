using System.ComponentModel;

namespace SimpleShare.ComponentModel
{
    /// <summary>
    /// Base class that implements <see cref="INotifyPropertyChanged" />.
    /// </summary>
    public class ObservableObjectBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Notifies that the property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The event that is fired when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
