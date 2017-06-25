using SimpleShare.ComponentModel;
using System.Threading.Tasks;

namespace SimpleShare.ViewModels
{
    /// <summary>
    /// A base class for ViewModels.
    /// </summary>
    public abstract class ViewModelBase : ObservableObjectBase
    {
        /// <summary>
        /// Loads the state.
        /// </summary>
        public virtual Task LoadState()
        {
            return Task.CompletedTask;
        }
    }
}
