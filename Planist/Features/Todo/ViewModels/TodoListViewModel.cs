using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Planist.Features.Todo.Models;
using Planist.Interfaces;

namespace Planist.Features.Todo.ViewModels
{
    public partial class TodoListViewModel : ObservableObject, ITransientService
    {
        private readonly TodoService Service;

        [ObservableProperty]
        private List<TodoListModel> _todoLists = [];

        [ObservableProperty]
        private TodoListModel _selectedTodoList = new();

        public TodoListViewModel(TodoService service)
        {
            this.Service = service;
        }

        [RelayCommand]
        private async Task RefreshLists()
        {
            this.TodoLists = await this.Service.GetTodoLists();
        }

        [RelayCommand]
        private async Task AddListTapped()
        {
            // TODO: show popup to add todo lists

            // string name = await this.DialogService.ShowDialog<AddTodoListDialog>();
            string name = "Example";

            await this.Service.CreateTodoList(name);
        }

        [RelayCommand]
        private void TodoListTapped(TodoListModel list)
        {
            // de-select all lists
            foreach(TodoListModel otherList in this.TodoLists)
            {
                otherList.IsSelected = false;
            }

            // set the selected list
            list.IsSelected = true;

            this.SelectedTodoList = list;
        }

        [RelayCommand]
        private void TodoItemTapped(TodoItemModel item)
        {

        }        
    }
}
