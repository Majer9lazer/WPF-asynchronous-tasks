using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using UI.Logic;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ThreadWorker _worker;
        public MainWindow()
        {
            InitializeComponent();
            _worker = new ThreadWorker();

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {



            if (int.TryParse(FactorialInput.Text, out int val))
            {
                if (_worker.CheckTask1Val(val))
                {
                    ResultOfTask1TextBlock.Dispatcher.InvokeAsync(() =>
                        {
                            ResultOfTask1TextBlock.Text = _worker.Factorial((uint)val).ToString();
                        });
                }
                else
                {
                    MessageBox.Show("Warning your number doesn't match the expression");
                }
            }
            else
            {
                MessageBox.Show("Warning your input data contains string");
            }
        }
    }
}
