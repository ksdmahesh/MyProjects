using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Xml.Serialization;
using TaskManager;

namespace TaskManager.Models
{
    [XmlRoot]
    public class Base
    {

        #region private variables

        private int _year, _month, _day, _hour, _minute;

        private object _isExpanded;

        private bool _isAlert;

        private Bundle _timer;

        private string _title,_path;

		private CurrentContent _content = CurrentContent.Main;

		private CurrentStatus _status=CurrentStatus.ToDo;

        #endregion

        #region public properties

        [XmlAttribute]
        public string Title 
        {
            get
            {
                return _title;
            }
            set 
            {
                _title = value;
            }
        }

        [XmlAttribute]
        public bool IsAlert
        {
            get
            {
                return _isAlert;
            }
            set
            {
                _isAlert = value;
            }
        }

        [XmlAttribute]
        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }

        [XmlAttribute]
        public int Month
        {
            get
            {
                return _month;
            }
            set
            {
                _month = value;
            }
        }

        [XmlAttribute]
        public int Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;
            }
        }

        [XmlAttribute]
        public int Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                _hour = value;
            }
        }

        [XmlAttribute]
        public int Minute
        {
            get
            {
                return _minute;
            }
            set
            {
                _minute = value;
            }
        }

		[XmlAttribute]
		public CurrentStatus Status 
		{
			get
			{
				return _status;
			}
			set 
			{
				_status = value;
			}
		}

        [XmlElement]
        public string Description { get; set; }

        [XmlIgnore]
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        [XmlIgnore]       
        public Bundle TimePickerBundler
        {
            get
            {
                return _timer;
            }
            set
            {
                _timer = value;
            }
        }

        [XmlIgnore]
        public object CurrentView
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
            }
        }

		[XmlIgnore]
		public CurrentContent Content
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

        #endregion

    }
}