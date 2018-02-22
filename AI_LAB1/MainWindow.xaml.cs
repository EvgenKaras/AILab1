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
using System.IO;
using System.Diagnostics;

namespace AI_LAB1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        int[,] field = new int[4,4];

        StreamWriter w = new StreamWriter("log.txt");

        int maxCountState = 4;
        List<int> ideal = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};
        List<int> numbers = new List<int> {1,2,3,7,4,5,8,9,6};
        int count = 0;
        public MainWindow()
        {
            InitializeComponent();

            UIElementCollection quads = Field.Children;
            List<int>.Enumerator en = numbers.GetEnumerator();
            foreach (UIElement e in quads)
            {
                if (e is TextBlock)
                {
                    en.MoveNext();
                    (e as TextBlock).Text = en.Current.ToString();
                   
                }
            }
            Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            w.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Stack<Node> O = new Stack<Node>();
            O.Push(new Node(numbers));
            w.WriteLine("Open Node has been added. Field:"+O.Peek().FieldToString()+". State: "+O.Peek().State.ToString()+"\n\n");
            List<Node> C = new List<Node>();

            while(O.Count !=0)
            {
                Node x = O.Pop();
                if (x.Equals(new Node(ideal)))
                {
                    C.Add(x);
                    break;
                }
                if (C.Contains(x))
                {
                    x.Parent.CountOfChild -= 1;
                    if (x.Parent.CountOfChild == 0)
                    {
                        C.Remove(x.Parent);
                    }
                    continue;
                }
 
                C.Add(x);
                List<int> temp;
                count++;
                w.WriteLine("Parent. Field: " + x.FieldToString() + ". State: " + x.State.ToString() + "\n");
                for (int i=0;x.rotate(i+1,out temp);i++)
                {
                    
                    Node node = new Node(temp, i + 1);
                    node.Parent = x;
                    node.Parent.CountOfChild += 1;
                    if (!O.Contains(node) && !C.Contains(node))
                    {
                        O.Push(node);
                        
                        w.WriteLine("Open Node has been added. Field: " + O.Peek().FieldToString() + ". State: " + O.Peek().State.ToString());
                    }
                    else
                    {
                        ;
                    }
                    
                   w.WriteLine();

                }
                /*if (count == 20)
                    return;*/
            }
        }

        private bool rotate(int number)
        {
            Dictionary<int, int> values = new Dictionary<int, int>();
            switch(number)
            {
                default:
                    {
                        return false;
                    }
                case 1:
                    {
                        values.Add(0, numbers[0]);
                        values.Add(1, numbers[1]);
                        values.Add(3, numbers[3]);
                        values.Add(4, numbers[4]);

                        numbers[0] = values[1];
                        numbers[3] = values[0];
                        numbers[4] = values[3];
                        numbers[1] = values[4];
                    }
                    break;
                case 2:
                    {
                        values.Add(1, numbers[1]);
                        values.Add(2, numbers[2]);
                        values.Add(4, numbers[4]);
                        values.Add(5, numbers[5]);

                        numbers[1] = values[2];
                        numbers[4] = values[1];
                        numbers[5] = values[4];
                        numbers[2] = values[5];
                    }
                    break;
                case 3:
                    {
                        values.Add(3, numbers[3]);
                        values.Add(4, numbers[4]);
                        values.Add(6, numbers[6]);
                        values.Add(7, numbers[7]);

                        numbers[3] = values[4];
                        numbers[6] = values[3];
                        numbers[7] = values[6];
                        numbers[4] = values[7];
                    }
                    break;
                case 4:
                    {
                        values.Add(4, numbers[4]);
                        values.Add(5, numbers[5]);
                        values.Add(7, numbers[7]);
                        values.Add(8, numbers[8]);

                        numbers[4] = values[5];
                        numbers[7] = values[4];
                        numbers[8] = values[7];
                        numbers[5] = values[8];
                    }
                    break;
            }
            Confirm();
            return true;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.NumPad1:
                    {
                        rotate(1);
                    }break;
                case Key.NumPad2:
                    {
                        rotate(2);
                    }break;
                case Key.NumPad3:
                    {
                        rotate(3);
                    }break;
                case Key.NumPad4:
                    {
                        rotate(4);
                    }break;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            w.Close();
            System.Diagnostics.Process.Start("notepad", "log.txt");
        }
        private void Confirm()
        {
            List<int>.Enumerator e = numbers.GetEnumerator();
            foreach(UIElement ui in Field.Children)
            {
                if(ui is TextBlock)
                {
                    e.MoveNext();
                    (ui as TextBlock).Text = e.Current.ToString();
                }
            }
        }
    }
}
