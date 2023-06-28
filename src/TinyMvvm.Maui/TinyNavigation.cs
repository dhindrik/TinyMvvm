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

        //Check if there are parameters in the query string
        if (key.Contains("?"))
        {
            var parts = key.Split("?");

            if (parts[1].Length > 0)
            {
                await Shell.Current.GoToAsync(key);
                return;
            }
        }


        //If no querystring, add a empty parameter so OnParameterSet can be triggered
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
