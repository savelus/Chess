using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Point = System.Drawing.Point;

namespace Chess.Desktop
{
    /// <summary>
    /// Логика взаимодействия для WPFBoard.xaml
    /// </summary>
    public partial class WPFBoard : Window
    {
        Board board = new Board();
        BoardDrawer drawer;
        Point? startPoint = null;
        public WPFBoard()
        {
            InitializeComponent();
            drawer = new BoardDrawer();
            GetCells();

        }

        private void GetCells()
        {
            BoardCanvas.Children.Clear();

            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 8; i++)
                {
                    Border border = new Border();
                    //border.HorizontalAlignment = HorizontalAlignment.Center;
                    //border.VerticalAlignment = VerticalAlignment.Center;
                    border.MouseDown += Border_MouseDown;
                    Figure figure = board.Field[i, j].FigureInCell;
                    border.Background = (i + j) % 2 == 0 ? Brushes.Black : Brushes.White;
                    if (!board.Field[i, j].IsEmpty)
                    {
                        border.Child = new ContentControl()
                        {
                            Content = figure,
                            Foreground = figure.IsWhite ? Brushes.Gray : Brushes.Red,
                            FontSize = 30,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center
                            
                        };
                    }
                    else
                    {
                        border.DataContext = new Point() { X = i, Y = j };
                    }
                        BoardCanvas.Children.Add(border);

                }
            }
        }
        
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            Point position;
            Figure figure;
            if (border.Child is ContentControl)
            {
                var contentControl = border.Child as ContentControl;
                figure = contentControl.Content as Figure;
                position = board.GetFigureCoordinate(figure);
            }
            else
            {
                position = (Point)border.DataContext;
            }
            if (startPoint == null)
            {
                startPoint = position;
            }
            else
            {
                figure = board.Field[startPoint.Value.X, startPoint.Value.Y].FigureInCell;
                if (figure.IsCorrectMove(startPoint.Value, position, board))
                {
                    board.Field[startPoint.Value.X, startPoint.Value.Y].FigureInCell = null;
                    board.Field[startPoint.Value.X, startPoint.Value.Y].IsEmpty = true;
                    board.Field[position.X, position.Y].FigureInCell = figure;
                    board.Field[position.X, position.Y].IsEmpty = false;

                    GetCells();
                    GetBorderSize(BoardCanvas);
                    
                }
                startPoint = null;
            }
            
        }

        private void GetBorderSize(Canvas canvas)
        {
            int counter = 1;
            Thickness marginCells = new Thickness(0, 0, 0, 0); 
            foreach (var child in canvas.Children)
            {
                if (child is Border)
                {
                    var boardChild = child as Border;
                    boardChild.Width = canvas.ActualWidth / 8;
                    boardChild.Height = canvas.ActualHeight / 8;
                    boardChild.Margin = marginCells;
                    if(counter % 8 == 0)
                    {
                        marginCells.Left = 0;
                        marginCells.Top += canvas.ActualHeight / 8;
                    }
                    else
                    {
                        marginCells.Left += canvas.ActualWidth / 8;
                    }
                    counter += 1;
                }
            }
        }

        private void BoardCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GetBorderSize((Canvas)sender);
        }
    }
}
