using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp11
{
    //반환형이 없고 매개변수로 string,int를 하나씩 받는 함수를 담은 델리게이트.
    public delegate void DDayResultEvent(int days,  int hours, int minutes, int seconds);
    internal class DDay
    {
        public static void Calculate(DDayResultEvent onResult)
        {
            Console.WriteLine("D-Day계산기");
            
            Console.Write("몇 년:");
            int year = int.Parse(Console.ReadLine());
            
            Console.Write("몇 월:");
            int month = int.Parse(Console.ReadLine());
           
            Console.Write("몇 일:");
            int day = int.Parse(Console.ReadLine());

            Console.Write("첫 날을 1일로 하시겠습니까(Y/N)");
            bool isFirst = Console.ReadKey().KeyChar == 'y';
            Console.WriteLine();

            DateTime dday = new DateTime(year, month, day);
            TimeSpan span = DateTime.Today- dday;
            int days = (int)span.TotalDays + (isFirst ? 1 : 0);

            onResult(days,(int)span.TotalHours,(int)span.TotalMinutes, (int)span.TotalSeconds);
            
        }
    }
}
