using libmas;
using Microsoft.Win32;
using System.Data.Common;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Практическая_работа__13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int[,] matrix;

        private void OpenMasCalc(int[,] matrix, int rows, int cols)
        {

            double sum = 0;
            foreach (double element in matrix)
            {
                sum += element;
            }
            double average = sum / (rows * cols);

            double minDifference = double.MaxValue;
            int bestRow = -1;
            int bestCol = -1;
            int avgMatrix = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double difference = Math.Abs(matrix[i, j] - average);
                    if (difference < minDifference)
                    {
                        minDifference = difference;
                        bestRow = i;
                        bestCol = j;
                        avgMatrix = matrix[bestRow, bestCol];
                    }
                }
            }

            lbMatrix.Items.Clear();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string rowString = "";
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    rowString += matrix[i, j].ToString() + " ";
                }
                lbMatrix.Items.Add(rowString);
            }

            tbResult.Text = Convert.ToString(avgMatrix);
            tblockRows.Text = "Строки:" + Convert.ToString(rows);
            tblockCols.Text = "Столбцы:" + Convert.ToString(cols);
        }

        private void btResult_Click(object sender, RoutedEventArgs e)
        {
            bool boolRows = int.TryParse(tbRows.Text, out int rows);
            bool boolCols = int.TryParse(tbCols.Text, out int cols);
            bool boolRandMas = int.TryParse(MaxMas.Text, out int randMas);
            if (boolCols || boolRows || boolRandMas)
            {
                RadomMatrix.InitMatrix(out matrix, cols, rows, randMas);

                double sum = 0;
                foreach (double element in matrix)
                {
                    sum += element;
                }
                double average = sum / (rows * cols);

                double minDifference = double.MaxValue;
                int bestRow = -1;
                int bestCol = -1;
                int avgMatrix = 0;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        double difference = Math.Abs(matrix[i, j] - average);
                        if (difference < minDifference)
                        {
                            minDifference = difference;
                            bestRow = i;
                            bestCol = j;
                            avgMatrix = matrix[bestRow, bestCol];
                        }
                    }
                }

                lbMatrix.Items.Clear();
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    string rowString = "";
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        rowString += matrix[i, j].ToString() + " ";
                    }
                    lbMatrix.Items.Add(rowString);
                }

                tbResult.Text = Convert.ToString(avgMatrix);
                tblockRows.Text = "Строки:" + Convert.ToString(rows);
                tblockCols.Text = "Столбцы:" + Convert.ToString(cols);
            }
            else MessageBox.Show("Неверные данные или типо того");
        }
        private void OpenMas(int[,] matrix)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы *.*|*.*|Текстовый файл|*txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";

            if (open.ShowDialog() == true)
            {
                StreamReader file = new StreamReader(open.FileName);
                string[] str = file.ReadToEnd().Split('\n');

                int row = Convert.ToInt32(str[0]);
                int column = Convert.ToInt32(str[1]);
                matrix = new int[row, column];
                for (int i = 0; i < row; i++)
                {
                    string[] line = str[i + 2].Split(';');
                    for (int j = 0; j < column; j++)
                    {
                        matrix[i, j] = int.Parse(line[j]);
                    }
                }
                file.Close();

                OpenMasCalc(matrix,row,column);
            }
        }

        private void SaveMas(int[,] matrix)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы *.*|*.*|Текстовый файл|*txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (save.ShowDialog() == true)
            {
                if (matrix != null)
                {
                    StreamWriter file = new StreamWriter(save.FileName);
                    file.WriteLine(matrix.GetLength(0));
                    file.WriteLine(matrix.GetLength(1));
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            file.Write(matrix[i, j] + ";");
                        }
                        file.WriteLine();
                    }
                    file.Close();
                }
                else MessageBox.Show("Нету матрицы!!!");
            }
        }

        private void OpenMas_Click(object sender, RoutedEventArgs e)
        {
            OpenMas(matrix);
        }
        private void SaveMas_Click(object sender, RoutedEventArgs e)
        {
            SaveMas(matrix);
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Информация");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            tbResult.Text = "";
        }

    }
}