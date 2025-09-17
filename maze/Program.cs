using static maze.Func;
namespace maze
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание экз. кл. ген. лабиринта
            var generator = new GenerateMaze();

            // Генерация лабиринта (можно задать свои размеры (не менее 5х5))
            var (maze, path, rows, cols) = generator.Generate(21, 21);

            // Отображение лабиринта
            showMaze(maze, rows, cols);

            // Старт
            int startLeft = 1, startTop = 1;

            Console.CursorVisible = false;
            drawPosition(startLeft, startTop, '*');

            // Основной цикл
            while (maze[startTop, startLeft] != '=')
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char key = keyInfo.KeyChar;

                switch (key)
                {
                    case 'w':
                    case 'a':
                    case 's':
                    case 'd':
                        {
                            // Если есть возможность двигаться
                            if (canMove(maze, rows, cols, startTop, startLeft, key))
                            {
                                // Очистка позиции 
                                clearPosition(startLeft, startTop);

                                // Выбор направления 
                                switch (key)
                                {
                                    case 'w': 
                                        startTop--; 
                                        break;
                                    case 'a': 
                                        startLeft--; 
                                        break;
                                    case 's': 
                                        startTop++; 
                                        break;
                                    case 'd': 
                                        startLeft++; 
                                        break;
                                }

                                // Отрисовка игрока
                                drawPosition(startLeft, startTop, '*');
                            }
                        }
                        break;

                    // Вызов подсказки
                    case 'r':
                        {
                            // Отрисовка маршрута
                            drawRoute(path, startLeft, startTop);
                        }
                        break;
                }
            }

            // Окончание
            Console.WriteLine("\n\nПобеда ! Нажмите -Q- для выхода.");
            ConsoleKeyInfo keyEnd;
            do
            {
                keyEnd = Console.ReadKey(true);
            } while (keyEnd.Key != ConsoleKey.Q);
        }
    }
}