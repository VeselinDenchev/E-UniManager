import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/students`;

export async function getHeaderData(bearerToken) {
    const response = await fetch(`${baseUrl}/header`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await handleHttpResponse(response);

    return result;
}

export async function getDetails(bearerToken) {
    const response = await fetch(`${baseUrl}/details`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await handleHttpResponse(response);

    return result;
}