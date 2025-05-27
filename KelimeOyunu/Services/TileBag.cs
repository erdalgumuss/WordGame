using WordGame.Models;
using System;
using System.Collections.Generic;

namespace KelimeOyunu.Services
{
    /// <summary>
    /// Oyun sırasında kullanılacak harf taşlarını barındıran ve yöneten torbadır.
    /// Oyuna özel harf frekanslarını içerir ve rastgele taş çekilmesini sağlar.
    /// </summary>
    public class TileBag
    {
        private List<LetterTile> _tiles; // Torbadaki mevcut taşlar
        private Random _random;          // Karıştırmak için RNG

        /// <summary>
        /// Torbada hiç taş kalmamış mı?
        /// </summary>
        public bool IsEmpty => _tiles.Count == 0;

        /// <summary>
        /// Yeni bir taş torbası oluşturur, tüm taşları doldurur ve karıştırır.
        /// </summary>
        public TileBag()
        {
            _tiles = new List<LetterTile>();
            _random = new Random();
            Fill();     // Taşları doldur
            Shuffle();  // Rastgele sırala
        }

        /// <summary>
        /// Oyun için gerekli tüm harf taşlarını (harf, adet, puan) tanımlar ve listeye ekler.
        /// </summary>
        private void Fill()
        {
            var tileDefinitions = new (char letter, int count, int point)[]
            {
                ('A', 12, 1), ('B', 2, 3), ('C', 2, 4), ('Ç', 2, 4), ('D', 2, 3), ('E', 8, 1),
                ('F', 1, 7), ('G', 1, 5), ('Ğ', 1, 8), ('H', 1, 5), ('I', 4, 2), ('İ', 7, 1),
                ('J', 1, 10), ('K', 7, 1), ('L', 7, 1), ('M', 4, 2), ('N', 5, 1), ('O', 3, 2),
                ('Ö', 1, 7), ('P', 1, 5), ('R', 6, 1), ('S', 3, 2), ('Ş', 2, 4), ('T', 5, 1),
                ('U', 3, 2), ('Ü', 2, 3), ('V', 1, 7), ('Y', 2, 3), ('Z', 2, 4), ('*', 2, 0)
            };

            foreach (var (letter, count, point) in tileDefinitions)
            {
                for (int i = 0; i < count; i++)
                    _tiles.Add(new LetterTile(letter, point));
            }
        }

        /// <summary>
        /// Torbadaki taşların sırasını rastgele karıştırır.
        /// </summary>
        private void Shuffle()
        {
            for (int i = _tiles.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (_tiles[i], _tiles[j]) = (_tiles[j], _tiles[i]);
            }
        }

        /// <summary>
        /// Torbadan bir taş çeker. Torba boşsa null döner.
        /// </summary>
        public LetterTile? Draw()
        {
            if (IsEmpty)
                return null;

            var tile = _tiles[0];
            _tiles.RemoveAt(0);
            return tile;
        }

        /// <summary>
        /// Torbadan istenilen sayıda taş çeker. Taş sayısı yetersizse eldeki kadarını döner.
        /// </summary>
        public List<LetterTile> DrawMultiple(int count)
        {
            var result = new List<LetterTile>();
            for (int i = 0; i < count && !IsEmpty; i++)
                result.Add(Draw()!); // null olmayacağı daha önce kontrol edildi
            return result;
        }

        /// <summary>
        /// Torbada kalan toplam taş sayısını döner.
        /// </summary>
        public int RemainingCount => _tiles.Count;
    }
}
