import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Container, Typography, Box } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import LockIcon from '@mui/icons-material/Lock';
import { RoleContext } from '../../../contexts/RoleContext';
import { UserRoles } from '../../../utils/userRoles';

export default function Unauthorized() {
  const { userRole } = useContext(RoleContext);
  const navigate = useNavigate();

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

  return (
    <Container maxWidth="sm" style={{ textAlign: 'center', marginTop: '50px' }}>
      <Box sx={{ marginBottom: 4 }}>
        <LockIcon style={{ fontSize: 100, color: 'grey' }} />
      </Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Неоторизиран достъп
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