import React from 'react';
import { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { UserContext } from '../contexts/UserContext';
import { RoleContext } from '../contexts/RoleContext';
import { UserRoles } from '../utils/userRoles';
import TeacherHome from '../components/pages/teacher/home/TeacherHome';
import TeacherNavigation from '../components/teacher/TeacherNavigation';
import TeacherSchedule from '../components/pages/teacher/schedule/TeacherSchedule';
import TeacherActivitiesList from '../components/pages/teacher/activity/TeacherActivitiesList';
import TeacherActivityResourcesList from '../components/pages/teacher/resource/TeacherActivityResourcesList';
import AssignmentSolutionsList from '../components/pages/teacher/assignment-solution/AssignmentSolutionsList';
import NotFound from '../components/pages/error/NotFound';

export default function TeacherLayout () {
    const { isAuthenticated } = useContext(UserContext);
    const { userRole } = useContext(RoleContext);

    if (!isAuthenticated) {
        return <Navigate to='/login' />;
      }
    
    if (userRole !== UserRoles.TEACHER) {
      return <Navigate to='/forbidden' />
    }
    
    return (
      <>
        <TeacherNavigation />
        <Routes>
          <Route path='/home' element={<TeacherHome />} />
          <Route path='/schedule' element={<TeacherSchedule />} />
          <Route path='/activities' element={<TeacherActivitiesList />} />
          <Route path='/activities/:activityId/resources' element={<TeacherActivityResourcesList />} />
          <Route path='/assignment-solutions/assignments/:assignmentId' element={<AssignmentSolutionsList />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </>
  );
};