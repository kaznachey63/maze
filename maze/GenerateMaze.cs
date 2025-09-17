using System;
using System.Collections.Generic;
using static maze.Func;

namespace maze
{
    public class GenerateMaze
    {
        private readonly Random _random;

        public GenerateMaze()
        {
            _random = new Random(1);
        }

        /// <summary>
        /// Генерация лабиринта
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public (char[,] Grid, List<(int x, int y)> Path, int Rows, int Cols) Generate(int width, int height)
        {
            // Не менее 5 клеток
            if (width < 5) width = 5;
            if (height < 5) height = 5;

            // Матрица лабиринта
            char[,] grid = new char[height, width];

            // Заполнение матрицы стенами
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    grid[y, x] = '@'; 

            // Направления 
            (int dx, int dy)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };

            // Стек, для хранения клеток-проходов
            var stack = new Stack<(int x, int y)>();

            // Мап для хранения пути от старта до финиша
            var parent = new Dictionary<(int x, int y), (int x, int y)>();

            // Старт
            grid[1, 1] = ' ';

            stack.Push((1, 1));
            parent[(1, 1)] = (-1, -1); 

            // Финиш
            (int x, int y) exitPos = (width - 2, height - 2);

            bool reachedExit = false;

            while (stack.Count > 0)
            {
                // Чтение послдней лежащей клетки
                var (x, y) = stack.Peek();
                    
                // Перемешка направлений
                var shuffled = new List<(int dx, int dy)>(directions);
                shuffle(shuffled, _random);

                bool found = false;
                foreach (var (dx, dy) in shuffled)
                {
                    // Задание координат клетки через одну от текущей, для образования прохода
                    int nx = x + dx * 2;
                    int ny = y + dy * 2;

                    // Если новая клетка не выходит за границы и это стена
                    if (nx >= 1 && nx < width - 1 && 
                        ny >= 1 && ny < height - 1 && 
                        grid[ny, nx] == '@')
                    {
                        // Очищение клетки между текущей и новой
                        grid[y + dy, x + dx] = ' ';

                        // Очищение новой клетки
                        grid[ny, nx] = ' ';

                        // Добавление новой клетки-прохода
                        stack.Push((nx, ny));
                        parent[(nx, ny)] = (x, y); // ← ЗАПОМИНАЕМ РОДИТЕЛЯ

                        // Если цикл дошел до конца лабиринта
                        if (nx == exitPos.x && ny == exitPos.y)
                        {
                            reachedExit = true;
                        }

                        found = true;
                        break;
                    }
                }

                // Если направления не найдено - удаляем данную точку
                if (!found)
                {
                    stack.Pop();
                }
            }

            // Путь
            var path = new List<(int x, int y)>();

            // Если да
            if (reachedExit)
            {
                var current = exitPos;

                // Добавление точек в лист
                while (current != (-1, -1))
                {
                    path.Add(current);           // ← ДОБАВЛЯЕМ ТЕКУЩУЮ КЛЕТКУ
                    current = parent[current];   // ← ПЕРЕХОДИМ К РОДИТЕЛЮ
                }
                path.Reverse(); 
            }

            // Отрисовка границ
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1)
                        grid[y, x] = '-';
                    else if (x == 0 || x == width - 1)
                        grid[y, x] = '|';
                }
            }

            // Отрисовка финиша
            grid[height - 2, width - 2] = '=';

            // Отрисовка углов
            grid[0, 0] = '+';
            grid[0, width - 1] = '+';
            grid[height - 1, 0] = '+';
            grid[height - 1, width - 1] = '+';

            return (grid, path, height, width);
        }
    }
}