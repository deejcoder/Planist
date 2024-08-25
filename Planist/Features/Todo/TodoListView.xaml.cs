using CommunityToolkit.Mvvm.Input;
using Planist.Features.Todo.Models;

namespace Planist.Features.Todo;

public partial class TodoListView : ContentView
{
    #region ItemsSource
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IEnumerable<TodoItemModel>),
        typeof(TodoListView),
        default(IEnumerable<TodoItemModel>),
        propertyChanged: OnItemsSourceChanged);

    public IEnumerable<TodoItemModel> ItemsSource
    {
        get => (IEnumerable<TodoItemModel>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is TodoListView todoListView)
        {            
        }
    }

    #endregion

    #region ItemTappedCommand

    public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create(
        nameof(ItemTappedCommand),
        typeof(IRelayCommand<TodoItemModel>),
        typeof(TodoListView),
        null);

    public IRelayCommand<TodoItemModel> ItemTappedCommand
    {
        get => (IRelayCommand<TodoItemModel>)GetValue(ItemTappedCommandProperty);
        set => SetValue(ItemTappedCommandProperty, value);
    }

    #endregion

    public TodoListView()
	{
		InitializeComponent();
	}

    private void TodoItemTapped(object sender, TodoItemModel item)
    {
        ItemTappedCommand?.Execute(item);
    }
}