using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goooal
{
    public class EditDataViewModel: NotifyPropertyChanged
    {
        private string m_PlayName;
        public string PlayName
        {
            get => m_PlayName;
            set
            {
                m_PlayName = value;
                OnPropertyChanged("PlayName");
            }
        }
        private string m_Team1;
        public string Team1
        {
            get => m_Team1;
            set
            {
                m_Team1 = value;
                OnPropertyChanged("Team1");
            }
        }
        private string m_Team2;
        public string Team2
        {
            get => m_Team2;
            set
            {
                m_Team2 = value;
                OnPropertyChanged("Team2");
            }
        }
        private int m_Minutes;
        public int Minutes
        {
            get => m_Minutes;
            set
            {
                m_Minutes = value;
                OnPropertyChanged("Minutes");
            }
        }
    }
}
