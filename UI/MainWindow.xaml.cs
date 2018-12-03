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
    }
}
