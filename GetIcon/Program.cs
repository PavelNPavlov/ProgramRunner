using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetIcon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filename = @"F:\MATLab\bin\matlab.exe";
            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filename);
           
            using (FileStream fs = new FileStream("iconForProgram1.ico", FileMode.Create))
            {
                icon.Save(fs);
            }
                
        }
    }
}
