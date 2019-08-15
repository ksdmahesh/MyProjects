using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        static Dictionary<string, string> sans = new Dictionary<string, string>()
        {
            #region Hindi
	        //{ "kə", "क" },
            //{ "kʰə", "ख" },
            //{ "ɡə", "ग" },
            //{ "ɡʱə", "घ" },
            //{ "ŋə", "ङ" },
            //{ "tʃə", "च" },
            //{ "tʃʰə", "छ" },
            //{ "d͡ʑə", "ज" },
            //{ "d͡ʑʱə", "झ" },
            //{ "ɲə", "ञ" },
            //{ "ʈə", "ट" },
            //{ "ʈʰə", "ठ" },
            //{ "ɖə", "ड" },
            //{ "ɖʱə", "ढ" },
            //{ "ɳə", "ण" },
            //{ "t̪ə", "त" },
            //{ "t̪ʰə", "थ" },
            //{ "d̪ə", "द" },
            //{ "d̪ʱə", "ध" },
            //{ "nə", "न" },
            //{ "pə", "प" },
            //{ "pʰə", "फ" },
            //{ "bə", "ब" },
            //{ "bʱə", "भ" },
            //{ "mə", "म" },
            //{ "jə", "य" },
            //{ "rə", "र" },
            //{ "lə", "ल" },
            //{ "ʋə", "व" },
            //{ "sə", "स" },
            //{ "ʂə", "ष" },
            //{ "ɕə", "श" },
            //{ "ɦə", "ह" },
            //{ "ə", "अ" },
            //{ "aː", "आ" },
            //{ "i", "इ" },
            //{ "iː", "ई" },
            //{ "u", "उ" },
            //{ "uː", "ऊ" },
            //{ "r̩", "ऋ" },
            //{ "r̩ː", "ॠ" },
            //{ "l̩", "ऌ" },
            //{ "l̩ː", "ॡ" },
            //{ "eː", "ए" },
            //{ "əi", "ऐ" },
            //{ "oː", "ओ" },
            //{ "əu", "औ" }, 
            //{ "ṁ", "ं" },
            //{ "h", "ः" }
	#endregion
            { "kə", "क" },
            { "kʰə", "ख" },
            { "ɡə", "ग" },
            { "ɡʱə", "घ" },
            { "ŋə", "ङ" },
            { "cə", "च" },
            { "cʰə", "छ" },
            { "ɟə", "ज" },
            { "ɟʱə", "झ" },
            { "ɲə", "ञ" },
            { "ʈə", "ट" },
            { "ʈʰə", "ठ" },
            { "ɖə", "ड" },
            { "ɖʱə", "ढ" },
            { "ɳə", "ण" },
            { "t̪ə", "त" },
            { "t̪ʰə", "थ" },
            { "d̪ə", "द" },
            { "d̪ʱə", "ध" },
            { "n̪ə", "न" },
            { "pə", "प" },
            { "pʰə", "फ" },
            { "bə", "ब" },
            { "bʱə", "भ" },
            { "mə", "म" },
            { "jə", "य" },
            { "rə", "र" },
            { "lə", "ल" },
            { "ʋə", "व" },
            { "s̪ə", "स" },
            { "ʂə", "ष" },
            { "ɕə", "श" },
            { "ɦə", "ह" },
            { "ə", "अ" },
            { "ɑː", "आ" },
            { "i", "इ" },
            { "iː", "ई" },
            { "u", "उ" },
            { "uː", "ऊ" },
            { "r̩", "ऋ" },
            { "r̩ː", "ॠ" },
            { "l̩", "ऌ" },
            { "l̩ː", "ॡ" },
            { "eː", "ए" },
            { "əi", "ऐ" },
            { "oː", "ओ" },
            { "əu", "औ" },
            //{ "ɦ", "ः" },
            { ((char)771) + "", "ँ" },
            { "m", "ं" },
            { "hə", "ः" },
            { "oːm", "ॐ" },

            //Extra
            //{ "ĕ", "ऎ" },
            //{ "ŏ", "ऒ" },
            //{ "oːm", "ऩ" },
            //{ "ɽ", "ऱ" },
            //{ "oːm", "ऺ" },
            //{ "oːm", ""़" },
            //{ "oːm", "ऽ" },
            //{ "ĕ", ""ॆ" },
            //{ "ŏ", ""ॉ" },
            //{ "ŏ", ""ॊ" },
            //{ "oːm", ""॑" },
            //{ "oːm", "।" },
            //{ "oːm", "॥" },
            //{ "oːm", "०" },
            //{ "oːm", "१" },
            //{ "oːm", "२" },
            //{ "oːm", "३" },
            //{ "oːm", "४" },
            //{ "oːm", "५" },
            //{ "oːm", "६" },
            //{ "oːm", "७" },
            //{ "oːm", "८" },
            //{ "oːm", "९" },
            //{ "oːm", "॰" }
        },
            dia = new Dictionary<string, string>()
            {
                { "aː", "ा" },
                { "i", "ि" },
                { "iː", "ी" },
                { "u", "ु" },
                { "uː", "ू" },
                { "r̩", "ृ" },
                { "r̩ː", "ॄ" },
                { "l̩", "ॢ" },
                { "l̩ː", "ॣ" },
                { "eː", "े" },
                { "əi", "ै" },
                { "oː", "ो" },
                { "əu", "ौ" }
            };

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Run()
        {
            string text = "द्वैपायनेन यत्प्रोक्तं पुराणं परमर्षिणा सुरैर्ब्रह्मर्षिभिश्चैव श्रुत्वा यदभिपूजितम्";
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.Voice = SpeechSynthesizer.AllVoices.FirstOrDefault(a => a.Gender == VoiceGender.Female && a.Language == "hi-IN");
            SpeechSynthesisStream speechSynthesisStream = await speechSynthesizer.SynthesizeSsmlToStreamAsync(getPhoneme(text));
            MediaElement mediaElement = new MediaElement();
            mediaElement.SetSource(speechSynthesisStream, "");
            mediaElement.Play();
            speechSynthesizer.Dispose();
        }

        private static string getPhoneme(string text)
        {
            foreach (var item in sans)
            {
                text = text.Replace(item.Value, item.Key);
            }
            foreach (var item in dia)
            {
                text = Regex.Replace(text, "((ə)(?=" + item.Value + "))" + item.Value + "", item.Key, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            }
            //text = text.Replace("ँ", "m̐");
            text = Regex.Replace(text, "((ə)(?=्))्", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            text = getSSML(text);
            return text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Run();
        }

        private static string getSSML(string text)
        {
            StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" ?>\r\n<speak version=\"1.0\"\r\n xmlns=\"http://www.w3.org/2001/10/synthesis\"\r\n xml:lang=\"hi-IN\">\r\n");
            foreach (var item in text.Split(' '))
            {
                sb.Append("\t<phoneme ph=\"" + item + "\">" + item + "</phoneme> \r\n");
            }
            sb.Append("</speak>");
            //File.WriteAllText(@"C:\Users\Jaffa\Desktop\z.txt", sb.ToString());
            return sb.ToString();
        }

    }
}
