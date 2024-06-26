import React, { createContext } from 'react';
import { useLocalStorage } from '../hooks/useLocalStorage';

export const RoleContext = createContext();

export const RoleProvider = ({ 
    children 
}) => {
    const [userRole, setUserRole] = useLocalStorage('userRole', {});

    return (
        <RoleContext.Provider value={{
            userRole,
            setUserRole
        }}>
            {children}
        </RoleContext.Provider>
    );
};