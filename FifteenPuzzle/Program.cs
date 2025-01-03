using System;

class Program
{
    static int[,] board = new int[4, 4];
    static int emptyRow = 3; // Позиция пустой плитки
    static int emptyCol = 3;

    static void Main(string[] args)
    {
        InitializeBoard();
        while (true)
        {
            PrintBoard();
            ConsoleKeyInfo key = Console.ReadKey(true);
            Move(key.Key);

            if (IsSolved())
            {
                PrintBoard();
                Console.WriteLine("Поздравляю! Вы выиграли!");
                Console.WriteLine("Нажмите Enter для перезапуска или Esc для выхода.");

                while (true)
                {
                    ConsoleKeyInfo restartKey = Console.ReadKey(true);
                    if (restartKey.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Выход из игры... Нажмите любую клавишу для завершения.");
                        Console.ReadKey();
                        return; // Выход из программы
                    }
                    else if (restartKey.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("Перезапуск игры...");
                        InitializeBoard();
                        break; // Выход из внутреннего цикла для перезапуска игры
                    }
                }
            }
        }
    }

    static void InitializeBoard()
    {
        int num = 1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == 3 && j == 3)
                {
                    board[i, j] = 0; // Пустая плитка
                }
                else
                {
                    board[i, j] = num++;
                }
            }
        }
        ShuffleBoard();
    }

    static void ShuffleBoard()
    {
        Random rand = new Random();
        for (int i = 0; i < 100; i++)
        {
            int direction = rand.Next(4);
            switch (direction)
            {
                case 0: Move(ConsoleKey.UpArrow); break;
                case 1: Move(ConsoleKey.DownArrow); break;
                case 2: Move(ConsoleKey.LeftArrow); break;
                case 3: Move(ConsoleKey.RightArrow); break;
            }
        }
    }

    static void PrintBoard()
    {
        Console.Clear();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (board[i, j] == 0)
                {
                    Console.Write("   "); // Пустая плитка
                }
                else
                {
                    Console.Write($"{board[i, j],-3}");
                }
            }
            Console.WriteLine();
        }
    }

    static void Move(ConsoleKey key)
    {
        int newRow = emptyRow;
        int newCol = emptyCol;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                newRow++;
                break;
            case ConsoleKey.DownArrow:
                newRow--;
                break;
            case ConsoleKey.LeftArrow:
                newCol++;
                break;
            case ConsoleKey.RightArrow:
                newCol--;
                break;
            default:
                return; // Игнорируем другие клавиши
        }

        // Проверяем, находится ли новая позиция в пределах доски
        if (newRow >= 0 && newRow < 4 && newCol >= 0 && newCol < 4)
        {
            // Меняем местами пустую плитку и плитку, на которую мы перемещаемся
            board[emptyRow, emptyCol] = board[newRow, newCol];
            board[newRow, newCol] = 0;
            emptyRow = newRow;
            emptyCol = newCol;
        }
    }

    static bool IsSolved()
    {
        int num = 1;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == 3 && j == 3)
                {
                    if (board[i, j] != 0) return false; // Последняя плитка должна быть пустой
                }
                else
                {
                    if (board[i, j] != num++) return false;
                }
            }
        }
        return true;
    }
}
