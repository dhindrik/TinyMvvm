using System.Collections;
using System.Reflection.Metadata;

namespace TinyMvvm;

public class TinyNavigation : INavigation
{
    public static INavigation Current { get; set; } = null!;

    public TinyNavigation()
    {
        Current = this;
    }

    /// <inheritdoc />
    public async Task NavigateTo(string key)
    {
        var parameters = new Dictionary<string, object>
        {
            { "tinyEmpty", string.Empty }
        };

        await Shell.Current.GoToAsync(key, parameters);

    }

    /// <inheritdoc />
    public async Task NavigateTo(string key, object parameter)
    {
        if (parameter is IDictionary<string, object> dictionary)
        {
            await Shell.Current.GoToAsync(key, dictionary);
            return;
        }

        var parameters = new Dictionary<string, object>
        {
            { "tinyParameter", parameter }
        };

        await Shell.Current.GoToAsync(key, parameters);
    }
}
