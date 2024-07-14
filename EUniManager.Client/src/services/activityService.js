import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/activities`;

export async function getStudentActivities(bearerToken) {
    const response = await fetch(`${baseUrl}/students`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const studentActivities = await handleHttpResponse(response);

    return studentActivities;
}

export async function getTeacherActivities(bearerToken) {
    const response = await fetch(`${baseUrl}/teachers`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const teacherActivities = await handleHttpResponse(response);

    return teacherActivities;
}

export async function toggleActivity(id, bearerToken) {
    const response = await fetch(`${baseUrl}/${id}/toggle`, {
        method: HttpMethod.PATCH,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await handleHttpResponse(response);

    return result;
}