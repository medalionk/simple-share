using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SimpleShare.Extensions
{
    /// <summary>
    /// Provides helper methods for working with the XAML tree.
    /// </summary>
    public static class VisualTreeHelperExtensions
    {
        /// <summary>
        /// Finds all descendants of the given object.
        /// </summary>
        /// <typeparam name="T">The type of descendants to find.</typeparam>
        /// <param name="dependencyObject">The parent object.</param>
        /// <returns>The collection of descendants.</returns>
        public static IEnumerable<T> FindDescendants<T>(DependencyObject dependencyObject) where T : class
        {
            if (dependencyObject == null)
            {
                throw new ArgumentException(nameof(dependencyObject));
            }

            var queue = new Queue<DependencyObject>();

            if (dependencyObject is T)
            {
                yield return dependencyObject as T;
            }

            queue.Enqueue(dependencyObject);

            while (queue.Any())
            {
                var currentElement = queue.Dequeue();
                var currentCount = VisualTreeHelper.GetChildrenCount(currentElement);

                for (var i = 0; i < currentCount; i++)
                {
                    var currentChild = VisualTreeHelper.GetChild(currentElement, i);

                    if (currentChild is T)
                    {
                        yield return currentChild as T;
                    }

                    queue.Enqueue(currentChild);
                }
            }
        }
    }
}
