import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/resources`;

export async function getActivityResources(activityId, bearerToken) {
    const response = await fetch(`${baseUrl}/activities/${activityId}`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const resources = await handleHttpResponse(response); 

    return resources;
}

export async function createResource(resource, bearerToken) {
    const response = await fetch(baseUrl, {
        method: HttpMethod.POST,
        headers: getDefaultHeaders(bearerToken),
        body: JSON.stringify(resource)
    });

    const result = await handleHttpResponse(response);

    return result;
}

export async function updateResource(id, resource, bearerToken) {
    const response = await fetch(`${baseUrl}/${id}`, {
        method: HttpMethod.PUT,
        headers: getDefaultHeaders(bearerToken),
        body: JSON.stringify(resource)
    });

    const result = await handleHttpResponse(response);

    return result;
}

export async function deleteResource(id, resource, bearerToken) {
    const response = await fetch(`${baseUrl}/${id}`, {
        method: HttpMethod.DELETE,
        headers: getDefaultHeaders(bearerToken),
        body: JSON.stringify(resource)
    });

    const result = await handleHttpResponse(response);

    return result;
}