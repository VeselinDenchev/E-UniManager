import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/identity`;

export async function login(userData) {
    const response = await fetch(`${baseUrl}/login`, {
        method: HttpMethod.POST,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });

    const result = await handleHttpResponse(response, true);

    return result;
}

export async function register(userData) {
    const response = await fetch(`${baseUrl}/register`, {
        method: HttpMethod.POST,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });

    const result = await handleHttpResponse(response);

    return result;
}