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

## 📄 Lisans

Bu proje MIT lisansı ile lisanslanmıştır.
