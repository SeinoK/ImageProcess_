using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegate_
{
    class Program
    {
        public delegate void BuyTicketHandle();

        public class Zhang
        {
            public static void BuyTicket()
            {
                Console.WriteLine("又是我去买票！");
            }

            public static void RentCar()
            {
                Console.WriteLine("又叫老子去打的");
            }
        }

        public class Wang
        {
           // public delegate void BuyTicketHandle();
            static void Main(string[] args)
            {
                BuyTicketHandle myDelegate = new BuyTicketHandle(Zhang.BuyTicket);
                myDelegate += Zhang.RentCar;
                myDelegate();
                Console.ReadLine();

            }
        }
    }
}
