using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UI.Logic;

namespace UI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private double _saveProgress;
        public double SaveProgress
        {
            get { return _saveProgress; }
            private set { this.MutateVerbose(ref _saveProgress, value, RaisePropertyChanged()); }
        }
        private bool _isSaving;
        public bool IsSaving
        {
            get { return _isSaving; }
            private set { this.MutateVerbose(ref _isSaving, value, RaisePropertyChanged()); }
        }
        private string _factorialInput;
        public string FactorialText
        {
            get => _factorialInput;
            set => this.MutateVerbose(ref _factorialInput, value, RaisePropertyChanged());
        }
        private string _task1ResultText;
        public string Task1ResultText { get => _task1ResultText; set => this.MutateVerbose(ref _task1ResultText, value, RaisePropertyChanged()); }
        private string _task1ActionLog;
        public string Task1ActionLog
        {
            get => _task1ActionLog;
            set => this.MutateVerbose(ref _task1ActionLog, value, RaisePropertyChanged());
        }
        private string _task2ActionLog;
        public string Task2ActionLog
        {
            get => _task2ActionLog;
            set => this.MutateVerbose(ref _task2ActionLog, value, RaisePropertyChanged());
        }
        private string _timeToConvertInput;
        private string _task2ResultText;
        public string Task2ResultText { get => _task2ResultText; set => this.MutateVerbose(ref _task2ResultText, value, RaisePropertyChanged()); }

        public string TimeToConvertInput
        {
            get => _timeToConvertInput;
            set => this.MutateVerbose(ref _timeToConvertInput, value, RaisePropertyChanged());
        }

        public MainWindowViewModel()
        {
            // default value to test
            // TimeToConvertInput = "09:45:12 PM";
        }
        private FactorialHelper _factorialHelper = new FactorialHelper();
        private TimeConvertHelper _timeConvertHelper = new TimeConvertHelper();

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        public void Start()
        {
            try
            {
                SaveProgress = 0.0;
                IsSaving = true;
                Task factorialTask = new Task(GetFactorial);
                factorialTask.Start();

                Task convertTask = new Task(ConvertToMilitary);
                convertTask.Start();
                while (!factorialTask.IsCompleted)
                {
                    SaveProgress += 0.5;
                    Thread.Sleep(10);
                }

                while (!convertTask.IsCompleted)
                {
                    SaveProgress += 0.5;
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                IsSaving = false;
                MessageBox.Show(e.Message);
            }
        }

        public void Finish()
        {
            SaveProgress = 100.0;
            IsSaving = false;
        }
        private void ConvertToMilitary()
        {
            try
            {
                if (_timeConvertHelper.Check(TimeToConvertInput))
                {

                    IsSaving = true;
                    string convertedDate = _timeConvertHelper.ConvertToMilitary(TimeToConvertInput).ToString(CultureInfo.InvariantCulture);
                    Thread.Sleep(500);
                    Task2ActionLog += $"Started to convert time to military of {TimeToConvertInput}\n";
                    Thread.Sleep(500);
                    Task2ResultText = $"Converted date = {convertedDate}";
                    Thread.Sleep(500);
                    Task2ActionLog += $"Started to write to log file\n";
                    Task.Run(() =>
                    {
                        FileWriter.WriteToFileConvertedDate(TimeToConvertInput, convertedDate);
                    });
                    Thread.Sleep(500);
                    Task2ActionLog += $"Completed to do work of thread 2 ";
                }
                else
                {
                    IsSaving = false;
                    Task2ResultText = $"Your date doesn't match rules Example is : 07:45:12 PM";
                }
            }
            catch (Exception e)
            {
                IsSaving = false;
                Task2ActionLog = e.ToString();
            }

        }
        private void GetFactorial()
        {
            try
            {
                if (byte.TryParse(_factorialInput, out byte factorial))
                {
                    if (_factorialHelper.CheckInput(factorial))
                    {
                        IsSaving = true;

                        Task1ActionLog += $"Started to find factorial of {factorial}\n";
                        Thread.Sleep(500);
                        BigInteger factorialRes = _factorialHelper.Factorial(factorial);
                        Thread.Sleep(500);
                        Task1ResultText = $"!{factorial} = {factorialRes}";
                        Thread.Sleep(500);
                        Task1ActionLog += $"Started to write to log file\n";
                        Thread.Sleep(500);
                        Task.Run(() =>
                        {
                            FileWriter.WriteToFileFactorial(factorialRes, factorial);
                        });
                        Thread.Sleep(500);
                        Task1ActionLog += $"Completed to do work of thread 1 ";
                    }
                    else
                    {
                        IsSaving = false;
                        Task1ResultText = "Your factorial num is bigger than 100 or less than 1 ";
                    }

                }
                else
                {
                    IsSaving = false;
                    Task1ResultText = "Your number contains string) or doesn't contains anything or your number bigger than 100";
                }
            }
            catch (Exception e)
            {
                IsSaving = false;
                Task1ActionLog = e.ToString();
            }

        }

    }
    public static class NotifyPropertyChangedExtension
    {
        public static void MutateVerbose<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs> raise, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
        }
    }
}
