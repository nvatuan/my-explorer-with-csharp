using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Collections;
using System.Configuration;
using System.Net.Configuration;

namespace MyExplorer
{
    public partial class MyExplorer : Form
    {
        // -- short-hand Methods
        public string GetPwd() { return Directory.GetCurrentDirectory(); }
        public void SetPwd(string Path) { Directory.SetCurrentDirectory(Path); }
        public string GetDriveLabel() { return Path.GetPathRoot(this.GetPwd()); }
        public void ClearSearchBox() {
            this.SearchBox.Font = new Font(this.SearchBox.Font, FontStyle.Regular);
            this.SearchBox.Text = "";
        }
        // -- directory crawling with bfs related:
        // Dictionary mapping DirPath -> dirs,files at that DirPath
        Dictionary<string, string[]> DirsAt = new Dictionary<string, string[]>();
        Dictionary<string, string[]> FilesAt = new Dictionary<string, string[]>();
        // Dictionary mapping DirPath -> TreeNode that represents that folder in TreeView
        Dictionary<string, TreeNode> PathToNodeMap = new Dictionary<string, TreeNode>();
        Dictionary<TreeNode, string> NodeToPathMap = new Dictionary<TreeNode, string>();

        // -- Tag class for ListViewItem
        public class ListViewItemTag {
            public string Type { get; }
            public string Path { get; }
            public ListViewItemTag(string T, string P) { Type = T; Path = P; }

        }

