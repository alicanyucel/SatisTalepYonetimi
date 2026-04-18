import http from 'k6/http';
import { check, sleep } from 'k6';

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5000';

export const options = {
    stages: [
        { duration: '10s', target: 100 },   // Ani spike: 10 saniyede 100 kullanıcı
        { duration: '1m', target: 100 },     // 1 dakika sabit
        { duration: '10s', target: 500 },    // Ani spike: 10 saniyede 500 kullanıcı
        { duration: '3m', target: 500 },     // 3 dakika sabit
        { duration: '10s', target: 100 },    // Geri düşüş
        { duration: '3m', target: 100 },     // Toparlanma süresi
        { duration: '10s', target: 0 },      // Ramp-down
    ],
    thresholds: {
        http_req_duration: ['p(95)<2000'],
        http_req_failed: ['rate<0.15'],
    },
};

export default function () {
    const endpoints = [
        '/api/Customers/GetAll',
        '/api/Products/GetAll',
        '/api/SalesRequests/GetAll',
        '/api/Suppliers/GetAll',
    ];

    const endpoint = endpoints[Math.floor(Math.random() * endpoints.length)];
    const res = http.get(`${BASE_URL}${endpoint}`);

    check(res, {
        'status is 200': (r) => r.status === 200,
    });

    sleep(0.5);
}
