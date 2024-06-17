import { Routes, Route } from 'react-router-dom';
import './App.css'
import TeacherHome from './components/pages/teacher/home/TeacherHome';
import Login from './components/pages/login/Login'
import { UserProvider } from './contexts/UserContext'
import { StudentProvider } from './contexts/StudentContext';
import StudentLayout from './layouts/StudentLayout'

function App() {
  return (
    <>
      <UserProvider>
        <StudentProvider>
          <Routes>
            <Route path="/students/*" element={<StudentLayout />} />
            <Route exact path="/teachers" element={<TeacherHome />} />
            <Route exact path="/login" element={<Login />} />
          </Routes>
        </StudentProvider>
      </UserProvider>
    </>
  )
}

export default App