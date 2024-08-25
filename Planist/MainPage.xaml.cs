using Planist.Features.Todo;

namespace Planist;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		this.Dispatcher.Dispatch(async () => await PlanistNavigator.NavigateToAsync<TodoListPage>());
	}	
}