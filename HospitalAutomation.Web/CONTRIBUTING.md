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
- Yetki kontrolü hem UI seviyesinde hem servis/iç katmanýnda doðrulanmalýdýr.

## Güvenlik
- Kullanýcý þifreleri güvenli þekilde saklanmalýdýr: PBKDF2 (Rfc2898DeriveBytes) veya bcrypt/argon2 kullanýlmalýdýr. Sabit salt kullanýlmamalýdýr; kullanýcý baþýna benzersiz salt oluþturulmalýdýr.
- 'Beni hatýrla' veya yerel credential saklama için DPAPI / ProtectedData veya iþletim sistemi kimlik depolarý (Windows Credential Manager) kullanýlmalýdýr. Basit XOR/Base64 yöntemleri veya sabit anahtar ile þifreleme kabul edilmez.
- Yerel kayýt alanlarýnda (registry, dosya) tersine çevrilebilir biçimde parola saklanmamalýdýr; yalnýzca token veya OS-backed güvenli saklama kullanýlmalýdýr.
- Hassas veriler loglara, MessageBox veya hata mesajlarýna düz metin olarak yazýlmamalýdýr.

## Veritabaný ve Seed
- Geliþtirme ortamýnda seed verisi eklenebilir fakat production ortamýnda otomatik DB silme/yeniden oluþturma (örn. DropCreateDatabaseAlways) kullanýlmamalýdýr.
- Migration tabanlý yaklaþým ile schema deðiþiklikleri yönetilmeli; büyük deðiþikliklerde migration scriptleri PR ile gelmelidir.
- Eðer LocalDB/SQL Server kullanýlýyorsa `README` veya proje kökünde `sql/` klasöründe veritabaný oluþturma scriptleri bulunmalýdýr.

## Kod Standartlarý
- Proje kökünde bir `.editorconfig` dosyasý bulunmalýdýr. Kod formatlama ve stil kurallarý `.editorconfig` ile tanýmlanacaktýr.

## Credential Storage Policy
- Uygulamada "Beni Hatýrla" davranýþý için registry ya da dosyaya sabit anahtar ile tersine çevrilebilir þifreleme ile kayýt yapýlamaz.
- Kullanýcý parolalarý veritabanýnda veya sunucuda sadece güçlü hash algoritmalarý (PBKDF2/bcrypt/argon2) ile saklanmalýdýr.
- Yerel hatýrlama gerekiyorsa Windows DPAPI/ProtectedData veya Windows Credential Manager gibi platform tarafýndan saðlanan güvenli saklama çözümleri kullanýlmalýdýr.

## Diðer
- Logging / History opsiyonel ama tercih edilir; hassas veri loglanmamalýdýr.
- Proje teslimi GitHub üzerinden yapýlmalýdýr; public repo linki paylaþýlacaktýr.