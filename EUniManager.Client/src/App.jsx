import { Routes, Route } from 'react-router-dom';
import './App.css'
import Login from './components/pages/login/Login'
import { UserProvider } from './contexts/UserContext'
import { RoleProvider } from './contexts/RoleContext';
import StudentLayout from './layouts/StudentLayout';
import TeacherLayout from './layouts/TeacherLayout';
import Unauthorized from './components/pages/error/Unauthorized';

function App() {
  return (
    <>
      <UserProvider>
        <RoleProvider>
          <Routes>
            <Route path='/students/*' element={<StudentLayout />} />
            <Route path='/teachers/*' element={<TeacherLayout />} />
            <Route exact path='/login' element={<Login />} />
            <Route exact path='/unauthorized' element={<Unauthorized />} />
          </Routes>
        </RoleProvider>
      </UserProvider>
    </>
  )
}

export default App