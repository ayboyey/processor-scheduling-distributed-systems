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
using System.Threading;

namespace lab_0s_06
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DekkersPage dekPage = new DekkersPage();
        BankersPage banPage = new BankersPage();

        /// <summary>
        /// This method initializes the main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            frame.NavigationService.Navigate(banPage);

        }

        /// <summary>
        /// This method allows the user to move the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void GridMouseDown(Object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// This method allows the user to close the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {    
            Close();
        }

        private void DekkerClick(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(dekPage);
        }

        private void BankerClick(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(banPage);
        }
    }
}
