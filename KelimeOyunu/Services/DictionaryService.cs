using System;
using System.Collections.Generic;
using System.IO;

namespace WordGame.Services
{
    /// <summary>
    /// Dış sözlük dosyasından kelime yükleyip,
    /// girilen kelimelerin geçerli olup olmadığını kontrol eden hizmet sınıfı.
    /// </summary>
    public class DictionaryService
    {
        private readonly HashSet<string> _validWords; // Sözlükteki geçerli kelimeler

        /// <summary>
        /// Belirtilen dosya yolundan sözlüğü yükler.
        /// </summary>
        /// <param name="dictionaryPath">Geçerli kelimelerin bulunduğu metin dosyası</param>
        public DictionaryService(string dictionaryPath)
        {
            if (!File.Exists(dictionaryPath))
                throw new FileNotFoundException("Dictionary file not found.", dictionaryPath);

            _validWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            LoadWords(dictionaryPath);
        }

        /// <summary>
        /// Sözlük dosyasındaki tüm kelimeleri belleğe yükler.
        /// Boş veya geçersiz satırlar atlanır.
        /// </summary>
        private void LoadWords(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                string word = line.Trim().ToUpperInvariant();
                if (!string.IsNullOrWhiteSpace(word))
                    _validWords.Add(word);
            }
        }

        /// <summary>
        /// Verilen kelimenin sözlükte geçerli olup olmadığını kontrol eder.
        /// </summary>
        public bool IsValidWord(string word)
        {
            return _validWords.Contains(word.ToUpperInvariant());
        }
    }
}
