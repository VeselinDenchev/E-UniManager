export const HttpMethod = Object.freeze({
    GET: 'GET',
    POST: 'POST',
    PUT: 'PUT',
    PATCH: 'PATCH',
    DELETE: 'DELETE'
});

export const HttpResponseStatusCode = Object.freeze({
    OK: 200,
    CREATED: 201,
    ACCEPTED: 202,
    NO_CONTENT: 204,
    BAD_REQUEST: 400,
    UNAUTHORIZED: 401,
    FORBIDDEN: 403,
    NOT_FOUND: 404,
    METHOD_NOT_ALLOWED: 405,
    INTERNAL_SERVER_ERROR: 500
});

export const getDefaultHeaders = (bearerToken) => {
    return {
        'Content-Type': 'application/json',
        "Authorization": `${bearerToken}`
    };
}

export const handleHttpResponse = async (httpResponse, isLogin) => {
    switch (httpResponse.status) {
        case HttpResponseStatusCode.OK:
          return await httpResponse.json();
        case HttpResponseStatusCode.CREATED:
        case HttpResponseStatusCode.ACCEPTED:
        case HttpResponseStatusCode.NO_CONTENT:
          return null;
        case HttpResponseStatusCode.UNAUTHORIZED && !isLogin:
          window.location.href = '/login';
          return null;
        case HttpResponseStatusCode.BAD_REQUEST:
        case HttpResponseStatusCode.FORBIDDEN:
        case HttpResponseStatusCode.NOT_FOUND:
        case HttpResponseStatusCode.METHOD_NOT_ALLOWED:
        case HttpResponseStatusCode.INTERNAL_SERVER_ERROR:
          throw new Error(httpResponse.statusText);
        default:
          return 'Unknown Status Code';
      }
}