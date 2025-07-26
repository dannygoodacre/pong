namespace Pong;

class Program
{
    static void Main()
    {
        using var game = new Pong();

        game.Run();
    }
}
