Let's skip to 

> How to overcome this effect?

If I understand your code, you may want to hook the `ContextMenuOpening` event of the `DataGrid` and either mark it `Handled` or not, depending on the value of `IsAdvancedMode`. Marking it `Handled` will prevent the context menu from showing.

___

#### Code behind

```
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    new MainWindowDataContext DataContext =>(MainWindowDataContext)base.DataContext;

    private void ContextMenu_Opening(object sender, ContextMenuEventArgs e)
    {
        e.Handled = !DataContext.IsAdvancedMode;
    }
}
```

##### Xaml minimal reproducible example

```
<Window x:Class="wpf_context_menu.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:wpf_context_menu"
        xmlns:conv="clr-namespace:wpf_context_menu.Converters"
        mc:Ignorable="d"
        Title="MainWindow" Width="500" Height="300">
    <Window.Resources>
        <local:MenuDataContext x:Key="ProxyElement" />
    </Window.Resources>
    <Window.DataContext>
        <local:MainWindowDataContext />
    </Window.DataContext>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="50" />
        </Grid.RowDefinitions>
        <DataGrid
            x:Name="dataGrid"
            ItemsSource="{Binding JobItems}"
            ContextMenuOpening="ContextMenu_Opening"
            AutoGenerateColumns="False"
            FontSize="16">
            <DataGrid.ContextMenu>
                <ContextMenu
                    ItemsSource="{Binding MenuItems, Source={StaticResource ProxyElement}}" />
            </DataGrid.ContextMenu>
            <DataGrid.Columns>
                <DataGridTextColumn Header="Job Name" Binding="{Binding JobName}" Width="*" />
                <DataGridTextColumn Header="Start Time" Binding="{Binding StartTime}" />
                <DataGridTextColumn Header="Status" Binding="{Binding Status}" />
            </DataGrid.Columns>
        </DataGrid>
        <StackPanel Grid.Row="1" Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
            <CheckBox IsChecked="{Binding IsAdvancedMode}" />
            <TextBlock Text="Is Advanced Mode" Margin="5,0,0,0" VerticalAlignment="Center" />
        </StackPanel>
    </Grid>
</Window>
```

##### Data Context minimal reproducible example

```
partial class MainWindowDataContext : ObservableObject
{
    [ObservableProperty]
    private bool _isAdvancedMode;
    public ObservableCollection<JobItem> JobItems { get; } = new ObservableCollection<JobItem>
    {
        new JobItem { JobName = "Data Backup", StartTime = DateTime.Now.AddMinutes(-30), Status = Status.SUCCESS },
        new JobItem { JobName = "Data Analysis", StartTime = DateTime.Now.AddMinutes(-60), Status = Status.FAILED },
        new JobItem { JobName = "Report Generation", StartTime = DateTime.Now.AddMinutes(-120), Status = Status.SUCCESS },
    };
}
```