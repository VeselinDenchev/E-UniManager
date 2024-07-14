import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/exams`;

export async function getExamsSchedule(bearerToken) {
    const response = await fetch(`${baseUrl}/students`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const examsSchedule = await handleHttpResponse(response);

    return examsSchedule;
}