# TRODHA - Ağız ve Diş Sağlığı Hedef Takip Sistemi

TRODHA (Türkiye Oral ve Dental Hijyen Asistanı), kullanıcıların ağız ve diş sağlığı ile ilgili hedeflerini belirlemelerine, takip etmelerine ve bu hedeflere ulaşma süreçlerini yönetmelerine yardımcı olan kapsamlı bir web uygulamasıdır. Modern bir arayüz ve güçlü bir altyapı ile kullanıcıların diş fırçalama, diş ipi kullanma, ağız bakımı gibi düzenli alışkanlıklar edinmelerine ve bu alışkanlıkları sürdürmelerine destek olur.

## Proje Hakkında

TRODHA, ağız ve diş sağlığı konusunda farkındalık yaratmak ve kullanıcıların düzenli ağız bakımı alışkanlıkları geliştirmelerine yardımcı olmak amacıyla tasarlanmıştır. Uygulama, diş hekimlerinin önerdiği düzenli bakım rutinlerini (günde iki kez diş fırçalama, günlük diş ipi kullanımı, ağız gargarası yapma vb.) takip etmeyi kolaylaştırır ve kullanıcıları bu alışkanlıkları sürdürmeleri için motive eder.

Proje, ASP.NET Core 8.0 backend ve Angular 19 frontend kullanılarak geliştirilmiş bir full-stack web uygulamasıdır. Temiz mimari prensipleri doğrultusunda tasarlanmış olup, katmanlı bir yapıya sahiptir:

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

#### Ağız ve Diş Sağlığı Hedef Yönetimi
- Diş fırçalama, diş ipi kullanma, ağız gargarası gibi ağız bakım hedefleri oluşturma
- Kişiselleştirilmiş hedef sıklığı belirleme (günde 2 kez, haftada 3 kez vb.)
- Hedefleri önem seviyesine göre kategorize etme
- Hedef listeleme ve detaylarını görüntüleme
- Hedef güncelleme ve silme
- Hedefleri aktif/pasif yapma

#### Hedef Durumu Takibi
- Günlük diş bakımı kayıtlarını ekleme
- Tamamlanan ve tamamlanmayan bakım rutinlerini görüntüleme
- Bakım süresi ve notları kaydetme
- Tarih aralığına göre filtreleme ve ilerleme takibi

#### Ağız Sağlığı Not Sistemi
- Diş hekimi ziyaretleri ve tedaviler hakkında notlar oluşturma
- Ağız bakımı ipuçları ve hatırlatıcılar kaydetme
- Notları listeleme ve düzenleme
- Önemli bilgileri kaydetme ve silme

#### Ağız ve Diş Sağlığı Önerileri Sistemi
- Diş hekimleri tarafından onaylanmış genel ağız bakımı önerilerini görüntüleme
- Kullanıcının ağız sağlığı alışkanlıklarına özel kişiselleştirilmiş öneriler alma
- Yaş ve ihtiyaca göre özelleştirilmiş bakım tavsiyeleri

#### Dashboard
- Ağız bakımı alışkanlıklarının genel görünümü
- Aktif diş bakımı hedeflerini görüntüleme
- Tamamlanan ve eksik bakım rutinlerinin istatistiklerini görüntüleme
- Ağız sağlığı ilerlemesini takip etme

### Yarım Kalan Özellikler

#### Kullanıcı Yönetimi
- Şifre sıfırlama özelliği (backend altyapısı var, frontend tamamlanmamış)
- Kullanıcı profil sayfası ve diş sağlığı bilgileri (temel yapı var, detaylar eksik)
- Diş hekimi randevularını kaydetme ve hatırlatma sistemi

#### Ağız Sağlığı Not Sistemi
- Notlara diş ve ağız sağlığı fotoğrafları ekleme (backend altyapısı hazır, frontend tamamlanmamış)
- Diş hekimi raporlarını ve reçeteleri kaydetme özelliği

#### Dashboard
- İleri seviye ağız sağlığı istatistikleri ve grafikler
- Zaman içindeki diş bakımı alışkanlıklarını gösteren grafikler
- Diş bakımı hedeflerinin tamamlanma oranlarını gösteren görselleştirmeler

### Eksik Olan Özellikler

