# TRODHA - Kişisel Hedef Takip Sistemi

TRODHA, kullanıcıların kişisel hedeflerini belirlemelerine, takip etmelerine ve bu hedeflere ulaşma süreçlerini yönetmelerine yardımcı olan kapsamlı bir web uygulamasıdır. Modern bir arayüz ve güçlü bir altyapı ile kullanıcıların hedeflerine odaklanmalarını ve ilerlemelerini izlemelerini sağlar.

## Proje Hakkında

TRODHA, ASP.NET Core 8.0 backend ve Angular 19 frontend kullanılarak geliştirilmiş bir full-stack web uygulamasıdır. Temiz mimari prensipleri doğrultusunda tasarlanmış olup, katmanlı bir yapıya sahiptir:

- **TRODHA.Core**: Temel veri modelleri ve arayüzler
- **TRODHA.Infrastructure**: Veritabanı işlemleri ve repository sınıfları
- **TRODHA.Application**: İş mantığı ve servis katmanı
- **TRODHA.Server**: API kontrolcüleri ve web sunucusu
- **trodha.client**: Angular tabanlı kullanıcı arayüzü

## Özellikler

### Tamamlanan Özellikler

#### Temel Altyapı
- Backend (ASP.NET Core) temel yapısı
- Frontend (Angular) temel yapısı
- Veritabanı modelleri ve ilişkileri
- JWT tabanlı kimlik doğrulama sistemi

#### Kullanıcı Yönetimi
- Kullanıcı kaydı
- Kullanıcı girişi
- Token doğrulama
- Admin hesabı ekleme
- "Admin olarak giriş yap" butonu
- Çıkış yapma fonksiyonu

#### Hedef Yönetimi
- Hedef oluşturma
- Hedef listeleme
- Hedef detaylarını görüntüleme
- Hedef güncelleme
- Hedef silme
- Hedefleri aktif/pasif yapma

#### Hedef Durumu Takibi
- Hedef durumu ekleme
- Hedef durumlarını listeleme
- Hedef durumu silme
- Tarih aralığına göre filtreleme

#### Not Yönetimi
- Not oluşturma
- Not listeleme
- Not güncelleme
- Not silme

#### Öneriler Sistemi
- Genel önerileri görüntüleme
- Kullanıcıya özel önerileri görüntüleme

#### Dashboard
- Temel dashboard görünümü
- Aktif hedefleri görüntüleme
- Basit istatistikleri görüntüleme

### Yarım Kalan Özellikler

#### Kullanıcı Yönetimi
- Şifre sıfırlama özelliği (backend altyapısı var, frontend tamamlanmamış)
- Kullanıcı profil sayfası (temel yapı var, detaylar eksik)

#### Not Yönetimi
- Notlara resim ekleme (backend altyapısı hazır, frontend tamamlanmamış)

#### Dashboard
- İleri seviye istatistikler ve grafikler (temel istatistikler var, grafikler eksik)

### Eksik Olan Özellikler

#### Admin Paneli
- Kullanıcı yönetimi
- Öneri yönetimi
- Sistem istatistikleri
- Admin rolü ve yetkilendirme sistemi

#### Kullanıcı Arayüzü İyileştirmeleri
- Responsive tasarımın iyileştirilmesi
- Tema seçenekleri
- Kullanıcı deneyiminin geliştirilmesi
- Animasyonlar ve geçişler

#### İleri Seviye Özellikler
- Hedef hatırlatıcıları
- Bildirim sistemi
- Hedef kategorileri
- Hedef şablonları

## Teknolojiler

### Backend
- ASP.NET Core 8.0
- Entity Framework Core
- JWT Authentication
- SQL Server
- Clean Architecture
- Repository Pattern
- Dependency Injection
- FluentValidation

### Frontend
- Angular 19
- TypeScript
- Bootstrap
- Reactive Forms
- Angular Router
- HttpClient
- RxJS

## Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Node.js ve npm
- SQL Server (LocalDB veya tam sürüm)
- Visual Studio 2022 veya Visual Studio Code

### Adımlar

1. Projeyi klonlayın:
```
git clone https://github.com/kullanici/TRODHA.git
cd TRODHA
```

2. Veritabanını oluşturun:
```
cd TRODHA.Server
dotnet ef database update
```

3. Backend'i çalıştırın:
```
dotnet run
```

4. Frontend'i çalıştırın:
```
cd ../trodha.client
npm install
npm start
```

5. Tarayıcınızda uygulamayı açın:
```
https://localhost:57438
```

## Otomatik Başlatma

Projeyi otomatik olarak başlatmak için kök dizindeki PowerShell scriptlerini kullanabilirsiniz:

```
.\restart-app-final.ps1
```

Bu script, backend ve frontend uygulamalarını otomatik olarak başlatacaktır.

## API Erişimi

Backend API'ye aşağıdaki URL üzerinden erişilebilir:

```
http://localhost:5253/api
```

Swagger dokümantasyonuna erişmek için:

```
http://localhost:5253/swagger
```

## Proje Yapısı

```
TRODHA/
├── TRODHA.Core/                # Temel veri modelleri ve arayüzler
├── TRODHA.Infrastructure/      # Veritabanı işlemleri ve repository sınıfları
├── TRODHA.Application/         # İş mantığı ve servis katmanı
├── TRODHA.Server/              # API kontrolcüleri ve web sunucusu
└── trodha.client/              # Angular tabanlı kullanıcı arayüzü
```

## Katkıda Bulunma

Projeye katkıda bulunmak için lütfen bir fork oluşturun ve pull request gönderin. Büyük değişiklikler için, lütfen önce bir issue açarak değişikliği tartışmaya açın.

## Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.
