using KelimeOyunu.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordGame.Models;
using WordGame.Services;
using WordGame.Utils;

namespace WordGame
{
    /// <summary>
    /// Oyunun ana kontrol sınıfıdır.
    /// Tüm akışı (başlangıç, sıralar, hamleler, skorlar, bitiş) yönetir.
    /// </summary>
    public class Game
    {
        private readonly Board _board;
        private readonly TileBag _tileBag;
        private readonly ScoreCalculator _scoreCalculator;
        private readonly DictionaryService _dictionaryService;
        private readonly List<Player> _players;
        private int _currentPlayerIndex;

        public Game()
        {
            _board = new Board();
            _tileBag = new TileBag();
            _scoreCalculator = new ScoreCalculator(_board);

            string dictionaryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Dictionary.txt");
            _dictionaryService = new DictionaryService(dictionaryPath);

            _players = PlayerFactory.CreatePlayers();
            _currentPlayerIndex = 0;
        }

        /// <summary>
        /// Oyunu başlatır. Sıralı olarak her oyuncuya hamle yaptırır.
        /// Oyun, taşlar bittiğinde ve bir oyuncunun elinde taş kalmadığında sona erer.
        /// </summary>
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to WordGame!\n");

            DistributeInitialTiles();

            while (!IsGameOver())
            {
                var player = _players[_currentPlayerIndex];

                Console.Clear();
                _board.Print();
                ConsoleHelper.PrintPlayer(player);

                Console.WriteLine("\nEnter your move (e.g., 7 7 H WORD) or type EXIT to quit:");
                var input = Console.ReadLine();

                if (input?.Trim().ToUpper() == "EXIT")
                    return;

                if (!InputParser.TryParse(input, out var move))
                {
                    Console.WriteLine("❌ Invalid input. Format should be: Row Col Direction Word");
                    ConsoleHelper.Wait();
                    continue;
                }

                try
                {
                    // Taşı tahtaya yerleştir
                    var coords = _board.PlaceWord(move.Word, move.Start, move.IsHorizontal, player);

                    // İlk hamle değilse, temas zorunluluğunu kontrol et
                    if (_board.HasAnyTiles() && !_board.IsTouchingExistingTiles(coords))
                    {
                        Console.WriteLine("❌ Word must touch at least one existing tile.");
                        ConsoleHelper.Wait();
                        continue;
                    }

                    // Oluşan tüm kelimeleri çıkar
                    var extractedWords = WordExtractor.FindFormedWordsWithCoords(_board, coords);

                    // Sözlük kontrolü
                    if (!extractedWords.All(w => _dictionaryService.IsValidWord(w.Word)))
                    {
                        Console.WriteLine("❌ One or more words are not in the dictionary.");
                        ConsoleHelper.Wait();
                        continue;
                    }

                    // Skor hesapla ve oyuncuya puan ver
                    int score = _scoreCalculator.CalculateTotalScore(extractedWords);
                    player.UpdateScore(score);

                    // Yeni taş çek
                    _tileBag.DrawMultiple(7 - player.Tiles.Count).ForEach(player.AddTile);

                    Console.WriteLine($"\n✔️ Move accepted. You scored {score} points.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Invalid move: {ex.Message}");
                }

                ConsoleHelper.Wait();
                SwitchTurn();
            }

            ConsoleHelper.PrintFinalScores(_players);
        }

        /// <summary>
        /// Oyunculara başlangıçta 7'şer harf verir.
        /// </summary>
        private void DistributeInitialTiles()
        {
            foreach (var player in _players)
                _tileBag.DrawMultiple(7).ForEach(player.AddTile);
        }

        /// <summary>
        /// Sıradaki oyuncuya geçiş yapar.
        /// </summary>
        private void SwitchTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        }

        /// <summary>
        /// Oyun bitiş şartı:
        /// Torba boşsa ve oyunculardan biri elindeki tüm taşları kullandıysa.
        /// </summary>
        private bool IsGameOver()
        {
            return _tileBag.IsEmpty && _players.Any(p => p.Tiles.Count == 0);
        }
    }
}
