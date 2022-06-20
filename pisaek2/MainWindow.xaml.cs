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
        int sizeOfWindow = 32;
        List<Rectangle> lista = new List<Rectangle>();
        int cursorRow = 0;
        int cursorCol = 0;
        int[,] mapa;
        public MainWindow()
        {
            InitializeComponent();
            Init();
            Petla();
        }

        void Init()
        {
            mapa = new int[sizeOfWindow, sizeOfWindow];
            for(int i = 0; i < sizeOfWindow; i++)
            {
                Siatka.RowDefinitions.Add(new RowDefinition());
                Siatka.ColumnDefinitions.Add(new ColumnDefinition());
                
                for(int j = 0; j < sizeOfWindow; j++)
                {
                    mapa[i, j] = 0;
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
            if (e.Key == Key.A && cursorCol > 0)
                cursorCol--;
            if (e.Key == Key.D && cursorCol < sizeOfWindow -1)
                cursorCol++;
            if (e.Key == Key.W && cursorRow > 0)
                cursorRow--;
            if (e.Key == Key.S && cursorRow < sizeOfWindow - 1)
                cursorRow++;

            Kursor.SetValue(Grid.RowProperty, cursorRow);
            Kursor.SetValue(Grid.ColumnProperty, cursorCol);

            if (e.Key == Key.Space && mapa[cursorRow,cursorCol] == 0)
            {
                Rectangle kwadrat = new Rectangle()
                {
                    Fill = Brushes.LightYellow,
                };
                Siatka.Children.Add(kwadrat);
                kwadrat.SetValue(Grid.RowProperty, cursorRow);
                kwadrat.SetValue(Grid.ColumnProperty, cursorCol);
                lista.Add(kwadrat);
            }

        }

        async void Petla() 
        {
            for(; ; )
            {

                foreach (Rectangle piasek in lista)
                {
                    int posX = int.Parse(piasek.GetValue(Grid.ColumnProperty).ToString());
                    int posY = int.Parse(piasek.GetValue(Grid.RowProperty).ToString());
                    Tekst.Content = string.Format($"ilosc elementow: {lista.Count} X: {posX} Y: {posY}   Row: {cursorRow} Col: {cursorCol}");

                    if (posY + 1< sizeOfWindow)
                    {
                        if(mapa[posY + 1,posX] != 1)
                        {
                            mapa[posY, posX] = 0;
                            mapa[posY + 1, posX] = 1;
                            piasek.SetValue(Grid.RowProperty, posY + 1);
                        }

                        else if(mapa[posY + 1,posX] == 1)
                        {
                            if(mapa[posY + 1, posX + 1] == 0)
                            {
                                mapa[posY, posX] = 0;
                                mapa[posY + 1, posX + 1] = 1;
                                piasek.SetValue(Grid.RowProperty, posY + 1);
                                piasek.SetValue(Grid.ColumnProperty, posX + 1);
                            }
                            else if(mapa[posY + 1, posX - 1] == 0)
                            {
                                mapa[posY, posX] = 0;
                                mapa[posY + 1, posX - 1] = 1;
                                piasek.SetValue(Grid.RowProperty, posY + 1);
                                piasek.SetValue(Grid.ColumnProperty, posX - 1);
                            }
                        }
                    }

                }
                    await Task.Delay(120);
            }
        }

    }
}
