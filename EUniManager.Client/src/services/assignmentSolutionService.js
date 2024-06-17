import { apiRoute } from '../utils/baseRoutes.js';
import { HttpMethod } from '../utils/httpMethods.js';
import { getDefaultHeaders } from '../utils/headersUtils.js';

const baseUrl = `${apiRoute}/assignment-solutions`;

export async function uploadAssignmentSolution(id, data, bearerToken) {
  const response = await fetch(`${baseUrl}/${id}`, {
    method: HttpMethod.PUT,
    headers: getDefaultHeaders(bearerToken),
    body: JSON.stringify(data)
  });

  if (!response.ok) {
    throw new Error('Failed to upload the assignment solution');
  }

  // Check if the response status is 204 No Content
  if (response.status === 204) {
    return null; // or any appropriate value indicating success
  }

  return await response.json();
}