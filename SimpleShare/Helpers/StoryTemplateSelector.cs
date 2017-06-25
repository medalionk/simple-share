using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SimpleShare.Data;

namespace SimpleShare.Helpers
{
    public class StoryTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HeroStory { get; set; }
        public DataTemplate Story { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var story = item as NewsItem;

            return story.IsHero ? HeroStory : Story;
        }
    }
}
