using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze
{
    internal static class Func
    {
        /// <summary>
        /// Можно ли передвинуться
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool canMove(char[,] maze, int rows, int cols, int top, int left, char key)
        {
            int newTop = top;
            int newLeft = left;

            switch (key)
            {
                case 'w': newTop--; break;
                case 'a': newLeft--; break;
                case 's': newTop++; break;
                case 'd': newLeft++; break;
                default: return false;
            }

            if (newTop < 0 || newTop >= rows || newLeft < 0 || newLeft >= cols)
                return false;

            char cell = maze[newTop, newLeft];
            return cell != '@' && cell != '|' && cell != '-';
        }
       
        /// <summary>
        /// Отрисовка позиции
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public static void drawPosition(int left, int top, char el)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(el);
        }

        /// <summary>
        /// Очистка позиции
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public static void clearPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(" ");
        }

        /// <summary>
        /// Отображение лабиринта
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public static void showMaze(char[,] maze, int rows, int cols)
        {
            Console.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Отображение путя до  финиша
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="leftPos"></param>
        /// <param name="topPos"></param>
        public static void drawRoute(List<(int x, int y)> path, int leftPos, int topPos)
        {
            if (path == null || path.Count == 0) return;

            // Финиш
            var (lastX, lastY) = path.Last();

            foreach (var (x, y) in path)
            {
                if (x == lastX && y == lastY)
                    drawPosition(x, y, '='); 

                // Клетка = . (маршрут до финиша)
                else
                    drawPosition(x, y, '.');
            }

            // Игрок на место
            drawPosition(leftPos, topPos, '*');
        }

        /// <summary>
        /// Перемешка направлений
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void shuffle<T>(List<T> list, Random random)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}