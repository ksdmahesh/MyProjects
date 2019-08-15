using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Util;
using TaskManager.Models;
using Java.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using Android.Content.PM;
using System.Xml;
using System.Reflection;

namespace TaskManager
{

    #region public enum

    public enum CurrentContent
    {
        Main, Save
    }

    public enum CurrentStatus
    {
        ToDo, Waiting, Done
    }

    #endregion

    [Activity(Label = "TaskManager", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ISerializable, Java.Lang.ICloneable
    {

        #region private variables

        private Base baseModel = new Base();

        private List<Base> list = new List<Base>();

        private Handler handler;

        private Button done, add, close;

        private CheckBox isAlert;

        private EditText timePick, datePick, title, description;

        private DatePickerDialog datePickerDialog;

        private TimePickerDialog timePickerDialog;

        private ScrollView tab1Content, tab2Content, tab3Content;

        private Calendar calender;

        private Date from, to;

        #endregion

        #region protected methods

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (bundle != null)
            {
                this.baseModel = (bundle.GetSerializable("Base") as MainActivity).baseModel;
            }
            if (baseModel.Content == CurrentContent.Main)
            {
                IsMain(true, true);
            }
            else if (baseModel.Content == CurrentContent.Save)
            {
                IsSave();
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            if (datePickerDialog != null)
            {
                baseModel.Year = datePickerDialog.DatePicker.Year;
                baseModel.Month = datePickerDialog.DatePicker.Month - 1;
                baseModel.Day = datePickerDialog.DatePicker.DayOfMonth;
            }
            if (timePickerDialog != null)
            {
                baseModel.TimePickerBundler = timePickerDialog.OnSaveInstanceState();
            }
            outState.PutSerializable("Base", this);
        }

        protected override void OnStop()
        {
            try
            {
                handler.Dispose();
                Serialize();
            }
            catch (Exception) { }
            base.OnStop();
        }

        #endregion

        #region private Properties

        private List<Base> BaseList
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }

        #endregion

        #region public constructor

        public MainActivity()
        {
            baseModel.Path = Android.OS.Environment.ExternalStorageDirectory + "/TaskManager/Data/Temp/Temp.txt";
            File folder = new File(baseModel.Path.Substring(0, baseModel.Path.LastIndexOf("/")));
            if (!folder.Exists())
            {
                folder.Mkdirs();
            }
            File file = new File(baseModel.Path);
            if (!file.Exists())
            {
                file.CreateNewFile();
            }
            Recall();
        }

        #endregion

        #region private methods

        private Date GetDate(Base item = null)
        {
            calender = Calendar.GetInstance(Java.Util.TimeZone.Default, Locale.Default);
            if (item != null)
            {
                return new Date(item.Year, item.Month - 1, item.Day, item.Hour, item.Minute, calender.Get(CalendarField.Second));
            }
            else
            {
                return new Date(calender.Get(CalendarField.Year), calender.Get(CalendarField.Month), calender.Get(CalendarField.DayOfMonth), calender.Get(CalendarField.HourOfDay), calender.Get(CalendarField.Minute), calender.Get(CalendarField.Second));
            }
        }

        private void SetTimer(Base timerObject)
        {
            if (timerObject != null)
            {
                handler = new Handler();
                from = GetDate();
                to = GetDate(timerObject);
                handler.PostDelayed(Recall, (from.Time - to.Time) <= 0 ? 0 : (from.Time - to.Time));
            }
        }

        private void Recall()
        {
            BaseList.Sort((x, y) =>
            {
                from = GetDate(x);
                to = GetDate(y);
                return to.CompareTo(from);
            });
            BaseList = BaseList.Where(a =>
             {
                 from = GetDate(a);
                 to = GetDate();
                 if (from.Time <= to.Time && a.Status == CurrentStatus.ToDo)
                 {
                     a.Status = CurrentStatus.Waiting;
                 }
                 if (from.Time > to.Time && a.Status == CurrentStatus.Waiting)
                 {
                     a.Status = CurrentStatus.ToDo;
                 }
                 return true;
             }
            ).ToList();
            if (tab1Content != null)
            {
                tab1Content.RemoveAllViews();
            }
            if (tab2Content != null)
            {
                tab2Content.RemoveAllViews();
            }
            if (tab3Content != null)
            {
                tab3Content.RemoveAllViews();
            }
            if (BaseList.Any(a => a.Status == CurrentStatus.ToDo))
            {
                OnLoad(tab1Content, BaseList.Where(a => a.Status == CurrentStatus.ToDo).ToList());
            }
            if (BaseList.Any(a => a.Status == CurrentStatus.Waiting))
            {
                OnLoad(tab2Content, BaseList.Where(a => a.Status == CurrentStatus.Waiting).ToList());
            }
            if (BaseList.Any(a => a.Status == CurrentStatus.Done))
            {
                OnLoad(tab3Content, BaseList.Where(a => a.Status == CurrentStatus.Done).ToList());
            }
            SetTimer(BaseList.LastOrDefault(b => b.Status == CurrentStatus.ToDo));
        }

        private void IsSave()
        {
            SetContentView(Resource.Layout.Save_Update);
            baseModel.Content = CurrentContent.Save;
            close = (Button)FindViewById(Resource.Id.Close);
            done = (Button)FindViewById(Resource.Id.Done);
            close.Click += Button_Click;
            done.Click += Button_Click;
            timePick = (EditText)FindViewById(Resource.Id.TimePick);
            datePick = (EditText)FindViewById(Resource.Id.DatePick);
            timePick.Focusable = true;
            datePick.Focusable = true;
            datePick.LongClickable = true;
            timePick.LongClickable = true;
            datePick.LongClick += Pick_LongClick;
            timePick.LongClick += Pick_LongClick;
            timePick.FocusChange += Pick_FocusChange;
            datePick.FocusChange += Pick_FocusChange;
            timePick.Click += Pick_Click;
            datePick.Click += Pick_Click;

            title = (EditText)FindViewById(Resource.Id.Title);
            description = (EditText)FindViewById(Resource.Id.Description);
            title.TextChanged += TextBox_TextChanged;
            description.TextChanged += TextBox_TextChanged;

            isAlert = (CheckBox)FindViewById(Resource.Id.IsAlert);
            isAlert.CheckedChange += CheckBox_CheckedChange;
        }

        private void IsMain(bool isInit = true, bool isReq = false)
        {
            SetContentView(Resource.Layout.Main);
            baseModel.Content = CurrentContent.Main;
            TabHost tabHost = (TabHost)FindViewById(Resource.Id.tabHost);
            tabHost.Setup();

            Android.Widget.TabHost.TabSpec spec1 = tabHost.NewTabSpec("Tab 1");
            spec1.SetIndicator("Todo");
            spec1.SetContent(Resource.Id.tab1);

            Android.Widget.TabHost.TabSpec spec2 = tabHost.NewTabSpec("Tab 2");
            spec2.SetIndicator("Waiting");
            spec2.SetContent(Resource.Id.tab2);

            Android.Widget.TabHost.TabSpec spec3 = tabHost.NewTabSpec("Tab 3");
            spec3.SetIndicator("Done");
            spec3.SetContent(Resource.Id.tab3);

            add = (Button)FindViewById(Resource.Id.Add);
            add.Click += Main_Button_Click;

            tab1Content = (ScrollView)FindViewById(Resource.Id.tab1Content);
            tab2Content = (ScrollView)FindViewById(Resource.Id.tab2Content);
            tab3Content = (ScrollView)FindViewById(Resource.Id.tab3Content);
            tabHost.AddTab(spec1);
            tabHost.AddTab(spec2);
            tabHost.AddTab(spec3);
            AddToContent(isInit, isReq);
        }

        private void OnLoad(ScrollView scrollView, List<Base> item)
        {
            LinearLayout relativeLayout = new LinearLayout(this) { Orientation = Orientation.Vertical };
            relativeLayout.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            foreach (var value in item)
            {
                LinearLayout linearLayout = new LinearLayout(this) { LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent) };
                LinearLayout innerLayout = new LinearLayout(this) { LayoutParameters = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.WrapContent, 0.6f), Orientation = Orientation.Vertical };
                linearLayout.SetBackgroundResource(Resource.Xml.Border);
                TextView titleTextView = new TextView(this) { Text = value.Title, TextSize = 20, Gravity = GravityFlags.CenterVertical };
                TextView dateTextView = new TextView(this) { Text = value.Year + " - " + value.Month + " - " + value.Day, TextSize = 15, Gravity = GravityFlags.CenterVertical };
                innerLayout.AddView(titleTextView);
                innerLayout.AddView(dateTextView);
                innerLayout.SetGravity(GravityFlags.CenterVertical);
                Button button = new Button(this) { Text = "Ok" };
                button.SetPadding(10, 20, 10, 20);
                button.Gravity = GravityFlags.Center;
                button.SetBackgroundResource(Resource.Xml.Border);
                button.LayoutParameters = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.WrapContent, 0.4f);
                linearLayout.AddView(innerLayout);
                linearLayout.AddView(button);
                relativeLayout.AddView(linearLayout);
            }
            scrollView.AddView(relativeLayout);
        }

        private void AddToContent(bool isInit, bool isReq = false)
        {
            if (isReq)
            {
                BaseList = DeSerialize(BaseList);
            }
            if (!isInit)
            {
                BaseList.Add(CloneTo(baseModel));
            }
            Recall();
            if (!isInit)
            {
                Serialize();
            }
        }

        private T CloneTo<T>(T item)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, item);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
            }
        }

        private void CheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            baseModel.IsAlert = (sender as CheckBox).Checked;
        }

        private void TextBox_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (sender == title)
            {
                baseModel.Title = (sender as EditText).Text;
            }
            else if (sender == description)
            {
                baseModel.Description = (sender as EditText).Text;
            }
        }

        private void Main_Button_Click(object sender, EventArgs e)
        {
            calender = Calendar.GetInstance(Java.Util.TimeZone.Default);
            baseModel.Title = null;
            baseModel.Description = null;
            baseModel.IsAlert = false;
            baseModel.Status = CurrentStatus.ToDo;
            baseModel.Year = calender.Get(CalendarField.Year);
            baseModel.Month = calender.Get(CalendarField.Month) + 1;
            baseModel.Day = calender.Get(CalendarField.DayOfMonth);
            baseModel.Hour = calender.Get(CalendarField.HourOfDay);
            baseModel.Minute = calender.Get(CalendarField.Minute);
            IsSave();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == done)
            {
                if (!string.IsNullOrEmpty(title.Text))
                {
                    IsMain(false);
                }
                else
                {
                    IsMain();
                }
            }
            else if (sender == close)
            {
                IsMain();
            }
        }

        private void Pick_LongClick(object sender, View.LongClickEventArgs e)
        {
            (sender as EditText).Text = null;
            baseModel.CurrentView = sender;
        }

        private void Pick_Click(object sender, EventArgs e)
        {
            ShowDT(sender, false);
        }

        private void Pick_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if ((sender as EditText).IsFocused && (baseModel.CurrentView == null || baseModel.CurrentView is bool || (baseModel.CurrentView as EditText).Id != (sender as EditText).Id))
            {
                ShowDT(sender, baseModel.CurrentView == null);
            }
        }

        private void ShowDT(object sender, bool reset)
        {
            if (reset)
            {
                calender = Calendar.GetInstance(Java.Util.TimeZone.Default);
                baseModel.Year = calender.Get(CalendarField.Year);
                baseModel.Month = calender.Get(CalendarField.Month) + 1;
                baseModel.Day = calender.Get(CalendarField.DayOfMonth);
                baseModel.Hour = calender.Get(CalendarField.HourOfDay);
                baseModel.Minute = calender.Get(CalendarField.Minute);
            }
            baseModel.CurrentView = false;
            if (sender == datePick)
            {
                datePickerDialog = new DatePickerDialog(this, OnDateShow, baseModel.Year, baseModel.Month - 1, baseModel.Day);
                datePickerDialog.Show();
            }
            else if (sender == timePick)
            {

                timePickerDialog = new TimePickerDialog(this, OnTimeShow, baseModel.Hour, baseModel.Minute, true);
                if (baseModel.TimePickerBundler != null)
                {
                    timePickerDialog.OnRestoreInstanceState(baseModel.TimePickerBundler);
                }
                timePickerDialog.Show();
            }
        }

        private void OnTimeShow(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            timePick.Text = e.HourOfDay + " / " + e.Minute;
            baseModel.CurrentView = timePick;
            baseModel.Hour = e.HourOfDay;
            baseModel.Minute = e.Minute;
        }

        private void OnDateShow(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            datePick.Text = e.DayOfMonth + " / " + (e.MonthOfYear + 1) + " / " + e.Year;
            baseModel.CurrentView = datePick;
            baseModel.Day = e.DayOfMonth;
            baseModel.Month = e.MonthOfYear + 1;
            baseModel.Year = e.Year;
        }

        private void Serialize()
        {
            FileOutputStream file = new FileOutputStream(baseModel.Path);
            string output = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<ArrayOfBase xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">";
            foreach (Base item in BaseList)
            {
                output += "\r\n<Base";
                if (!string.IsNullOrEmpty(item.Title))
                {
                    output += " Title=\"" + item.Title + "\"";
                }
                output += " IsAlert=\"" + item.IsAlert.ToString().ToLower() + "\" Year=\"" + item.Year + "\" Month=\"" + item.Month + "\" Day=\"" + item.Day + "\" Hour=\"" + item.Hour + "\" Minute=\"" + item.Minute + "\" Status=\"" + item.Status + "\" >";
                if (!string.IsNullOrEmpty(item.Description))
                {
                    output += "\r\n<Description>" + item.Description + "</Description>";
                }
                output += "\r\n</Base>";
            }
            output += "\r\n</ArrayOfBase>";
            file.Write(Encoding.ASCII.GetBytes(output));
            file.Close();
        }

        private T DeSerialize<T>(T item)
        {
            try
            {
                XmlSerializer deserialize = new XmlSerializer(typeof(T));
                File file = new File(baseModel.Path);
                byte[] output = new byte[file.Length()];
                FileInputStream reader = new FileInputStream(file);
                reader.Read(output);
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(output);
                System.IO.TextReader stream = new System.IO.StreamReader(memoryStream);
                item = (T)deserialize.Deserialize(stream);
                stream.Close();
                memoryStream.Close();
            }
            catch (Exception)
            { }
            return item;
        }

        #endregion

        #region interface members

        IntPtr IJavaObject.Handle
        {
            get { return this.Handle; }
        }

        void IDisposable.Dispose()
        {
            base.Dispose();
        }

        #endregion

    }

}


