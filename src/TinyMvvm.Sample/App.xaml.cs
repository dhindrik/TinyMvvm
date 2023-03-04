namespace TinyMvvm.Sample;

public partial class App : TinyApplication
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}

