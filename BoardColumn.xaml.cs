﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LiteDB;
using MaterialDesignThemes.Wpf;


namespace Kanaban
{
    /// <summary>
    /// Interaction logic for BoardColumn.xaml
    /// </summary>
    public partial class BoardColumn : INotifyPropertyChanged
    {
        private ListType _listType;

        public ListType ListType
        {
            get => _listType;
            set
            {
                _listType = value;
                OnPropertyChanged();
            }
        }


        public BoardColumn()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StaticHolder.dragSource = this;
            object data = GetDataFromListBox(StaticHolder.dragSource.lst, e.GetPosition(lst));

            if (data != null)
            {
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            if (source.InputHitTest(point) is UIElement element)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }

                    if (element == source)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }

            return null;
        }

        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            if (StaticHolder.dragSource.Equals(this)) return;
            object data = e.Data.GetData(typeof(WorkItem));
            var currentItem = (WorkItem) data;
            StaticHolder.dragSource.lst.Items.Remove(currentItem);
            this.lst.Items.Add(currentItem);


            currentItem.Type = ListType;

            DB.CurrentProject.Add(currentItem);
            SortItems();
            DB.Log($"{currentItem} Moved From {StaticHolder.dragSource.ListType} to {this.ListType}");
        }

        private void SortItems()
        {
            List<WorkItem> items = new List<WorkItem>();
            foreach (object lstItem in lst.Items)
            {
                items.Add((WorkItem) lstItem);
            }

            lst.Items.Clear();
            var a = items.OrderBy(x => x.Priority);

            foreach (var workItem in a)
            {
                lst.Items.Add(workItem);
            }
        }

        private void btn_add_click(object sender, RoutedEventArgs e)
        {
            WorkItemEdit edit = new WorkItemEdit(ListType);
            edit.Added += LoadData;
            host.DialogContent = (edit);
            host.IsOpen = true;
        }

        private void AListBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            LoadData();
            MainWindow.Instance.DatabaseChanged += LoadData;
        }

        internal void LoadData()
        {
            lst.Items.Clear();
            var db = DB.CurrentProject.WorkItems;
            foreach (var workItem in db.FindAll(x => x.Type == ListType))
            {
                lst.Items.Add(workItem);
            }

            SortItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (lst.SelectedItem is WorkItem wi)
            {
                DB.CurrentProject.Remove(wi);
                LoadData();
            }
        }
    }


    class StaticHolder
    {
        internal static string NewProjectName => "4A3C2DC7981E446EB81655F35493B1DA";

        public static BoardColumn dragSource;
    }

    public class DB
    {
        internal static string logtext = "Log Started At " + DateTime.Now + " in " + Environment.CurrentDirectory;

        internal static void Log(string nlog)
        {
            logtext += "\n";
            logtext += nlog;
        }

        private static Project _project;

        internal static Project CurrentProject
        {
            get => _project ?? new Project {_id = ObjectId.NewObjectId(), Name = StaticHolder.NewProjectName};
            set => _project = value;
        }

        private static LiteDatabase dbfile;
        internal static LiteCollection<Project> Projects;

        public static Project InitializeDatabase()
        {
            dbfile = new LiteDatabase("Database.db");
            Projects = dbfile.GetCollection<Project>("Projects");

            var lastproject = Properties.Settings.Default.LastOpenedProject;
            if (lastproject == null)
            {
                _project = Projects.FindOne(x => x._id != ObjectId.Empty);
            }
            else
            {
                try
                {
                    _project = Projects.FindOne(x => x.Name == lastproject);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _project = Projects.FindOne(x => x._id != ObjectId.Empty);
                }
            }

            return CurrentProject;
        }
    }

    public class Project
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }

        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();

        public void Save()
        {
            if (Name == StaticHolder.NewProjectName)
            {
                DB.Projects.Insert(this);
                var a = new ProjectEdit(this);
                a.Added += (x) =>
                {
                    DB.Projects.Upsert(x);
                    DB.CurrentProject = x;
                    MainWindow.Instance.SelectProject(x);
                };
                MainWindow.Instance.host.DialogContent = a;
                MainWindow.Instance.host.IsOpen = true;
            }
            else
            {
                DB.Projects.Upsert(this);
                DB.CurrentProject = this;
            }
            
        }

        public override string ToString()
        {
            return Name ?? _id.ToString();
        }


        public void Add(WorkItem nested)
        {
            if (WorkItems.Exists(x => x._id == nested._id))
            {
                WorkItems.RemoveAll(x => x._id == nested._id);
                WorkItems.Add(nested);
            }
            else
            {
                WorkItems.Add(nested);
            }

            Save();
        }


        public void Remove(WorkItem wi)
        {
            var a = this.WorkItems.FindAll(x => x._id == wi._id);
            var b = 3;

            this.WorkItems.RemoveAll(x => x._id == wi._id);
            this.Save();
        }
    }

    public class WorkItem
    {
        public WorkItem()
        {
        }

        public WorkItem(string title)
        {
            _id = ObjectId.NewObjectId();
            Title = title;
            Description = "No Description Provided";
            Priority = Priority.Normal;
        }

        public WorkItem(string title, string description, Priority priority)
        {
            Title = title;
            Description = description;
            Priority = priority;
        }

        public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{nameof(Title)}: {Title}, {nameof(Description)}: {Description}";
        }

        public string Color { get; set; }
        public ListType Type { get; set; }
        public Priority Priority { get; set; }
    }


    public enum Priority
    {
        High,
        Normal,
        Low
    }

    public enum ListType
    {
        Working,
        Done,
        TODO
    }
}