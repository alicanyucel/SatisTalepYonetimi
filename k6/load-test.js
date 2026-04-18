import http from 'k6/http';
import { check, sleep, group } from 'k6';
import { Rate, Trend } from 'k6/metrics';

// Custom metrics
const errorRate = new Rate('errors');
const customerListTrend = new Trend('customer_list_duration');
const productListTrend = new Trend('product_list_duration');
const salesRequestListTrend = new Trend('sales_request_list_duration');

// Test configuration
const BASE_URL = __ENV.BASE_URL || 'http://localhost:5000';
const AUTH_TOKEN = __ENV.AUTH_TOKEN || '';

export const options = {
    stages: [
        // Ramp-up: 1 dakikada 10 kullanıcıya çık
        { duration: '1m', target: 10 },
        // Sabit yük: 3 dakika boyunca 10 kullanıcı
        { duration: '3m', target: 10 },
        // Spike: 1 dakikada 50 kullanıcıya çık
        { duration: '1m', target: 50 },
        // Sabit yük: 2 dakika boyunca 50 kullanıcı
        { duration: '2m', target: 50 },
        // Ramp-down: 1 dakikada 0'a in
        { duration: '1m', target: 0 },
    ],
    thresholds: {
        http_req_duration: ['p(95)<500', 'p(99)<1000'],   // %95 < 500ms, %99 < 1s
        http_req_failed: ['rate<0.05'],                     // Hata oranı < %5
        errors: ['rate<0.1'],                               // Custom hata oranı < %10
    },
};

function getHeaders() {
    const headers = { 'Content-Type': 'application/json' };
    if (AUTH_TOKEN) {
        headers['Authorization'] = `Bearer ${AUTH_TOKEN}`;
    }
    return headers;
}

export default function () {
    const headers = getHeaders();

    // ── Müşteriler ──
    group('Customers', function () {
        // GET - Tüm müşteriler
        const listRes = http.get(`${BASE_URL}/api/Customers/GetAll`, { headers, tags: { name: 'GET_Customers' } });
        check(listRes, {
            'customers status 200': (r) => r.status === 200,
            'customers has data': (r) => r.json() !== null,
        }) || errorRate.add(1);
        customerListTrend.add(listRes.timings.duration);

        // POST - Müşteri oluştur
        const customerPayload = JSON.stringify({
            name: `K6 Test Müşteri ${Date.now()}`,
            email: `k6test${Date.now()}@test.com`,
            phone: '05551234567',
            address: 'Test Adres',
        });
        const createRes = http.post(`${BASE_URL}/api/Customers`, customerPayload, { headers, tags: { name: 'POST_Customer' } });
        check(createRes, {
            'customer create success': (r) => r.status === 200 || r.status === 201,
        }) || errorRate.add(1);
    });

    sleep(1);

    // ── Ürünler ──
    group('Products', function () {
        const listRes = http.get(`${BASE_URL}/api/Products/GetAll`, { headers, tags: { name: 'GET_Products' } });
        check(listRes, {
            'products status 200': (r) => r.status === 200,
        }) || errorRate.add(1);
        productListTrend.add(listRes.timings.duration);
    });

    sleep(1);

    // ── Satış Talepleri ──
    group('SalesRequests', function () {
        const listRes = http.get(`${BASE_URL}/api/SalesRequests/GetAll`, { headers, tags: { name: 'GET_SalesRequests' } });
        check(listRes, {
            'sales requests status 200': (r) => r.status === 200,
        }) || errorRate.add(1);
        salesRequestListTrend.add(listRes.timings.duration);
    });

    sleep(1);

    // ── Tedarikçiler ──
    group('Suppliers', function () {
        const listRes = http.get(`${BASE_URL}/api/Suppliers/GetAll`, { headers, tags: { name: 'GET_Suppliers' } });
        check(listRes, {
            'suppliers status 200': (r) => r.status === 200,
        }) || errorRate.add(1);
    });

    sleep(1);

    // ── Bakım Kartları ──
    group('MaintenanceCards', function () {
        const listRes = http.get(`${BASE_URL}/api/MaintenanceCards/GetAll`, { headers, tags: { name: 'GET_MaintenanceCards' } });
        check(listRes, {
            'maintenance cards status 200': (r) => r.status === 200,
        }) || errorRate.add(1);
    });

    sleep(1);

    // ── Bakım Biletleri ──
    group('MaintenanceTickets', function () {
        const listRes = http.get(`${BASE_URL}/api/MaintenanceTickets/GetAll`, { headers, tags: { name: 'GET_MaintenanceTickets' } });
        check(listRes, {
            'maintenance tickets status 200': (r) => r.status === 200,
        }) || errorRate.add(1);
    });

    sleep(1);
}
