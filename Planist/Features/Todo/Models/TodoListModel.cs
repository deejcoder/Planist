using CommunityToolkit.Mvvm.ComponentModel;

namespace Planist.Features.Todo.Models
{
    public partial class TodoListModel : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private List<TodoItemModel> _todoItems = [];

        [ObservableProperty]
        private bool _isSelected = false;
    }
}
