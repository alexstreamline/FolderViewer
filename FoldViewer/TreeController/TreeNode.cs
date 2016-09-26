using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FoldViewer.TreeController
{
   public class TreeNode
    {
        public TreeNode ParentNode { get; set; }
        public List<TreeNode> ChildNodes { get; set; }
        public DirectoryInfo TreeNodeDirectoryInfo { get; set; }
        public int? FolderSize { get; set; } = null;
        public string FolderFullPath { get; set; }
        public string FolderName { get; set; }

        public TreeNode(TreeNode parentNode)
        {
            ParentNode = parentNode;
        }
        public TreeNode(TreeNode parentNode, DirectoryInfo treeNodeDirectoryInfo)
        {
            ParentNode = parentNode;
            TreeNodeDirectoryInfo = treeNodeDirectoryInfo;
        }
        public TreeNode(TreeNode parentNode, string folderName, string folderFullPath)
        {
            ParentNode = parentNode;
            FolderName = folderName;
            FolderFullPath = folderFullPath;
        }

        public TreeNode(TreeNode parentNode, List<TreeNode> childNodes)
        {
            ParentNode = parentNode;
            ChildNodes = childNodes;
        }
        public TreeNode(TreeNode parentNode, List<TreeNode> childNodes, string fullPath)
        {
            ParentNode = parentNode;
            ChildNodes = childNodes;
            FolderFullPath = fullPath;
        }

        public void AddChildNode(TreeNode childNode)
        {
            ChildNodes.Add(childNode);
        }

        public long GetFolderSize(TreeNode treeNode)
        {
            long size = 0;
           if (treeNode.ChildNodes.Count == 0)
           {
             size = treeNode.TreeNodeDirectoryInfo
                    .GetFiles()
                    .Select(x => x.Length)
                    .Sum();
           }
            //int size = 0;
            //treeNode.ChildNodes.Select(x => x.GetFolderSize(x)).Select(x => size+=x);
            return size;
        }
    }
}
