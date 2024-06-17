import React from 'react';
import StudentSchedule from '../../../student/StudentSchedule';

export default function FilteredStudentSchedule() {
  return <StudentSchedule 
            title={'График на занятията според профила'} 
            type={'students'}
          />
}