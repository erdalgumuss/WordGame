using WordGame.Models;
using WordGame.Utils;
using System;
using System.Collections.Generic;

namespace WordGame.Services
{
    /// <summary>
    /// Oyun tahtasında oluşturulan kelimelere göre puan hesaplaması yapar.
    /// Hücre çarpanlarını dikkate alarak kelime skorunu belirler.
    /// </summary>
    public class ScoreCalculator
    {
        private readonly Board _board;

        public ScoreCalculator(Board board)
        {
            _board = board;
        }

        /// <summary>
        /// Tüm kelimelerin toplam puanını hesaplar.
        /// </summary>
        /// <param name="extractedWords">Kelime ve koordinat bilgisi içeren yapı</param>
        public int CalculateTotalScore(List<ExtractedWord> extractedWords)
        {
            int totalScore = 0;

            foreach (var ew in extractedWords)
            {
                totalScore += CalculateWordScore(ew.Word, ew.Coordinates);
            }

            return totalScore;
        }

        /// <summary>
        /// Tek bir kelimenin skorunu hesaplar. 
        /// Harf çarpanları (H2, H3) ve kelime çarpanları (K2, K3) dikkate alınır.
        /// </summary>
        private int CalculateWordScore(string word, List<Coordinate> coords)
        {
            int wordScore = 0;
            int wordMultiplier = 1;

            for (int i = 0; i < word.Length; i++)
            {
                var coord = coords[i];
                var cell = _board.GetCell(coord);
                var tile = cell.Tile;

                if (tile == null) continue;

                int letterPoint = tile.Point;

                // Harf çarpanlarını uygula
                switch (cell.Type)
                {
                    case CellType.DoubleLetter:
                        letterPoint *= 2;
                        break;
                    case CellType.TripleLetter:
                        letterPoint *= 3;
                        break;
                }

                wordScore += letterPoint;

                // Kelime çarpanlarını uygula
                switch (cell.Type)
                {
                    case CellType.DoubleWord:
                        wordMultiplier *= 2;
                        break;
                    case CellType.TripleWord:
                        wordMultiplier *= 3;
                        break;
                }
            }

            return wordScore * wordMultiplier;
        }
    }
}
