namespace WordGame.Models
{
    /// <summary>
    /// Oyuncuların elindeki harf taşlarını temsil eder.
    /// Her taşın bir karakteri ve puanı vardır.
    /// Joker taş (*) her harfi temsil edebilir ve puanı 0'dır.
    /// </summary>
    public class LetterTile
    {
        public char Character { get; } // Harf karakteri (örn: A, B, Ç, *)
        public int Point { get; }      // Harfin puanı

        /// <summary>
        /// Joker taş mı? '*' karakteriyse doğrudur.
        /// </summary>
        public bool IsJoker => Character == '*';

        public LetterTile(char character, int point)
        {
            Character = character;
            Point = point;
        }

        /// <summary>
        /// Taşı okunabilir şekilde döndürür: "A (1)" veya "*" şeklinde.
        /// </summary>
        public override string ToString()
        {
            return IsJoker ? "*" : $"{Character} ({Point})";
        }

        public override bool Equals(object? obj)
        {
            return obj is LetterTile other &&
                   Character == other.Character &&
                   Point == other.Point;
        }

        public override int GetHashCode() => HashCode.Combine(Character, Point);
    }
}
