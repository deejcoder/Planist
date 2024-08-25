using CommunityToolkit.Mvvm.Input;

namespace Planist.Components;

public partial class PlanistIconButton : ContentView
{
    #region Source

    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        nameof(Source),
        typeof(string),
        typeof(PlanistIconButton),
        default(string),
        propertyChanged: OnSourceChanged);

    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PlanistIconButton iconButton)
        {
            iconButton.icon.Source = newValue as string;
        }
    }

    #endregion

    #region IconSize

    public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(
        nameof(IconSize),
        typeof(double),
        typeof(PlanistIconButton),
        16.0,
        propertyChanged: OnIconSizeChanged);

    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    private static void OnIconSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is PlanistIconButton iconButton)
        {
            iconButton.iconContainer.HeightRequest = iconButton.IconSize;
            iconButton.iconContainer.WidthRequest = iconButton.IconSize;
        }
    }

    #endregion

    #region Command

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(IRelayCommand),
        typeof(PlanistIconButton),
        null);

    public IRelayCommand Command
    {
        get => (IRelayCommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion


    public PlanistIconButton()
	{
		InitializeComponent();
	}
}