using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_15
{
    internal class Game
    {
        int size;
        int[,] map;
        int space_x, space_y;
        static Random rand = new Random();

        public Game(int size)
        {
            //проверка размера поля
            if (size < 2) size = 2;
            if (size > 5) size = 5;
            this.size = size;
            map = new int[size, size];
        }

        public void start()//готовим поле к игре
        {
            //заполняем поле значениями
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    map[x, y] = coords_to_position(x, y) + 1;//добавляем 1 потому что позиции начинаются с 0
            //определение пустой ячейки
            space_x = size - 1;
            space_y = size - 1;
            map[space_x, space_y] = 0;//пустая позиция
        }

        public void shift(int position) // метод перемещения значения с кнопки на кнопку
        {
            int x, y;
            position_to_coords(position, out x, out y);

            //проверка для перемещения клетки только рядом с пустой
            //если сумма равна 2 значит мы перепрыгиваем вверх или в низ
            if (Math.Abs(space_x - x) + Math.Abs(space_y - y) != 1)
                return;

            // в путую клетку записывам значение на которое нажали
            map[space_x, space_y] = map[x, y];
            //в предыдущую клетку записываем ноль
            map[x, y] = 0;
            space_x = x;
            space_y = y;
        }

        public void shift_random()
        {
            //shift(rand.Next(0, size * size));// много ходов в холостю
            int a = rand.Next(0, 4); //максимальное количеств ходов одной плашки 4, по этому расматриваем от 0 до 4
            int x = space_x; //пустая плашка по координате х
            int y = space_y; //пустая плашка по координате у
            switch (a)
            {
                case 0: x--; break; //идем в лево
                case 1: x++; break; //идем в право
                case 2: y--; break; //идем в верх
                case 3: y++; break; //идем в низ
            }
            shift(coords_to_position(x, y));
        }

        public bool chek_numbers()  //проверка окончания игры
        {
            if (!(space_x == size - 1 && space_y == size - 1)) return false; //пустая плашка должна быть в равом нижнем углу
            for (int x = 0; x < size; x++)  // перебираем массив
                for (int y = 0; y < size; y++)
                    if (!(x == size - 1 && y == size - 1)) //если не последняя плашка в которой должен быть пробел идем дальше
                        if (map[x,y]!=coords_to_position(x,y)+1)
                             return false;
            return true;
        }

        public int get_number(int position) //передаем номер кнопки и возвращаем что на ней написано
        {
            int x, y;
            position_to_coords(position, out x, out y);
            if (x < 0 || x >= size) return 0;  //проверка чтобы небыло переполения массива
            if (y < 0 || y >= size) return 0;
            return map[x, y];// возвращаем какое число должно быть написано на этой клетке
        }

        private int coords_to_position(int x, int y) // определение позиции
        {
            //проверка выхода за пределы массива координатами x,y
            //проверяем именно здесь т.к. данная проверка пройдет в любом случае
            if (x < 0) x = 0;
            if (x > size - 1) x = size - 1;
            if (y < 0) y = 0;
            if (y > size - 1) y = size - 1;
            return y * size + x;
        }

        private void position_to_coords(int pozition, out int x, out int y)//получает позицию и возвращает два числа параметрами
        {
            if (pozition < 0) pozition = 0;
            if (pozition > size * size - 1) pozition = size * size - 1;
            x = pozition % size;
            y = pozition / size;
        }
    }
}
