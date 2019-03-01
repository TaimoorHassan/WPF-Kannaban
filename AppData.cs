using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Kanaban.Classes;
using LiteDB;
using FileMode = System.IO.FileMode;

namespace Kanaban
{
    public class AppData
    {
        public static string ArchiveDir = Environment.CurrentDirectory + @"\Archive";

        public static event ProjectChangedEventHandler ProjectChanged;
        //cahnge
        private static  LiteDatabase            db             = new LiteDatabase("KanabanDB");
        internal static LiteCollection<Project> Projects       = db.GetCollection<Project>("Projects");
        internal static LiteCollection<Project> ProjectArchive = db.GetCollection<Project>("ProjectArchive");

        internal static void LoadNewProject(Project project)
        {

            try
            {
                DB.InitializeDatabase(project.DatabasePath);
                ProjectChanged?.Invoke(project);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void UnloadProject()
        {
            DB.dbfile.Dispose();
            ProjectChanged?.Invoke(null);
        }
    }



    public class FileMover
    {
        public static string GetAbsolutePath(string str)
        {
            return str.Contains(@":\") ? str : $"{Environment.CurrentDirectory}\\{str}";
        }

        public static async Task Copy(string input, string output)
        {
            await Task.Run(() =>
            {
                var inputFilePath = GetAbsolutePath(input);
                var outputFilePath = GetAbsolutePath(output);
                int bufferSize = 1024 * 1024;

                using (FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write,
                        FileShare.ReadWrite))
                    //using (FileStream fs = File.Open(<file-path>, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.ReadWrite);
                    fileStream.SetLength(fs.Length);
                    int bytesRead = -1;
                    byte[] bytes = new byte[bufferSize];

                    while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
                    {
                        fileStream.Write(bytes, 0, bytesRead);
                    }
                }
            });
        }
    }
}