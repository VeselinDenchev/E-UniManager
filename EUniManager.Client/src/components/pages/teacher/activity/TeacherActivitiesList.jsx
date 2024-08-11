import React, { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../../../../contexts/UserContext';
import { getTeacherActivities, toggleActivity } from '../../../../services/activityService';
import EnterButton from '../../../common/buttons/EnterButton';
import {
  Container,
  Box,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Switch,
  Snackbar,
  Alert
} from '@mui/material';

export default function TeacherActivitiesList() {
  const [activities, setActivities] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');

  useEffect(() => {
    getTeacherActivities(bearerToken)
      .then(data => setActivities(data))
      .catch(error => {
        console.log(error);
        navigate('/login');
      });
  }, [bearerToken, navigate]);

  const handleStatusToggle = async (id, isStopped) => {
    try {
      await toggleActivity(id, bearerToken);
      const updatedActivities = await getTeacherActivities(bearerToken);
      setActivities(updatedActivities);
      
      const message = `Ресурсите за дисциплината бяха ${isStopped ? 'активирани' : 'деактивирани'} успешно`;
      setSnackbarMessage(message);
      setSnackbarSeverity('success');
      setSnackbarOpen(true);
    } 
    catch (error) {
      console.log('Error toggling activity status:', error);

      const message = `Възникна грешка при ${isStopped ? 'активирането' : 'деактивирането'} на ресурсите за дисциплината`;
      setSnackbarMessage(message);
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
      navigate('/login');
    }
  };

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  return (
    <Container sx={{ mt: 4 }}>
      <Box sx={{ mb: 6 }}>
        <Typography variant="h4" gutterBottom>Учебни ресурси</Typography>
      </Box>
      <TableContainer component={Paper} elevation={6}>
        <Table sx={{ minWidth: 650 }}>
          <TableHead>
            <TableRow sx={{ backgroundColor: '#1976d2' }}>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '20%' }}>Специалност</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '9%' }}>Форма на обучение</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '5%' }}>ОКС</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '20%' }}>Факултет</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '26%' }}>Дисциплина</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '5%' }}>Семестър</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '5%' }}>Вид занятие</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '5%' }}>Статус</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '5%' }}>Вход</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {activities.map(activity => (
              <TableRow key={activity.id} sx={{ backgroundColor: activities.indexOf(activity) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.specialtyName}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.educationType}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.educationalAndQualificationDegree}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.facultyName}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.courseName}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.semester}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.activityType}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                  <Switch
                    checked={!activity.isStopped}
                    onChange={() => handleStatusToggle(activity.id, activity.isStopped)}
                    color="primary"
                  />
                </TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                  {!activity.isStopped && (
                    <EnterButton to={`/teachers/activities/${activity.id}/resources`} state={{ activity }}  />
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleSnackbarClose}
      >
        <Alert onClose={handleSnackbarClose} severity={snackbarSeverity} sx={{ width: '100%' }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </Container>
  );
}