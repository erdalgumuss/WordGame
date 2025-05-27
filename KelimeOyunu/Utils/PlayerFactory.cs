using WordGame.Models;

public static class PlayerFactory
{
    public static List<Player> CreatePlayers(int count = 2)
    {
        var players = new List<Player>();

        for (int i = 1; i <= count; i++)
        {
            Console.Write($"Enter name for Player {i}: ");
            string firstName = Console.ReadLine() ?? $"Player{i}";
            Console.Write("Last name: ");
            string lastName = Console.ReadLine() ?? "Unknown";
            Console.Write("Age: ");
            int age = int.TryParse(Console.ReadLine(), out var a) ? a : 0;

            players.Add(new Player(firstName, lastName, age));
        }

        return players;
    }
}
