using EventManagerDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerDemo.Helpers
{
    internal class DataContext
    {
        internal static List<Service1Data> GetService1Data()
        {
            Thread.Sleep(5000);
            return new List<Service1Data>()
            {
                new Service1Data("Для вас, души моей царицы,"),
                new Service1Data("Красавицы, для вас одних"),
                new Service1Data("Времен минувших небылицы,"),
                new Service1Data("В часы досугов золотых,"),
                new Service1Data("Под шепот старины болтливой,"),
                new Service1Data("Рукою верной я писал;")
            };
        }
    }
}