        // This function uses BFS to crawl for StartingPath's content
        // doesn't allow crawling at depth lower than MaxAllowedDepth
        // MaxAllowedDepth is set to -1 by default, meaning allow infinite depth.
        public void BuildDirTreeBFS(string StartingPath, int MaxAllowedDepth = -1) {
            // -- initialize
            Queue<KeyValuePair<string, int>> BFSQueue = new Queue<KeyValuePair<string, int>>();
            BFSQueue.Enqueue(new KeyValuePair<string, int>(StartingPath, 0));

            // -- check if parent entry is on Node-Path tables, if not add them
            if ( PathToNodeMap.ContainsKey(StartingPath) == false ) {
                TreeNode tmpNode = new TreeNode(new DirectoryInfo(StartingPath).Name);
                tmpNode.Name = StartingPath;
                //
                DirTree.Nodes.Add(tmpNode);
                PathToNodeMap[StartingPath] = tmpNode;
                NodeToPathMap[tmpNode] = StartingPath;
            }

            // -- bfs
            while (BFSQueue.Count > 0) {
                // -- initialize
                string[] SubDirs = new string[0];
                string[] SubFiles = new string[0];

                // -- retrieve front element and pop
                string BFSPath = BFSQueue.Peek().Key;
                int BFSDepth = BFSQueue.Peek().Value;
                TreeNode BFSParent = PathToNodeMap[BFSPath];
                BFSQueue.Dequeue();

                // -- not allow too deep crawling
                if (BFSDepth == MaxAllowedDepth) continue;

                // -- trying retrieve folders and expand bfs queue
                try {
                    // -- Get content, push to table, enqueue entries.
                    SubDirs = Directory.GetDirectories(BFSPath);
                    DirsAt[BFSPath] = SubDirs;
                    foreach (string subdir in SubDirs) BFSQueue.Enqueue(new KeyValuePair<string, int>(subdir, BFSDepth + 1));
                    // do the same with files but without enqueuing
                    SubFiles = Directory.GetFiles(BFSPath);
                    FilesAt[BFSPath] = SubFiles;
                } catch (UnauthorizedAccessException) {
                    continue;
                } catch (Exception e) {
                    Console.WriteLine("$$ Unexpected exception " + e.ToString());
                    Console.WriteLine("$$ at " + BFSPath);
                    continue;
                }

                // -- trying to write entries to Path - Node hash tables
                try {
                    if (BFSParent.Nodes.Count != 0) continue;
                    foreach (string subdir in SubDirs) {
                        TreeNode child = new TreeNode((new DirectoryInfo(subdir)).Name);
                        child.Name = subdir;

                        BFSParent.Nodes.Add(child);
                        PathToNodeMap[subdir] = child;
                        NodeToPathMap[child] = subdir;
                        //
                    }
                } catch (Exception e) {
                    Console.WriteLine("$$ Unexpected exception " + e.ToString());
                    Console.WriteLine("$$ at " + BFSPath);
                    continue;
                }
            }
        }
        private void UpdateDirBrowser(string Path) {
            this.DirBrowser.Items.Clear();
            
            // -- dealing with Directory entries
            string[] Entries = null;
            try {
                DirsAt.TryGetValue(Path, out Entries);
                if (Entries != null) 
                    foreach (string subfile in Entries) {
                        ListViewItem item = new ListViewItem(new DirectoryInfo(subfile).Name, 1);
                        item.Name = item.Text;
                        item.Tag = new ListViewItemTag("Dir", subfile);
                        this.DirBrowser.Items.Add(item);
                    }
            } catch (ArgumentNullException ArgNullEx) {
                Console.WriteLine("$$ " + ArgNullEx.ToString());
                Console.WriteLine("$$ key is null.");
            } catch (Exception Ex) {
                Console.WriteLine("$$ " + Ex.ToString());
                Console.WriteLine("$$ Unrecognized exception.");
            }

            // -- dealing with File entries
            Entries = null;
            try {
                FilesAt.TryGetValue(Path, out Entries);
                if (Entries != null)
                    foreach (string subfile in Entries) {
                        ListViewItem item = new ListViewItem(new DirectoryInfo(subfile).Name, 0);
                        item.Name = item.Text;
                        item.Tag  = new ListViewItemTag("File", subfile); ;
                        this.DirBrowser.Items.Add(item);
                    }
            } catch (ArgumentNullException ArgNullEx) {
                Console.WriteLine("$$ " + ArgNullEx.ToString());
                Console.WriteLine("$$ key is null.");
            } catch (Exception Ex) {
                Console.WriteLine("$$ " + Ex.ToString());
                Console.WriteLine("$$ Unrecognized exception.");
            }
        }
        private void ActOnItemOpened(ListViewItem item) {
            ListViewItemTag currentTag = ((ListViewItemTag)item.Tag);
            if (currentTag == null) MessageBox.Show("File/Folder's tag is Null.", "Error");
            else
            switch ( currentTag.Type ) {
                case "Dir":
                        try {
                            this.SetPwd(currentTag.Path);
                            this.ClearSearchBox();
                            BuildDirTreeBFS(this.GetPwd(), 1);
                            UpdateDirBrowser(this.GetPwd());
                            WorkingDir.Text = this.GetPwd();
                            PathToNodeMap[this.GetPwd()].Expand();
                        } catch (UnauthorizedAccessException) {
                            MessageBox.Show("Unauthorized Access to Directory", "Error: Not allowed access");
                            return;
                        } catch (Exception) {
                            MessageBox.Show("Unexpected exception caught.", "Error");
                            return;
                        }
                    break;
                case "File":
                    MessageBox.Show($"File name: \"{item.Name}\" \nFile path: \"{currentTag.Path}\"",
                        "File Open");
                    break;
                default:
                    break;
            } 
        }
        // ------------------ ENTRANCE --------------------
        public MyExplorer(string StartingPath) {
            if (Directory.Exists(StartingPath)) this.SetPwd(StartingPath);
            else this.SetPwd(@"D:\");
            // -- Init
            InitializeComponent();
            //
            BuildDirTreeBFS(this.GetPwd(), 1);
            WorkingDir.Text = this.GetPwd();

            ImageList largeImageList = new ImageList();
            ImageList smallImageList = new ImageList();
            Console.WriteLine(Directory.GetCurrentDirectory());
            largeImageList.Images.Add(new Bitmap(Properties.Resources.File_512));
            largeImageList.Images.Add(new Bitmap(Properties.Resources.Folder_512));
            smallImageList.Images.Add(new Bitmap(Properties.Resources.File_256));            
            smallImageList.Images.Add(new Bitmap(Properties.Resources.Folder_256));
            
            
            DirBrowser.LargeImageList = largeImageList;
            DirBrowser.SmallImageList = largeImageList;

            // -- Testing Adding items to ListView
            ListViewItem item;
            item = new ListViewItem("TEST_FOLDER", 1);
            DirBrowser.Items.Add(item);
            item = new ListViewItem("TEST_FILE", 0);
            DirBrowser.Items.Add(item);
            // -- End testing

        }

        // ------------------ END MAIN --------------------
        // -- Event handling
        private void WorkingDir_KeyPress(object sender, KeyPressEventArgs e) {
            string Text = ((TextBox)sender).Text;
            char KeyPressed = e.KeyChar;
            if (KeyPressed == (char) Keys.Return) {
                if (Directory.Exists(Text)) {
                    // -- Set and Get to get correctly formatted path
                    try {
                        this.SetPwd(Text); Text = this.GetPwd();
                        ((TextBox)sender).Text = Text;
                        this.ClearSearchBox();
                    } catch (UnauthorizedAccessException) {
                        MessageBox.Show("Unauthorized Access to Directory", "Error: Not allowed access");
                        return;
                    } catch (Exception) {
                        MessageBox.Show("Unexpected exception caught.", "Error");
                        return;
                    }
                    //
                    // expand root node
                    PathToNodeMap[this.GetDriveLabel()].Expand();

                    // first slash after the Drive label
                    int PathCurrentSlashIndex = Text.IndexOf("\\", 0);

                    // second and forward slashes
                    while (true) {
                        int PathNextSlashIndex = Text.IndexOf("\\", PathCurrentSlashIndex + 1);
                        if (PathNextSlashIndex == -1) {
                            this.SetPwd(Text); BuildDirTreeBFS(Text, 1);
                            UpdateDirBrowser(this.GetPwd());
                            // 
                            break;
                        }
                        // -- set and get to format path
                        string SubPath = Text.Substring(0, PathNextSlashIndex);
                        this.SetPwd(SubPath); SubPath = this.GetPwd();
                        // exploring the Path each depth at 
                        BuildDirTreeBFS(SubPath, 1);
                        PathToNodeMap[SubPath].Expand();
                        // Move Index forward
                        PathCurrentSlashIndex = PathNextSlashIndex;
                    }
                } else {
                    MessageBox.Show($"Path \"{Text}\" does not exist.");
                    ((TextBox)sender).Text = this.GetPwd();
                }
                //MessageBox.Show("@CURRENT_WORKING_DIR:" + Text);                
            } else
            if (KeyPressed == (char)Keys.Escape) {
                //MessageBox.Show("Escape @ CURRENT_WORKING_DIR");
                ((TextBox)sender).Text = this.GetPwd();
            }
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e) {
            string Text = ((TextBox)sender).Text;
            char KeyPressed = e.KeyChar;
            SearchBox.Font = new Font(SearchBox.Font, FontStyle.Regular);

            // -- reset the browser first
            //UpdateDirBrowser(this.GetPwd());
            //
            if (KeyPressed == (char)Keys.Return) {
                //MessageBox.Show("@SEARCH_BOX:" + Text);
                UpdateDirBrowser(this.GetPwd());
                SearchBox.Font = new Font(SearchBox.Font, FontStyle.Italic);
                //
                Text.Trim(new char[] { '\n' });
                foreach (ListViewItem item in DirBrowser.Items)
                    if (item.Name.StartsWith(Text) == false)
                        DirBrowser.Items.Remove(item);
            } else
            if (KeyPressed == (char)Keys.Escape) {
                ((TextBox)sender).Text = "";
                UpdateDirBrowser(this.GetPwd());
                //MessageBox.Show("Escape @ SEARCH_BOX"); 
            }
        }

        // -- Explorer the content before expanding the tree view
        private void DirTree_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            BuildDirTreeBFS(e.Node.Name, 2);
        }

