import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/subjects`;

export async function getStudentSubjects(bearerToken) {
    const response = await fetch(`${baseUrl}/students`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await handleHttpResponse(response);

    return result;
}