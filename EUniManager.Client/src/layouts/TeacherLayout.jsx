import React from 'react';
import { useContext } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { UserContext } from '../contexts/UserContext';
import { RoleContext } from '../contexts/RoleContext';
import TeacherHome from '../components/pages/teacher/home/TeacherHome';
import TeacherNavigation from '../components/teacher/TeacherNavigation';
import TeacherSchedule from '../components/pages/teacher/schedule/TeacherSchedule';
import { UserRoles } from '../utils/userRoles';
import TeacherActivitiesList from '../components/pages/teacher/activity/TeacherActivitiesList';
import TeacherActivityResourcesList from '../components/pages/teacher/resource/TeacherActivityResourcesList';

export default function TeacherLayout () {
    const { isAuthenticated } = useContext(UserContext);
    const { userRole } = useContext(RoleContext);

    if (!isAuthenticated) {
        return <Navigate to='/login' />;
      }
    
    if (userRole !== UserRoles.TEACHER) {
      return <Navigate to='/unauthorized' />
    }
    
    return (
      <>
        <TeacherNavigation />
        <Routes>
          <Route path='/home' element={<TeacherHome />} />
          <Route path='/schedule' element={<TeacherSchedule />} />
          <Route path='/activities' element={<TeacherActivitiesList />} />
          <Route path='/activities/:activityId/resources' element={<TeacherActivityResourcesList />} />
        </Routes>
      </>
  );
};