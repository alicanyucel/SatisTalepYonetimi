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
| **Yük Testi** | k6 |
| **Health Check** | AspNetCore.HealthChecks.SqlServer |
| **Rate Limiting** | ASP.NET Core Rate Limiting (built-in) |

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
| **MockQueryable** | IQueryable mock desteği |
| **FluentAssertions** | Okunabilir assertion'lar |

### Test Coverage

Projede `coverlet.runsettings` dosyası ile coverage ayarları yapılandırılmıştır. Opencover + Cobertura formatlarında rapor üretir, `Migrations`, `Program`, `Startup` sınıflarını kapsam dışı bırakır.

```bash
# runsettings ile test çalıştırma
dotnet test --settings coverlet.runsettings

# HTML rapor oluşturma
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.opencover.xml" -targetdir:"coveragereport" -reporttypes:Html
```

---

## 🔍 SonarQube Kod Kalitesi Analizi

Proje kök dizinindeki `sonar-project.properties` dosyası temel SonarQube ayarlarını (proje key, exclusion'lar, coverage rapor yolları) içerir.

### SonarQube ile Lokal Analiz

```bash
dotnet tool install --global dotnet-sonarscanner

dotnet sonarscanner begin /k:"SatisTalepYonetimi" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="YOUR_TOKEN" /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"

dotnet build
dotnet test --settings coverlet.runsettings

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

## 🔄 CI/CD Pipeline (GitHub Actions)

`.github/workflows/ci-cd.yml` dosyası ile otomatik build, test, coverage, SonarQube analizi ve Docker image push işlemleri yapılmaktadır.

```
CI/CD Pipeline
├── build        → Restore, Build, Test (Coverage ile), HTML Rapor, Artifact Upload
├── sonarqube    → SonarScanner ile statik kod analizi + coverage
└── docker       → Docker Hub'a image build & push (sadece master push)
```

### Gerekli GitHub Secrets

| Secret | Açıklama |
|---|---|
| `SONAR_PROJECT_KEY` | SonarQube proje anahtarı |
| `SONAR_HOST_URL` | SonarQube sunucu adresi |
| `SONAR_TOKEN` | SonarQube authentication token |
| `DOCKER_USERNAME` | Docker Hub kullanıcı adı |
| `DOCKER_PASSWORD` | Docker Hub şifresi |

---

## 🗂️ Proje Yapısı

```
SatisTalepYonetimi/
├── .github/workflows/
│   └── ci-cd.yml                          → CI/CD pipeline
├── grafana/provisioning/
│   ├── dashboards/
│   │   ├── dashboard.yml
│   │   └── json/aspnetcore-dashboard.json
│   └── datasources/datasource.yml
├── k6/
│   ├── load-test.js                       → Standart yük testi
│   ├── stress-test.js                     → Stres testi
│   └── spike-test.js                      → Spike testi
├── prometheus/
│   └── prometheus.yml
├── SatisTalepYonetimi/
│   ├── SatisTalepYonetimi.Domain/
│   │   ├── Entities/                      → Customer, Product, SalesRequest, SalesRequestItem,
│   │   │                                    Supplier, PurchaseQuote, MaintenanceCard,
│   │   │                                    MaintenanceTicket, AppUser
│   │   ├── Enums/                         → SalesRequestStatusEnum, MaintenanceStatusEnum
│   │   └── Repositories/                  → ICustomerRepository, IProductRepository,
│   │                                        ISalesRequestRepository, ISalesRequestItemRepository,
│   │                                        ISupplierRepository, IPurchaseQuoteRepository,
│   │                                        IMaintenanceCardRepository, IMaintenanceTicketRepository
│   ├── SatisTalepYonetimi.Application/
│   │   ├── Features/
│   │   │   ├── Customers/                 → Create, Update, Delete, GetAll
│   │   │   ├── Products/                  → Create, Update, Delete, GetAll
│   │   │   ├── SalesRequests/             → Create, Delete, UpdateStatus, GetAll, GetById
│   │   │   ├── Suppliers/                 → Create, Delete, GetAll
│   │   │   ├── PurchaseQuotes/            → Create, ApproveQuote, SubmitForApproval, GetBySalesRequest
│   │   │   ├── MaintenanceCards/          → Create, Delete, GetAll
│   │   │   └── MaintenanceTickets/        → UpdateStatus, GetAll
│   │   ├── Mapping/MappingProfile.cs
│   │   ├── Services/IMaintenanceCheckService.cs
│   │   └── DependencyInjection.cs
│   ├── SatisTalepYonetimi.Infrastructure/
│   │   ├── Context/ApplicationDbContext.cs
│   │   ├── Configurations/               → Entity type configurations
│   │   ├── Repositories/                 → Repository implementasyonları
│   │   └── Migrations/
│   └── SatisTalepYonetimi.WebAPI/
│       ├── Controllers/                   → Customers, Products, SalesRequests, Suppliers,
│       │                                    PurchaseQuotes, MaintenanceCards, MaintenanceTickets
│       ├── Middlewares/                   → ExceptionHandler, ExtensionsMiddleware
│       ├── Program.cs
│       ├── appsettings.json
│       └── appsettings.Docker.json
├── Test/
│   ├── Domain/                            → Enum testleri
│   └── Features/                          → Command/Query handler testleri
├── coverlet.runsettings                   → Test coverage ayarları
├── sonar-project.properties               → SonarQube yapılandırması
├── Dockerfile
├── docker-compose.yml
├── entrypoint.sh
└── .dockerignore
```

---

## 📊 Gözlemlenebilirlik

- **Tracing**: OpenTelemetry → OTLP Exporter ile dağıtık izleme
- **Metrics**: Prometheus exporter → `/metrics` endpoint'i
- **Logging**: Serilog ile konsol ve dosya bazlı loglama
- **Dashboard**: Grafana üzerinde hazır ASP.NET Core dashboard'u

---

## 🔥 k6 Yük Testleri

Proje, [k6](https://k6.io/) ile yazılmış 3 farklı yük testi senaryosu içermektedir:

```
k6/
├── load-test.js    → Standart yük testi (10→50 kullanıcı, tüm endpoint'ler)
├── stress-test.js  → Stres testi (100→200→300 kullanıcı, dayanıklılık)
└── spike-test.js   → Spike testi (ani 500 kullanıcı, toparlanma)
```

### Kurulum

```bash
# k6 kurulumu (Windows - Chocolatey)
choco install k6

# veya Docker ile
docker pull grafana/k6
```

### Testleri Çalıştırma

```bash
# Standart yük testi
k6 run k6/load-test.js

# Stres testi
k6 run k6/stress-test.js

# Spike testi
k6 run k6/spike-test.js

# Özel URL ile çalıştırma
k6 run -e BASE_URL=http://localhost:5000 k6/load-test.js

# JWT token ile çalıştırma
k6 run -e BASE_URL=http://localhost:5000 -e AUTH_TOKEN=your_jwt_token k6/load-test.js
```

### Test Senaryoları

| Senaryo | Kullanıcı | Süre | Amaç |
|---|---|---|---|
| **Load Test** | 10 → 50 | ~8 dk | Normal yük altında API performansı |
| **Stress Test** | 100 → 300 | ~26 dk | Sistemin sınırlarını belirleme |
| **Spike Test** | 100 → 500 | ~7 dk | Ani trafik artışına dayanıklılık |

### Eşik Değerler (Thresholds)

| Metrik | Load Test | Stress Test | Spike Test |
|---|---|---|---|
| **p(95) Response Time** | < 500ms | < 1000ms | < 2000ms |
| **Hata Oranı** | < %5 | < %10 | < %15 |

### Grafana ile k6 Metrikleri

k6 sonuçlarını Prometheus'a göndermek için:

```bash
K6_PROMETHEUS_RW_SERVER_URL=http://localhost:9090/api/v1/write k6 run --out experimental-prometheus-rw k6/load-test.js
```

---

## 🏥 Health Check

Uygulama, ASP.NET Core Health Checks altyapısı ile sağlık durumu izleme endpoint'leri sunmaktadır:

| Endpoint | Açıklama |
|---|---|
| `GET /health` | Tüm kontrollerin detaylı JSON raporu |
| `GET /health/ready` | Veritabanı bağlantı durumu (Readiness) |
| `GET /health/live` | Uygulama çalışma durumu (Liveness) |

### Örnek `/health` Yanıtı

```json
{
  "status": "Healthy",
  "checks": [
    {
      "name": "sqlserver",
      "status": "Healthy",
      "description": null,
      "duration": "45.23ms"
    },
    {
      "name": "self",
      "status": "Healthy",
      "description": null,
      "duration": "0.12ms"
    }
  ],
  "totalDuration": "46.01ms"
}
```

### Kontroller

| Kontrol | Tag | Açıklama |
|---|---|---|
| **SQL Server** | `db`, `sql` | Veritabanı bağlantısını doğrular |
| **Self** | `self` | Uygulamanın ayakta olduğunu doğrular |

> Kubernetes veya Docker ortamlarında `liveness` ve `readiness` probe olarak kullanılabilir.

---

## 🚦 Rate Limiting

API, ASP.NET Core'un built-in Rate Limiting middleware'i ile istek sınırlandırma desteği sunmaktadır. Aşırı istek durumunda `429 Too Many Requests` yanıtı döner.

### Politikalar

| Politika | Tür | Limit | Pencere | Kuyruk |
|---|---|---|---|---|
| **fixed** | Fixed Window | 100 istek | 1 dakika | 10 |
| **sliding** | Sliding Window | 60 istek | 1 dakika (6 segment) | 5 |
| **Global** | IP bazlı Fixed Window | 200 istek | 1 dakika | - |

### Kullanım

Global limiter tüm endpoint'lere otomatik olarak uygulanır. Belirli bir controller veya action'a özel politika atamak için:

```csharp
[EnableRateLimiting("fixed")]
[HttpGet]
public async Task<IActionResult> GetAll() { ... }

[DisableRateLimiting]
[HttpGet("health")]
public IActionResult Health() => Ok();
```

### Yanıt Başlıkları

| Başlık | Açıklama |
|---|---|
| `Retry-After` | Yeniden istek göndermek için bekleme süresi |
| `X-RateLimit-Limit` | Toplam izin verilen istek sayısı |
| `X-RateLimit-Remaining` | Kalan istek sayısı |

---

## 📄 Lisans

Bu proje MIT lisansı ile lisanslanmıştır.

---

## 🗺️ Yol Haritası (Roadmap)

Projenin gelecek sürümlerinde planlanmakta olan geliştirmeler:

| Özellik | Açıklama | Durum |
|---|---|---|
| **Event-Driven Architecture** | RabbitMQ / Kafka ile asenkron olay tabanlı iletişim. Satış talebi onaylandığında otomatik bildirim, tedarikçi bilgilendirme vb. | 🔜 Planlandı |
| **Distributed Transactions / Saga Pattern** | Mikroservis mimarisine geçişte dağıtık işlem tutarlılığı için Saga (Orchestration / Choreography) pattern uygulaması | 🔜 Planlandı |
| **Multi-Tenant Yapı** | Tek uygulama üzerinden birden fazla şirkete (tenant) hizmet verebilen çoklu kiracı mimarisi (DB-per-tenant / Schema-per-tenant) | 🔜 Planlandı |
| **Distributed Caching (Redis)** | Redis ile dağıtık önbellekleme. Sık sorgulanan veriler (ürün listesi, müşteri listesi) için performans artışı | 🔜 Planlandı |
| **API Gateway** | Ocelot / YARP ile API Gateway katmanı. Merkezi routing, load balancing, rate limiting ve authentication | 🔜 Planlandı |
| **Advanced Security (OAuth2 / OpenIddict)** | OpenIddict veya IdentityServer ile OAuth2 / OpenID Connect desteği. Authorization Code, Client Credentials flow'ları | 🔜 Planlandı |

### Detaylar

#### 🐰 Event-Driven Architecture (RabbitMQ / Kafka)
- Satış talebi durumu değiştiğinde event publish
- Tedarikçilere otomatik e-posta / bildirim
- PurchaseQuote onaylandığında stok güncelleme event'i
- Dead letter queue ile hata yönetimi
- Outbox pattern ile event güvenilirliği

#### 🔄 Saga Pattern
- Satış talebi → Teklif toplama → Onay → Satınalma akışı için orchestration-based saga
- Compensating transaction'lar ile geri alma desteği
- Saga state machine ile durum takibi

#### 🏢 Multi-Tenant
- Tenant bazlı veri izolasyonu
- Subdomain / header bazlı tenant çözümleme
- Tenant-specific konfigürasyon ve özelleştirme

#### ⚡ Redis Caching
- `IDistributedCache` ile entegrasyon
- Cache invalidation stratejileri
- Sık erişilen endpoint'lerde response caching

#### 🌐 API Gateway
- Merkezi authentication ve authorization
- Rate limiting politikalarının gateway seviyesinde yönetimi
- Request/response transformation
- Circuit breaker pattern

#### 🔐 Advanced Security
- OAuth2 Authorization Code flow (SPA / mobil uygulama)
- Client Credentials flow (servisler arası iletişim)
- Refresh token desteği
- Scope bazlı yetkilendirme
