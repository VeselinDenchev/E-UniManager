import React, { useState, useContext, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { UserContext } from '../../../../contexts/UserContext';
import { getStudentActivities } from '../../../../services/activityService';
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
    IconButton
} from '@mui/material';
import EnterButton from '../../../common/buttons/EnterButton';

export default function StudentActivitiesList() {
  const [activities, setActivities] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    getStudentActivities(bearerToken)
      .then(data => setActivities(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [bearerToken, navigate]);

  return (
    <Container sx={{ mt: 10 }}>
      <Box sx={{ mb: 6 }}>
        <Typography variant="h4" gutterBottom>Учебни ресурси</Typography>
      </Box>
      <TableContainer component={Paper} elevation={6}>
        <Table sx={{ minWidth: 650 }}>
          <TableHead>
            <TableRow sx={{ backgroundColor: '#1976d2' }}>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Създадена на</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Семестър</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дисциплина</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Вид заетост</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Преподавател</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Статус</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Вход</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {activities.map(activity => (
              <TableRow key={activity.id} sx={{ backgroundColor: activities.indexOf(activity) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.createdAt}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.semester}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.subjectCourseName}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.activityType}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.teacherFullNameWithRank}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{activity.isStopped ? 'Спрян' : 'Активен'}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                  {!activity.isStopped && (
                    <EnterButton to={`/students/activities/${activity.id}/resources`} state = {{ activity }} />
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
}