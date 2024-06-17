import { React, useState, useContext } from 'react';
import { useNavigate } from "react-router-dom";
import { Container, Box, Button, Typography, TextField } from '@mui/material';
import { login } from '../../../services/identityService';
import { UserContext } from '../../../contexts/UserContext';
import { StudentContext } from '../../../contexts/StudentContext';
import { containsOnlyDigits } from '../../../utils/regexUtil';
// import { ThemeContext } from '../../../contexts/ThemeContext';

export default function Login() {
    // const { isDarkMode, toggleTheme } = useContext(ThemeContext);
    const { userLogin } = useContext(UserContext);
    const { setIsStudent } = useContext(StudentContext);

    const navigate = useNavigate();

    const [loginData, setLoginData] = useState({
        email: '',
        password: ''
    });
    
    const inputChangeHandler = (event) => setLoginData({...loginData, [event.target.name]: event.target.value});

    const handleLogin = () => {
        login(loginData).then(authData => {
            userLogin(authData);

            if (containsOnlyDigits(loginData.email)) {
                setIsStudent(true);
                navigate('/students/home');
            }
            else {
                setIsStudent(false);
                navigate('/teachers');
            }

        })
        .catch(error => {
            console.log(error);
        });
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
            <Box component="form" noValidate autoComplete="off" sx={{ mt: 1, width: '100%' }}>
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
};