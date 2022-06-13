using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pisaek2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int sizeOfWindow = 16;
        List<Rectangle> lista = new List<Rectangle>();
        int[,] mapa =
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0 },
        };
        public MainWindow()
        {
            InitializeComponent();
            Init();
            Petla();
        }

        void Init()
        {
            for(int i = 0; i < sizeOfWindow; i++)
            {
                Siatka.RowDefinitions.Add(new RowDefinition());
                Siatka.ColumnDefinitions.Add(new ColumnDefinition());
                for(int j = 0; j < sizeOfWindow; j++)
                {

                    if(mapa[i,j] == 1)
                    {
                        Rectangle kwadrat = new Rectangle()
                        {
                            Fill = Brushes.LightYellow,
                        };
                        Siatka.Children.Add(kwadrat);
                        kwadrat.SetValue(Grid.RowProperty, i);
                        kwadrat.SetValue(Grid.ColumnProperty, j);
                        lista.Add(kwadrat);
                    }
                }

            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            //if(e.Key == Key.A)
                //Petla();
        }

        async void Petla() 
        { 

            for(; ; )
            {

                foreach (Rectangle piasek in lista)
                {
                    int posX, posY;
                    posX = int.Parse(piasek.GetValue(Grid.ColumnProperty).ToString());
                    posY = int.Parse(piasek.GetValue(Grid.RowProperty).ToString());
                    if (posY + 1 < sizeOfWindow)
                    {

                        
                    if(mapa[posY+1,posX] == 0)
                    {
                        piasek.SetValue(Grid.RowProperty, posY + 1);
                    mapa[posY, posY] = 0;
                    mapa[posY + 1, posY] = 1;

                    }
                    }

                }
                    await Task.Delay(500);
            }
        }

    }
}
