import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod } from '../utils/httpMethods.js';
import { getDefaultHeaders } from '../utils/headersUtils.js';
 
const baseUrl = `${apiRoute}/course-schedule-units`;

export async function getSchedule(scheduleType, semesterType, bearerToken) {
    const response = await fetch(`${baseUrl}/schedule/${scheduleType}/semester-type/${semesterType}`, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const result = await response.json(); 

    return result;
}