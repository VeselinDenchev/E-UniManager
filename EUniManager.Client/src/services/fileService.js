import { apiRoute } from '../utils/baseRoutes.js';
import { HttpMethod } from '../utils/httpMethods.js';
import { getDefaultHeaders } from '../utils/headersUtils.js';

const baseUrl = `${apiRoute}/files`;

export async function download(id, bearerToken) {
    const response = await fetch(`${baseUrl}/download/${id}`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    if (!response.ok) {
        throw new Error('Network response was not ok');
    }

    const blob = await response.blob();
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = id;
    document.body.appendChild(a);
    a.click();
    a.remove();
    window.URL.revokeObjectURL(url);
}