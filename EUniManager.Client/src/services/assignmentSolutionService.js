import { apiRoute } from '../utils/baseRoutes.js';
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';

const baseUrl = `${apiRoute}/assignment-solutions`;

export async function getAllSolutionsToAssignment(assignmentId, bearerToken) {
  const response = await fetch(`${baseUrl}/assignments/${assignmentId}`, {
    method: HttpMethod.GET,
    headers: getDefaultHeaders(bearerToken),
  });

  const result = await handleHttpResponse(response);

  return result;
}

export async function submitAssignmentSolution(id, assignmentSolution, bearerToken) {
  const response = await fetch(`${baseUrl}/${id}`, {
    method: HttpMethod.PUT,
    headers: getDefaultHeaders(bearerToken),
    body: JSON.stringify(assignmentSolution)
  });

  const result = await handleHttpResponse(response);

  return result;
}

export async function updateMark(id, mark, bearerToken) {
  const response = await fetch(`${baseUrl}/${id}/mark/${mark}`, {
      method: HttpMethod.PATCH,
      headers: getDefaultHeaders(bearerToken)
  });

  const result = await handleHttpResponse(response);

  return result;
}

export async function updateComment(id, comment, bearerToken) {
  const response = await fetch(`${baseUrl}/${id}/comment`, {
      method: HttpMethod.PATCH,
      headers: getDefaultHeaders(bearerToken),
      body: JSON.stringify(comment)
  });

  const result = await handleHttpResponse(response);

  return result;
}