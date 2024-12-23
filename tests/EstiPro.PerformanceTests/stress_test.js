import http from 'k6/http';
import {check, sleep} from 'k6';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '20s', target: 100 }, // traffic ramp-up from 1 to a higher 200 users over 10 minutes.
        { duration: '20s', target: 1000 }, // stay at higher 200 users for 30 minutes
        { duration: '20s', target: 0 }, // ramp-down to 0 users
    ],
    // thresholds: {
    //     http_req_duration: ["p(99) < 500"] // Assert that 99% of requests finish within 150ms.
    // }
};

const API_BASE_URL = "https://localhost:5001";

export default () => {
    const token = "TOKEN_HERE";

    const headers = {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
    };

    const room = {
        name: "TestRoom"
    }

    const payload = JSON.stringify(room);

    let response = http.post(`${API_BASE_URL}/api/Rooms`, payload, {headers: headers});

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
    
    sleep(1);
}