#### Diş Hekimi ve Admin Paneli
- Kullanıcı ağız sağlığı verilerinin yönetimi
- Ağız ve diş sağlığı önerilerinin yönetimi
- Sistem istatistikleri ve kullanıcı davranış analizleri
- Diş hekimi rolü ve yetkilendirme sistemi

#### Kullanıcı Arayüzü İyileştirmeleri
- Mobil cihazlara uyumlu responsive tasarım
- Gece/gündüz tema seçenekleri
- Kullanıcı deneyiminin geliştirilmesi ve daha sezgisel arayüz
- Diş bakımı animasyonları ve eğitici görsel içerikler

#### İleri Seviye Özellikler
- Diş fırçalama ve bakım hatırlatıcıları
- Diş hekimi randevu bildirimleri
- Ağız sağlığı kategorileri (diş, diş eti, dil, vb.)
- Yaşa ve ihtiyaca göre özelleştirilmiş diş bakımı şablonları
- Diş hekimleriyle iletişim modülü

## Teknolojiler

### Backend
- ASP.NET Core 8.0 - Modern ve yüksek performanslı web API geliştirme
- Entity Framework Core - Veritabanı işlemleri için ORM
- JWT Authentication - Güvenli kullanıcı kimlik doğrulama
- SQL Server - Ağız ve diş sağlığı verilerinin güvenli depolanması
- Clean Architecture - Sürdürülebilir ve test edilebilir kod yapısı
- Repository Pattern - Veri erişim katmanı soyutlama
- Dependency Injection - Bağımlılık yönetimi
- FluentValidation - Veri doğrulama

### Frontend
- Angular 19 - Modern ve reaktif kullanıcı arayüzü
- TypeScript - Tip güvenliği ve gelişmiş kod yapısı
- Bootstrap - Responsive ve mobil uyumlu tasarım
- Reactive Forms - Dinamik form yönetimi
- Angular Router - Sayfa yönlendirme ve navigasyon
- HttpClient - Backend API ile iletişim
- RxJS - Asenkron veri akışı yönetimi
- Chart.js - Ağız sağlığı verilerinin görselleştirilmesi

## Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Node.js ve npm
- SQL Server (LocalDB veya tam sürüm)
- Visual Studio 2022 veya Visual Studio Code

### Adımlar

1. Projeyi klonlayın:
```bash
git clone https://github.com/kullanici/TRODHA.git
cd TRODHA
```

2. Veritabanını oluşturun:
```bash
cd TRODHA.Server
dotnet ef database update
```

3. Backend'i çalıştırın:
```bash
dotnet run
```

4. Frontend'i çalıştırın:
```bash
cd ../trodha.client
npm install
npm start
```

5. Tarayıcınızda uygulamayı açın:
```text
https://localhost:57438
```


## API Erişimi

Backend API'ye aşağıdaki URL üzerinden erişilebilir:

```text
http://localhost:5253/api
```

Swagger dokümantasyonuna erişmek için:

```text
http://localhost:5253/swagger
```

## Proje Yapısı

```text
TRODHA/
├── TRODHA.Core/                # Temel veri modelleri ve ağız sağlığı varlıkları
├── TRODHA.Infrastructure/      # Veritabanı işlemleri ve repository sınıfları
├── TRODHA.Application/         # Ağız sağlığı iş mantığı ve servis katmanı
├── TRODHA.Server/              # API kontrolcüleri ve web sunucusu
└── trodha.client/              # Angular tabanlı kullanıcı arayüzü
```

## Ağız ve Diş Sağlığı Önemi

TRODHA projesi, düzenli ağız ve diş bakımının önemini vurgulamak ve kullanıcıların sağlıklı alışkanlıklar geliştirmesine yardımcı olmak amacıyla geliştirilmiştir. Düzenli diş fırçalama, diş ipi kullanımı ve ağız bakımı:

- Diş çürüklerini önler
- Diş eti hastalıklarını azaltır
- Ağız kokusunu engeller
- Genel sağlığa olumlu katkı sağlar
- Diş kaybını önler ve estetik bir gülüş sunar

Uygulama, kullanıcıların bu önemli sağlık alışkanlıklarını düzenli olarak takip etmelerine ve sürdürmelerine yardımcı olarak toplum sağlığına katkıda bulunmayı hedeflemektedir.

