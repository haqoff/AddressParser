using FiasParserLib;
using System;

namespace TestApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new FiasParser();

            parser.Parse(@"347000, РОСТОВСКАЯ ОБЛ, БЕЛОКАЛИТВИНСКИЙ Р-Н, СОСНЫ П, БУДЕННОГО УЛ, ДОМ № 7, КОРПУС Г");

            parser.Parse(@"АРЗАМАС Нижегородская область ЛЕНИНА 129 А");

            parser.Close();

            Console.ReadKey();       
        }
    }
}
