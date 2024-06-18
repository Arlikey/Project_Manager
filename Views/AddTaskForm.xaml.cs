using Project_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Manager.Views
{
    /// <summary>
    /// Interaction logic for AddTaskForm.xaml
    /// </summary>
    public partial class AddTaskForm : Window
    {
        private Models.Task task;
        public event Action<Models.Task> AddTask;

        public AddTaskForm()
        {
            InitializeComponent();
            DataContext = this;

        }
        public AddTaskForm(Models.Task task)
        {
            InitializeComponent();
            this.task = task;
            DataContext = task;
        }
        public Priority SelectedPriority { get; set; }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (task == null)
            {
                Models.Task task = new Models.Task()
                {
                    Name = nameTextBox.Text,
                    Description = descriptionTextBox.Text,
                    Priority = SelectedPriority,
                    IsCompleted = (bool)statusCheckBox.IsChecked
                };

                AddTask?.Invoke(task);
            }
            else
            {
                task.Name = nameTextBox.Text;
                task.Description = descriptionTextBox.Text;
                task.Priority = SelectedPriority;
                task.IsCompleted = (bool)statusCheckBox.IsChecked;
            }
            Close();
        }
    }
}
