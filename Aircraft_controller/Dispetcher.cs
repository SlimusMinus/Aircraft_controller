using System;
using static System.Console;


namespace Airplane_exam
{
       class Dispatcher
    {
        public string name { get; set; }
        public Dispatcher(string name)
        {
            this.name=name;
        }
        public event Flydelegate fly_event;
        public event DelegPoints delegpoints;

        public void Flight_correction(int speed, int height)
        {
            if (fly_event!=null)
            {
                fly_event(speed, height);
            }
        }

        public void PointsEvent(int recomend_heidht, int height, int height_comparison)
        {
            if (delegpoints!=null)
            {
                delegpoints(recomend_heidht, height, height_comparison);
            }
        }

        public void Exeptions_null(int speed_or_height)
        {
            try
            {
                if (speed_or_height <= 0)
                    throw new Exception("Самолет разбился, скорость или высота не должны быть равными 0");
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }

    }
}
