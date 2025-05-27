using WordGame.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Konsola yazdırma işlemleri için yardımcı sınıftır.
/// Oyuncu bilgileri, skorlar ve yönlendirmeleri ekrana yazdırır.
/// </summary>
public static class ConsoleHelper
{
    /// <summary>
    /// Oyuncunun temel bilgilerini ve elindeki taşları yazdırır.
    /// </summary>
    public static void PrintPlayer(Player player)
    {
        Console.WriteLine($"\n{player}");
        PrintPlayerTiles(player);
    }

    /// <summary>
    /// Oyuncunun elindeki harf taşlarını yazdırır.
    /// </summary>
    public static void PrintPlayerTiles(Player player)
    {
        Console.Write("Tiles: ");
        foreach (var tile in player.Tiles)
            Console.Write(tile.IsJoker ? "* " : $"{tile.Character} ");
        Console.WriteLine();
    }

    /// <summary>
    /// Konsolda kullanıcıdan devam etmek için giriş bekler.
    /// </summary>
    public static void Wait(string message = "\nPress ENTER to continue...")
    {
        Console.WriteLine(message);
        Console.ReadLine();
    }

    /// <summary>
    /// Oyun sonunda tüm oyuncuların skorlarını temiz bir ekranla gösterir.
    /// </summary>
    public static void PrintFinalScores(IEnumerable<Player> players)
    {
        Console.Clear();
        Console.WriteLine("\n🎉 Game Over – Final Scores:");
        foreach (var p in players)
            Console.WriteLine($"{p.FirstName} {p.LastName} – Score: {p.Score}");
    }
}
