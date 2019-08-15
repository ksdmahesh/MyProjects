using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.aforeach();
            program.aconvertingtochar();
        }
        public void aforeach()
        {
            string[] astores = {"apple","ball","cat","doll","egg","fun"};
            foreach (string item in astores)
            {
                Console.WriteLine(item);
            }
        }
         void aconvertingtochar()
        {
            string[] astores11={"midhunkumar"};
            char[] achararray1=new char[13];
            achararray1[0]=astores11[0].ToCharArray(0,1);
            foreach (var item in achararray1)
            {
                Console.WriteLine(item);
            }
            
           
        }

    }



    class Fortestingpurpose:Program
    {
        public void thetesting()
        {
            Fortestingpurpose fortestingpurpose = new Fortestingpurpose();
            
        }
    }
}
