using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using UI.Annotations;
using UI.Logic;

namespace UI
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
        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        public void Init()
        {
            DateTime started= DateTime.Now;
            
            DispatcherTimer time= new DispatcherTimer();
            IsSaving = true;
            for (double i = 0.0; i <= 100.0; i++)
            {
                this.SaveProgress = i;
                Thread.Sleep(50);
            }

            IsSaving = false;
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ThreadWorker _worker;
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _worker = new ThreadWorker();
            _worker.Start();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        //private void StartButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string text = TimeToConvertDatePicker.Text;
        //        if (text.Contains("AM"))
        //        {
        //            text = text.Replace("AM", string.Empty);
        //            DateTime dateTime = DateTime.ParseExact(text, "hh:mm:ss", CultureInfo.InvariantCulture);
        //        }
        //        else if (text.Contains("PM"))
        //        {
        //            //text = text.Replace("PM", string.Empty);
        //            string s = string.Format("{0:HH:mm:ss tt }", text);
        //            DateTime dateTime = DateTime.ParseExact(text, "hh:mm:sstt", CultureInfo.InvariantCulture);
        //        }
        //        Log("Started task 1...");
        //        Start_Task_1();
        //        Log("Started to write to file factorial value ...");

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogTextBlock.Text += $"Exception = {ex}\n";
        //    }
        //}

        //private void Log(string text)
        //{
        //    Task.Run(() =>
        //    {
        //        ErrorLogTextBlock.Dispatcher.InvokeAsync(() =>
        //        {
        //            ErrorLogTextBlock.Text += $" {text}\n";
        //        });
        //    });
        //}
        //private void Start_Task_1()
        //{
        //    if (byte.TryParse(FactorialInput.Text, out byte val))
        //    {
        //        if (_worker.CheckTask1Val(val))
        //        {
        //            Task1ProgressBar.Maximum = val;
        //            Task<BigInteger> task = new Task<BigInteger>(() => GetFactorialInfo(val));
        //            task.Start();



        //        }
        //        else
        //        {
        //            MessageBox.Show("Warning your number doesn't match the expression");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Warning your input data contains string");
        //    }


        //}
        //private BigInteger GetFactorialInfo(byte val)
        //{
        //    for (byte i = 1; i <= val; i++)
        //    {
        //        ResultOfTask1TextBlock.Dispatcher.InvokeAsync(() =>
        //        {
        //            ResultOfTask1TextBlock.Text += $"!{i} = {Factorial(i)}\n";
        //        });
        //        ResultsOfTask1ScrollViewer.Dispatcher.InvokeAsync(() =>
        //        {
        //            ResultsOfTask1ScrollViewer.ScrollToBottom();
        //        });
        //        Task1ProgressBar.Dispatcher.InvokeAsync(() => { Task1ProgressBar.Value = i; });
        //        Task.Run(() => FileWriter.WriteToFileFactorial(Factorial(i), i));
        //        Thread.Sleep(50);
        //    }

        //    return Factorial(val);
        //}
        //private BigInteger Factorial(BigInteger n)
        //{
        //    if (n == 0)
        //        return 0;
        //    if (n == 1)
        //        return 1;

        //    return n * Factorial(n - 1);
        //}

        private void Start_To_Work_ButtonClick(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                _viewModel.Init();
            }).ContinueWith(t => { MessageBox.Show("Completed"); }, TaskScheduler.FromCurrentSynchronizationContext());
            Task task = new
                Task(() =>
                {


                });
            task.Start();

        }
    }
}
