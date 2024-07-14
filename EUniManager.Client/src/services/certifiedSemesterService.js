import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/certified-semesters`;

export async function getStudentCertifiedSemesters(bearerToken) {
    const response = await fetch(`${baseUrl}/students`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const certifiedSemesters = await handleHttpResponse(response);

    return certifiedSemesters;
}