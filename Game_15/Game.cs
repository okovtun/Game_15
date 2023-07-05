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
            int x,y;
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
            return y * size + x;
        }

        private void position_to_coords(int pozition, out int x, out int y)//получает позицию и возвращает два числа параметрами
        {
            x = pozition % size;
            y = pozition / size;
        }


    }
}
