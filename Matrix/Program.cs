
using Matrix;
using System.Diagnostics;

var matrixCount = 20;

var matrixes = GenerateMatrixes(matrixCount);

var threadRanges = new List<(int startIndex, int count)>()
{ 
    (startIndex: 0,  count: 2),
    (startIndex: 2,  count: 5),
    (startIndex: 7,  count: 4),
    (startIndex: 11, count: 3),
    (startIndex: 14, count: 6),
};

var outputSync = new object();

var time = Stopwatch.StartNew();

var tasks = threadRanges.Select(r => Task.Run(() =>
{
    for (int i = r.startIndex; i < r.startIndex + r.count; i++)
    {
        var isSymmetric = matrixes[i].IsSymmetric();
        var isMagic = matrixes[i].IsMagic();
        var threadName = Thread.CurrentThread.ManagedThreadId;
        var matrixString = matrixes[i].ToString();

        lock (outputSync)
        {
            Console.WriteLine("Thread Name: " + threadName);
            Console.WriteLine("Is Magic: " + isMagic + " IsSymmetric: " + isSymmetric);
            Console.WriteLine();
            Console.WriteLine(matrixString);
            Console.WriteLine();
        }
    }
}));

await Task.WhenAll(tasks);

time.Stop();

Console.WriteLine("Time: " + time.ElapsedMilliseconds);

Console.ReadLine();

static List<IntMatrix> GenerateMatrixes(int count)
{
    return Enumerable
            .Range(0, count).Select(i =>
            {
                var n = Random.Shared.Next(3, 10);
                var m = Random.Shared.Next(3, 10);

                return IntMatrix.Generate(n, m);
            })
            .ToList();
}