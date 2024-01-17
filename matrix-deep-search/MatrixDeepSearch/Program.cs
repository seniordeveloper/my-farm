using MatrixDeepSearch;
using System.Reflection;

class Program
{
    public static int Main(string[] args)
    {
        var fileName = GetFileName(args);

        Console.WriteLine(Matrix.FromFile(fileName));

        return 0;
    }

    private static string GetFileName(string[] args) 
    {
        if (args?.Length >= 2) 
        {
            return args[0];
        }
        return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "samples.txt");
    }
}