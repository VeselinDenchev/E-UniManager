import React from 'react';
import { useBackToHome } from '../../../hooks/useBackToHome';
import { Button, Container, Typography, Box } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import LockIcon from '@mui/icons-material/Lock';

export default function Forbidden() {
  const handleBackToHome = useBackToHome();

  return (
    <Container maxWidth="sm" style={{ textAlign: 'center', marginTop: '50px' }}>
      <Box sx={{ marginBottom: 4 }}>
        <LockIcon style={{ fontSize: 100, color: 'grey' }} />
      </Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Забранен достъп
      </Typography>
      <Typography variant="body1" gutterBottom>
        Нямате нужните права, за да достъпите тази страница.
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