using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace USB_Bootable
{
    class Program
    {
        static string input, conti, path, path1;

        static List<Volume> drives = new List<Volume>();

        static Volume currentVolume;

        static int currentIndex;

        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                drives.Clear();
                RunCommand(@"List Volume", out path);
                int st = path.IndexOf("\r\n", path.IndexOf("DISKPART"));
                string table = path.Split(new string[] { "DISKPART>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var rows = table.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Header header = new Header(rows[2]);

                foreach (string row in rows.Skip(3))
                {
                    Volume volume = new Volume();

                    volume.Index = row.Substring(header.Index, header.Ltr - header.Index).Trim();

                    volume.Ltr = row.Substring(header.Ltr, header.Label - header.Ltr).Trim();

                    volume.Label = row.Substring(header.Label, header.Fs - header.Label).Trim();

                    volume.Fs = row.Substring(header.Fs, header.Type - header.Fs).Trim();

                    volume.Type = row.Substring(header.Type, header.Size - header.Type).Trim();

                    volume.Size = row.Substring(header.Size, header.Status - header.Size).Trim();

                    volume.Status = row.Substring(header.Status, header.Info - header.Status).Trim();

                    volume.Info = row.Substring(header.Info).Trim();

                    drives.Add(volume);
                }

                do
                {
                    Console.WriteLine("Enter Volume Number/Name   ");
                    input = Console.ReadLine();
                    currentIndex = drives.FindIndex(a => a.Ltr == input || a.Index.Replace("Volume ", "") == input);
                }
                while (currentIndex == -1 || string.IsNullOrWhiteSpace(input));
                currentVolume = drives[currentIndex];
                Console.WriteLine("Selected Volume " + input + "\tType " + currentVolume.Type);
                do
                {
                    Console.WriteLine("Do You Want to continue?(Y/N)");
                    conti = Console.ReadLine();
                }
                while (conti.ToLower() != "y" && conti.ToLower() != "n");
                if (conti.ToLower() == "n")
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            RunCommand(@"Select Volume " + input + "\r\nclean\r\ncreate part pri\r\nselect part 1\r\nformat fs=fat32 quick\r\nactive", out path1);
            using (OpenFileDialog fileDialog = new OpenFileDialog() { Multiselect = false, Filter = "Image files (*.iso)|*.iso|All files (*.*)|*.*" })
            {
                if (DialogResult.OK == fileDialog.ShowDialog())
                {
                    Copy(fileDialog.FileName, currentVolume.Ltr + "\\" + fileDialog.SafeFileName);
                    Console.WriteLine("Copy Done.");
                    //File.Copy(fileDialog.FileName, currentVolume.Ltr + ":\\" + fileDialog.SafeFileName);
                }
            };
        }

        private static void Web_DownloadProgressChanged(double percent)
        {
            Console.WriteLine(percent.ToString("N2") + "%");
        }

        private static bool RunCommand(string cmd, out string output)
        {
            Process p = new Process();
            p.StartInfo.FileName = "diskpart.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.StandardInput.WriteLine(cmd);
            p.StandardInput.WriteLine("exit");
            output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            Console.WriteLine(output);
            return string.IsNullOrWhiteSpace(error);
        }

        private static void Copy(string SourceFilePath, string DestFilePath)
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            bool cancelFlag = false;

            using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;
                using (FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double persentage = (double)totalBytes * 100.0 / fileLength;

                        dest.Write(buffer, 0, currentBlockSize);

                        cancelFlag = false;
                        Web_DownloadProgressChanged(persentage);

                        if (cancelFlag == true)
                        {
                            // Delete dest file here
                            break;
                        }
                    }
                }
            }

        }


    }

    class Volume
    {
        public string Index { get; set; }
        public string Ltr { get; set; }
        public string Label { get; set; }
        public string Fs { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string Info { get; set; }
    }

    class Header
    {
        public Header(string data)
        {
            Index = data.IndexOf("-");
            Ltr = data.IndexOf(" -", Index) + 1;
            Label = data.IndexOf(" -", Ltr) + 1;
            Fs = data.IndexOf(" -", Label) + 1;
            Type = data.IndexOf(" -", Fs) + 1;
            Size = data.IndexOf(" -", Type) + 1;
            Status = data.IndexOf(" -", Size) + 1;
            Info = data.IndexOf(" -", Status) + 1;
        }
        public int Index { get; private set; }
        public int Ltr { get; private set; }
        public int Label { get; private set; }
        public int Fs { get; private set; }
        public int Type { get; private set; }
        public int Size { get; private set; }
        public int Status { get; private set; }
        public int Info { get; private set; }
    }
}
