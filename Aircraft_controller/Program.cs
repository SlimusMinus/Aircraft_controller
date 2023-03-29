using System;
using System.Text;
using static System.Console;
using System.IO;

namespace Airplane_exam
{
    public delegate void Flydelegate(int speed, int height);
    public delegate void DelegPoints(int recomend_heidht, int height, int height_comparison);


    internal class Program
    {

        public static void Main(string[] args)
        {
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.DarkBlue;
            Clear();
            string fPath = "Black_Box.txt", str = "";
            int height = 0, speed = 0, distance = 0, points = 0, height_comparison = 0, recomend_heidht = 0, x = 10, y = 40;
            Random random = new Random();
            //корректировка погодных условий 
            int wether = random.Next(-200, 200);
            int wether2 = random.Next(-200, 200);

            ConsoleKeyInfo key;

            Airplane air = new Airplane(height, speed, points);

            WriteLine("Введите имя первого диспетчера");
            string name1 = ReadLine();
            Dispatcher dispatcher1 = new Dispatcher(name1);
            //подписка на событие вывода на консоль текущей скорости и высоты
            dispatcher1.fly_event += air.Flight_correction;
            //подписка на событие штрафных баллов
            dispatcher1.delegpoints += air.Points_penal;

            WriteLine("Введите имя второго диспетчера");
            string name2 = ReadLine();
            Dispatcher dispatcher2 = new Dispatcher(name2);
            dispatcher2.fly_event += air.Flight_correction;
            dispatcher2.delegpoints += air.Points_penal;
            Clear();

            Write( $"Управление самолетом переходит диспетчеру {name1}\n");
            Write( "Начните полет с набора скорости на взлетной полосе клавишой ->\n");
             
            do
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow)
                    air.Myspeed += 50;
                else
                    WriteLine("Вы начинаете движение самолета не с той кнопки, повторите ввод");

            } while (key.Key != ConsoleKey.RightArrow);
            Clear();
            Write( "Вы начинаете набор скорости и высоты\n");

            do
            {

                Clear();
                SetCursorPosition(x, y);
                WriteLine("^==/==>");

                key = ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    air.Myheight += 50;
                    y-=2;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.W)
                {
                    air.Myheight += 150;
                    y-=2;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (air.Myheight <= 0)
                        dispatcher1.Exeptions_null(air.Myheight);
                    else
                        air.Myheight -= 50;
                    y+=2;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.S)
                {
                    if (air.Myheight <= 0)
                        dispatcher1.Exeptions_null(air.Myheight);
                    else
                        air.Myheight -= 150;
                    y+=2;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    air.Myspeed += 50;
                    if (air.Myspeed > 1000)
                        Write($"Немедленно снизьте скорость, штрафные баллы - {air.Mypoints += 100}\n");
                    x+=4;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.D)
                {
                    air.Myspeed += 150;
                    if (air.Myspeed > 1000)
                        Write($"Немедленно снизьте скорость, штрафные баллы - {air.Mypoints += 100}\n");
                    x+=4;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (air.Myspeed <= 0)
                        dispatcher1.Exeptions_null(air.Myspeed);
                    else
                        air.Myspeed -= 50;
                    x-=4;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.A)
                {
                    if (air.Myspeed <= 0)
                        dispatcher1.Exeptions_null(air.Myspeed);
                    else
                        air.Myspeed -= 150;
                    x-=4;
                }
                //событие вывода на консоль текущей скорости и высоты
                dispatcher1.Flight_correction(air.Myspeed, air.Myheight);

                Write( $"Рекомендуемая высота {recomend_heidht = 4 * air.Myspeed - wether}\n");
                WriteLine("***********************************************************");
                //разница между рекомендуемой высотой и текущей
                height_comparison = recomend_heidht - air.Myheight;
                if (height_comparison < 0)
                    height_comparison*=-1;
                //метод класса по выоте более 1000 км и разницей между текущей и рекомендуемой высотой
                air.Penalty_points_height(air.Myheight, height_comparison);
                //увеличение дистанции
                distance += 50;
               
