using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClassA a = new ClassB();
            a.Method1();
            a.Method2();
            a.Method3();

            ClassA a1 = new ClassC();
            a1.Method1();
            a1.Method2();
            a1.Method3();

            ClassB b = new ClassC();
            b.Method1();
            b.Method2();
            b.Method3();

            
        }
    }

    internal class ClassA
    {
        public void Method1() => Console.WriteLine("ClassA");
       
        public virtual void Method2() => Console.WriteLine("ClassA");

        public virtual void Method3() => Console.WriteLine("ClassA");
    }

    internal class ClassB : ClassA
    {
        public new void Method1() => Console.WriteLine("ClassB");
        public override void Method2() => Console.WriteLine("ClassB");
      
        public override void Method3() => Console.WriteLine("ClassB");
    }

    internal class ClassC : ClassB
    {
        public override void Method2() => Console.WriteLine("ClassC");

        public new virtual void Method3() => Console.WriteLine("ClassC");
    }
}
