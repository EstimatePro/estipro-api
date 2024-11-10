import http from 'k6/http';
import {check, sleep} from 'k6';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '30s', target: 100 }, 
        { duration: '30', target: 500 }, 
        { duration: '30', target: 500 }, 
        { duration: '30s', target: 0 },
    ]
};

const API_BASE_URL = "https://localhost:5001";

export default () => {
    const token = "TOKEN_HERE";

    const headers = {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
    };

    let response = http.get(`${API_BASE_URL}/api/Rooms/84e988d8-d9e7-42ba-91fb-22e702edf079`, {headers: headers});

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
    
    sleep(1);
}