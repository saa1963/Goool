using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Stateless;

namespace Goooal
{
    enum TabloStates
    {
        State1, State5, State6, State8, State11, State12, State16
    }
    enum TabloProcesses
    {
        Space, Z, Self, Timer, Timer_1, Ctrl_Space
    }
    public class MainWindowViewModel: NotifyPropertyChanged
    {
        private TimeSpan m_InitIntervalValue = new TimeSpan(0, 1, 0);
        private TimeSpan m_InitIntervalValue_1 = new TimeSpan(0, 0, 15);

        private readonly TimeSpan second = new TimeSpan(0, 0, 1);
        private DispatcherTimer Timer { get; set; }
        private DispatcherTimer Timer_1 { get; set; }

        public MainWindowViewModel()
        {
            stateMachine = new StateMachine<TabloStates, TabloProcesses>(TabloStates.State1);
            stateMachine.Configure(TabloStates.State1).Permit(TabloProcesses.Ctrl_Space, TabloStates.State6);
            stateMachine.Configure(TabloStates.State1).Permit(TabloProcesses.Ctrl_Space, TabloStates.State8);
            stateMachine.Configure(TabloStates.State1).Permit(TabloProcesses.Ctrl_Space, TabloStates.State11);
            stateMachine.Configure(TabloStates.State1).Permit(TabloProcesses.Ctrl_Space, TabloStates.State12);
            stateMachine.Configure(TabloStates.State1).Permit(TabloProcesses.Ctrl_Space, TabloStates.State16);
            Interval = m_InitIntervalValue;
            Interval_1 = m_InitIntervalValue_1;
            SetTimer();
            SetTimer_1();
        }
        private bool m_TimerStopped = false;
        public bool TimerStopped
        {
            get => m_TimerStopped;
            set
            {
                m_TimerStopped = value;
                OnPropertyChanged("TimerStopped");
            }
        }
        private bool m_Timer_1_InitOrEnded = true;
        public bool Timer_1_InitOrEnded
        {
            get => m_Timer_1_InitOrEnded;
            set
            {
                m_Timer_1_InitOrEnded = value;
                OnPropertyChanged("Timer_1_InitOrEnded");
            }
        }

        private void ResetTimer(object obj)
        {
            if (Timer.IsEnabled)
            {
                Timer.Stop();
            }
            TimerStopped = false;
            Timer_1_InitOrEnded = true;
            Interval = m_InitIntervalValue;
        }

        private void ResetTimer_1(object obj)
        {
            if (Timer_1.IsEnabled)
            {
                Timer_1.Stop();
            }
            Interval_1 = m_InitIntervalValue_1;
        }

        private void SwitchTimer(object obj)
        {
            if (!Timer.IsEnabled)
            {
                Timer.Start();
                TimerStopped = false;
                Timer_1_InitOrEnded = false;
            }
            else
            {
                Timer.Stop();
                TimerStopped = true;
            }
        }

        private void SwitchTimer_1(object obj)
        {
            ResetTimer_1(null);
            if (!Timer_1.IsEnabled)
            {
                Timer_1.Start();
            }
            else
            {
                Timer_1.Stop();
            }
        }

        private void SetTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Tick += M_Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void SetTimer_1()
        {
            Timer_1 = new DispatcherTimer();
            Timer_1.Tick += M_Timer_1_Tick;
            Timer_1.Interval = new TimeSpan(0, 0, 1);
        }

        private void M_Timer_Tick(object sender, EventArgs e)
        {
            if (Interval.Ticks > 0)
            {
                Interval = Interval.Subtract(second);
            }
            else
            {
                Timer.Stop();
                TimerStopped = true;
            }
        }

        private void M_Timer_1_Tick(object sender, EventArgs e)
        {
            if (Interval_1.Ticks > 0)
            {
                Interval_1 = Interval_1.Subtract(second);
            }
            else
            {
                Timer_1.Stop();
                m_Timer_1_InitOrEnded = true;
            }
        }

