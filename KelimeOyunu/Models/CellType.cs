/// Oyun tahtasındaki bir hücrenin puan çarpanı türünü tanımlar.
public enum CellType
{
    Normal,         // Çarpansız normal hücre
    DoubleLetter,   // Harf puanını 2 ile çarpar (H2)
    TripleLetter,   // Harf puanını 3 ile çarpar (H3)
    DoubleWord,     // Kelime puanını 2 ile çarpar (K2)
    TripleWord      // Kelime puanını 3 ile çarpar (K3)
}
