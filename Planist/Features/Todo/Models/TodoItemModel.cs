using CommunityToolkit.Mvvm.ComponentModel;

namespace Planist.Features.Todo.Models
{
    public partial class TodoItemModel : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _text = string.Empty;
    }
}
