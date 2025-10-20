OCPI(Open Charge Point Interface) - .NET ile Geliştirilmiş Yüksek Performanslı OCPI Platformu

*OCPI protokolünü kullanarak Elektrikli Araç (EV) şarj ağları arasında tam entegrasyon ve birlikte çalışabilirlik sağlayan modern ve ölçeklenebilir bir platform.

Projenin Amacı

Elektrikli araç ekosistemi hızla büyürken, farklı şarj ağı operatörlerinin ve hizmet sağlayıcılarının birbiriyle sorunsuz bir şekilde iletişim kurması kritik bir hale gelmiştir. Bu proje, OCPI standardını temel alarak bu iletişimi sağlayacak sağlam bir altyapı sunar ve aşağıdaki sorunlara çözüm getirir:

-   Veri Karmaşıklığı:** Farklı sistemler arasındaki veri formatı uyumsuzluklarını ortadan kaldırır.
-   Gerçek Zamanlı Bilgi Eksikliği:** Şarj noktası durumu, lokasyon bilgisi ve ücretlendirme gibi kritik verilerin anlık olarak paylaşılmasını sağlar.
-   Entegrasyon Zorlukları:** Yeni bir CPO veya EMSP'nin ağa dahil olma sürecini standartlaştırır ve hızlandırır.
-   Ölçeklenebilirlik:** Artan şarj noktası ve kullanıcı sayısına kolayca adapte olabilen bir mimari sunar.


Öne Çıkan Özellikler

- Tam OCPI v2.2.1 Desteği: Protokolün tüm temel modüllerini içerir:
    -  'Tokens'
    -  'Locations' (EVSEs, Connectors dahil)
    -  'Sessions'
    -  'CDRs' (Charge Detail Records)
    -  'Tariffs'
    -  'Commands' (START_SESSİON, STOP_SESSİON)
    -  'Charging Profiles'
    -  'Credentials' 
    -  'Energy'
    
      
-   Tam OCPI v2.2.1 Desteği: Protokolün tüm temel modüllerini içerir:
    -   `Tokens`, `Locations` (EVSEs, Connectors dahil), `Sessions`, `CDRs` (Charge Detail Records), `Tariffs`, `Commands` (START_SESSION, STOP_SESSION)
-   Modern .NET Mimarisi:** .NET 8 ve ASP.NET Core'un sunduğu performans ve güvenlik avantajlarından tam olarak yararlanır. Minimal APIs ile daha temiz ve hızlı endpoint'ler.
-   Güvenlik Odaklı:** ASP.NET Core Identity veya JWT (JSON Web Tokens) tabanlı kimlik doğrulama ve yetkilendirme mekanizmaları.
-   Modüler ve Genişletilebilir Tasarım:** SOLID prensiplerine uygun, yeni OCPI versiyonlarına veya özel iş mantıklarına kolayca adapte edilebilir esnek yapı.
-   Docker Desteği:** `docker-compose` ile tek komutla geliştirme ve production ortamını ayağa kaldırma imkanı.
-   ORM Desteği:** Entity Framework Core ile veritabanı işlemlerinde yüksek verimlilik ve esneklik.
-   API Dokümantasyonu:** Swashbuckle (Swagger) entegrasyonu ile tüm endpoint'lerin interaktif olarak test edilebilmesi ve belgelenmesi.

     Kullanılan Teknolojiler

-   Backend: C# 12, ASP.NET Core, .NET 8
-   Framework: Entity Framework Core 8
-   Veritabanı: PostgreSQL / SQL Server / SQLite
-   Mimari: Clean Architecture / CQRS Pattern (Eğer kullandıysanız ekleyebilirsiniz)
-   Containerization: Docker, Docker Compose
-   API Dokümantasyonu: Swashbuckle (Swagger)
-   Loglama: Serilog


Aşağıda temel OCPI modülleri için birkaç örnek endpoint bulunmaktadır.

| Metot  | Endpoint                                | Açıklama                                                |
| :----- | :-------------------------------------- | :------------------------------------------             |
| 'Get'  | '/ocpi/2.2.1/locations'                 | Sistemdeki tüm şarj noktası lokasyonlarını getirir.     |
| 'POST' | '/ocpi/2.2.1/command/START_SESSION'     | Belirtilen bir şarj noktasında yeni bir oturum başlatır.|
| `'GET' | '/ocpi/2.2.1/tariffs'                   | Geçerli tüm ücret tarifelerini listeler.                |
| 'PUT'  | '/ocpi/2.2.1/cdr/{cdr_id}'              | Yeni bir şarj detayı kaydı (CDR) gönderir.              |

