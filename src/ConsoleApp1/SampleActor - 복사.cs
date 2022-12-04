using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Proto;

namespace ConsoleApp1;

public static class Extensions
{
    public static void ToConsole(this string value, ConsoleColor color)
    {
        var old = Console.ForegroundColor;
        try
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
        }
        finally
        {
            Console.ForegroundColor = old;
        }
    }
}
