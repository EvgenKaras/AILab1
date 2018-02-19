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

        List<int> ideal = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};
        bool isLeft;
        bool isTop;
        bool isDoubleTop;
        bool isDoubleLeft;
        List<int> numbers = new List<int> { 1,2,5,4,3,6,8,7,9};
        public MainWindow()
        {
            InitializeComponent();
            isLeft = true;
            isTop = true;
            isDoubleTop = true;
            isDoubleLeft = true;

            UIElementCollection quads = Field.Children;
            List<int>.Enumerator en = numbers.GetEnumerator();
            foreach (UIElement e in quads)
            {
                if (e.GetType().ToString().Contains("TextBlock"))
                {
                    en.MoveNext();
                    ((TextBlock)e).Text = en.Current.ToString();
                   
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

            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();
            Debug.WriteLine("Begin Search");
            Stack<Node> nodes = new Stack<Node>();
            nodes.Push(new Node(numbers));
            w.WriteLine(">>> Node has been added:{0}. State:{1}",nodes.Peek().FieldToString(),nodes.Peek().State );
            while(!nodes.Peek().isVictory())
            {
                
                if (rotate(nodes.Peek().State))
                {
                    nodes.Push(new Node(numbers));
                    w.WriteLine(">>> Node has been added:{0}. State:{1}", nodes.Peek().FieldToString(),nodes.Peek().State);

                    int iter = 0;
                    foreach (Node n in nodes)
                    {

                        if (n.Field.SequenceEqual(nodes.Peek().Field) && iter != 0)
                        {
                            w.WriteLine(">>> Node has been remove:{0}", nodes.Peek().FieldToString(),nodes.Peek().State);
                            nodes.Pop();
                            nodes.Peek().State += 1;
                            numbers = new List<int>(nodes.Peek().Field);
                            break;
                        }
                        iter++;
                    }
                }
                else
                {
                    w.WriteLine(">>> Node has been remove:{0}", nodes.Peek().FieldToString(), nodes.Peek().State);
                    nodes.Pop();
                    nodes.Peek().State += 1;
                    numbers = new List<int>(nodes.Peek().Field);
                }
                
            }
            sw.Stop();
            Debug.WriteLine("End search");
            MessageBox.Show("Поиск завершен");
            MessageBox.Show("Кол-во вершин: " + nodes.Count);
            MessageBox.Show("Время исполнения: "+sw.ElapsedMilliseconds);
            String path = "";
            foreach(Node n in nodes)
            {
                path += n.State + " ";
            }
            w.WriteLine(path);
            w.Close();
        }

        private bool rotate(int number)
        {
            List<TextBlock> elements = new List<TextBlock>();
            switch(number)
            {
                default:
                    {
                        return false;
                    }
                case 1:
                    {
                        elements.AddRange(new TextBlock[] { q1, q2, q4, q5 });
                    }break;
                case 2:
                    {
                        elements.AddRange(new TextBlock[] { q2, q3, q5, q6 });
                    }
                    break;
                case 3:
                    {
                        elements.AddRange(new TextBlock[] { q4, q5, q7, q8 });
                    }
                    break;
                case 4:
                    {
                        elements.AddRange(new TextBlock[] { q5, q6, q8, q9 });
                    }
                    break;
            }
            String firstElementText = elements[0].Text;
            String secondElementText = elements[1].Text;
            String thirdElementText = elements[2].Text;
            String fourthElementText = elements[3].Text;

            elements[0].Text = secondElementText;
            elements[1].Text = fourthElementText;
            elements[2].Text = firstElementText;
            elements[3].Text = thirdElementText;

            numbers.Clear();
            foreach (UIElement el in Field.Children)
            {
                if (el.GetType().ToString().Contains("TextBlock"))
                {
                    int temp = Int32.Parse(((TextBlock)el).Text);
                    numbers.Add(temp);
                }
            }
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
            System.Diagnostics.Process.Start("code", "log.txt");
        }
    }
}
