using WordGame.Models;
using WordGame.Services;
using System.Collections.Generic;

namespace WordGame.Utils
{
    /// <summary>
    /// Tahtaya yerleştirilen harf taşları üzerinden oluşan kelimeleri ve ilgili koordinatları çıkarır.
    /// Hem yatay hem dikey yönde kontrol yapar.
    /// </summary>
    public static class WordExtractor
    {
        /// <summary>
        /// Verilen taş yerleştirme koordinatları üzerinden,
        /// tahtada oluşan tüm kelimeleri (yatay/dikey) tespit eder.
        /// Her kelime için hem yazı hem konum bilgisi döndürür.
        /// </summary>
        public static List<ExtractedWord> FindFormedWordsWithCoords(Board board, List<Coordinate> placedCoords)
        {
            var extractedWords = new List<ExtractedWord>();
            var visited = new HashSet<string>(); // Aynı kelime iki kez eklenmesin

            foreach (var coord in placedCoords)
            {
                // Dikey kelime kontrolü
                var vertical = ExtractWord(board, coord, vertical: true);
                if (vertical != null && vertical.Word.Length > 1 && visited.Add(vertical.Word))
                    extractedWords.Add(vertical);

                // Yatay kelime kontrolü
                var horizontal = ExtractWord(board, coord, vertical: false);
                if (horizontal != null && horizontal.Word.Length > 1 && visited.Add(horizontal.Word))
                    extractedWords.Add(horizontal);
            }

            return extractedWords;
        }

        /// <summary>
        /// Verilen hücreden başlayarak yatay veya dikey bir kelimeyi geriye ve ileriye genişleterek çıkarır.
        /// Hem metni hem koordinat listesini döner.
        /// </summary>
        private static ExtractedWord? ExtractWord(Board board, Coordinate origin, bool vertical)
        {
            int deltaRow = vertical ? -1 : 0;
            int deltaCol = vertical ? 0 : -1;

            // Sol/üst uç noktasına kadar geri sar
            int row = origin.Row;
            int col = origin.Column;
            while (IsWithinBounds(row + deltaRow, col + deltaCol) &&
                   board.GetCell(new Coordinate(row + deltaRow, col + deltaCol)).IsUsed)
            {
                row += deltaRow;
                col += deltaCol;
            }

            // Sağ/alt yönde ilerleyerek kelimeyi topla
            deltaRow = vertical ? 1 : 0;
            deltaCol = vertical ? 0 : 1;

            var word = "";
            var coords = new List<Coordinate>();

            while (IsWithinBounds(row, col) && board.GetCell(new Coordinate(row, col)).IsUsed)
            {
                var coord = new Coordinate(row, col);
                var tile = board.GetCell(coord).Tile;
                word += tile?.Character;
                coords.Add(coord);

                row += deltaRow;
                col += deltaCol;
            }

            return string.IsNullOrWhiteSpace(word) ? null : new ExtractedWord(word, coords);
        }

        /// <summary>
        /// Tahta sınırları içinde mi? (0-14 arası)
        /// </summary>
        private static bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < 15 && col >= 0 && col < 15;
        }
    }
}
