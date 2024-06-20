using Project_Manager.Commands;
using Project_Manager.Models;
using Project_Manager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Project_Manager.ViewModels
{
    public class ManagementViewModel : ViewModelBase
    {
        private ObservableCollection<Project> _projects;
        private ICollectionView projectsView;
        private int itemsPerPage = 5;
        private int currentPage;
        private Project selectedProject;
        private Models.Task selectedTask;

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged();
            }
        }

        public Project SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;
                OnPropertyChanged();
            }
        }

        public Models.Task SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;
                OnPropertyChanged();
            }
        }
        public ICollectionView ProjectsView
        {
            get => projectsView;
            set
            {
                projectsView = value;
                OnPropertyChanged();
            }
        }

        public int CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged();
                ProjectsView.Refresh();
            }
        }

        public int ItemsPerPage
        {
            get => itemsPerPage;
            set
            {
                itemsPerPage = value;
                OnPropertyChanged();
                ProjectsView.Refresh();
            }
        }

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand AddProjectCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public ManagementViewModel()
        {
            Projects = new ObservableCollection<Project>();
            ProjectsView = CollectionViewSource.GetDefaultView(Projects);
            ProjectsView.Filter = ProjectsFilter;

            NextPageCommand = new RelayCommand(NextPage, CanGoToNextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage, CanGoToPreviousPage);
            AddProjectCommand = new RelayCommand(AddProject);
            EditProjectCommand = new RelayCommand(EditProject, CanEditOrDeleteProject);
            DeleteProjectCommand = new RelayCommand(DeleteProject, CanEditOrDeleteProject);
            AddTaskCommand = new RelayCommand(AddTask);
            EditTaskCommand = new RelayCommand(EditTask, CanEditOrDeleteTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask, CanEditOrDeleteTask);

            CurrentPage = 1;
        }

        private void AddProject()
        {
            AddProjectForm addProjectForm = new AddProjectForm();
            addProjectForm.AddProject += AddProjectForm_AddProject;
            addProjectForm.ShowDialog();
        }

        private void AddProjectForm_AddProject(Project obj)
        {
            Projects.Add(obj);
        }

        private void EditProject()
        {
            AddProjectForm addProjectForm = new AddProjectForm(SelectedProject);
            addProjectForm.ShowDialog();
        }

        private void DeleteProject()
        {
            if (SelectedProject != null)
            {
                Projects.Remove(SelectedProject);
                SelectedProject = null;
            }
        }

        private bool CanEditOrDeleteProject()
        {
            return SelectedProject != null;
        }

        private void AddTask()
        {
            if (SelectedProject != null)
            {
                AddTaskForm addTaskForm = new AddTaskForm();
                addTaskForm.AddTask += AddTaskForm_AddTask;
                addTaskForm.ShowDialog();
            }
        }

        private void AddTaskForm_AddTask(Models.Task obj)
        {
            SelectedProject.Tasks.Add(obj);
        }

        private void EditTask()
        {
            AddTaskForm addTaskForm = new AddTaskForm(SelectedTask);
            addTaskForm.ShowDialog();
        }

        private void DeleteTask()
        {
            if (SelectedProject != null && SelectedTask != null)
            {
                SelectedProject.Tasks.Remove(SelectedTask);
                SelectedTask = null;
            }
        }

        private bool CanEditOrDeleteTask()
        {
            return SelectedProject != null && SelectedTask != null;
        }
        private bool ProjectsFilter(object obj)
        {
            if (obj is Project project)
            {
                int index = Projects.IndexOf(project);
                return index >= (CurrentPage - 1) * ItemsPerPage && index < CurrentPage * ItemsPerPage;
            }
            return false;
        }

        private void NextPage()
        {
            if (CanGoToNextPage())
            {
                CurrentPage++;
            }
        }

        private bool CanGoToNextPage()
        {
            return (CurrentPage * ItemsPerPage) < Projects.Count;
        }

        private void PreviousPage()
        {
            if (CanGoToPreviousPage())
            {
                CurrentPage--;
            }
        }

        private bool CanGoToPreviousPage()
        {
            return CurrentPage > 1;
        }
    }
}
