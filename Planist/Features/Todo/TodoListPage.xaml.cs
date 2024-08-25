using Planist.Features.Todo.ViewModels;
using Planist.Interfaces;

namespace Planist.Features.Todo;

public partial class TodoListPage : ContentPage, IPlanistRoutable, ITransientService
{
	private TodoListViewModel? ViewModel => (TodoListViewModel?)this.BindingContext;

	public TodoListPage(TodoListViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;

		viewModel.RefreshListsCommand.Execute(null);
	}
}