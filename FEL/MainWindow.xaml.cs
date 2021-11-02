using Sofia.BLL.Service;
using Sofia.DAL.Repository;
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

namespace FEL
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Menu.Content = new Default();
        }

        private void LearnerBtnClick(object sender, RoutedEventArgs e)
        {
            Menu.Content = new LearnerView();

        }

        private void TutorBtnClick(object sender, RoutedEventArgs e)
        {
            Menu.Content = new TutorView();
        }

        private void MatchBtnClick(object sender, RoutedEventArgs e)
        {
            Menu.Content = new MatchView();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
