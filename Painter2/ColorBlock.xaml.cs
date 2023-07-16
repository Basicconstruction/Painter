using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Painter2
{
    public partial class ColorBlock
    {
        private readonly Color _initialColor;
        private readonly Color _targetColor;
        private readonly int _cube;
        private readonly bool[,] _model;
        
        public ColorBlock(int cube,Color initial, Color target)
        {
            _initialColor = initial;
            _targetColor = target;
            _cube = cube;
            _model = new bool[_cube, _cube];
            InitializeComponent();
            Init();
        }

        public bool[,] Model => _model;

        public void Paint(Direction direction, int index)
        {
            if (direction == Direction.Col)
            {
                for (var i = 0; i < _cube; i++)
                {
                    _model[i, index] = false;
                    var rect = ((Rectangle)((Border)InnerGrid.Children[i * _cube + index]).Child);
                    rect.Fill = new SolidColorBrush(_initialColor);
                }
            }
            else
            {
                for (var j = 0; j < _cube; j++)
                {
                    _model[index, j] = true;
                    var rect = ((Rectangle)((Border)InnerGrid.Children[index * _cube + j]).Child);
                    rect.Fill = new SolidColorBrush(_targetColor);
                }
            }
        }
        private void Init()
        {
            for (var i = 0; i < _cube; i++)
            {
                InnerGrid.RowDefinitions.Add(new RowDefinition());
                InnerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (var row = 0; row < _cube; row++)
            {
                for (var col = 0; col < _cube; col++)
                {
                    _model[col, row] = false;
                    var rectangle = new Rectangle
                    {
                        Fill = new SolidColorBrush(_initialColor),
                        Tag = $"row {row} col {col}"
                    };
                    var border = new Border
                    {
                        BorderBrush = Brushes.Black, // 设置边框颜色
                        BorderThickness = new Thickness(1), // 设置边框厚度
                        Child = rectangle // 将Rectangle作为Border的子组件
                    };
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    InnerGrid.Children.Add(border);
                }
            }
        }
    }
}