import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate, useLocation } from "react-router-dom";
import { login } from '../../../services/identityService';
import { UserContext } from '../../../contexts/UserContext';
import { RoleContext } from '../../../contexts/RoleContext';
import { containsOnlyDigits } from '../../../utils/regexUtil';
import { UserRoles } from '../../../utils/userRoles';
import { Container, Box, Button, Typography, TextField, CircularProgress } from '@mui/material';

export default function Login() {
    const { userLogin, isAuthenticated  } = useContext(UserContext);
    const { userRole, setUserRole } = useContext(RoleContext);

    const navigate = useNavigate();
    const location = useLocation();

    // Extract the `from` path from the location state, if available
    const from = location.state?.from?.pathname;

    const [loginData, setLoginData] = useState({
        email: '',
        password: ''
    });

    const [loading, setLoading] = useState(true);

    const inputChangeHandler = (event) => setLoginData({...loginData, [event.target.name]: event.target.value});

    const handleLogin = () => {
        login(loginData).then(authData => {
            userLogin(authData);

            if (containsOnlyDigits(loginData.email)) {
                setUserRole(UserRoles.STUDENT);
                // Navigate to the previous page if it exists, otherwise to the student home
                navigate(from || '/students/home', { replace: true });
            } else {
                setUserRole(UserRoles.TEACHER);
                // Navigate to the previous page if it exists, otherwise to the teacher home
                navigate(from || '/teachers/home', { replace: true });
            }
        })
        .catch(error => {
            console.log(error);
        });
    };

    const handleKeyPress = (event) => {
        if (event.key === 'Enter') {
            handleLogin();
        }
    };

    useEffect(() => {
        if (isAuthenticated) {
            // Avoid triggering the effect unnecessarily by adding a condition
            setLoading(false); // Stop loading before navigating

            // Navigate to the previous page if it exists, otherwise to the appropriate home page
            if (from) {
                navigate(from, { replace: true });
            } 
            else {
                if (userRole === UserRoles.STUDENT) {
                    navigate('/students/home', { replace: true });
                } else if (userRole === UserRoles.TEACHER) {
                    navigate('/teachers/home', { replace: true });
                }
            }
        } else {
            setLoading(false); // Stop loading if user is not authenticated
        }
    }, [isAuthenticated, navigate, from]);

    if (loading) {
        return (
            <Container
                maxWidth="sm"
                sx={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    minHeight: '100vh',
                }}
            >
                <CircularProgress />
            </Container>
        );
    }

    return (
        <Container
          maxWidth="sm"
          sx={{
            backgroundColor: 'background.default',
            color: 'text.primary',
            minHeight: '50vh',
            height: '80vh',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            overflow: 'hidden',
          }}
        >
          <Box
            display="flex"
            flexDirection="column"
            justifyContent="center"
            alignItems="center"
            width="100%"
            sx={{
              p: 2,
              boxSizing: 'border-box',
            }}
          >
            <Typography variant="h4" component="h1" gutterBottom>
              Вход
            </Typography>
            <Box component="form" noValidate autoComplete="off" sx={{ mt: 1, width: '100%' }} onKeyPress={handleKeyPress}>
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                label="Потребителско име"
                name="email"
                autoComplete="username"
                autoFocus
                value={loginData.email}
                onChange={inputChangeHandler}
                InputLabelProps={{ style: { color: 'inherit' } }}
                InputProps={{ style: { color: 'inherit' } }}
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Парола"
                type="password"
                id="password"
                autoComplete="current-password"
                value={loginData.password}
                onChange={inputChangeHandler}
                InputLabelProps={{ style: { color: 'inherit' } }}
                InputProps={{ style: { color: 'inherit' } }}
              />
              <Button
                fullWidth
                variant="contained"
                color="primary"
                onClick={handleLogin}
                sx={{ mt: 3, mb: 2 }}
              >
                Вход
              </Button>
            </Box>
          </Box>
        </Container>
    );
}