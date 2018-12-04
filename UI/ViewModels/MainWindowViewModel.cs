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
            //TimeToConvertInput = "09:45:12 PM";
            //FactorialText = 22.ToString();
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
                IsSaving = true;
                SaveProgress = 0;

                Task factorialTask = new Task(GetFactorial);
                factorialTask.Start();

                Task convertTask = new Task(ConvertToMilitary);
                convertTask.Start();

                Task.WhenAll(factorialTask, convertTask).GetAwaiter().GetResult();

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
                    SaveProgress += 10;
                    Task2ActionLog += $"Started to convert time to military of {TimeToConvertInput}\n";
                    Thread.Sleep(200);
                    SaveProgress += 10;
                    Task2ResultText = $"Converted date = {convertedDate}";
                    Thread.Sleep(200);
                    SaveProgress += 10;
                    Task2ActionLog += $"Started to write to log file\n";
                    FileWriter.WriteToFileConvertedDate(TimeToConvertInput, convertedDate);
                    Task2ActionLog += $"Completed to do work of thread 2 ";
                    SaveProgress += 10;
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
                        Thread.Sleep(200);
                        SaveProgress += 10;
                        // допустим что значение SaveProgress = 60 то результат = 40
                        double elapsed = 90.0 - SaveProgress;
                        for (byte i = 1; i <= factorial; i++)
                        {
                            BigInteger factorialRes = _factorialHelper.Factorial(i);
                            Task1ResultText += $"!{i} = {factorialRes}\n";

                            Task1ActionLog += $"Started to write {factorialRes} to log file\n";
                            FileWriter.WriteToFileFactorial(factorialRes, i);

                            SaveProgress += elapsed / factorial;

                            Thread.Sleep(50);
                        }
                        Task1ActionLog += $"Completed to do work of thread 1 ";
                        SaveProgress += 10;
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
