using System;

namespace DateDifferenceCalculator
{
    class Program
    {
        //number of days for each month for regular year
        //started the array with 0 because I'll use my loops from 1 
        int[] monthsDays = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        //This function takes a year and return if it's a  leap year or not
        bool LeapYearCheck(int year)
        {
            if (((year % 400) == 0) || ((year % 100) != 0 && (year % 4) == 0))
                return true;
            return false;
        }


        int CalculateDays(int d1, int m1, int y1, int d2, int m2, int y2)
        {
            int total = 0;
            Program p = new Program();
            //if the date is in same year and same month then number of days is just the difference between d1 and d2
            if (y1 == y2 && m1 == m2)
            {
                total = d2 - d1;
            }
            //if the date is in same year then first i calculated all days between the two months then removed extra days
            else if (y1 == y2)
            {
                for (int i = m1; i < m2 + 1; i++)
                {
                    total += monthsDays[i];

                }
                //add one day in case of a leap year
                if (m1 <= 2 && m2 > 2 && p.LeapYearCheck(y1))
                {
                    total++;
                }
                //removed days before and after the desired days
                total -= d1 + (monthsDays[m2] - d2);
            }
            //same as above but with different yers
            else
            {
                int FirstYearDays = 0;
                for (int i = m1; i < 13; i++)
                {
                    FirstYearDays += monthsDays[i];
                }

                if (m1 <= 2 && p.LeapYearCheck(y1))
                {
                    FirstYearDays++;
                }
                FirstYearDays -= d1;

                int LastYearDays = 0;
                for (int i = 1; i < m2 + 1; i++)
                {
                    LastYearDays += monthsDays[i];
                }
                if ((m2 > 2 && p.LeapYearCheck(y2)) || (m2 == 2 && p.LeapYearCheck(y2) && d2 == 29))
                {
                    LastYearDays++;
                }
                LastYearDays -= monthsDays[m2] - d2;
                total = FirstYearDays + LastYearDays;
            }
            //added days between the two years
            for (int year = y1 + 1; year < y2; year++)
            {
                if (p.LeapYearCheck(year))
                    total += 366;
                else
                    total += 365;
            }
            return total;
        }

        bool ValidateInputs(int d, int m)
        {
            //days should be be min 1 and max 31
            if (d < 1 || d > 31)
            {
                Console.WriteLine("Day number is not valid.");
                return false;
            }
            //months should be be min 1 and max 12
            if (m < 1 || m > 12)
            {
                Console.WriteLine("Month number is not valid.");
                return false;
            }
            //day number should be in the range of the specified month
            if (monthsDays[m] < d)
            {
                Console.WriteLine("Day number is greater than month days.");
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine("Enter First Date in the form d.m.yyyy: ");
            string[] BEGIN = Console.ReadLine().Split('.');
            int d1 = int.Parse(BEGIN[0]);
            int m1 = int.Parse(BEGIN[1]);
            int y1 = int.Parse(BEGIN[2]);
            if (!p.ValidateInputs(d1, m1))
            {
                return;
            }
            

            Console.WriteLine("Enter Second Date in the form d.m.yyyy: ");
            string[] END = Console.ReadLine().Split('.');
            int d2 = int.Parse(END[0]);
            int m2 = int.Parse(END[1]);
            int y2 = int.Parse(END[2]);
            if (!p.ValidateInputs(d2, m2))
            {
                return;
            }

            if (y1 > y2 || (y1 == y2 && m1 > m2) || (y1 == y2 && m1 == m2 && d1 > d2))
            {
                Console.WriteLine("First date can't be greater than second one.");
                return;
            }

            int NumOfDays = p.CalculateDays(d1, m1, y1, d2, m2, y2);
            int NumOfMonths = (int)(NumOfDays / 30.4375); //devided total days on the average of month length (number of year days / 12) 
            int NumOfYears = NumOfMonths / 12;

            Console.WriteLine("Number Of Years: " + NumOfYears.ToString());
            Console.WriteLine("Number Of Months: " + NumOfMonths.ToString());
            Console.WriteLine("Number Of Days: " + NumOfDays.ToString());
        }
    }
}