        // -- Move to the selected treeNode path
        private void DirTree_AfterSelect(object sender, TreeViewEventArgs e) {
            try {
                this.SetPwd(e.Node.Name);
                UpdateDirBrowser(e.Node.Name);
                this.WorkingDir.Text = e.Node.Name;
                this.ClearSearchBox();
            } catch (UnauthorizedAccessException) {
                MessageBox.Show("Unauthorized Access to Directory", "Error: Not allowed access");
                return;
            } catch (Exception) {
                MessageBox.Show("Unexpected exception caught.", "Error");
                return;
            }
        }

        // -- Double-clicking on an item
        private void DirBrowser_ItemActivate(object sender, EventArgs e) {
            /*
            var selectedItems = DirBrowser.SelectedItems;
            string itemsName = "";
            foreach (ListViewItem item in selectedItems) {
                itemsName += "|" + item.Name + "|";
                selectedItems.ToString();
            }
            MessageBox.Show("Item(s) Activated: " + itemsName);
            */
            // -- NOTE: user can select multiple items when the ItemActivate event occurs
            // dealing with this is a bit troubling so i will just pick the first item in the list.
            // ListViewItem firstItem = DirBrowser.SelectedItems[0];
            // -- NOTE 2: I found ListView.FocusedItem which does the exact thing i want, 
            // only 1 item highlighted at the moment
            ListViewItem focusedItem = DirBrowser.FocusedItem;
            if (focusedItem == null) MessageBox.Show("Attempting to act on a focused item but none is found!", "Error: No focused item found");
            else ActOnItemOpened(focusedItem);
        }

