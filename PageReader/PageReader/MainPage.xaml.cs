using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Core;
using Windows.Media.Editing;
using Windows.Media.MediaProperties;
using Windows.Media.SpeechSynthesis;
using Windows.Media.Transcoding;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PageReader
{
    #region public enums

    public enum MediaFileType { avi, m4a, mp3, mp4, wav, wma, wmv }

    #endregion

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        #region private fields

        private string _sources;

        private string _text;

        private string _fileName;

        private List<string> _content = new List<string>();

        private byte[] buffer;

        private double percent;

        private CultureInfo cultureInfo = new CultureInfo("en-US");

        private string _SSML_Header = "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"", _SSML_Footer = "</speak>";

        private StringBuilder stringBuilder = new StringBuilder();

        private List<TimeSpan> _trackLength = new List<TimeSpan>();

        private StorageFolder knownFolder = KnownFolders.VideosLibrary;

        private StorageFolder saveFolder;

        private CancellationTokenSource cancellationTokenSource;

        private Dictionary<string, string> _itemSource = new Dictionary<string, string>();

        private TimeSpan subTimeSpan = new TimeSpan();

        private SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.SkyBlue);

        private SolidColorBrush invertSolidColorBrush = new SolidColorBrush(Colors.Beige);

        private List<StorageFile> audioVideo = new List<StorageFile>();

        #endregion

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region public properties

        public List<string> ContentList
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        public string Sources
        {
            get
            {
                return _sources;
            }
            set
            {
                if (_sources != value)
                {
                    _sources = value;
                    ContentList.Clear();
                    value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(a =>
                    {
                        ContentList.AddRange(getList(a));
                        return ContentList;
                    }).ToList();
                    ContentList.RemoveAll(a => a.Trim().Length == 0);
                }
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                }
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }

        public List<TimeSpan> TrackLength
        {
            get
            {
                return _trackLength;
            }

            set
            {
                _trackLength = value;
            }
        }

        public Dictionary<string, string> ItemSource
        {
            get
            {
                return _itemSource;
            }

            set
            {
                if (_itemSource != value)
                {
                    _itemSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ItemSource"));
                }
            }
        }

        #endregion

        #region public constructor

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            Save.IsEnabled = false;
        }

        #endregion

        #region private methods

        #region private sync methods

        private string GetSSML(List<string> items)
        {
            stringBuilder = new StringBuilder(_SSML_Header + cultureInfo.Name + "\" >. <break time=\"1s\"/>");
            foreach (var item in items)
            {
                stringBuilder.Append(item + "<mark name=\"" + items.IndexOf(item) + "\" />");
            }
            stringBuilder.Append(_SSML_Footer);
            return stringBuilder.ToString();
        }

        private List<string> getList(string item, int charCount = 50)
        {
            List<string> innerSubList = new List<string>();
            if (item.Length > charCount)
            {
                for (int start = 0, length = charCount; start < item.Length;)
                {
                    length = item.IndexOf(' ', length);
                    length = (length == -1 ? item.Length : length);
                    string temp = item.Substring(start, length - start);
                    innerSubList.Add(temp);
                    start = length;
                    length = (item.Length - start > charCount) ? start + charCount : item.Length;
                }
                return innerSubList;
            }
            else
            {
                innerSubList.Add(item);
            }
            return innerSubList;
        }

        private void ReportProgress(Ellipse ellipse, ProgressBar progressBar, TextBlock textResult, double value, Ellipse ellipse1 = null)
        {
            if (ellipse1 != null)
            {
                ellipse1.Fill = solidColorBrush;
            }
            if (progressBar != null && textResult != null && value > 0)
            {
                progressBar.Value = value;
                textResult.Text = string.Format("{0:0.00}", value) + "% done.";
            }
            if (value == 100)
            {
                ellipse.Fill = solidColorBrush;
            }
        }

        private void CancelTask(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        private void ResetStages(SolidColorBrush color, double value, string text)
        {
            Stage1.Fill = color;
            Stage1ProgressBar.Value = value;
            TextProgress1.Text = text;
            Stage2.Fill = color;
            Stage2ProgressBar.Value = value;
            TextProgress2.Text = text;
            Stage3.Fill = color;
            Stage3ProgressBar.Value = value;
            TextProgress3.Text = text;
            Stage4.Fill = color;
            Stage4ProgressBar.Value = value;
            TextProgress4.Text = text;
            Stage5.Fill = color;
            Stage5ProgressBar.Value = value;
            TextProgress5.Text = text;
            Stage6.Fill = color;
            Stage6ProgressBar.Value = 0;
            TextProgress6.Text = "";
            Stage7.Fill = invertSolidColorBrush;
        }

        #endregion

        #region private async methods

        private async Task ClearTempData(string except = null)
        {
            try
            {
                MediaElement1?.Stop();
                IReadOnlyList<StorageFolder> storageFolder = await knownFolder.GetFoldersAsync();
                foreach (var item in storageFolder)
                {
                    if (item.Name != except)
                    {
                        await item.DeleteAsync(StorageDeleteOption.PermanentDelete);
                    }
                }
            }
            catch (Exception) { }
        }

        private async Task<StorageFile> GetFile(string folderName, string fileName, CancellationToken cancellationToken)
        {
            IStorageItem checkTemporaryFolder = await knownFolder.TryGetItemAsync(folderName);

            CancelTask(cancellationToken);

            if (checkTemporaryFolder == null)
            {
                await MessageBox("source file not found");
                return null;
            }

            StorageFolder temporaryFolder = await knownFolder.GetFolderAsync(folderName);
            StorageFile temporaryFile = await temporaryFolder.GetFileAsync(fileName);

            if (temporaryFile == null)
            {
                await MessageBox("source file not found");
                return null;
            }

            CancelTask(cancellationToken);

            return temporaryFile;
        }

        private async Task<IReadOnlyList<StorageFile>> GetFiles(string folderName, CancellationToken cancellationToken)
        {
            IStorageItem checkTemporaryFolder = await knownFolder.TryGetItemAsync(folderName);

            CancelTask(cancellationToken);

            if (checkTemporaryFolder == null)
            {
                await MessageBox("source file not found");
                return null;
            }

            StorageFolder temporaryFolder = await knownFolder.GetFolderAsync(folderName);
            IReadOnlyList<StorageFile> temporaryFiles = await temporaryFolder.GetFilesAsync();

            if (temporaryFiles == null)
            {
                await MessageBox("source file not found");
                return null;
            }

            CancelTask(cancellationToken);

            return temporaryFiles;
        }

        private async Task<StorageFolder> CreateFolder(StorageFolder parent, string folderName, List<string> subFolders, CancellationToken cancellationToken)
        {
            IStorageItem checkTemporaryFolder = null;

            StorageFolder temporaryFolder = null;

            if (folderName != null)
            {
                checkTemporaryFolder = await parent.TryGetItemAsync(folderName);
            }
            else
            {
                temporaryFolder = parent;
            }

            CancelTask(cancellationToken);

            if (checkTemporaryFolder == null && folderName != null)
            {
                temporaryFolder = await parent.CreateFolderAsync(folderName);
            }

            temporaryFolder = await parent.GetFolderAsync(folderName);

            if (subFolders != null)
            {
                foreach (var item in subFolders)
                {
                    await temporaryFolder.CreateFolderAsync(item, CreationCollisionOption.OpenIfExists);
                }
            }

            return temporaryFolder;
        }

        private async Task<StorageFile> CreateFile(string folderName, string fileName, string extension, CancellationToken cancellationToken)
        {
            IStorageItem checkTemporaryFolder = await knownFolder.TryGetItemAsync(folderName);

            StorageFolder temporaryFolder = null;

            CancelTask(cancellationToken);

            if (checkTemporaryFolder == null)
            {
                temporaryFolder = await knownFolder.CreateFolderAsync(folderName);
            }

            temporaryFolder = await knownFolder.GetFolderAsync(folderName);
            StorageFile temporaryFile = await temporaryFolder.CreateFileAsync((fileName == "temp" ? "temp (1)" : fileName) + "." + extension, CreationCollisionOption.GenerateUniqueName);

            CancelTask(cancellationToken);

            return temporaryFile;
        }

        private async Task MessageBox(string msg)
        {
            MessageDialog mbox = new MessageDialog(msg, "Message");
            await mbox.ShowAsync();
            throw new Exception();
        }

        private async Task GetTextContent(CancellationToken cancellationToken, StorageFile folderBrowser = null)
        {

            StorageFile result = folderBrowser;

            ReportProgress(Stage2, Stage1ProgressBar, TextProgress1, 0);

            if (folderBrowser == null)
            {

                FileOpenPicker fileOpenPicker = new FileOpenPicker();
                fileOpenPicker.FileTypeFilter.Add(".html");
                fileOpenPicker.FileTypeFilter.Add(".htm");

                result = await fileOpenPicker.PickSingleFileAsync();
            }

            if (result == null)
            {
                throw new Exception("stop");
            }

            ResetStages(invertSolidColorBrush, 0, "");

            await ClearTempData();

            Save.IsEnabled = false;

            Open.Content = "Cancel";

            FileName = result.DisplayName;

            IRandomAccessStreamWithContentType output = await result.OpenReadAsync();

            ReportProgress(Stage2, Stage1ProgressBar, TextProgress1, 50, Stage1);

            using (Stream stream = output.AsStream())
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                Text = Encoding.GetEncoding(-0).GetString(buffer);
                if (Text.IndexOf("<head") > 0)
                {
                    Text = Text.Remove(Text.IndexOf("<head"), Text.IndexOf("<body") - Text.IndexOf("<head"));
                }
                Text = Windows.Data.Html.HtmlUtilities.ConvertToText(Text);
                Text = Regex.Replace(Text, "[<](?s)(.*)[>]", "", RegexOptions.Multiline);
                Text = Regex.Replace(Text, "[&](?!&amp;)", "&amp;", RegexOptions.Multiline);
                Text = Regex.Replace(Text, "\\[[a-z|0-9|\\ ]{0,}\\]", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                Text = Text.Replace("<", "lesser than ");
                Text = Text.Replace(">", "greater than ");
                //Text = Regex.Replace(Text, "[.][[]", ". [", RegexOptions.Multiline);
                Sources = Text ?? "";
            };

            ReportProgress(Stage2, Stage1ProgressBar, TextProgress1, 100);

            CancelTask(cancellationToken);

        }

        private async Task SetBackgroundAudio(string item, CancellationToken cancellationToken, bool source = false)
        {
            StorageFile temporaryFile = null;
            if (source)
            {
                temporaryFile = await CreateFile("BackgroundAudio", "temp (1)", "wav", cancellationToken);
            }
            else
            {
                temporaryFile = await CreateFile("Audio", "temp (1)", "wav", cancellationToken);
            }
            Stream result = await temporaryFile.OpenStreamForWriteAsync();
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Voice = SpeechSynthesizer.DefaultVoice;

            if (cultureInfo?.Name != speechSynthesizer.Voice.Language)
            {
                cultureInfo = new CultureInfo(speechSynthesizer.Voice.Language);
            }

            SpeechSynthesisStream speechSynthesisStream = null;

            CancelTask(cancellationToken);

            try
            {
                ReportProgress(Stage4, Stage3ProgressBar, TextProgress3, 0);
                speechSynthesisStream = await speechSynthesizer.SynthesizeSsmlToStreamAsync(item);
            }
            catch (Exception)
            {
                await MessageBox("error occured while reading a page");
                return;
            }

            if (speechSynthesisStream == null)
            {
                return;
            }

            CancelTask(cancellationToken);

            ReportProgress(Stage4, Stage3ProgressBar, TextProgress3, 50);

            TrackLength.Clear();

            foreach (var trackLength in speechSynthesisStream.Markers)
            {
                TrackLength.Add(trackLength.Time);
                CancelTask(cancellationToken);
            }

            using (Stream stream = speechSynthesisStream.AsStreamForRead())
            {
                buffer = new byte[stream.Length];
                CancelTask(cancellationToken);
                stream.Read(buffer, 0, buffer.Length);
                using (Stream writeStream = result)
                {
                    CancelTask(cancellationToken);
                    writeStream.Write(buffer, 0, buffer.Length);
                };
            };

            ReportProgress(Stage4, Stage3ProgressBar, TextProgress3, 100);

            speechSynthesisStream.Dispose();

            speechSynthesizer.Dispose();

            CancelTask(cancellationToken);

            audioVideo.Add(temporaryFile);

        }

        private async Task SetImage(string text, CancellationToken cancellationToken)
        {
            StorageFile temporaryFile = await CreateFile("Image", "temp (1)", "png", cancellationToken);
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            txt.Text = text;
            await renderTargetBitmap.RenderAsync(grid);

            CancelTask(cancellationToken);

            IBuffer pixels = await renderTargetBitmap.GetPixelsAsync();
            byte[] bytes = pixels.ToArray();

            CancelTask(cancellationToken);

            using (IRandomAccessStream stream = await temporaryFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                     BitmapAlphaMode.Straight,
                                     (uint)renderTargetBitmap.PixelWidth,
                                        (uint)renderTargetBitmap.PixelHeight,
                                     96, 96, bytes);
                await encoder.FlushAsync();
            };

            CancelTask(cancellationToken);

        }

        private async Task SetComp(StorageFile backgroundAudio, Dictionary<StorageFile, TimeSpan> imageFiles, CancellationToken cancellationToken)
        {
            StorageFile temporaryFile = await CreateFile("Media Composition", "temp (1)", "cmp", cancellationToken);
            BackgroundAudioTrack backgroundAudioTrack = await BackgroundAudioTrack.CreateFromFileAsync(backgroundAudio);
            AudioEncodingProperties audioEncodingProperties = backgroundAudioTrack.GetAudioEncodingProperties();
            TimeSpan duration = backgroundAudioTrack.OriginalDuration;
            MediaComposition composition = new MediaComposition();
            composition.BackgroundAudioTracks.Add(backgroundAudioTrack);

            CancelTask(cancellationToken);

            MediaClip clip;
            foreach (var item in imageFiles)
            {
                CancelTask(cancellationToken);
                clip = await MediaClip.CreateFromImageFileAsync(item.Key, item.Value);
                composition.Clips.Add(clip);
            }
            composition.CreateDefaultEncodingProfile();
            await composition.SaveAsync(temporaryFile);
            ReportProgress(Stage5, Stage4ProgressBar, TextProgress4, 100);

            CancelTask(cancellationToken);

        }

        private async Task SetVideo(MediaComposition mediaComposition, CancellationToken cancellationToken)
        {
            StorageFile temporaryFile = await CreateFile("Video", "temp (1)", "mp4", cancellationToken);

            IAsyncOperationWithProgress<TranscodeFailureReason, double> progress = mediaComposition.RenderToFileAsync(temporaryFile, MediaTrimmingPreference.Precise);

            CancelTask(cancellationToken);

            progress.Progress = new AsyncOperationProgressHandler<TranscodeFailureReason, double>(async (reason, progressInfo) =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    reason.Cancel();
                    await ClearTempData();
                }
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(async () =>
                 {
                     ReportProgress(Stage6, Stage5ProgressBar, TextProgress5, progressInfo);
                     if (progressInfo == 100)
                     {
                         Open.Content = "Browse File";
                         ItemSource = await GetProperties(temporaryFile, cancellationToken);
                         Save.IsEnabled = true;
                     }
                 }));
            });

            CancelTask(cancellationToken);

            MediaStreamSource mediaStreamSource = mediaComposition.GeneratePreviewMediaStreamSource(0, 0);

            MediaElement1.SetMediaStreamSource(mediaStreamSource);

            MediaElement1.Play();

            await progress;

            CancelTask(cancellationToken);

            audioVideo.Add(temporaryFile);

        }

        private async Task SetTranscoder(StorageFile source, StorageFile destination, MediaEncodingProfile mediaEncodingProfile, CancellationToken cancellationToken)
        {
            MediaTranscoder mediaTranscoder = new MediaTranscoder();
            mediaTranscoder.AlwaysReencode = true;

            CancelTask(cancellationToken);

            PrepareTranscodeResult prepareTranscodeResult = await mediaTranscoder.PrepareFileTranscodeAsync(source, destination, mediaEncodingProfile);
            IAsyncActionWithProgress<double> progress = prepareTranscodeResult.TranscodeAsync();
            progress.Progress = new AsyncActionProgressHandler<double>(async (progressInfo, info) =>
              {
                  if (cancellationToken.IsCancellationRequested)
                  {
                      progressInfo.Cancel();
                  }
                  await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
                   {
                       ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, info);
                       if (info == 100)
                       {
                           Save.Content = "Convert/Save";
                       }
                   }));
              });
            CancelTask(cancellationToken);
        }

        private async Task<Dictionary<string, string>> GetProperties(StorageFile temporaryFile, CancellationToken cancellationToken)
        {
            StorageItemContentProperties properties = temporaryFile.Properties;
            MusicProperties musicProperties = await properties.GetMusicPropertiesAsync();
            VideoProperties videoProperties = await properties.GetVideoPropertiesAsync();
            BasicProperties basicProperties = await temporaryFile.GetBasicPropertiesAsync();

            CancelTask(cancellationToken);

            Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
            tempDictionary["Display Name"] = temporaryFile.DisplayName;
            tempDictionary["File Type"] = temporaryFile.FileType;
            tempDictionary["Current Folder Path"] = temporaryFile.FolderRelativeId;
            tempDictionary["Size"] = string.Format("{0:0.00}", (Convert.ToDouble(basicProperties.Size) / 1048576d)) + " Mb";
            tempDictionary["Creation Date"] = temporaryFile.DateCreated.ToString();
            tempDictionary["Modified Date"] = basicProperties.DateModified.ToString();
            tempDictionary["Audio Bit Rate"] = musicProperties.Bitrate + " bps";
            tempDictionary["Length"] = videoProperties.Duration.ToString("hh\\:mm\\:ss");
            tempDictionary["Frame Width"] = videoProperties.Width + "";
            tempDictionary["Frame Height"] = videoProperties.Height + "";
            tempDictionary["Orientation"] = videoProperties.Orientation + "";
            tempDictionary["Total Bit Rate"] = videoProperties.Bitrate + " bps";

            CancelTask(cancellationToken);

            return tempDictionary;
        }

        private async Task<List<StorageFile>> OnOpen(CancellationToken cancellationToken, StorageFile folderBrowser = null)
        {

            #region Content Loading

            audioVideo.Clear();

            await GetTextContent(cancellationToken, folderBrowser);

            #endregion

            #region Frame Creation

            for (int i = 0; i < ContentList.Count; i++)
            {
                percent = (Convert.ToDouble(i + 1) / Convert.ToDouble(ContentList.Count)) * 100d;
                ReportProgress(Stage3, Stage2ProgressBar, TextProgress2, percent);
                await SetImage(ContentList[i], cancellationToken);
            }

            #endregion

            #region Audio Creation 

            await SetBackgroundAudio(GetSSML(ContentList), cancellationToken, true);

            #endregion

            #region Composition Creation

            StorageFile audioBackgroundFile = await GetFile("BackgroundAudio", "temp (1).wav", cancellationToken);

            if (audioBackgroundFile == null)
            {
                return null;
            }

            IReadOnlyList<StorageFile> imageTemporaryFiles = await GetFiles("Image", cancellationToken);

            if (imageTemporaryFiles == null)
            {
                return null;
            }

            if (TrackLength.Count != imageTemporaryFiles.Count)
            {
                await MessageBox("some audio files or frames are missing.");
                return null;
            }

            Dictionary<StorageFile, TimeSpan> temporaryFilesWithDuration = new Dictionary<StorageFile, TimeSpan>();
            for (int i = 0; i < imageTemporaryFiles.Count; i++)
            {
                try
                {
                    percent = (Convert.ToDouble(i) / Convert.ToDouble(ContentList.Count)) * 100d;
                    temporaryFilesWithDuration.Add(imageTemporaryFiles[i], TrackLength[i].Subtract((i == 0 ? subTimeSpan : TrackLength[i - 1])));
                    ReportProgress(Stage5, Stage4ProgressBar, TextProgress4, percent);
                }
                catch (Exception) { }
            }

            await SetComp(audioBackgroundFile, temporaryFilesWithDuration, cancellationToken);

            #endregion

            #region Video Creation

            StorageFile compTemporaryFile = await GetFile("Media Composition", "temp (1).cmp", cancellationToken);

            if (compTemporaryFile == null)
            {
                return null;
            }

            MediaComposition mediaComposition = await MediaComposition.LoadAsync(compTemporaryFile);

            await SetVideo(mediaComposition, cancellationToken);

            return audioVideo;

            #endregion

        }

        private async Task OnSave(CancellationToken cancellationToken)
        {
            FileSavePicker fileSavePicker = new FileSavePicker();

            fileSavePicker.FileTypeChoices.Add("Auto", new List<string>() { ".mp4" });
            fileSavePicker.FileTypeChoices.Add("Avi_HD1080p", new List<string>() { ".avi_HD1080p" });
            fileSavePicker.FileTypeChoices.Add("Avi_HD720p", new List<string>() { ".avi_HD720p" });
            fileSavePicker.FileTypeChoices.Add("Avi_Ntsc", new List<string>() { ".avi_Ntsc" });
            fileSavePicker.FileTypeChoices.Add("Avi_Pal", new List<string>() { ".avi_Pal" });
            fileSavePicker.FileTypeChoices.Add("Avi_Qvga", new List<string>() { ".avi_Qvga" });
            fileSavePicker.FileTypeChoices.Add("Avi_Vga", new List<string>() { ".avi_Vga" });
            fileSavePicker.FileTypeChoices.Add("Avi_Wvga", new List<string>() { ".avi_Wvga" });
            fileSavePicker.FileTypeChoices.Add("M4a_High", new List<string>() { ".m4a_High" });
            fileSavePicker.FileTypeChoices.Add("M4a_Low", new List<string>() { ".m4a_Low" });
            fileSavePicker.FileTypeChoices.Add("M4a_Medium", new List<string>() { ".m4a_Medium" });
            fileSavePicker.FileTypeChoices.Add("Mp3_High", new List<string>() { ".mp3_High" });
            fileSavePicker.FileTypeChoices.Add("Mp3_Low", new List<string>() { ".mp3_Low" });
            fileSavePicker.FileTypeChoices.Add("Mp3_Medium", new List<string>() { ".mp3_Medium" });
            fileSavePicker.FileTypeChoices.Add("Mp4_HD1080p", new List<string>() { ".mp4_HD1080p" });
            fileSavePicker.FileTypeChoices.Add("Mp4_HD720p", new List<string>() { ".mp4_HD720p" });
            fileSavePicker.FileTypeChoices.Add("Mp4_Ntsc", new List<string>() { ".mp4_Ntsc" });
            fileSavePicker.FileTypeChoices.Add("Mp4_Pal", new List<string>() { ".mp4_Pal" });
            fileSavePicker.FileTypeChoices.Add("Mp4_Qvga", new List<string>() { ".mp4_Qvga" });
            fileSavePicker.FileTypeChoices.Add("Mp4_Vga", new List<string>() { ".mp4_Vga" });
            fileSavePicker.FileTypeChoices.Add("Mp4_Wvga", new List<string>() { ".mp4_Wvga" });
            fileSavePicker.FileTypeChoices.Add("Wav_High", new List<string>() { ".wav_High" });
            fileSavePicker.FileTypeChoices.Add("Wav_Low", new List<string>() { ".wav_Low" });
            fileSavePicker.FileTypeChoices.Add("Wav_Medium", new List<string>() { ".wav_Medium" });
            fileSavePicker.FileTypeChoices.Add("Wma_High", new List<string>() { ".wma_High" });
            fileSavePicker.FileTypeChoices.Add("Wma_Low", new List<string>() { ".wma_Low" });
            fileSavePicker.FileTypeChoices.Add("Wma_Medium", new List<string>() { ".wma_Medium" });
            fileSavePicker.FileTypeChoices.Add("Wmv_HD1080p", new List<string>() { ".wmv_HD1080p" });
            fileSavePicker.FileTypeChoices.Add("Wmv_HD720p", new List<string>() { ".wmv_HD720p" });
            fileSavePicker.FileTypeChoices.Add("Wmv_Ntsc", new List<string>() { ".wmv_Ntsc" });
            fileSavePicker.FileTypeChoices.Add("Wmv_Pal", new List<string>() { ".wmv_Pal" });
            fileSavePicker.FileTypeChoices.Add("Wmv_Qvga", new List<string>() { ".wmv_Qvga" });
            fileSavePicker.FileTypeChoices.Add("Wmv_Vga", new List<string>() { ".wmv_Vga" });
            fileSavePicker.FileTypeChoices.Add("Wmv_Wvga", new List<string>() { ".wmv_Wvga" });

            fileSavePicker.SuggestedFileName = FileName ?? "output";

            StorageFile outputFile = await fileSavePicker.PickSaveFileAsync();

            if (outputFile == null)
            {
                throw new Exception();
            }

            ResetStages(solidColorBrush, 100, "100% done.");

            Save.Content = "Cancel";

            StorageFile temporaryFile = await GetFile("Video", "temp (1).mp4", cancellationToken);

            if (temporaryFile == null)
            {
                return;
            }

            MediaEncodingProfile mediaEncodingProfile = null;

            string fileType = outputFile.FileType.Replace(".", "");

            if (fileType.ToLower() == "mp4")
            {
                using (Stream stream = await temporaryFile.OpenStreamForReadAsync())
                {
                    buffer = new byte[stream.Length];
                    CancelTask(cancellationToken);
                    stream.Read(buffer, 0, buffer.Length);
                    ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, 45);
                    using (Stream writeStream = await outputFile.OpenStreamForWriteAsync())
                    {
                        CancelTask(cancellationToken);
                        writeStream.Write(buffer, 0, buffer.Length);
                    };
                    ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, 90);
                };
                ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, 100);
                Save.Content = "Convert/Save";
                return;
            }
            else
            {
                try
                {
                    await outputFile.RenameAsync(outputFile.DisplayName + "." + fileType.Split('_')[0], NameCollisionOption.GenerateUniqueName);
                }
                catch (Exception ex)
                {
                    await MessageBox(ex.Message);
                }
            }

            CancelTask(cancellationToken);

            MediaFileType mediaFileType = default(MediaFileType);

            VideoEncodingQuality videoEncodingQuality = default(VideoEncodingQuality);

            AudioEncodingQuality audioEncodingQuality = default(AudioEncodingQuality);

            Enum.TryParse<MediaFileType>(fileType.Split('_')[0], true, out mediaFileType);

            Enum.TryParse<VideoEncodingQuality>(fileType.Split('_')[1], true, out videoEncodingQuality);

            Enum.TryParse<AudioEncodingQuality>(fileType.Split('_')[1], true, out audioEncodingQuality);

            switch (mediaFileType)
            {
                case MediaFileType.avi:
                    {
                        mediaEncodingProfile = MediaEncodingProfile.CreateAvi(videoEncodingQuality);
                        break;
                    }
                case MediaFileType.m4a:
                    {
                        mediaEncodingProfile = MediaEncodingProfile.CreateM4a(audioEncodingQuality);
                        break;
                    }
                case MediaFileType.mp3:
                    {
                        mediaEncodingProfile = MediaEncodingProfile.CreateMp3(audioEncodingQuality);
                        break;
                    }
                case MediaFileType.mp4:
                    {
                        mediaEncodingProfile = MediaEncodingProfile.CreateMp4(videoEncodingQuality);
                        break;
                    }
                case MediaFileType.wav:
                    {
                        mediaEncodingProfile = MediaEncodingProfile.CreateWav(audioEncodingQuality);
                        break;
                    }
                case MediaFileType.wma:
                    {
                        mediaEncodingProfile = MediaEncodingProfile.CreateWma(audioEncodingQuality);
                        break;
                    }
                case MediaFileType.wmv:
                    {
                        mediaEncodingProfile = MediaEncodingProfile.CreateWmv(videoEncodingQuality);
                        break;
                    }
            }

            CancelTask(cancellationToken);

            await SetTranscoder(temporaryFile, outputFile, mediaEncodingProfile, cancellationToken);
        }

        #endregion

        #region private delegate methods

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender).Content.ToString() == "Convert/Save")
                {
                    cancellationTokenSource = new CancellationTokenSource();
                    await OnSave(cancellationTokenSource.Token);
                }
                else
                {
                    ((Button)sender).Content = "Convert/Save";
                    cancellationTokenSource.Cancel(true);
                }
            }
            catch (Exception)
            {
                ((Button)sender).Content = "Convert/Save";
            }
        }

        private async void Open_Dir_Click(object sender, RoutedEventArgs e)
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add(".");
            saveFolder = await folderPicker.PickSingleFolderAsync();
        }

        private async void Save_Dir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender).Content.ToString() == "Save Folder")
                {
                    if (saveFolder == null)
                    {
                        throw new Exception();
                    }
                    cancellationTokenSource = new CancellationTokenSource();
                    FolderPicker folderPicker = new FolderPicker();
                    folderPicker.FileTypeFilter.Add(".html");
                    folderPicker.FileTypeFilter.Add(".htm");
                    StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                    IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByName);
                    List<string> subFolders = new List<string>() { "Audio", "Video" };
                    StorageFolder temporaryFolder = null;
                    foreach (var item in fileList)
                    {
                        string[] path = item.Path.Split('\\');
                        temporaryFolder = await CreateFolder(saveFolder, path.Length > 2 ? path[path.Length - 2] : null, subFolders, cancellationTokenSource.Token);

                        List<StorageFile> aV = await OnOpen(cancellationTokenSource.Token, item);
                        StorageFolder audioFolder = await temporaryFolder.GetFolderAsync("Audio");

                        ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, 25);

                        StorageFolder videoFolder = await temporaryFolder.GetFolderAsync("Video");

                        ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, 50);

                        await aV[0].CopyAsync(audioFolder, (FileName ?? "output") + ".wav");

                        ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, 50);

                        await aV[1].CopyAsync(videoFolder, (FileName ?? "output") + ".mp4");

                        ReportProgress(Stage7, Stage6ProgressBar, TextProgress6, 100);

                    }
                }
                else
                {
                    ((Button)sender).Content = "Save Folder";
                    cancellationTokenSource.Cancel(true);
                }
            }
            catch (Exception ex)
            {
                ((Button)sender).Content = "Save Folder";
                if (ex.Message != "stop")
                {
                    await ClearTempData();
                    Save.IsEnabled = false;
                }
            }
        }

        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender).Content.ToString() == "Browse File")
                {
                    cancellationTokenSource = new CancellationTokenSource();
                    IReadOnlyList<StorageFolder> temporaryFolders = await knownFolder.GetFoldersAsync();
                    if (Convert.ToInt32(temporaryFolders?.Count) == 0)
                    {
                        Save.IsEnabled = false;
                    }
                    else
                    {
                        Save.IsEnabled = true;
                    }
                    await OnOpen(cancellationTokenSource.Token);
                }
                else
                {
                    ((Button)sender).Content = "Browse File";
                    cancellationTokenSource.Cancel(true);
                }
            }
            catch (Exception ex)
            {
                ((Button)sender).Content = "Browse File";
                if (ex.Message != "stop")
                {
                    await ClearTempData();
                    Save.IsEnabled = false;
                }
            }
        }

        #endregion

        #endregion

    }
}
