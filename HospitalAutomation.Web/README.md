# Hospital Automation

MVC mimarisi ile geliştirilen Hastane Otomasyonu, kullanıcı yönetimi, hasta takibi ve randevu planlama süreçlerini merkezileştirir.

## Genel Bakış
- Çok katmanlı mimari: Web (UI), Service (iş mantığı), Data (EF Core).
- Kimlik doğrulama ve yetkilendirme: ASP.NET Core Cookie Authentication + rol bazlı politikalar.
- Günlükleme: Serilog ile konsol ve dosya çıktısı.
- Veri erişimi: SQLite veritabanı, EF Core DbContext ve Unit of Work.

## Gereksinimler
- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- SQLite (geliştirme sırasında dahili dosya kullanır)
- Node.js (opsiyonel, ön uç derlemeleri için)

## Kurulum
```bash
git clone <repo-url>
cd HospitalAutomation.Web
dotnet restore
dotnet ef database update   # Migration'lar varsa
dotnet run
```

## Geliştirme Akışı
- `Program.cs` içerisinde servis kayıtlarını genişleterek yeni modüller ekleyin.
- Controller seviyesinde `LoggingActionFilter` otomatik olarak aksiyonları günlükler.
- `SessionManager` ve `AuthorizationHelper` HttpContext üzerinden global kullanılabilir.

## Test ve Doğrulama
- Birim testleri için `dotnet test` komutu kullanılabilir.
- Kimlik doğrulaması gerektiren sayfalar için test kullanıcılarını `DbInitializer` üzerinden ekleyin.

## Günlükleme
- Günlük dosyaları: `logs/hospital-<tarih>.log`
- Serilog seviyeleri `appsettings.*.json` üzerinden özelleştirilebilir.

## Proje Yapısı
- `HospitalAutomation.Data`: DbContext, Repository, Migration.
- `HospitalAutomation.Services`: Domain servisleri, iş kuralları.
- `HospitalAutomation.Web`: MVC katmanı, middleware, filtreler.

## Katkı Rehberi
1. Yeni bir dal açın.
2. Kod standartlarına ve mevcut mimariye uyun.
3. PR açmadan önce testleri çalıştırın.
# Hospital-Automation
