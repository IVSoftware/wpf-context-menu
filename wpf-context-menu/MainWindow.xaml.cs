using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace wpf_context_menu
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
    }
    partial class MainWindowDataContext : ObservableObject
    {
        const bool POPULATE_WITH_TEST_DATA = true;

        public MainWindowDataContext()
        {
            if(POPULATE_WITH_TEST_DATA)
            {
                JobItems.Add(new JobItem { JobName = "Data Backup", StartTime = DateTime.Now.AddMinutes(-30), Status = Status.SUCCESS });
                JobItems.Add(new JobItem { JobName = "Data Analysis", StartTime = DateTime.Now.AddMinutes(-60), Status = Status.FAILED });
                JobItems.Add(new JobItem { JobName = "Report Generation", StartTime = DateTime.Now.AddMinutes(-120), Status = Status.SUCCESS });
            }
        }

        [ObservableProperty]
        private bool _isAdvancedMode;
        public ObservableCollection<JobItem> JobItems { get; } = new ObservableCollection<JobItem>();
    }
    partial class MenuDataContext : ObservableObject
    {
    }

     class MenuItem
    {
        public string? Text { get; set; }
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