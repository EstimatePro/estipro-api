import http from 'k6/http';
import {check} from 'k6';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    // vus: 1000,
    // duration: '10s',
    stages: [
        { duration: '10m', target: 200 }, // traffic ramp-up from 1 to a higher 200 users over 10 minutes.
        { duration: '30m', target: 200 }, // stay at higher 200 users for 30 minutes
        { duration: '5m', target: 0 }, // ramp-down to 0 users
    ]
};

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

    let response = http.post('https://localhost:5001/api/Rooms', payload, {headers: headers});

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}