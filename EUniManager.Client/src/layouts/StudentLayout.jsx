import React from 'react';
import { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { UserContext } from '../contexts/UserContext';
import { StudentContext } from '../contexts/StudentContext';
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
import ActivityResourcesList from '../components/pages/resources/ActivityResourcesList';
import Assignment from '../components/pages/student/assignment/Assignment';

export default function StudentLayout () {
    const { isAuthenticated } = useContext(UserContext);
    const { isStudent } = useContext(StudentContext);

    if (!isAuthenticated) {
        return <Navigate to='/login' />;
      }
    
    if (!isStudent) {
      return <Navigate to='/unauthorized' />
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
          <Route path='/activities/:activityId' element={<ActivityResourcesList />} />
          <Route path='/assignments/:assignmentId' element={<Assignment />} />
        </Routes>
      </>
  );
};