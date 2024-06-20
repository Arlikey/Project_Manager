using Project_Manager.Models;
using Project_Manager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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
    public partial class AddTaskForm : Window, INotifyPropertyChanged
    {
        private Models.Task task;
        private Priority _selectedPriority;
        public event Action<Models.Task> AddTask;

        public AddTaskForm()
        {
            InitializeComponent();
            Binding binding = new Binding("SelectedPriority");
            priorityComboBox.SetBinding(ComboBox.SelectedItemProperty, binding);
            
            DataContext = this;

        }
        public AddTaskForm(Models.Task task)
        {
            InitializeComponent();
            this.task = task;
            Binding binding = new Binding("Priority");
            binding.Mode = BindingMode.OneWay;
            priorityComboBox.SetBinding(ComboBox.SelectedItemProperty, binding);
            DataContext = task;
        }
        public Priority SelectedPriority 
        { 
            get => _selectedPriority;
            set
            {
                if (_selectedPriority != value)
                {
                    _selectedPriority = value;
                    OnPropertyChanged();
                }
            }
        }

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
                task.Priority = (Priority)priorityComboBox.SelectedItem;
                task.IsCompleted = (bool)statusCheckBox.IsChecked;
            }
            Close();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
