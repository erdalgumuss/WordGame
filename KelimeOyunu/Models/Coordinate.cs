namespace WordGame.Models
{
    /// <summary>
    /// Tahtadaki bir hücrenin satır ve sütun koordinatını temsil eder.
    /// </summary>
    public class Coordinate
    {
        public int Row { get; }
        public int Column { get; }

        public Coordinate(int row, int column)
        {
            // 15x15 tahtanın sınırları dışına çıkmayı engeller.
            if (row < 0 || row >= 15 || column < 0 || column >= 15)
                throw new ArgumentOutOfRangeException("Coordinates must be between 0 and 14 for a 15x15 board.");

            Row = row;
            Column = column;
        }

        public override string ToString() => $"({Row}, {Column})";

        public override bool Equals(object? obj)
        {
            return obj is Coordinate other &&
                   Row == other.Row && Column == other.Column;
        }

        public override int GetHashCode() => HashCode.Combine(Row, Column);

        // İsteğe bağlı: == ve != operatörleri (modern kullanım için)
        public static bool operator ==(Coordinate? a, Coordinate? b)
            => a is null ? b is null : a.Equals(b);

        public static bool operator !=(Coordinate? a, Coordinate? b)
            => !(a == b);
    }
}
