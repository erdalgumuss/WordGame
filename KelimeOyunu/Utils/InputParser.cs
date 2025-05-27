using WordGame.Models;

/// <summary>
/// Kullanıcının hamlesini temsil eden veri yapısıdır.
/// İçinde başlangıç koordinatı, yön (yatay/dikey) ve kelime bulunur.
/// </summary>
public class MoveInput
{
    public Coordinate Start { get; set; }        // Başlangıç koordinatı
    public bool IsHorizontal { get; set; }       // Yön bilgisi (H: yatay, V: dikey)
    public string Word { get; set; }             // Oynanmak istenen kelime
}

/// <summary>
/// Kullanıcının metin girişini parse eden yardımcı sınıftır.
/// Geçerli format: "row col H|V WORD"
/// </summary>
public static class InputParser
{
    /// <summary>
    /// Giriş dizesini çözümleyerek MoveInput nesnesine dönüştürür.
    /// </summary>
    /// <param name="input">Kullanıcıdan alınan hamle (örn: "7 7 H KELİME")</param>
    /// <param name="move">Başarılıysa oluşturulan hareket nesnesi</param>
    /// <returns>Parse işlemi başarılı mı?</returns>
    public static bool TryParse(string input, out MoveInput? move)
    {
        move = null;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        var parts = input.Trim().Split(' ');
        if (parts.Length != 4) return false;

        if (!int.TryParse(parts[0], out int row)) return false;
        if (!int.TryParse(parts[1], out int col)) return false;
        if (string.IsNullOrWhiteSpace(parts[2])) return false;

        bool isHorizontal = parts[2].ToUpper() == "H";
        string word = parts[3].ToUpperInvariant();

        move = new MoveInput
        {
            Start = new Coordinate(row, col),
            IsHorizontal = isHorizontal,
            Word = word
        };

        return true;
    }
}
