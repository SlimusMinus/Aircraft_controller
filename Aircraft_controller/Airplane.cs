using System;
using System.Text;
using static System.Console;
using System.IO;

namespace Airplane_exam
{
    class Airplane
    {
        string fPath = "Black_Box.txt";
        string str;
        protected int speed;
        public int Myspeed
        {
            get { return speed; }
            set { speed = value; }
        }
        protected int height;

        public int Myheight
        {
            get { return height; }
            set { height = value; }
        }
        private int points;

        public int Mypoints
        {
            get { return points; }
            set { points = value; }
        }

        public void Flight_correction(int speed, int height)
        {

            SetCursorPosition(0, 0);
            WriteLine("***********************************************************");
            Write(str = $"скорость самолёта составляет {speed} км*ч высота {height} км\n");
            WriteLine("***********************************************************");
            using (FileStream fs = new FileStream(fPath, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] str_byte = Encoding.UTF8.GetBytes(str);
                fs.Write(str_byte, 0, str_byte.Length);
            }
        }

        public void Points_penal(int recomend_heidht, int height, int height_comparison)
        {
            SetCursorPosition(0, 5);
            if (recomend_heidht != height && height_comparison > 300 && height_comparison <= 600)
            {
                Write(str = $"Штрафные баллы - {points += 25}\n");
                WriteLine("***********************************************************");

            }
            else if (recomend_heidht != height && height_comparison > 600 && height_comparison < 1300)
            {
                Write(str = $"Штрафные баллы - {points += 50}\n");
                WriteLine("***********************************************************");
            }
            try
            {
                if (points > 1000)
                    throw new Exception(str = "Непригоден к полетам, вы набрали штафных баллов более 1000");
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine(ex.Message);
                Environment.Exit(0);
            }
            using (FileStream fs = new FileStream(fPath, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] str_byte = Encoding.UTF8.GetBytes(str);
                fs.Write(str_byte, 0, str_byte.Length);
            }
        }

        public Airplane(int speed, int height, int points)
        {
            this.speed = speed;
            this.height = height;
            this.points = points;
        }
        public void Penalty_points_height(int height, int height_comparison)
        {
            try
            {
                if (height > 1300)
                    throw new Exception(str = "Непригоден к полетам, вы набрали недопустимую высоту");
                if (height_comparison > 1300)
                    throw new Exception(str = "Самолет разбился, разница между рекомендуемой и текущей высотой более 1300 км");
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine(ex.Message);
                Environment.Exit(0);
            }
            using (FileStream fs = new FileStream(fPath, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] str_byte = Encoding.UTF8.GetBytes(str);
                fs.Write(str_byte, 0, str_byte.Length);
            }
        }

    }
}
