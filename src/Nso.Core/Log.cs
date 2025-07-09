namespace Nso.Core;
public static class Log
{
    public static void I(string m) => Console.WriteLine($"[I] {m}");
    public static void W(string m) => Console.WriteLine($"[W] {m}");
    public static void E(string m) => Console.WriteLine($"[E] {m}");
}
