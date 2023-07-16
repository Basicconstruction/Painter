using System;
using System.Collections.Generic;
using System.Linq;

namespace Reverse
{
    public class Cube
    {
        private readonly PaintType[,] _miss;
        private readonly int _axis;

        public Cube(int axis)
        {
            _axis = axis;
            _miss = new PaintType[axis, axis];
            
        }

        public Cube(int axis, PaintType[,] miss)
        {
            _axis = axis;
            _miss = miss;
        }

        public void Init()
        {
            for (var i = 0; i < _axis; i++)
            {
                for (var j = 0; j < _axis; j++)
                {
                    _miss[i, j] = PaintType.Zero;
                }
            }
        }

        public static List<Result> Sort(List<Result> results)
        {
            var tmp = new List<Result>();
            var res = new List<Result>();
            if (results.Count < 1)
            {
                return null;
            }
            var direction = results.First().Direction;
            foreach (var ele in results)
            {
                if (ele.Direction == direction)
                {
                    tmp.Add(ele);
                }
                else
                {
                    // Console.WriteLine("after sort");
                    // foreach (var result in PartSort(tmp))
                    // {
                    //     Console.WriteLine("    sort "+result);
                    // }
                    foreach (var result in PartSort(tmp))
                    {
                        res.Add(result);
                    }
                    // res.AddRange(PartSort(tmp));
                    tmp.Clear();
                    direction = ele.Direction;
                    tmp.Add(ele);
                }
            }

            if (tmp.Count > 0)
            {
                res.AddRange(PartSort(tmp));
            }

            return res;
        }

        private static List<Result> PartSort(List<Result> results)
        {
            // Console.WriteLine("before sort");
            // foreach (var result in results)
            // {
            //     Console.WriteLine(result);
            // }
            // results.OrderBy(r => r.Num).ToList();
            results.Sort((r1, r2) => r1.Num.CompareTo(r2.Num));
            // Console.WriteLine("after sort");
            // foreach (var result in results)
            // {
            //     Console.WriteLine(result);
            // }
            // return results.OrderBy(r => r.Num).ToList();
            return results;
        }

        public void RandomMix(int count)
        {
            var random = new Random((int)DateTimeOffset.Now.Ticks);
            var i = random.Next(_axis);
            var j = random.Next(_axis); 
            _miss[i, j] = PaintType.One;
            Console.WriteLine($"Item {i} {j}");
            var start = new List<(int, int)> { (i,j) };
            while (count > 0)
            {
                var list = new List<(int,int)>();
                var top = (i - 1, j);
                var bottom = (i + 1, j);
                var left = (i, j - 1);
                var right = (i, j + 1);
                if (Valid(top)&&NotResult(top))
                {
                    list.Add(top);
                }

                if (Valid(bottom)&&NotResult(bottom))
                {
                    list.Add(bottom);
                }

                if (Valid(left)&&NotResult(left))
                {
                    list.Add(left);
                }

                if (Valid(right)&&NotResult(right))
                {
                    list.Add(right);
                }

                if (list.Count != 0)
                {
                    var tuple = list[random.Next(list.Count)];
                    _miss[tuple.Item1, tuple.Item2] = PaintType.One;
                    count--;
                    i = tuple.Item1;
                    j = tuple.Item2;
                    start.Add((tuple.Item1,tuple.Item2));
                    Console.WriteLine($"Item {tuple.Item1} {tuple.Item2}");
                }
                else
                {
                    Console.WriteLine("Back");
                    var tuple = start.Last();
                    start.Remove(tuple);
                    i = tuple.Item1;
                    j = tuple.Item2;
                }
            }
        }

        private bool NotResult((int, int) tuple)
        {
            return _miss[tuple.Item1, tuple.Item2] != PaintType.One;
        }

        private bool Valid((int,int) tuple)
        {
            return Valid(tuple.Item1) && Valid(tuple.Item2);
        }

        private bool Valid(int index)
        {
            return index >= 0 && index < _axis;
        }
        public class Result
        {
            public Direction Direction
            {
                get;
                set;
            }

            public int Num
            {
                get;
                set;
            }

            public Result()
            {
            }

