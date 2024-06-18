using Project_Manager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for AddProjectForm.xaml
    /// </summary>
    public partial class AddProjectForm : Window
    {
        private Project project;
        public event Action<Project> AddProject;
        public AddProjectForm()
        {
            InitializeComponent();
        }

        public AddProjectForm(Project project)
        {
            InitializeComponent();
            this.project = project;
            DataContext = project;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(project == null)
            {
                Project project = new Project()
                {
                    Name = nameTextBox.Text,
                    Description = descriptionTextBox.Text,
                    Tasks = new ObservableCollection<Models.Task>()
                };

                AddProject?.Invoke(project);
            }
            else
            {
                project.Name = nameTextBox.Text;
                project.Description = descriptionTextBox.Text;
            }
            
            Close();
        }
    }
}
