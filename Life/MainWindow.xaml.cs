using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Life
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int X = 49, Y = 33;
        private static int _rectWidth = 20, _rectHeight = 20;
        private Rectangle[,] _rectangles = new Rectangle[Y, X];
        private List<bool[,]> _prevValues = new List<bool[,]>();
        private SolidColorBrush _pressedBrush = new SolidColorBrush(Colors.Blue);
        private SolidColorBrush _unpressedBrush = new SolidColorBrush(Colors.LightGray);
        private bool _isChecked = false;
        private double _speed = 500;
        private int _gen = 0;

        public MainWindow()
        {
            InitializeComponent();
            StartButton.Checked += (sender, e) =>
            {
                StartButton.Content = "Стоп";
                _isChecked = true;
            };

            StartButton.Unchecked += (sender, e) =>
            {
                StartButton.Content = "Старт";
                _isChecked = false;
            };

            Speed.ValueChanged += (sender, e) => _speed = Speed.Value;

            Width.Text = X.ToString();
            Height.Text = Y.ToString();

            InitField();

            Width.TextChanged += Width_TextChanged;
            Height.TextChanged += Height_TextChanged;
        }

        private void Height_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(Height.Text, out int y))
            {
                if (y > 0 && y <= 50)
                {
                    ResetButton_Click(null, null);
                    Y = y;
                    InitField();
                }
            }
        }

        private void Width_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(Width.Text, out int x))
            {
                if (x > 0 && x <= 50)
                {
                    ResetButton_Click(null, null);
                    X = x;
                    InitField();
                }
            }
        }

        private void InitField()
        {
            InitSize();

            Field.Children.Clear();
            _rectangles = new Rectangle[Y, X];

            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    Rectangle button = new Rectangle()
                    {
                        Fill = _unpressedBrush,
                        Width = _rectWidth,
                        Height = _rectHeight
                    };

                    button.MouseMove += (sender, e) =>
                    {
                        if (Mouse.LeftButton == MouseButtonState.Pressed)
                            (sender as Rectangle).Fill = _pressedBrush;
                    };
                    button.MouseLeftButtonUp += (sender, e) => (sender as Rectangle).Fill = _pressedBrush;

                    Field.Children.Add(button);
                    _rectangles[i, j] = button;
                }
            }
        }

        private void InitSize()
        {
            Field.Width = X * _rectWidth;
            Field.Height = Y * _rectHeight;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                while (_isChecked)
                {
                    Dispatcher.Invoke(() => NextButton_Click(sender, e));
                    Thread.Sleep(1011 - (int)_speed);
                }
            });
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_gen == 0)
                {
                    bool[,] firstvalue = new bool[Y, X];
                    for (int i = 0; i < Y; i++)
                    {
                        for (int j = 0; j < X; j++)
                            firstvalue[i, j] = _rectangles[i, j].Fill == _pressedBrush;
                    }
                    _prevValues.Add(firstvalue);
                }
                bool[,] prevValue = new bool[Y, X];
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        if (_rectangles[i, j].Fill == _pressedBrush)//если была жива
                        {
                            int alive = GetActiveNeighbours(i, j);
                            prevValue[i, j] = (alive == 2 || alive == 3);
                        }
                        else//если была мертва
                            prevValue[i, j] = GetActiveNeighbours(i, j) == 3;
                    }
                }
                var last = _prevValues.Last();
                bool isEqual = true, isEmpty = true;
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        if (prevValue[i, j] != last[i, j])
                            isEqual = false;
                        if (prevValue[i, j])
                            isEmpty = false;
                        if (prevValue[i, j] && _rectangles[i, j].Fill != _pressedBrush)
                            _rectangles[i, j].Fill = _pressedBrush;
                        else if (!prevValue[i, j] && _rectangles[i, j].Fill != _unpressedBrush)
                            _rectangles[i, j].Fill = _unpressedBrush;
                    }
                }
                if (isEmpty)
                {
                    StartButton.IsChecked = false;
                    MessageBox.Show("Система пуста!");
                    return;
                }
                if (isEqual)
                {
                    StartButton.IsChecked = false;
                    MessageBox.Show("Система устойчива!");
                    return;
                }
                _prevValues.Add(prevValue);
                Generation.Text = $"Поколение: {++_gen}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int GetActiveNeighbours(int y, int x)
        {
            int result = 0;
            if (y - 1 >= 0 && x - 1 >= 0 && _rectangles[y - 1, x - 1].Fill == _pressedBrush)//левый верхний
                result++;
            if (y - 1 >= 0 && _rectangles[y - 1, x].Fill == _pressedBrush)//верхний
                result++;
            if (y - 1 >= 0 && x + 1 < X && _rectangles[y - 1, x + 1].Fill == _pressedBrush)//правый верхний
                result++;
            if (x + 1 < X && _rectangles[y, x + 1].Fill == _pressedBrush)//правый
                result++;
            if (y + 1 < Y && x + 1 < X && _rectangles[y + 1, x + 1].Fill == _pressedBrush)//правый нижний
                result++;
            if (y + 1 < Y && _rectangles[y + 1, x].Fill == _pressedBrush)//нижний
                result++;
            if (y + 1 < Y && x - 1 >= 0 && _rectangles[y + 1, x - 1].Fill == _pressedBrush)//левый нижний
                result++;
            if (x - 1 >= 0 && _rectangles[y, x - 1].Fill == _pressedBrush)//левый
                result++;
            return result;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_gen == 0)
                return;
            try
            {
                _gen -= 1;
                _prevValues.Remove(_prevValues.Last());
                bool[,] prevValue = _prevValues.Last();
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        if (prevValue[i, j] && _rectangles[i, j].Fill != _pressedBrush)
                            _rectangles[i, j].Fill = _pressedBrush;
                        else if (!prevValue[i, j] && _rectangles[i, j].Fill != _unpressedBrush)
                            _rectangles[i, j].Fill = _unpressedBrush;
                    }
                }
                Generation.Text = $"Поколение: {_gen}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsChecked = false;
            try
            {
                for (int i = 0; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        if (_rectangles[i, j] != null)
                            _rectangles[i, j].Fill = _unpressedBrush;
                    }
                }
                _gen = 0;
                _prevValues = new List<bool[,]>();
                Generation.Text = $"Поколение: {_gen}";
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}