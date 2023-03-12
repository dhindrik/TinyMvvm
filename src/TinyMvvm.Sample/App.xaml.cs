namespace TinyMvvm.Sample;

public partial class App : TinyApplication
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override async Task Initialize()
    {
        await base.Initialize();


		//To test that it not hangs the application.
		for(int i = 0; i < 100; i++)
		{
			await Task.Delay(1000);
		}
    }
}

