using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using FileMode = System.IO.FileMode;

namespace Kanaban.Classes
{


    public static class FileWatcher
    {

        public static FileSystemWatcher wa;

        internal static void StartWatching()
        {
            StartFileWatcher();
        }



        private static void StartFileWatcher()
        {
            if (Properties.Settings.Default.SharedDB.Length > 0)
            {
                var fi = new FileInfo(Properties.Settings.Default.SharedDB);
                var dir = fi.Directory;
                wa = new FileSystemWatcher(dir.FullName)
                {
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = true
                    ,
                    NotifyFilter = NotifyFilters.LastWrite
                };
                wa.Changed += OnWaOnChanged;
            }
            else
            {
                MessageBox.Show("Define Shared DB");
            }
        }

        private static int a = 4;
        private static void OnWaOnChanged(object sender, FileSystemEventArgs args)
        {
            a--;
            if (a == 0)
            {
                MainWindow.Instance.InvokeExternalDBChanged();
                a = 4;
            }
        }

        internal static void StopFileWatcher()
        {
            if (wa != null)
            {
                wa.Changed -= OnWaOnChanged;
                wa.Dispose();
            }
        }
    }



    public class Project
    {

        public Guid _id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DatabasePath { get; set; }

        public bool IsShared { get; set; }



        public static Project Load(string fileName)
        {
            try
            {
                using (Stream fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return (Project)new XmlSerializer(typeof(Project)).Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }



    }
}
