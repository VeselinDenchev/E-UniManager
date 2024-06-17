import { createContext } from 'react';
import { useLocalStorage } from '../hooks/useLocalStorage';

export const UserContext = createContext();

export const UserProvider = ({
    children,
}) => {
    const [auth, setAuth] = useLocalStorage('auth', {});

    const userLogin = (authData) => setAuth(authData);

    const userLogout = () => setAuth({});

    return (
        <UserContext.Provider value={{
            user: auth,
            userLogin,
            userLogout,
            bearerToken: `${auth.tokenType} ${auth.accessToken}`,
            isAuthenticated: !!auth.accessToken
        }}>
            {children}
        </UserContext.Provider>  
    );
};