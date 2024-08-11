import React from 'react';
import { useBackToHome } from '../../../hooks/useBackToHome';
import { Button, Container, Typography, Box } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import ReportProblemIcon from '@mui/icons-material/ReportProblem';

export default function Error() {
    const handleBackToHome = useBackToHome();

    return (
        <Container maxWidth="sm" style={{ textAlign: 'center', marginTop: '50px' }}>
        <Box sx={{ marginBottom: 4 }}>
            <ReportProblemIcon style={{ fontSize: 100, color: 'grey' }} />
        </Box>
        <Typography variant="h4" component="h1" gutterBottom>
            Нещо се обърка
        </Typography>
        <Typography variant="body1" gutterBottom>
            Възникна неочаквана грешка. Моля, опитайте отново по-късно.
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
