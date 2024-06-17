import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod } from '../utils/httpMethods.js';
import { getDefaultHeaders } from '../utils/headersUtils.js';
 
const baseUrl = `${apiRoute}/students`;

export async function getHeaderData(bearerToken) {
    const response = await fetch(`${baseUrl}/header`, {
        method: HttpMethod.GET,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': bearerToken
        }
    });

    const result = await response.json(); 

    return result;
}

export async function getDetails(bearerToken) {
    const response = await fetch(`${baseUrl}/details`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await response.json(); 

    return result;
}