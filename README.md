
# 🧩 Kelime Oyunu - Konsol Tabanlı Scrabble Klonu

Bu proje, C# dilinde geliştirilen ve konsol üzerinden oynanabilen iki kişilik bir **Scrabble benzeri kelime oyunu**dur. Her oyuncu rastgele taşlar alır, bu taşlarla geçerli Türkçe kelimeler oluşturarak tahtaya yerleştirir ve puan toplamaya çalışır.

## 📁 Proje Dosya Yapısı

```
KelimeOyunu/
│
├── Models/
│   ├── BoardCell.cs
│   ├── CellType.cs
│   ├── Coordinate.cs
│   ├── LetterTile.cs
│   └── Player.cs
│
├── Services/
│   ├── Board.cs
│   ├── ScoreCalculator.cs
│   ├── TileBag.cs
│   ├── DictionaryService.cs
│   └── PlayerFactory.cs
│
├── Utils/
│   ├── BoardUtils.cs
│   ├── WordExtractor.cs
│   ├── ConsoleHelper.cs
│   ├── InputParser.cs
│   └── ExtractedWord.cs
│
├── Data/
│   └── Dictionary.txt
│
├── Game.cs
└── Program.cs
```

## 🎯 Amaç

- Nesne yönelimli tasarım (OOP) ilkeleriyle,
- Ayrık ve test edilebilir yapılar kurarak,
- Gerçek oyun kurallarını simüle eden,
- Sade, okunabilir ve genişletilebilir bir sistem geliştirmek.

## 🧠 Mimarideki Ana Parçalar

### Models/
Veri modelleri (domain nesneleri): `Player`, `LetterTile`, `Coordinate`, `BoardCell`, `CellType`

### Services/
Oyun kuralları ve iş akışı: `Board`, `TileBag`, `ScoreCalculator`, `DictionaryService`

### Utils/
Yardımcı fonksiyonlar: `BoardUtils`, `WordExtractor`, `ConsoleHelper`, `InputParser`, `ExtractedWord`

## ✅ Kurallar

- Oyuncuların elinde en fazla **7 taş** olur.
- Taşlar bonus karelere denk gelirse puanlar çarpılır:
  - `H2`: Harf x2
  - `H3`: Harf x3
  - `K2`: Kelime x2
  - `K3`: Kelime x3
- Sözlük dışı kelimeler geçersizdir.
- İlk hamle hariç tüm kelimeler tahtadaki mevcut taşlara temas etmelidir.

## ▶️ Nasıl Çalıştırılır?

1. `dotnet run` ile başlatılır.
2. Oyuncular bilgilerini girer.
3. Örnek giriş: `7 7 H MERHABA`
4. Oyun sonunda skorlar görüntülenir.

## 📦 Dictionary.txt Dosyası

- `Data/Dictionary.txt` dosyası her satırda bir kelime içermelidir.
- Küçük-büyük harf duyarsızdır.

## 🧪 Test Edilebilirlik

- `BoardUtils`, `ScoreCalculator`, `WordExtractor` gibi sınıflar birim testine uygundur.
- Konsol çıktısı `ConsoleHelper` ile ayrılmıştır.

## 🚀 Geliştirme Önerileri

- UI: WPF veya Web tabanlı arayüz
- AI oyuncu desteği
- Online multiplayer (SignalR)
- JSON ile skor kaydı

## 👨‍💻 Geliştirici Notu

Bu proje temiz mimari, OOP ve modüler yapı hedeflenerek geliştirilmiştir. Kodlar single-responsibility prensibiyle ayrılmıştır.

## 📜 Lisans

Eğitim, ödev ve kişisel projelerde kullanılmak üzere açık kaynak olarak sunulmuştur.
