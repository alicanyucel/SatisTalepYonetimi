# 🛒 Satış Talep Yönetimi

Kurumsal satış talep süreçlerini uçtan uca yöneten, **Clean Architecture** prensiplerine uygun olarak geliştirilmiş bir Web API uygulamasıdır. Satış talepleri, tedarikçi yönetimi, satınalma teklifleri, bakım kartları ve bakım biletleri gibi iş süreçlerini kapsar.

---

## 📐 Mimari (Clean Architecture)

```
SatisTalepYonetimi/
├── Domain           → Entity, Enum, Repository arayüzleri
├── Application      → CQRS (Command/Query), Validator, Mapping, Servisler
├── Infrastructure   → EF Core DbContext, Repository implementasyonları, Migration
└── WebAPI           → Controller, Middleware, Program.cs
```

---

## 🔄 İş Akışı

```
Satış Talebi Oluştur (Pending)
    → Yönetici Onayı (ManagerApproval → ManagerApproved)
        → Satınalma Süreci (ProcurementPending)
            → Teklifler Toplandı (QuotesCollected)
                → Yönetim Onayı (ManagementApproval)
                    → Teklif Onaylandı (QuoteApproved)
                        → Tamamlandı (Completed)

* Her aşamada "Reddedildi" veya "İptal Edildi" durumuna geçilebilir.
```

---

## 🚀 Teknolojiler

| Katman | Teknoloji |
|---|---|
| **Framework** | .NET 8 / ASP.NET Core Web API |
| **ORM** | Entity Framework Core 8 |
| **Veritabanı** | Microsoft SQL Server 2022 |
| **CQRS & Mediator** | MediatR |
| **Validation** | FluentValidation |
| **Object Mapping** | AutoMapper |
| **Smart Enum** | Ardalis.SmartEnum |
| **Result Pattern** | TS.Result |
| **Kimlik Doğrulama** | JWT Bearer Authentication |
| **Yetkilendirme** | ASP.NET Core Identity |
| **Arka Plan İşleri** | Hangfire (SQL Server Storage) |
| **Loglama** | Serilog (Console, File, Seq) |
| **Gözlemlenebilirlik** | OpenTelemetry (Tracing + Metrics) |
| **Metrik Toplama** | Prometheus |
| **Dashboard** | Grafana |
| **API Dokümantasyonu** | Swagger / Swashbuckle |
| **DI Scan** | Scrutor |
| **Konteynerizasyon** | Docker & Docker Compose |
| **CI/CD** | GitHub Actions |
| **Unit Test** | xUnit, NSubstitute |
| **Test Coverage** | coverlet (opencover + cobertura) |
| **Kod Kalitesi** | SonarQube |

---

## 📦 Modüller

| Modül | Açıklama |
|---|---|
| **Müşteriler (Customers)** | Müşteri CRUD işlemleri |
| **Ürünler (Products)** | Ürün CRUD işlemleri |
| **Satış Talepleri (SalesRequests)** | Talep oluşturma, durum güncelleme, onay süreci |
| **Tedarikçiler (Suppliers)** | Tedarikçi CRUD işlemleri |
| **Satınalma Teklifleri (PurchaseQuotes)** | Teklif toplama, onaya sunma, onaylama |
| **Bakım Kartları (MaintenanceCards)** | Bakım kartı yönetimi |
| **Bakım Biletleri (MaintenanceTickets)** | Bakım bilet durumu takibi |

---

## ⚙️ Kurulum

