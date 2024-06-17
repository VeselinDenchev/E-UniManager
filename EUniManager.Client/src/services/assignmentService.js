import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod } from '../utils/httpMethods.js';
import { getDefaultHeaders } from '../utils/headersUtils.js';
 
const baseUrl = `${apiRoute}/assignments`;

export async function getAssignmentByIdWithSolution(id, bearerToken) {
    const response = await fetch(`${baseUrl}/${id}/with-solution`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const assignment =  await response.json();

    console.log(JSON.stringify(assignment));

    return assignment;
}

export async function getAllAssignmentsForStudent(bearerToken) {
    const response = await fetch(`${baseUrl}/students`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const assignments =  await response.json();

    return assignments;
}

export async function getAllAssignmentsForTeacher(bearerToken) {
    const response = await fetch(`${baseUrl}/teachers`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const assignments =  await response.json();

    return assignments;
}