        private string m_Team1 = "Команда1";
        public string Team1
        {
            get => m_Team1;
            set
            {
                m_Team1 = value;
                OnPropertyChanged("Team1");
            }
        }
        private string m_Team2 = "Команда2";
        public string Team2
        {
            get => m_Team2;
            set
            {
                m_Team2 = value;
                OnPropertyChanged("Team2");
            }
        }
        private int m_Fouls1 = 0;
        public int Fouls1
        {
            get => m_Fouls1;
            set
            {
                m_Fouls1 = value;
                OnPropertyChanged("Fouls1");
            }
        }
        private int m_Fouls2 = 0;
        public int Fouls2
        {
            get => m_Fouls2;
            set
            {
                m_Fouls2 = value;
                OnPropertyChanged("Fouls2");
            }
        }
        private string m_PlayName = "Название игры";
        public string PlayName
        {
            get => m_PlayName;
            set
            {
                m_PlayName = value;
                OnPropertyChanged("PlayName");
            }
        }
        private int m_Score1 = 0;
        public int Score1
        {
            get => m_Score1;
            set
            {
                m_Score1 = value;
                OnPropertyChanged("Score1");
            }
        }
        private int m_Score2 = 0;
        public int Score2
        {
            get => m_Score2;
            set
            {
                m_Score2 = value;
                OnPropertyChanged("Score2");
            }
        }
        private TimeSpan m_Interval;
        private StateMachine<TabloStates, TabloProcesses> stateMachine;

        public TimeSpan Interval
        {
            get => m_Interval;
            set
            {
                m_Interval = value;
                OnPropertyChanged("Interval");
            }
        }
        private TimeSpan m_Interval_1;
        public TimeSpan Interval_1
        {
            get => m_Interval_1;
            set
            {
                m_Interval_1 = value;
                OnPropertyChanged("Interval_1");
            }
        }
        public RelayCommand Score2IncCommand
        {
            get => new RelayCommand(s => Score2++);
        }
        public RelayCommand Score2DecCommand
        {
            get => new RelayCommand(s => Score2--);
        }
        public RelayCommand Score1IncCommand
        {
            get => new RelayCommand(s => Score1++);
        }
        public RelayCommand Score1DecCommand
        {
            get => new RelayCommand(s => Score1--);
        }
        public RelayCommand SwitchTimerCommand
        {
            get => new RelayCommand(SwitchTimer);
        }
        public RelayCommand SwitchTimer_1Command
        {
            get => new RelayCommand(SwitchTimer_1);
        }
        public RelayCommand ResetTimerCommand
        {
            get => new RelayCommand(ResetTimer);
        }
        public RelayCommand ResetTimer_1Command
        {
            get => new RelayCommand(ResetTimer_1);
        }
        public RelayCommand Fouls2IncCommand
        {
            get => new RelayCommand(s => Fouls2++);
        }
        public RelayCommand Fouls2DecCommand
        {
            get => new RelayCommand(s => Fouls2--);
        }
        public RelayCommand Fouls1IncCommand
        {
            get => new RelayCommand(s => Fouls1++);
        }
        public RelayCommand Fouls1DecCommand
        {
            get => new RelayCommand(s => Fouls1--);
        }
        public RelayCommand EditDataCommand
        {
            get => new RelayCommand(EditData);
        }

        private void EditData(object obj)
        {
            var vm = new EditDataViewModel() { Team1 = Team1, Team2 = Team2, PlayName = PlayName, Minutes = m_InitIntervalValue.Minutes };
            var f = new EditDataView() { DataContext = vm };
            if (f.ShowDialog() ?? false)
            {
                Team1 = vm.Team1;
                Team2 = vm.Team2;
                PlayName = vm.PlayName;
                var oldInterval = m_InitIntervalValue;
                m_InitIntervalValue = new TimeSpan(0, vm.Minutes, 0);
                if (!Timer.IsEnabled && Interval.Minutes == oldInterval.Minutes)
                {
                    Interval = m_InitIntervalValue;
                }
            }
        }

        public RelayCommand HelpCommand
        {
            get => new RelayCommand(Help);
        }

        private void Help(object obj)
        {
            var vm = new HelpViewModel();
            var f = new HelpView() { DataContext = vm };
            f.ShowDialog();
        }
    }
}
