using System;

namespace Reverse
{
   public class Program
    {
        public static void Main(string[] args)
        {
            // var cube = new Cube(4);
            // cube.Init();
            // cube.RandomMix(6);

            // const int testNum = 2;
            // var i = 0;
            // while (i<testNum)
            // {
            //     var cube = new Cube(4);
            //     cube.Init();
            //     cube.RandomMix(6);
            //     cube.Print();
            //     var ps = Cube.PureSolve(cube);
            //     if (ps != null)
            //     {
            //         foreach (var result in ps)
            //         {
            //             Console.WriteLine(result);
            //         }
            //     }
            //     i++;
            // }
            
            
            const int testNum = 1;
            var i = 0;
            while (i<testNum)
            {
                var cube = new Cube(20);
                cube.Init();
                cube.RandomMix(39);
                cube.Print();
                var ps = Cube.PureSolve(cube);
                if (ps != null)
                {
                    foreach (var result in ps)
                    {
                        Console.WriteLine(result);
                    }
                }
                i++;
            }
            
            // PaintType[,] miss = new PaintType[4, 4]
            // {
            //     { PaintType.Zero,PaintType.One,PaintType.One,PaintType.One },
            //     { PaintType.Zero ,PaintType.One,PaintType.One,PaintType.Zero},
            //     { PaintType.Zero ,PaintType.One,PaintType.One,PaintType.Zero},
            //     { PaintType.Zero ,PaintType.Zero,PaintType.Zero,PaintType.Zero}
            // };
            // var cube = new Cube(4, miss);
            // cube.Print();
            // var ps = Cube.PureSolve(cube);
            // foreach (var result in ps)
            // {
            //     Console.WriteLine(result);
            // }
            
            // var res = cube.FindReverse();
            // while (res != null)
            // {
            //     Console.WriteLine("可反推上一步");
            //     cube.Reverse(res);
            //     Console.WriteLine($"上一步的操作是 {res.Direction} {res.Num}.上一步是： ");
            //     cube.Print();
            //     res = cube.FindReverse();
            // }
            // Console.WriteLine("无法继续反推,反推结束");
             Console.ReadKey();
        }
    }
}