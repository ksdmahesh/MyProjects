using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cal.ViewModels
{
    public class Notifier:INotifyPropertyChanged
    {
        #region public events

        public event PropertyChangedEventHandler PropertyChanged; 

        #endregion

        #region public methods

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
            lab:
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
                catch (Exception) { goto lab; }
            }
        } 

        #endregion
    }
}
