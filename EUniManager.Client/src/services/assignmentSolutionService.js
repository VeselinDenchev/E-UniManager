import { apiRoute } from '../utils/baseRoutes.js';
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';

const baseUrl = `${apiRoute}/assignment-solutions`;

export async function submitAssignmentSolution(id, data, bearerToken) {
  const response = await fetch(`${baseUrl}/${id}`, {
    method: HttpMethod.PUT,
    headers: getDefaultHeaders(bearerToken),
    body: JSON.stringify(data)
  });

  const result = await handleHttpResponse(response);

  return result;
}