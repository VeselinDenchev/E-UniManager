// navigationUtils.js
import { useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import { RoleContext } from '../contexts/RoleContext';
import { UserRoles } from '../utils/userRoles';

export const useBackToHome = () => {
    const navigate = useNavigate();
    const { userRole } = useContext(RoleContext);

    const handleBackToHome = () => {
        switch(userRole) {
            case UserRoles.ADMIN:
                navigate('/admin/home');
                break;
            case UserRoles.STUDENT:
                navigate('/students/home');
                break;
            case UserRoles.TEACHER:
                navigate('/teachers/home');
                break;
            default:
                navigate('/error');
                break;
        }
    };

    return handleBackToHome;
};