                //событие штрафных быллов
                air.Points_penal(recomend_heidht, air.Myheight, height_comparison);
                points = air.Mypoints;
                ReadKey();

            } while (distance != 500);
            Clear();
            WriteLine("**************************************************************************************");
            Write( $"Вы пролетели {distance} км и теперь управление самолетом переходит диспетчеру {name2}\n");
            WriteLine("**************************************************************************************");
            ReadKey();
            do
            {

                Clear();
                SetCursorPosition(x, y);
                WriteLine("^==/==>");
               
                key = ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    air.Myheight += 50;
                    y-=2;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.W)
                {
                    air.Myheight += 150;
                    y-=2;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (air.Myheight <= 0)
                        dispatcher2.Exeptions_null(air.Myheight);
                    else
                        air.Myheight -= 50;
                    y+=2;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.S)
                {
                    if (air.Myheight <= 0)
                        dispatcher2.Exeptions_null(air.Myheight);
                    else
                        air.Myheight -= 150;
                    y+=2;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    air.Myspeed += 50;
                    if (air.Myspeed > 1000)
                        Write($"Немедленно снизьте скорость, штрафные баллы - {air.Mypoints += 100}\n");
                    x+=4;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.D)
                {
                    air.Myspeed += 150;
                    if (air.Myspeed > 1000)
                        Write($"Немедленно снизьте скорость, штрафные баллы - {air.Mypoints += 100}\n");
                    x+=4;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (air.Myspeed <= 0)
                        dispatcher2.Exeptions_null(air.Myspeed);
                    else
                        air.Myspeed -= 50;
                    x-=4;
                }
                else if (key.Modifiers == ConsoleModifiers.Shift && key.Key == ConsoleKey.A)
                {
                    if (air.Myspeed <= 0)
                        dispatcher2.Exeptions_null(air.Myspeed);
                    else
                        air.Myspeed -= 150;
                    x-=4;
                }
                //событие вывода на консоль текущей скорости и высоты
                dispatcher2.Flight_correction(air.Myspeed, air.Myheight);

                Write( $"Рекомендуемая высота {recomend_heidht = 4 * air.Myspeed - wether2}\n");
                WriteLine("***********************************************************");
                height_comparison = recomend_heidht - air.Myheight;
                if (height_comparison < 0)
                    height_comparison*=-1;
                air.Penalty_points_height(air.Myheight, height_comparison);
                distance += 50;
                air.Points_penal(recomend_heidht, air.Myheight, height_comparison);
                ReadKey();

            } while (distance != 950);
            Clear();
            WriteLine("***********************************************************");
            WriteLine( "Происходит посадка самолета, снизьте скорость и высоту до 0");
            WriteLine("***********************************************************");
            ReadKey();
            do
            {
                if (x < 0)
                    x = 0;
                else if (x >= BufferWidth)
                    x = BufferWidth - 1;

                if (y < 0)
                    y = 0;
                else if (y >= BufferHeight)
                    y = BufferHeight - 1;

                Clear();
                SetCursorPosition(x, y);
                WriteLine("^==/==>");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            air.Myheight += 150;
                            dispatcher2.Flight_correction(air.Myspeed, air.Myheight);
                            y-=2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            air.Myheight -= 150;
                            if (air.Myheight < 0)
                            {
                                SetCursorPosition(0, 0);
                                WriteLine("Вы уже на земле, снижайте скорость");
                                air.Myheight = 0;
                            }
                            else
                                dispatcher2.Flight_correction(air.Myspeed, air.Myheight);
                            y+=2;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        {
                            air.Myspeed += 50;
                            dispatcher2.Flight_correction(air.Myspeed, air.Myheight);
                            x+=4;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        {
                            air.Myspeed -= 50;
                            if (air.Myspeed < 0)
                            {
                                SetCursorPosition(0, 0);
                                WriteLine("Скорость равна 0, снижайтесь");
                                air.Myspeed = 0;
                            }
                            else
                                dispatcher2.Flight_correction(air.Myspeed, air.Myheight);
                            x-=4;
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
                ReadKey();

            } while (air.Myspeed > 0 && air.Myheight > 0);
            Clear();
            string tmp_str;
            WriteLine("******************************************************");
            Write(tmp_str = "Программа «Тренажер пилота самолета» завершена успешно\n");
            str += tmp_str;
            tmp_str = "";
            Write(tmp_str = $"Диспетчер {name1} начислил(а) {points} штрафных баллов\n");
            str += tmp_str;
            tmp_str = "";
            Write(tmp_str = $"Диспетчер {name2} начислил(а) {air.Mypoints - points} штрафных баллов\n");
            str += tmp_str;
            tmp_str = "";
            WriteLine("******************************************************");
            using (FileStream fs = new FileStream(fPath, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] str_byte = Encoding.UTF8.GetBytes(str);
                fs.Write(str_byte, 0, str_byte.Length);
            }

            WriteLine("\n\nЕсли хотите открыть черный ящик нажмите 1");
            short key_box = short.Parse(Console.ReadLine());

            if (key_box == 1)
            {
                Clear();
                using (FileStream fs = new FileStream(fPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] str_byte = new byte[(int)fs.Length];
                    fs.Read(str_byte, 0, str_byte.Length);
                    string str_new = Encoding.UTF8.GetString(str_byte);
                    WriteLine(str_new);
                }
            }
            else
                Environment.Exit(0);
        }
    }
}