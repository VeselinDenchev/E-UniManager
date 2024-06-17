import { useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../../../../contexts/UserContext';
import StudentHeader from '../../../student/StudenNavigation';

export default function TeacherHome() {
    const { user } = useContext(UserContext);

    const navigate = useNavigate();

    useEffect(() => {
        if (!user.email) {
            navigate('/login');
        }
    }, [])

    return <StudentHeader />
};