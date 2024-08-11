import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/assignments`;

export async function getAssignmentByIdWithSolution(id, bearerToken) {
    const response = await fetch(`${baseUrl}/${id}/with-solution`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const assignment =  await handleHttpResponse(response);

    return assignment;
}

export async function getAllAssignmentsForStudent(bearerToken) {
    const response = await fetch(`${baseUrl}/students`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const assignments = await handleHttpResponse(response);

    return assignments;
}

export async function getAllAssignmentsForTeacher(bearerToken) {
    const response = await fetch(`${baseUrl}/teachers`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const assignments = await handleHttpResponse(response);

    return assignments;
}

export async function createAssignment(assignment, bearerToken) {
    const response = await fetch(baseUrl, {
        method: HttpMethod.POST,
        headers: getDefaultHeaders(bearerToken),
        body: JSON.stringify(assignment)
    });

    const result = await handleHttpResponse(response);

    return result;
}

export async function updateAssignment(id, assignment, bearerToken) {
    const response = await fetch(`${baseUrl}/${id}`, {
        method: HttpMethod.PUT,
        headers: getDefaultHeaders(bearerToken),
        body: JSON.stringify(assignment)
    });

    const result = await handleHttpResponse(response);

    return result;
}