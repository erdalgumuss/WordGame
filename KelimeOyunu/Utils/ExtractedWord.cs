using WordGame.Models;
using System.Collections.Generic;

namespace WordGame.Utils
{
    /// <summary>
    /// Tahtada oluşmuş bir kelimeyi ve o kelimeyi oluşturan hücrelerin koordinatlarını temsil eder.
    /// Puan hesaplaması ve sözlük kontrolü için kullanılır.
    /// </summary>
    public class ExtractedWord
    {
        public string Word { get; set; }                    // Oluşan kelime
        public List<Coordinate> Coordinates { get; set; }   // Kelimeyi oluşturan hücreler

        public ExtractedWord(string word, List<Coordinate> coords)
        {
            Word = word;
            Coordinates = coords;
        }
    }
}
