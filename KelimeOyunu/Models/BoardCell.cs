using System;

namespace WordGame.Models
{
    /// <summary>
    /// Oyun tahtasındaki bir kareyi temsil eder.
    /// Her hücre bir konuma, puan çarpanı tipine ve (varsa) bir harf taşına sahiptir.
    /// </summary>
    public class BoardCell
    {
        public Coordinate Position { get; }       // Hücrenin tahtadaki konumu
        public CellType Type { get; private set; } // Hücre tipi (örneğin: H2, K3, Normal)
        public LetterTile? Tile { get; private set; } // Yerleştirilen taş (yoksa null)

        /// <summary>
        /// Hücre dolu mu? (taş yerleştirilmişse true)
        /// </summary>
        public bool IsUsed => Tile != null;

        public BoardCell(Coordinate position, CellType type)
        {
            Position = position;
            Type = type;
            Tile = null;
        }

        /// <summary>
        /// Hücreye bir taş yerleştirir.
        /// Bonus sadece bir kez geçerli olduğundan, yerleştirme sonrası hücre normal olur.
        /// </summary>
        public void PlaceTile(LetterTile tile)
        {
            if (IsUsed)
                throw new InvalidOperationException($"Cell at {Position} is already occupied.");

            Tile = tile;
            Type = CellType.Normal; // bonus çarpanı tek seferlik
        }

        /// <summary>
        /// Hücreyi konsola yazdırmak için sembol üretir:
        /// - Taş varsa karakteri
        /// - Boşsa hücre tipini (örneğin "H2", "K3", "__")
        /// </summary>
        public override string ToString()
        {
            if (Tile != null)
                return Tile.IsJoker ? "*" : Tile.Character.ToString();

            return Type switch
            {
                CellType.DoubleLetter => "H2",
                CellType.TripleLetter => "H3",
                CellType.DoubleWord => "K2",
                CellType.TripleWord => "K3",
                _ => "__"
            };
        }
    }
}
