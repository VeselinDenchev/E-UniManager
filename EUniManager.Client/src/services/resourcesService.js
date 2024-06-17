import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod } from '../utils/httpMethods.js';
import { getDefaultHeaders } from '../utils/headersUtils.js';
 
const baseUrl = `${apiRoute}/resources`;

export async function getActivityResources(activityId, bearerToken) {
    const response = await fetch(`${baseUrl}/activities/${activityId}`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await response.json(); 

    return result;
}