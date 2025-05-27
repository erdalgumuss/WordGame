using System;
using System.Collections.Generic;
using System.Linq;

namespace WordGame.Models
{
    /// <summary>
    /// Oyuncu bilgilerini, elindeki harf taşlarını ve skorunu temsil eder.
    /// </summary>
    public class Player
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }
        public int Score { get; private set; }

        private readonly List<LetterTile> _tiles; // Elindeki taşlar (max 7)
        public IReadOnlyList<LetterTile> Tiles => _tiles.AsReadOnly(); // Dışarıya salt-okunur sunulur

        public Player(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Score = 0;
            _tiles = new List<LetterTile>();
        }

        /// <summary>
        /// Oyuncuya yeni bir harf taşı verir. 7 taştan fazlasına izin verilmez.
        /// </summary>
        public void AddTile(LetterTile tile)
        {
            if (_tiles.Count >= 7)
                throw new InvalidOperationException("Player already has 7 tiles.");
            _tiles.Add(tile);
        }

        /// <summary>
        /// Kullanılan taşları oyuncunun elinden çıkarır.
        /// </summary>
        public void RemoveTiles(IEnumerable<LetterTile> usedTiles)
        {
            foreach (var tile in usedTiles)
            {
                var removed = _tiles.Remove(tile);
                if (!removed)
                    throw new InvalidOperationException($"Player does not have tile: {tile.Character}");
            }
        }

        /// <summary>
        /// Oyuncunun elinde verilen kelimeyi oluşturmak için yeterli taş var mı?
        /// Joker taşlar (*) eksik harf yerine kullanılabilir.
        /// </summary>
        public bool HasTilesFor(string word)
        {
            var tileCounts = _tiles.GroupBy(t => t.Character)
                                   .ToDictionary(g => g.Key, g => g.Count());

            foreach (char c in word.ToUpperInvariant())
            {
                if (tileCounts.TryGetValue(c, out int count) && count > 0)
                {
                    tileCounts[c]--;
                }
                else if (tileCounts.TryGetValue('*', out int jokerCount) && jokerCount > 0)
                {
                    tileCounts['*']--;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Oyuncunun skoruna verilen puanı ekler.
        /// </summary>
        public void UpdateScore(int points)
        {
            Score += points;
        }

        /// <summary>
        /// Oyuncunun elindeki taşları konsola yazdırır.
        /// </summary>


        public override string ToString()
        {
            return $"{FirstName} {LastName}, Age: {Age}, Score: {Score}";
        }
    }
}