            public Result(Direction direction, int num)
            {
                Direction = direction;
                Num = num;
            }

            public override string ToString()
            {
                return $"{Direction} {Num}";
            }
        }

        private bool Success()
        {
            for (var i = 0; i < _axis; i++)
            {
                for (var j = 0; j < _axis; j++)
                {
                    if (_miss[i, j] != PaintType.Any) return false;
                }
            }

            return true;
        }

        private Result FindReverse()
        {
            // 查找行 one
            for (var i = 0; i < _axis; i++)//行
            {
                var row = true;// 需要保持true,有一个false就可省略
                var notAllAny = false;
                for (var j = 0; j < _axis; j++)// 列
                {
                    if (!row)
                    {
                        break;
                    }

                    row = _miss[i, j] != PaintType.Zero;// one or any
                    var tmp = _miss[i, j] != PaintType.Any;
                    if (tmp)
                    {
                        notAllAny = true;
                    }
                    
                }

                //Console.WriteLine($"row {row} notAllAny {notAllAny}");
                if (row&&notAllAny)
                {
                    return new Result(Direction.Row,i);
                }
            }
            // 查找列 zero
            for (var j = 0; j < _axis; j++)// 列
            {
                var col = true;
                var notAllAny = false;
                for (var i = 0; i < _axis; i++)// 行
                {
                    if (!col)
                    {
                        break;
                    }

                    col = _miss[i,j] != PaintType.One;
                    var tmp = _miss[i, j] != PaintType.Any;
                    if (tmp)
                    {
                        notAllAny = true;
                    }
                }

                //Console.WriteLine($"col {col} notAllAny {notAllAny}");
                if (col&&notAllAny)
                {
                    return new Result(Direction.Col, j);
                }
            }

            return null;
        }

        private void Reverse(Result result)
        {
            Reverse(result.Direction,result.Num);
        }

        private void Reverse(Direction direction, int num)
        {
            if (direction == Direction.Row)
            {
                for (var i = 0; i < _axis; i++)// 列
                {
                    _miss[num,i] = PaintType.Any;
                }
            }
            else
            {
                for (var i = 0; i < _axis; i++)// 行
                {
                    _miss[i,num] = PaintType.Any;
                }
            }
        }

        public void Print()
        {
            for (var i = 0; i < _axis; i++)
            {
                for (var j = 0; j < _axis; j++)
                {
                    switch (_miss[i, j])
                    {
                        case PaintType.Any:
                            Console.Write("A ");
                            break;
                        case PaintType.One:
                            Console.Write("1 ");
                            break;
                        case PaintType.Zero:
                            Console.Write("0 ");
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                Console.WriteLine();
            }
        }

        public static List<Result> PureSolve(Cube cube)
        {
            if(cube==null) return null;
            var result = new List<Result>();
            var res = cube.FindReverse();
            while (res != null)
            {
                result.Add(res);
                cube.Reverse(res);
                //cube.Print();
                res = cube.FindReverse();
            }
            if (cube.Success())
            {
                Console.WriteLine("算法求解成功！！");
            }
            else
            {
                Console.WriteLine("算法求解失败！！初始状态");
                cube.Print();
            }

            if (result.Count <= 1)
            {
                return null;
            }
            while (result.Count>0&&result.Last().Direction==Direction.Col)
            {
                result.Remove(result.Last());
            }

            result.Reverse();
            result = Sort(result);
            return result;
        }
        public static void Solve(Cube cube)
        {
            if(cube==null) return;
            var res = cube.FindReverse();
            while (res != null)
            {
                Console.WriteLine("找到可倒退的一行或一列（行： 一行不全是Any,且全部不是Zero； 列" +
                                  "一行不全是Any，且全部不是One）。");
                cube.Reverse(res);
                Console.WriteLine($"上一步的操作是 {res.Direction} {res.Num}.上一步是： ");
                //cube.Print();
                res = cube.FindReverse();
            }

            if (cube.Success())
            {
                Console.WriteLine("算法求解成功！！");
            }
            else
            {
                Console.WriteLine("算法求解失败！！初始状态");
                cube.Print();
            }
            Console.WriteLine("算法完成！");
        }
    }
}