        // -- Testing event.., no actual uses.
        private void DirBrowser_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e) {
            //string hoveredItem = e.Item.Text;
            //MessageBox.Show(hoveredItem + "is hovered.");
        }

        // -- Display Context Menu (on Item or on Pane) on mouse right click
        private ListView.SelectedListViewItemCollection __selectedItems;
        private ListViewItem __focusedItem;
        private void DirBrowser_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (DirBrowser.FocusedItem != null && DirBrowser.FocusedItem.Bounds.Contains(e.Location)) {
                    __selectedItems = DirBrowser.SelectedItems;
                    __focusedItem = DirBrowser.FocusedItem;
                    ListViewItemContextMenu.Show(Cursor.Position);
                    return;
                } else {
                    ListViewContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            string msg = "", title = "";
            if (__selectedItems != null) {
                title = "Copy successful.";
                msg = "Item(s) copied: ";
                foreach (ListViewItem item in __selectedItems) {
                    msg += $"[{item.Name}]";
                }
            } else {
                title = "Error: Copy failed.";
                msg = "Selected target could not be found.";
            }
            MessageBox.Show(msg, title);
            if (DirBrowser.SelectedItems != null)
                DirBrowser.SelectedItems.Clear();
            if (DirBrowser.FocusedItem != null)
                DirBrowser.FocusedItem.Focused = false;
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e) {
            string msg = "", title = "";
            if (__selectedItems != null) {
                title = "Copy successful.";
                msg = "Item(s) deleted: ";
                foreach (ListViewItem item in __selectedItems) {
                    msg += $"[{item.Name}]";
                }
                foreach (ListViewItem item in __selectedItems) {
                    DirBrowser.Items.Remove(item);
                }
            } else {
                title = "Error: Delete failed";
                msg = "Selected target could not be found.";
            }
            MessageBox.Show(msg, title);
            if (DirBrowser.SelectedItems != null)
                DirBrowser.SelectedItems.Clear();
            if (DirBrowser.FocusedItem != null) 
                DirBrowser.FocusedItem.Focused = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (__focusedItem != null) {
                ActOnItemOpened(__focusedItem);
            } else {
                MessageBox.Show("Focused item could not be found.", "Error: Open failed");
            }
            if (DirBrowser.SelectedItems != null)
                DirBrowser.SelectedItems.Clear();
            if (DirBrowser.FocusedItem != null)
                DirBrowser.FocusedItem.Focused = false;
        }

        private void goToParentFolderToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                string Path = this.GetPwd();
                if (Path == this.GetDriveLabel()) {
                    MessageBox.Show("This is root directory. Cannot go any upper.", "Error: Current folder is Root");
                    return;
                }
                Path = Path.Substring(0, Path.LastIndexOf('\\'));
                
                Path += "\\";
                this.SetPwd(Path);
                this.ClearSearchBox();
                // re-get so it formats the path string
                Path = this.GetPwd();

                //MessageBox.Show(Path);
                UpdateDirBrowser(Path);
                this.WorkingDir.Text = Path;
            } catch (UnauthorizedAccessException) {
                MessageBox.Show("Unauthorized Access to Directory", "Error: Not allowed access");
                return;
            } catch (Exception) {
                MessageBox.Show("Unexpected exception caught.", "Error");
                return;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e) {
            ListViewItem item = new ListViewItem("New File", 0);
            item.Name = item.Text;
            item.Tag = new ListViewItemTag("File", this.GetPwd() + "\\New File");
            DirBrowser.Items.Add(item);
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e) {
            ListViewItem item = new ListViewItem("New Folder", 1);
            item.Name = item.Text;
            item.Tag = new ListViewItemTag("Dir", this.GetPwd() + "\\New Folder");
            DirBrowser.Items.Add(item);
        }
    }
}
