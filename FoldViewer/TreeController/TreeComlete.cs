using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FoldViewer.TreeController
{
    class TreeComlete
    {
        public void TreeStartComplete(Tree rootTree)
        {
            DriveInfo[] myDrives = DriveInfo.GetDrives();
           var directory =  myDrives.Where(x => x.IsReady)
                .Select(x => new DirectoryInfo(x.ToString()).EnumerateDirectories().ToList()
           /*.Select(y => y)
           .Where(y => (y.Attributes & FileAttributes.Hidden) == 0)*/)
           .ToList();
            var dir = myDrives.Where(x => (x.IsReady) && (x.Name != "C:\\" ) ).ToList();
            var dirNodes = dir.Select(x => new TreeNode(null,new DirectoryInfo(x.ToString()))).ToList();
            rootTree.ChildNodes = dirNodes;
            // directory
            foreach (var subDir in rootTree.ChildNodes)
            {
               SetTreeNode(subDir);
            }
           


        }

        public void SetTreeNodeInfo(TreeNode treeNode)
        {

            treeNode.ChildNodes =
                treeNode.TreeNodeDirectoryInfo.EnumerateDirectories().Select(x => new TreeNode(treeNode, x)).ToList();
            treeNode.FolderFullPath = treeNode.TreeNodeDirectoryInfo.FullName;
            treeNode.FolderName = treeNode.TreeNodeDirectoryInfo.Name;
        }

        public void SetTreeNode(TreeNode treeNode)
        {
           // if ((treeNode.TreeNodeDirectoryInfo.Attributes & FileAttributes.Hidden) == 0)
            if (treeNode.TreeNodeDirectoryInfo.Attributes != FileAttributes.System)
            {
                SetTreeNodeInfo(treeNode);
                foreach (var childNodes in treeNode.ChildNodes)
                {
                    SetTreeNode(childNodes);
                }
            }
        }
       
    }
}
