# CONTRIBUTING.md

## Giriþ
Bu proje için katký kurallarý, kodlama standartlarý ve proje gereksinimleri bu dokümanda toplanmýþtýr. Tüm geliþtiriciler bu kurallara uymalýdýr.

## Genel Gereksinimler
- Proje GitHub üzerinde public olarak paylaþýlacaktýr.
- Veritabaný olarak SQLite (uygulama içinde taþýnabilir) veya SQL Server (LocalDB/MS SQL) kullanýlabilir. Eðer SQL Server tercih edilirse veritabaný oluþturma scriptleri veya migration dosyalarý repoya eklenmelidir.
- Entity Framework kullanýlmalýdýr. Tercih edilen yaklaþým Code First olmakla birlikte Database First kabul edilebilir; migration kullanýmý teþvik edilir.
- SOLID prensiplerine uyulacaktýr.

## Authentication & Authorization
- Kullanýcý kayýt, giriþ ve çýkýþ akýþlarý olmalýdýr. Session tabanlý veya token tabanlý (JWT) yaklaþýmlardan birisi kullanýlabilir.
- Kullanýcý rolleri (Admin, Doctor, Nurse, Receptionist, Patient vb.) tanýmlanmalý ve yetkilendirme kontrolleri uygulanmalýdýr.
- Yetki kontrolü hem UI seviyesinde hem servis/iþ katmanýnda doðrulanmalýdýr.

## Güvenlik
- Kullanýcý þifreleri güçlü biçimde saklanmalýdýr: PBKDF2 (Rfc2898DeriveBytes) veya bcrypt/argon2 kullanýlmalýdýr. Sabit salt kullanýlmamalýdýr; kullanýcý baþýna salt oluþturulmalýdýr.
- 'Beni hatýrla' veya yerel credential saklama için DPAPI / ProtectedData veya iþletim sistemi kimlik depolarý (Windows Credential Manager) kullanýlmalýdýr. Basit XOR/Base64 yöntemleri kabul edilmez.
- Hassas veriler loglara, MessageBox veya hata mesajlarýna düz metin olarak yazýlmamalýdýr.

## Veritabaný ve Seed
- Geliþtirme ortamýnda seed verisi eklenebilir fakat production ortamýnda otomatik DB silme/yeniden oluþturma (ör. DropCreateDatabaseAlways) kullanýlmamalýdýr.
- Migration tabanlý yaklaþýmla schema deðiþiklikleri yönetilmeli; büyük deðiþikliklerde migration scriptleri PR ile gelmelidir.
- Eðer LocalDB/SQL Server kullanýlýyorsa `README` veya proje kökünde `sql/` klasöründe veritabaný oluþturma scriptleri bulunmalýdýr.

## Kod Stili ve Formatlama
- `.editorconfig` dosyasý proje kökünde bulunmalý; kod formatlama ve adlandýrma standartlarý bu dosyada belirtilmelidir.
- Ýsimlendirme: PascalCase for classes and methods, camelCase for local variables and parameters. Public member names must be clear.
- Exception yakalama: Genel `catch (Exception)` bloklarý minimal tutulmalý; mümkünse spesifik exception tipi yakalanmalý ve hata loglanmalýdýr.

## Unit Tests ve QA
- Kritik servisler ve iþ kurallarý için unit test eklenmelidir (MSTest, NUnit veya xUnit kabul edilir).
- Beste ciyle çalýþan basit test pipeline önerilir (GitHub Actions).

## UI
- Web projelerinde responsive tasarým (Bootstrap vb.) gereklidir.
- Masaüstü uygulamalarýnda uzun süren iþlemler UI thread'ini bloklamamalýdýr (async/Task.Run veya BackgroundWorker kullanýmý).

## Logging ve Hata Yönetimi
- Süreç loglarý ve hatalar kalýcý olarak saklanmalýdýr (örn. Serilog/NLog ile dosya veya konsol). Debug.WriteLine yetersizdir.
- Kullanýcýya gösterilecek hata mesajlarý kullanýcý dostu olmalýdýr; teknik detaylar loglara yazýlmalýdýr.

## Pull Request & Kod Ýncelemesi
- Tüm deðiþiklikler feature branch üzerinde olmalýdýr. PR açýklamasý deðiþikliðin ne yaptýðýný ve nedenini anlatmalýdýr.
- PR'larda kod stiline uymayan satýrlar veya güvenlik açýklarý için review yapýlacaktýr.

## Commit Mesajlarý
- Kýsa, anlamlý mesajlar kullanýn. Örnek: `feat(auth): add login endpoint`, `fix(db): prevent drop database in production`.

## Geliþtirme Önerileri (Opsiyonel)
- Stored credentials için Windows DPAPI kullanýmý
- Parola hash'leri için PBKDF2 kullanýmý
- Serilog ile dönen loglarýn sabit diske yazýlmasý

## Ýletiþim
- Proje ile ilgili sorular için issue açýnýz.