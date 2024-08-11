import React from 'react';
import { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { UserContext } from '../contexts/UserContext';
import { RoleContext } from '../contexts/RoleContext';
import { UserRoles } from '../utils/userRoles';
import StudentHome from '../components/pages/student/home/StudentHome';
import StudentNavigation from '../components/student/StudenNavigation';
import StudentPersonalData from '../components/pages/student/personal-data/StudentPersonalData';
import StudentSubjectsList from '../components/pages/student/subject/StudentSubjectsList';
import StudentRequestApplicationsList from '../components/pages/student/request-application/StudentRequestApplicationsList';
import StudentIndividualProtocolsList from '../components/pages/student/individual-protocol/StudentIndividualProtocolsList';
import StudentCertifiedSemestersList from '../components/pages/student/certified-semester/StudentCertifiedSemestersList'
import StudentPayedTaxesList from '../components/pages/student/payed-tax/StudentPayedTaxesList'
import SpecialtySchedule from '../components/pages/student/schedules/SpecialtySchedule';
import FilteredStudentSchedule from '../components/pages/student/schedules/FilteredStudentSchedule';
import StudentExamsSchedule from '../components/pages/student/exams-schedule/StudentExamsSchedule';
import StudentActivitiesList from '../components/pages/student/activity/StudentActivitiesList';
import StudentActivityResourcesList from '../components/pages/student/resource/StudentActivityResourcesList';
import StudentAssignment from '../components/pages/student/assignment/StudentAssignment';
import NotFound from '../components/pages/error/NotFound';

export default function StudentLayout () {
    const { isAuthenticated } = useContext(UserContext);
    const { userRole } = useContext(RoleContext);

    if (!isAuthenticated) {
        return <Navigate to='/login' />;
    }
    
    if (userRole !== UserRoles.STUDENT) {
      return <Navigate to='/forbidden' />
    }
    
    return (
      <>
        <StudentNavigation />
        <Routes>
          <Route path='/home' element={<StudentHome />} />
          <Route path='/personal-data' element={<StudentPersonalData />} />
          <Route path='/subjects' element={<StudentSubjectsList />} />
          <Route path='/request-applications' element={<StudentRequestApplicationsList />} />
          <Route path='/individual-protocols' element={<StudentIndividualProtocolsList />} />
          <Route path='/certified-semesters' element={<StudentCertifiedSemestersList />} />
          <Route path='/payed-taxes' element={<StudentPayedTaxesList />} />
          <Route path='/schedule/specialties' element={<SpecialtySchedule />} />
          <Route path='/schedule/filtered' element={<FilteredStudentSchedule />} />
          <Route path='/exams-schedule' element={<StudentExamsSchedule />} />
          <Route path='/activities' element={<StudentActivitiesList />} />
          <Route path='/activities/:activityId/resources' element={<StudentActivityResourcesList />} />
          <Route path='/assignments/:assignmentId' element={<StudentAssignment />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </>
  );
};