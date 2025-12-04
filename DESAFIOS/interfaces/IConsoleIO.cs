namespace Target.Interfaces
{
    public interface IConsoleIO
    {
        string? ReadLine();
        void WriteLine(string? text);
    }
}
