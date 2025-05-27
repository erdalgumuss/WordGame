using WordGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WordGame.Utils
{
    /// <summary>
    /// Tahta ile ilgili yardımcı fonksiyonları içerir.
    /// Bu sınıf, koordinat üretimi ve harf taşı seçimi gibi mantıksal işlemleri soyutlar.
    /// </summary>
    public static class BoardUtils
    {
        /// <summary>
        /// Bir kelimenin başlangıç noktası ve yönüne göre tahtadaki hücre koordinatlarını üretir.
        /// </summary>
        /// <param name="word">Kelime</param>
        /// <param name="start">Başlangıç koordinatı</param>
        /// <param name="isHorizontal">Yatay mı dikey mi</param>
        /// <returns>Kelimenin her harfi için bir koordinat listesi</returns>
        /// <exception cref="InvalidOperationException">Kelime tahtadan dışarı çıkıyorsa</exception>
        public static List<Coordinate> GenerateCoordinates(string word, Coordinate start, bool isHorizontal)
        {
            var coordinates = new List<Coordinate>();
            for (int i = 0; i < word.Length; i++)
            {
                int row = isHorizontal ? start.Row : start.Row + i;
                int col = isHorizontal ? start.Column + i : start.Column;

                if (row < 0 || row >= 15 || col < 0 || col >= 15)
                    throw new InvalidOperationException("Word exceeds board boundaries.");

                coordinates.Add(new Coordinate(row, col));
            }
            return coordinates;
        }

        /// <summary>
        /// Oyuncunun taşları arasından, kelimeyi oluşturacak harfleri seçer.
        /// Gerekirse joker (*) taşları kullanır.
        /// </summary>
        /// <param name="word">Oluşturulmak istenen kelime</param>
        /// <param name="player">Taşları seçilecek oyuncu</param>
        /// <returns>Kelimeyi oluşturacak harf taşları</returns>
        /// <exception cref="InvalidOperationException">Uygun taş bulunamazsa</exception>
        public static List<LetterTile> SelectTilesForWord(string word, Player player)
        {
            var selectedTiles = new List<LetterTile>();
            foreach (char c in word.ToUpperInvariant())
            {
                var tile = player.Tiles.FirstOrDefault(t => t.Character == c)
                        ?? player.Tiles.FirstOrDefault(t => t.IsJoker);

                if (tile == null)
                    throw new InvalidOperationException("Tile not found despite prior check.");

                selectedTiles.Add(tile);
            }
            return selectedTiles;
        }
    }
}
