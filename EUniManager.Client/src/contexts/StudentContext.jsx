import React, { createContext } from 'react';
import { useLocalStorage } from '../hooks/useLocalStorage';

export const StudentContext = createContext();

export const StudentProvider = ({ 
    children 
}) => {
    const [isStudent, setIsStudent] = useLocalStorage('isStudent', false);

    return (
        <StudentContext.Provider value={{
            isStudent,
            setIsStudent
        }}>
            {children}
        </StudentContext.Provider>
    );
};