import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod } from '../utils/httpMethods.js';
import { getDefaultHeaders } from '../utils/headersUtils.js';
 
const baseUrl = `${apiRoute}/payed-taxes`;

export async function getStudentPayedTaxes(bearerToken) {
    const response = await fetch(`${baseUrl}/students`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await response.json(); 

    return result;
}