### Gereksinimler
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker & Docker Compose](https://docs.docker.com/get-docker/)

### Docker ile Çalıştırma

```bash
docker-compose up -d
```

Uygulama başladıktan sonra:

| Servis | URL |
|---|---|
| **API (Swagger)** | http://localhost:5000/swagger |
| **Prometheus** | http://localhost:9090 |
| **Grafana** | http://localhost:3000 (admin / admin) |

### Lokalde Çalıştırma

```bash
# appsettings.json içindeki connection string'i düzenleyin
dotnet ef database update --project SatisTalepYonetimi/SatisTalepYonetimi.Infrastructure --startup-project SatisTalepYonetimi/SatisTalepYonetimi.WebAPI
dotnet run --project SatisTalepYonetimi/SatisTalepYonetimi.WebAPI
```

---

## 🔐 API Kimlik Doğrulama

Swagger UI üzerinden **Authorize** butonuna tıklayarak JWT Bearer token girebilirsiniz.

---

## 🧪 Unit Testler

Proje, **xUnit** ve **NSubstitute** kullanılarak yazılmış kapsamlı unit testler içermektedir. Testler `Test` projesi altında, Application katmanındaki Command/Query Handler'ları ve Domain katmanındaki iş kurallarını doğrular.

```
Test/
├── Domain/
│   └── SalesRequestStatusEnumTests           → Satış talebi durum enum doğrulamaları
└── Features/
    ├── Customers/
    │   ├── CreateCustomerCommandHandlerTests  → Müşteri oluşturma
    │   ├── UpdateCustomerCommandHandlerTests  → Müşteri güncelleme
    │   ├── DeleteCustomerCommandHandlerTests  → Müşteri silme
    │   └── GetAllCustomersQueryHandlerTests   → Müşteri listeleme
    ├── Products/
    │   ├── CreateProductCommandHandlerTests   → Ürün oluşturma
    │   ├── DeleteProductCommandHandlerTests   → Ürün silme
    │   └── GetAllProductsQueryHandlerTests    → Ürün listeleme
    ├── SalesRequests/
    │   ├── CreateSalesRequestCommandHandlerTests       → Satış talebi oluşturma
    │   ├── DeleteSalesRequestCommandHandlerTests       → Satış talebi silme
    │   ├── UpdateSalesRequestStatusCommandHandlerTests  → Durum güncelleme
    │   ├── GetAllSalesRequestsQueryHandlerTests        → Talep listeleme
    │   └── GetSalesRequestByIdQueryHandlerTests        → Talep detay sorgulama
    ├── Suppliers/
    │   └── GetAllSuppliersQueryHandlerTests   → Tedarikçi listeleme
    ├── PurchaseQuotes/
    │   └── GetQuotesBySalesRequestQueryHandlerTests → Teklif sorgulama
    ├── MaintenanceCards/
    │   └── GetAllMaintenanceCardsQueryHandlerTests  → Bakım kartı listeleme
    └── MaintenanceTickets/
        └── GetAllMaintenanceTicketsQueryHandlerTests → Bakım bileti listeleme
```

### Testleri Çalıştırma

```bash
dotnet test
```

| Araç | Amaç |
|---|---|
| **xUnit** | Test framework |
| **NSubstitute** | Mock/fake oluşturma |

### Test Coverage

Projede `coverlet.runsettings` dosyası ile coverage ayarları yapılandırılmıştır. Bu dosya opencover + cobertura formatlarında rapor üretir ve `Migrations`, `Program`, `Startup` sınıflarını kapsam dışı bırakır.

```bash
# runsettings ile test çalıştırma
dotnet test --settings coverlet.runsettings

# veya doğrudan
dotnet test --collect:"XPlat Code Coverage"
```

HTML rapor oluşturmak için **ReportGenerator** kullanabilirsiniz:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.opencover.xml" -targetdir:"coveragereport" -reporttypes:Html
```

---

## 🔍 SonarQube Kod Kalitesi Analizi

Proje, **SonarQube** ile statik kod analizi ve test coverage takibi yapılacak şekilde yapılandırılmıştır. Proje kök dizinindeki `sonar-project.properties` dosyası temel SonarQube ayarlarını (proje key, exclusion'lar, coverage rapor yolları) içerir.

### SonarQube ile Lokal Analiz

```bash
# SonarQube Scanner kurulumu
dotnet tool install --global dotnet-sonarscanner

# Analiz başlat
dotnet sonarscanner begin /k:"SatisTalepYonetimi" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="YOUR_TOKEN" /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"

# Build ve test (coverage ile)
dotnet build
dotnet test --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

# Analiz sonlandır
dotnet sonarscanner end /d:sonar.token="YOUR_TOKEN"
```

### SonarQube Dashboard Metrikleri

| Metrik | Açıklama |
|---|---|
| **Code Coverage** | Unit testlerin kod kapsama oranı |
| **Bugs** | Potansiyel hata tespitleri |
| **Vulnerabilities** | Güvenlik açığı tespitleri |
| **Code Smells** | Kod kalitesi iyileştirme önerileri |
| **Duplications** | Tekrarlayan kod oranı |
| **Technical Debt** | Teknik borç tahmini |

---

## 📊 Gözlemlenebilirlik

- **Tracing**: OpenTelemetry → OTLP Exporter ile dağıtık izleme
- **Metrics**: Prometheus exporter → `/metrics` endpoint'i
- **Logging**: Serilog ile konsol ve dosya bazlı loglama
- **Dashboard**: Grafana üzerinde hazır ASP.NET Core dashboard'u

---

## 📄 Lisans

Bu proje MIT lisansı ile lisanslanmıştır.
