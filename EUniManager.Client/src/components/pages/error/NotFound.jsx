import React from 'react';
import { useBackToHome } from '../../../hooks/useBackToHome';
import { Button, Container, Typography, Box } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';

export default function NotFound() {
    const handleBackToHome = useBackToHome();

    return (
        <Container maxWidth="sm" style={{ textAlign: 'center', marginTop: '50px' }}>
        <Box sx={{ marginBottom: 4 }}>
            <ErrorOutlineIcon style={{ fontSize: 100, color: 'grey' }} />
        </Box>
        <Typography variant="h4" component="h1" gutterBottom>
            Страницата не е намерена
        </Typography>
        <Typography variant="body1" gutterBottom>
            Съжаляваме, но страницата, която търсите, не съществува.
        </Typography>
        <Box sx={{ marginTop: 4 }}>
            <Button
            variant="contained"
            color="primary"
            startIcon={<HomeIcon />}
            onClick={handleBackToHome}
            size="large"
            >
            Обратно към началната страница
            </Button>
        </Box>
        </Container>
    );
}