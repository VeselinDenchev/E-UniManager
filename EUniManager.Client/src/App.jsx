import { Routes, Route, Navigate } from 'react-router-dom';
import './App.css'
import Login from './components/pages/login/Login'
import { UserProvider } from './contexts/UserContext'
import { RoleProvider } from './contexts/RoleContext';
import StudentLayout from './layouts/StudentLayout';
import TeacherLayout from './layouts/TeacherLayout';
import Forbidden from './components/pages/error/Forbidden';
import NotFound from './components/pages/error/NotFound';

function App() {
  return (
    <>
      <UserProvider>
        <RoleProvider>
          <Routes>
            <Route path='/' element={<Navigate to="/login" />} />
            <Route path='/students/*' element={<StudentLayout />}  />
            <Route path='/teachers/*' element={<TeacherLayout />} />
            <Route exact path='/login' element={<Login />} />
            <Route exact path='/forbidden' element={<Forbidden />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </RoleProvider>
      </UserProvider>
    </>
  )
}

export default App