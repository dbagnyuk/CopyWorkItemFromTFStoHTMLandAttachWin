using System;
using System.Net;
using System.IO;
using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace CopyWorkItemFromTFStoHTMLandAttachWin
{
    public abstract class TFStoHTML
    {
        // save the configuration for operate with
        // domain and login save from config as is
        public static string DomainName = Config.config[0];
        // decrypting password from config
        public static string Password = Cipher.Decrypt(Config.config[1], Program.Key);
        // add the last '\' to path if it's not entered by user in config
        public static string PathToTasks = (Config.config[2].EndsWith("\\") ? Config.config[2] : Config.config[2] + "\\");

        // create web link for tfs id
        public static string tfsLink = null;
        // create path and name to html file
        public static string PathToHtml = null;
        // create path and folder name for attachments
        public static string PathToAttach = null;

        public static WorkItem workItem = null;
        public static WorkItemStore workItemStore = null;

        public static void connectToTFS ()
        {
            // catch the authentication error
            try
            {
                // create the connection to the TFS server
                NetworkCredential netCred = new NetworkCredential(DomainName, Password);
                Microsoft.VisualStudio.Services.Common.WindowsCredential winCred = new Microsoft.VisualStudio.Services.Common.WindowsCredential(netCred);
                VssCredentials vssCred = new VssCredentials(winCred);
                TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri("https://tfs.mtsit.com/STS/"), vssCred);

                tpc.Authenticate();

                workItemStore = tpc.GetService<WorkItemStore>();
                workItem = workItemStore.GetWorkItem(Program.itemId);

                // create web link for tfs id
                tfsLink = tpc.Uri + workItem.AreaPath.Remove(workItem.AreaPath.IndexOf((char)92)) + "/_workitems/edit/";
                // create path and name to html file
                PathToHtml = PathToTasks + workItem.Type.Name + " " + workItem.Id + ".html";
                // create path and folder name for attachments
                PathToAttach = PathToTasks + workItem.Id;
            }
            catch (Exception ex)
            {
                Program.exExit(ex);
            }
        }

        public static void writeTFStoHTML()
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;

            // create/open the html file for write in to it
            if (File.Exists(PathToHtml))
                fileStream = new FileStream(PathToHtml, FileMode.Truncate);
            else
                fileStream = new FileStream(PathToHtml, FileMode.CreateNew);
            streamWriter = new StreamWriter(fileStream);

            // fill in the html file
            // head, title, encoding
            streamWriter.WriteLine("{0}", "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">");
            streamWriter.WriteLine("{0}", "<html>");
            streamWriter.WriteLine("<head>{0}</head>", "<meta charset=\"UTF-8\">");
            streamWriter.WriteLine("<title>{0} {1}</title>", workItem.Type.Name, workItem.Id);
            streamWriter.WriteLine("{0}", "<body>");
            streamWriter.WriteLine("{0}", "");
            // short info block: 'name', 'id', 'title', 'state' and 'assigned to' info of work item
            streamWriter.WriteLine(@"<p><font style=""background-color:rgb(255, 255, 255); color:rgb(0, 0, 0); font-family:Segoe UI; font-size:12px;"">"
                                   + workItem.Type.Name + " " + workItem.Id + ": " + workItem.Title
                                   + @"</font><p>");
            streamWriter.WriteLine(@"<p style=""border: 1px solid; color: red; width: 50%;"">"
                                   + @"<font style=""background-color:rgb(255, 255, 255); color:rgb(0, 0, 0); font-family:Segoe UI; font-size:12px;"">"
                                   + workItem.Type.Name + " is <b>" + workItem.State
                                   + (workItem.State == "Closed" ? "</b>" : "</b> and Assigned To <b>" + workItem.Fields["Assigned To"].Value + "</b>")
                                   + @"</font><p>");
            // blok 'title'
            streamWriter.WriteLine(@"<div style=""border: 1px solid black; background-color:lightgray;"">TITLE:</div>");
            streamWriter.WriteLine("<p>{0}</p>", workItem.Title);
            // block 'description'
            streamWriter.WriteLine(@"<div style=""border: 1px solid black; background-color:lightgray;"">DESCRIPTION:</div>");
            if (workItem.Type.Name == "Bug" || workItem.Type.Name == "Issue")
                streamWriter.WriteLine(workItem.Fields["REPRO STEPS"].Value);
            else if (workItem.Type.Name == "Task")
                streamWriter.WriteLine(workItem.Fields["DESCRIPTION"].Value);
            // block 'history'
            streamWriter.WriteLine(@"<div style=""border: 1px solid black; background-color:lightgray;"">HISTORY:</div><br>");
            for (int i = workItem.Revisions.Count - 1; i >= 0; i--)
            {
                streamWriter.WriteLine(@"<font style=""background-color:rgb(255, 255, 255); color:rgb(0, 0, 0); font-family:Segoe UI; font-size:12px; font-weight:bold;"">"
                                       + workItem.Revisions[i].Fields["Changed By"].Value
                                       + @"</font><br>");
                if (workItem.Revisions[i].Fields["History"].Value.Equals(""))
                    streamWriter.WriteLine(workItem.Revisions[i].Fields["History"].Value);
                else
                    streamWriter.WriteLine(workItem.Revisions[i].Fields["History"].Value
                                           + "<br>");
                streamWriter.WriteLine(@"<font style=""background-color:rgb(255, 255, 255); color:rgb(128, 128, 128); font-family:Segoe UI; font-size:12px;"">"
                                       + "&nbsp;"
                                       + workItem.Revisions[i].Fields["State Change Date"].Value
                                       + @"</font><br><br>");
            }
            // block with linked work items. in table
            streamWriter.WriteLine(@"<div style=""border: 1px solid black; background-color:lightgray;"">ALL LINKS:</div>");
            streamWriter.WriteLine(@"<p><table style=""width:100%; font-family:Segoe UI; font-size:12px;"">");
            streamWriter.WriteLine(@"<tr><th align=""left"">Link Type</th>
                                         <th align=""left"">Work Item Type</th>
                                         <th align=""left"">ID</th>
                                         <th align=""left"">State</th>
                                         <th align=""left"">Title</th>
                                         <th align=""center"">Assigned To</th></tr>");
            foreach (WorkItemLink link in workItem.WorkItemLinks)
            {
                WorkItem wiDeliverable = workItemStore.GetWorkItem(link.TargetId);
                streamWriter.WriteLine(@"<tr><td>{0}</td>", link.LinkTypeEnd.Name);
                streamWriter.WriteLine(@"<td>{0}</td>", wiDeliverable.Type.Name);
                streamWriter.WriteLine(@"<td><a href=""{0}{1}"">{1}</a></td>", tfsLink, wiDeliverable.Id);
                streamWriter.WriteLine(@"<td>{0}</td>", wiDeliverable.State);
                streamWriter.WriteLine(@"<td>{0}</td>", wiDeliverable.Title);
                streamWriter.WriteLine(@"<td>{0}</td></tr>", wiDeliverable.Fields["Assigned To"].Value);
            }
            streamWriter.WriteLine(@"</table></p>");
            // block with the web link to the thin client on to work item
            streamWriter.WriteLine(@"<div style=""border: 1px solid black; background-color:lightgray;"">LINK:</div>");
            streamWriter.WriteLine(@"<p><a href=""{0}{1}"">{0}{1}</a><p>", tfsLink, workItem.Id);

            // create the path to directory for saving attachments and search if the dir alredy exist
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(PathToTasks);
            FileSystemInfo[] filesAndDirs = hdDirectoryInWhichToSearch.GetFileSystemInfos("*" + workItem.Id + "*");

            // if folder for attach alredy exists change the the default name to it
            foreach (FileSystemInfo foundDir in filesAndDirs)
                if (foundDir.GetType() == typeof(DirectoryInfo))
                    PathToAttach = foundDir.FullName;

            // if folder exists, add the link to it
            if (Directory.Exists(PathToAttach) || Program.downConfirm)
            {
                // block with link to folder with attacments
                streamWriter.WriteLine(@"<div style=""border: 1px solid black; background-color:lightgray;"">ATTACHMENTS:</div>");
                streamWriter.WriteLine(@"<p><a href=""{0}"">{0}</a><p>", PathToAttach);
            }

            streamWriter.WriteLine("{0}", "</body>");
            streamWriter.WriteLine("{0}", "</html>");

            streamWriter.Close();
            fileStream.Close();

            // download the attachments from tfs item
            if (Program.downConfirm)
                downloadAttach();

            // open the created html file, will be open by default app for html files
            System.Diagnostics.Process.Start(PathToHtml);
        }

        public static void downloadAttach()
        {
            // catch the error with download the attacments
            try
            {
                // if folder is not exists, create it
                if (!Directory.Exists(PathToAttach))
                Directory.CreateDirectory(PathToAttach);

                // Get a WebClient object to do the attachment download
                WebClient webClient = new WebClient()
                {
                    UseDefaultCredentials = true
                };

                // Loop through each attachment in the work item.
                foreach (Attachment attachment in workItem.Attachments)
                {
                    // Construct a filename for the attachment
                    string filename = string.Format("{0}\\{1}", PathToAttach, attachment.Name);
                    // Download the attachment.
                    webClient.DownloadFile(attachment.Uri, filename);
                }
            }
            catch (Exception ex)
            {
                Program.exExit(ex);
            }
        }
    }
}
