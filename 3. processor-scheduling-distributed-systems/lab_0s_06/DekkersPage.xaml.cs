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

namespace lab_0s_06
{
    /// <summary>
    /// Interaction logic for DekkersPage.xaml
    /// </summary>
    public partial class DekkersPage : Page
    {
        int priorityVal;
        int[] arrProc1;
        int[] arrProc2;
        List<int> listProc1;
        List<int> listProc2;
        String[] temp;
        int idx = 0;
        SolidColorBrush mySolidColorBrush = new SolidColorBrush();

        public DekkersPage()
        {
            InitializeComponent();
        }



        private void ReadyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                priorityVal = int.Parse(priority.Text);
                Console.WriteLine(priorityVal+"-----------------------"+ priority.Text + "-----------------------\n");


                if (priorityVal != 2 && priorityVal != 1)
                {
                    textOutput.Text = "The value of priority must be either 1 or 2";
                    throw new FormatException();
                }

                turn.Text = $"Process {priorityVal} has priority";

                temp = process1.Text.Split(',');
                arrProc1 = new int[temp.Length];
                for (int i = 0; i < temp.Length; i++) arrProc1[i] = int.Parse(temp[i]);

                temp = process2.Text.Split(',');
                arrProc2 = new int[temp.Length];
                for (int i = 0; i < temp.Length; i++) arrProc2[i] = int.Parse(temp[i]);

                Array.Sort(arrProc1);
                Array.Sort(arrProc2);           
                listProc1 = arrProc1.ToList<int>();
                listProc2 = arrProc2.ToList<int>();



            } catch (FormatException)
            {
                textOutput.Text = "Please insert valid data";
            }
        }

        private void NextState()
        {

            if (listProc1 == null || listProc2 == null) return;

            if (listProc1.Count==0 && listProc2.Count == 0)
            {
                textOutput.Text = "Program has finished running";
                return;
            }

            if (listProc1.Count != 0 && listProc2.Count != 0)
            {

                if (listProc1[0] > listProc2[0])
                {
                    access.Text = "Process 1 has accessed data";
                    listProc1.RemoveAt(0);
                    return;
                }

                if (listProc1[0] < listProc2[0])
                {
                    access.Text = "Process 2 has accessed data";
                    listProc2.RemoveAt(0);
                    return;
                }



                if (listProc1[0] == listProc2[0])
                {
                    String str = $"Both programs wanted to access data at the same time.\nProcess {priorityVal} had priority\n";

                    if (priorityVal == 1)
                    {
                        str += "Program 1 has accessed data\n";
                        priorityVal = 2;
                        turn.Text = $"Process { priorityVal} has priority";
                        access.Text = str;
                        listProc1.RemoveAt(0);
                        return;
                    }

                    str += "Program 2 has accessed data\n";
                    priorityVal = 1;
                    turn.Text = $"Process {priorityVal} has priority";
                    access.Text = str;
                    listProc2.RemoveAt(0);
                    return;
                }
            }

            if(listProc1.Count != 0)
            {
                priorityVal = 1;
                access.Text = "Process 1 has accessed data data";
                access.Text = $"Process {priorityVal} has priority"; 
                listProc1.RemoveAt(0);
                return;
            }

            if (listProc2.Count != 0)
            {
                priorityVal = 2;
                access.Text = "Process 2 has accessed data data";
                access.Text = $"Process {priorityVal} has priority";
                listProc2.RemoveAt(0);
                return;
            }


        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            NextState();
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            priority.Text = "";
            process1.Text = "";
            process2.Text = "";
            listProc1.Clear();
            listProc2.Clear();
        }
    }
}
