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
    /// Interaction logic for BankersPage.xaml
    /// </summary>
    public partial class BankersPage : Page
    {
        int processes;
        int resources;
        int[] allocation;
        int[,] allocated;
        bool[] running;
        int[] claim;
        int[,] maxClaim;
        int[] available;
        int counter = 0;
        String[] temp;

        public BankersPage()
        {
            InitializeComponent();
        }

        private void ReadyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int i;
                int k;
                int bound;
                processes = int.Parse(processesBox.Text);
                resources = int.Parse(resourcesBox.Text);

                running = new bool[processes];
                claim = new int[resources];
                allocated = new int[processes, resources];
                allocation = new int[resources];
                available = new int[resources];
                maxClaim = new int[processes, resources];

                for (i=0 ; i < processes; i++)
                {
                    running[i] = true;
                    counter++;
                } 

                temp = claimBox.Text.Split(',');
                if (temp.Length == 0) {
                    textOutput.Text = "Please claim more resources";
                    throw new FormatException(); 
                }
                if (temp.Length > claim.Length) bound = claim.Length;
                else { bound = temp.Length; }

                for (i = 0; i < bound; i++) claim[i] = int.Parse(temp[i]); //CLAIM

                temp = allocatedBox.Text.Split(',');    //ALLOCATED
                if (temp.Length == 0)
                {
                    textOutput.Text = "Please claim more resources";
                    throw new FormatException();
                }

                int j = 0;
                for (i = 0; i < processes; i++)
                    for (k = 0; k < resources; k++)
                    {
                        allocated[i, k] = int.Parse(temp[j]);
                        if (j + 1 < temp.Length) j++;
                    }

                j = 0;

                temp = maxBox.Text.Split(',');    //MAX CLAIM
                if (temp.Length == 0)
                {
                    textOutput.Text = "Please claim more resources";
                    throw new FormatException();
                }
                for (i = 0; i < processes; i++)
                    for (k = 0; k < resources; k++)
                    {
                        maxClaim[i, k] = int.Parse(temp[j]);
                        if (j + 1 < temp.Length) j++;
                    }

                for (i = 0; i < processes; i++)
                    for (k = 0; k < resources; k++)
                    {
                        allocation[k] += allocated[i, k];
                    }

                for(i=0; i<resources; i++)
                {
                    available[i] = claim[i] - allocation[i];
                }

                Run();
            }
            catch (FormatException)
            {
                textOutput.Text = "Please enter proper data";

                Console.WriteLine("-------------------ERROR\n");
            }
        }


        private void Run()
        {
            if (processes == null || resources == null || claim == null || maxClaim == null || allocated == null) return;

            Console.WriteLine("-----------------RUNNING!!!\n");
            bool safe;
            bool Exection;
            String str = "";

            int j;
            int i;

                while (counter != 0)
                {
                    safe = false;
                    for (i = 0; i < this.processes; i++)
                    {
                        if (running[i])
                        {
                            Exection = true;
                            for (j = 0; j < resources; j++)
                            {
                                if ((maxClaim[i, j] - allocated[i, j]) > available[j])
                                {
                                    Exection = false;
                                    break;
                                }
                            }
                            if (Exection)
                            {
                                str += $"Process {i + 1} is Execting \n";
                                textOutput.Text = str;


                                running[i] = false;
                                counter--;
                                safe = true;

                                for (j = 0; j < resources; j++)
                                {
                                    available[j] += allocated[i, j];
                                }
                                break;
                            }
                        }
                    }
                    if (!safe)
                    {
                        str += "The processes are in unsafe state\n";
                        textOutput.Text = str;
                        break;
                    }
                    if (safe)
                    {
                        str += "The process is in safe state\n";
                        str += "Available Resources: \n";

                        for (i = 0; i < resources; i++) str += available[i] + ", ";
                        textOutput.Text = str.Substring(0,str.Length-2);

                    }
                }

        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            processesBox.Text = "";
            resourcesBox.Text = "";
            allocatedBox.Text = "";
            claimBox.Text = "";
            maxBox.Text = "";

        }
    }
}
