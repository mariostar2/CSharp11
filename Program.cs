using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;

namespace CSharp11
{

    class Item
    {
        private static int count;
        public static int itemCount
        {
            get
            {
                count++;            //카운트1 증가   
                if (count >= 4)     //카운트가 4 이상이되면
                    count = 0;      //카운트가 0으로
               
                return count;
            }
        }
        //static변수 (프로그램 시작 시 생성된다).
        string name;
        int id;

        public int Sort => id;

        public Item(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return $"({id}),{name}";
        }
    }
  

    
    internal class Program
    {

        delegate int CalculateEvent(int a, int b);
        static void Main(string[] args)

        {

            DDay.Calculate(delegate (int days, int months, int minutes, int seconds)
            {
                Console.WriteLine($"D-Day : {days}");
            });

            return;

            if (false)
            {


                //익명 메소드
                // => 이름이 없는 (함수)
                CalculateEvent onCalculate = delegate (int a, int b)
                {
                    return a + b;
                };

                int number = onCalculate(10, 20);
                Console.WriteLine($"number : {number}");


                //List<T>의 Sort 함수는 int Delegate(int, int)를 받아서 정렬 기준을 잡는다.
                //이때 이미 선언된 함수를 대입해도 되지만 이후 사용하지 않는다면
                //익명 메소드로 임시적인 처리가 가능하다.
                List<int> list = new List<int>(new int[] { 50, 20, 30, 40, 10 });
                list.Sort(delegate (int num1, int num2)
                {
                    //내림 차순을 기준으로 잡는다.
                    if (num1 > num2)
                        return -1;
                    else if (num1 < num2)
                        return 1;
                    else
                        return 0;
                });

                foreach (int num in list)
                    Console.Write($"{num},");

                // 아이디 리스트를 만들고 3개의 요소 추가
                List<Item> itemList = new List<Item>();
                itemList.Add(new Item(24, "무기"));
                itemList.Add(new Item(80, "잡화"));
                itemList.Add(new Item(74, "방어구"));

                //Sort 입장에서는 그냥 item 열거를 정렬하려고 하면 곤란하다( item? 뭘까?)
                // 그래서 우리는 누가더 작고 누가더 큰지 알 수 있는  int Compare(T a, T b)함수를 넘긴다.
                //이후 Sort는 Item이 뭔지 몰라도 두 아이템을 Compare에 대입해서 누가 더 크고 작은지 알 수 있다.

                // 아이디 리스트를 정렬 (Sort)
                itemList.Sort(delegate (Item item1, Item item2)
                {
                    if (item1.Sort < item2.Sort)
                        return -1;
                    else if (item1.Sort > item2.Sort)
                        return 0;
                    else
                        return 0;
                });

                // 아이디 리스트 값을 출력.(console)
                foreach (Item item in itemList)
                    Console.Write($"{item}");

            }

            DateTime date = DateTime.Now;       //현재 시간을 date에 대입
            Console.WriteLine($"오늘은: {date}");
            Console.WriteLine($"38일뒤는:{date.AddDays(38)}");

            Console.WriteLine(date.ToString());  //현재 시간을 출력.
                                                 //Timer();

            //1993 10월 8일은 무슨 요일?
            DateTime date2 = new DateTime(1993, 10, 8);
            string[] weekKoreans = new string[] { "일", "월", "화", "수", "목", "금", "토" };
            Console.WriteLine($"1993년 10월 8일은 {weekKoreans[(int)date2.DayOfWeek]}요일 입니다");

            //DateTime 
            //순수한 날짜에 대한 정보를 가지고 있다.

            //TimeSpan
            //날짜끼리 연산에 대한 결과를 가지고 있다.

            //int TotalDays :총 일수의 차이
            //int Days      : Day끼리의 뺀 차이

            // D-Day 계산기.
            //사귄날짝 2022년 2월 3일

            DateTime startDate = new DateTime(2022, 2, 3);
            DateTime today = DateTime.Now;
            TimeSpan dDay = today - startDate;

            bool isCheckDay = true;
            int totalDays = (int)dDay.TotalDays + (isCheckDay ? 1 : 0);
            Console.WriteLine($"사귄 날짜는 {(int)dDay.TotalDays}일입니다");

           

           /* IEnumerator input = Input();
            IEnumerator timer = Timer();
           
            while (true)
            {
                Console.Clear();
                input.MoveNext();
                timer.MoveNext();
            }
*/
            //Timer();
            //Input();

        }

        //IEnumerator
        static IEnumerator Input()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
                //키 입력이 일어났을 때.
                if (Console.KeyAvailable)
                    keyInfo = Console.ReadKey();                   //입력한 키의 정보를 대입.
                
                string key = keyInfo.Key.ToString();                           
                Console.WriteLine($"입력 키 :{key}");              //키를 출력.                        
                
                yield return null;                                 //잠시쉬면서 바깥으로나감               
            } 
        }

        static IEnumerator Timer()
        {
            DateTime start = DateTime.Now;
            int count = 0;
            while (true)
            {
                TimeSpan span = DateTime.Now - start;
                if (span.TotalSeconds >= 1.0f)
                {
                    count += 1;
                    Console.WriteLine(count);
                    start = DateTime.Now;
                }
                yield return null;

            }
        }
    }
}   