using CommunityToolkit.Mvvm.Input;
using Planist.Features.Todo.Models;

namespace Planist.Features.Todo;

public partial class TodoItemView : ContentView
{
    public event EventHandler<TodoItemModel>? ItemTapped;

    #region ItemTappedCommand

    public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
        nameof(ItemTappedCommand),
        typeof(IRelayCommand<TodoItemModel>),
        typeof(TodoItemView),
        null);

    public IRelayCommand<TodoItemModel> ItemTappedCommand
    {
        get => (IRelayCommand<TodoItemModel>)GetValue(ItemTappedCommandProperty);
        set => SetValue(ItemTappedCommandProperty, value);
    }

    #endregion

    public TodoItemView()
    {
        InitializeComponent();
    }

    private void HandleItemTapped(object sender, TappedEventArgs e)
    {
        if (this.BindingContext is TodoItemModel todoItem)
        {
            ItemTappedCommand?.Execute(todoItem);
            ItemTapped?.Invoke(this, todoItem);
        }
    }
}