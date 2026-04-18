import http from 'k6/http';
import { check, sleep } from 'k6';

const BASE_URL = __ENV.BASE_URL || 'http://localhost:5000';

export const options = {
    stages: [
        { duration: '2m', target: 100 },   // 2 dakikada 100 kullanıcıya çık
        { duration: '5m', target: 100 },    // 5 dakika boyunca 100 kullanıcı
        { duration: '2m', target: 200 },    // 2 dakikada 200 kullanıcıya çık
        { duration: '5m', target: 200 },    // 5 dakika boyunca 200 kullanıcı
        { duration: '2m', target: 300 },    // 2 dakikada 300 kullanıcıya çık
        { duration: '5m', target: 300 },    // 5 dakika boyunca 300 kullanıcı
        { duration: '5m', target: 0 },      // Ramp-down
    ],
    thresholds: {
        http_req_duration: ['p(95)<1000'],
        http_req_failed: ['rate<0.1'],
    },
};

export default function () {
    const res = http.get(`${BASE_URL}/api/Customers/GetAll`);
    check(res, {
        'status 200': (r) => r.status === 200,
        'response time < 1s': (r) => r.timings.duration < 1000,
    });
    sleep(1);
}
