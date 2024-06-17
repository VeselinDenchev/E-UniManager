export const getDefaultHeaders = (bearerToken) => {
    return {
        'Content-Type': 'application/json',
        "Authorization": `${bearerToken}`
    };
}