using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using FoldViewer.TreeController;

namespace FoldViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // TestWork();
            Tree rootTree = new Tree();
            TreeComlete a = new TreeComlete();
            a.TreeStartComplete(rootTree);
        }
        public void TestWork()
        {
            DriveInfo[] myDrives = DriveInfo.GetDrives();
            string[] drives = Directory.GetLogicalDrives();
           // string[] directories =   DirectoryInfo.GetDirectories(myDrives[0].ToString());
            DirectoryInfo directories = new DirectoryInfo(myDrives[0].ToString());
            var QTE = directories.EnumerateDirectories();
            
            foreach (var dir in QTE)
            {
                
                if ((dir.Attributes & FileAttributes.Hidden ) == 0)
                {
                    TextBox1.Text += dir.Name;
                    TextBox1.Text += " - " + dir.Attributes;
                    TextBox1.Text += "\n";
                    var filesList = dir.EnumerateFiles();
                    /* System.IO.FileInfo[] files =  dir.GetFiles().ToArray();*/
                    foreach (var file in filesList)
                    {
                        //MessageBox.Show(file.Name);
                        TextBox1.Text += file.Name;
                        TextBox1.Text += "\n";
                       
                    }
                }
               //  var acces =  dir.GetAccessControl().AccessRightType;
                

            }
            var SPC = directories.GetDirectories();
        }

        public void CreateDirectoryCollection()
        {
            

        }
        /// <summary>
        /// элемент дерева папок - одна папка
        /// </summary>
        public class TreeVIewOfDirectory
        {
            public long DirectoryLength { get; private set; }
            public bool IsDirectoryLengthComplete { get; private set; }
            public string DirectoryFullPath { get; private set; }
            public string DirectoryName { get; private set; }

            public List<TreeVIewOfDirectory> ChildrenDirectories { get; private set; }

            public TreeVIewOfDirectory(string name, string directoryPath)
            {
                DirectoryName = name;
                DirectoryFullPath = directoryPath;
            }


            /// <summary>
            /// Получить список поддиректориий (папок в папке)
            /// </summary>
            public void GetChildrenDirectories()
            {
                DirectoryInfo currentDirectory = new DirectoryInfo(DirectoryFullPath);
                var directories = currentDirectory.EnumerateDirectories();
                ChildrenDirectories.Clear();                                                         //на всякий случай очищаем коллекцию, т.к. в дальнейшем идет только добавление элементов
                foreach (var dir in directories)
                {
                    if ((dir.Attributes & FileAttributes.Hidden) == 0) 
                    {
                        ChildrenDirectories.Add(new TreeVIewOfDirectory(dir.Name, dir.FullName));
                    }
                }
                //ChildrenDirectories = directories;
            }
            /// <summary>
            /// получить размер папок если известен размер всех подпапок
            /// </summary>
            public void GetDirectoryLength()
            {
                if (ChildrenDirectories.Count == 0)
                {

                }
                else
                {
                    foreach (var dir in ChildrenDirectories)
                    {
                        if (dir.IsDirectoryLengthComplete)
                        {
                            DirectoryLength += dir.DirectoryLength;
                        }
                    }
                }
            }
        }

        /*public class HashTableWork
        {
            public 
        }*/
    }
}
