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
using UI.ViewModels;

namespace UI
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(true, interval: TimeSpan.FromMilliseconds(1), timeOut: 250D);
            DataContext = _viewModel;
        }



        private void Start_To_Work_ButtonClick(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                _viewModel.Start();
            }).ContinueWith(t =>
            {
                _viewModel.Finish();
            }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void Factorial_Number_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.FactorialText = _viewModel.RandomFactorialNum().ToString();
        }

        private void WithSecondsTimePicker_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.TimeToConvertInput = _viewModel.GetRandomTime();
        }
    }
}
