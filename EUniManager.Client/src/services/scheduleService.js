import { apiRoute } from '../utils/baseRoutes.js'
import { HttpMethod, getDefaultHeaders, handleHttpResponse } from '../utils/httpUtils.js';
 
const baseUrl = `${apiRoute}/course-schedule-units`;

export async function getSchedule(scheduleType, semesterType, bearerToken) {
    const url = `${baseUrl}/schedule/${scheduleType}/semester-type/${semesterType}`;
    const response = await fetch(url, {
        method: HttpMethod.GET,
        headers: getDefaultHeaders(bearerToken)
    });

    const schedule = await handleHttpResponse(response);

    return schedule;
}