using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace wpf_context_menu
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        new MainWindowDataContext DataContext => (MainWindowDataContext)base.DataContext;

        private void ContextMenu_Opening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = !DataContext.IsAdvancedMode;
        }
    }
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
    partial class MenuDataContext : ObservableObject
    {
        public ObservableCollection<MenuItem> MenuItems { get; } = new ObservableCollection<MenuItem>
        {
            new MenuItem{ Text = "Rerun" },
            new MenuItem{ Text = "Delete" },
        };
    }

    class MenuItem
    {
        public string? Text { get; set; }
        public override string ToString() => Text ?? "Menu Item";
    }
    enum Status
    {
        SUCCESS,
        FAILED,
    }
    class JobItem
    {
        [Description("Job Name")]
        public string? JobName { get; set; }

        [Description("Start Time")]
        public DateTime StartTime { get; set; }

        public Status Status { get; set; }
    }
}