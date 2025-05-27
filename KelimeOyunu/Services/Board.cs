using WordGame.Models;
using WordGame.Utils;
using System;
using System.Collections.Generic;

namespace WordGame.Services
{
    /// <summary>
    /// 15x15'lik oyun tahtasını temsil eder.
    /// Hücreleri yönetir, kelime yerleştirme ve temas kontrollerini yapar.
    /// </summary>
    public class Board
    {
        private const int Size = 15;
        private readonly BoardCell[,] _grid; // 15x15 hücre matrisi

        public Board()
        {
            _grid = new BoardCell[Size, Size];
            Initialize();
        }

        /// <summary>
        /// Tahtayı oluşturur ve her hücreye konumuna göre uygun puan çarpanı (bonus) tanımlar.
        /// </summary>
        private void Initialize()
        {
            var doubleLetterCoords = new List<Coordinate>
            {
                new(3, 0), new(11, 0), new(6, 2), new(8, 2), new(0, 3), new(7, 3), new(14, 3)
            };

            var tripleLetterCoords = new List<Coordinate>
            {
                new(5, 1), new(9, 1), new(1, 5), new(13, 5)
            };

            var doubleWordCoords = new List<Coordinate>
            {
                new(1, 1), new(13, 1), new(2, 2), new(12, 2)
            };

            var tripleWordCoords = new List<Coordinate>
            {
                new(0, 0), new(14, 0), new(0, 14), new(14, 14)
            };

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    var coord = new Coordinate(row, col);
                    var cellType = GetCellType(coord, doubleLetterCoords, tripleLetterCoords, doubleWordCoords, tripleWordCoords);
                    _grid[row, col] = new BoardCell(coord, cellType);
                }
            }
        }

        /// <summary>
        /// Verilen koordinatın hangi tür hücreye denk geldiğini belirler.
        /// </summary>
        private CellType GetCellType(Coordinate coord,
            List<Coordinate> h2, List<Coordinate> h3,
            List<Coordinate> k2, List<Coordinate> k3)
        {
            if (h2.Contains(coord)) return CellType.DoubleLetter;
            if (h3.Contains(coord)) return CellType.TripleLetter;
            if (k2.Contains(coord)) return CellType.DoubleWord;
            if (k3.Contains(coord)) return CellType.TripleWord;
            return CellType.Normal;
        }

        /// <summary>
        /// Tahtayı konsola yazdırır.
        /// </summary>
        public void Print()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Console.Write($"{_grid[row, col],4} ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Verilen kelimeyi belirli bir başlangıç noktasına ve yöne göre tahtaya yerleştirir.
        /// Uygun taşlar oyuncudan alınır ve hücreler güncellenir.
        /// </summary>
        public List<Coordinate> PlaceWord(string word, Coordinate start, bool isHorizontal, Player player)
        {
            word = word.ToUpperInvariant();

            var usedCoords = BoardUtils.GenerateCoordinates(word, start, isHorizontal);

            foreach (var coord in usedCoords)
            {
                if (GetCell(coord).IsUsed)
                    throw new InvalidOperationException($"Cell {coord} is already occupied.");
            }

            if (!player.HasTilesFor(word))
                throw new InvalidOperationException("Player does not have required tiles for this word.");

            var tilesToUse = BoardUtils.SelectTilesForWord(word, player);
            player.RemoveTiles(tilesToUse);

            for (int i = 0; i < usedCoords.Count; i++)
            {
                GetCell(usedCoords[i]).PlaceTile(tilesToUse[i]);
            }

            return usedCoords;
        }

        /// <summary>
        /// Verilen koordinattaki hücreyi döner.
        /// </summary>
        public BoardCell GetCell(Coordinate coord) => _grid[coord.Row, coord.Column];

        /// <summary>
        /// Tahtada en az bir taş yerleştirilmiş mi?
        /// </summary>
        public bool HasAnyTiles()
        {
            foreach (var cell in _grid)
            {
                if (cell.IsUsed)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Verilen hücreler, mevcut başka taşlara bitişik mi?
        /// Oyun kurallarına göre temas zorunluluğu kontrolü için kullanılır.
        /// </summary>
        public bool IsTouchingExistingTiles(List<Coordinate> coords)
        {
            foreach (var coord in coords)
            {
                foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int nr = coord.Row + dr;
                    int nc = coord.Column + dc;

                    if (nr >= 0 && nr < Size && nc >= 0 && nc < Size)
                    {
                        if (_grid[nr, nc].IsUsed && !coords.Contains(new Coordinate(nr, nc)))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
