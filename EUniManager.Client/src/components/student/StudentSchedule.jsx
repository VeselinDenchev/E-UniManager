import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  Container, 
  Box,
  Typography,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper 
} from '@mui/material';
import { UserContext } from '../../contexts/UserContext';
import { getSchedule } from '../../services/scheduleService';

export default function StudentSchedule({ title, type }) {
  const [semester, setSemester] = useState('Зимен');
  const [schedule, setSchedule] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    const semesterType = semester == 'Зимен' ? 0 : 1;
    getSchedule(type, semesterType, bearerToken)
      .then(data => setSchedule(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [semester, bearerToken, navigate]);

  const handleSemesterChange = (event) => {
    setSemester(event.target.value);
  };

  return (
    <Container sx={{ mt: 4 }}>
      <Box sx={{ mb: 6 }}>
        <Typography variant="h4" gutterBottom>{title}</Typography>
      </Box>
      <FormControl sx={{ mr: 2, minWidth: 150 }}>
      <InputLabel id="semester-label">Семестър</InputLabel>
      <Select
        labelId="semester-label"
        value={semester}
        onChange={handleSemesterChange}
        label="Семестър"
        sx={{ mb: 4 }}
      >
        <MenuItem value="Зимен">Зимен</MenuItem>
        <MenuItem value="Летен">Летен</MenuItem>
      </Select>
      </FormControl>
        <TableContainer component={Paper} elevation={6} sx={{ width: '100%' }}>
          <Table sx={{ width: '100%', tableLayout: 'fixed', borderCollapse: 'collapse' }}>
            <TableHead>
              <TableRow sx={{ backgroundColor: '#1976d2' }}>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '11%' }}>Ден</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '11%' }}>Час</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '8%'  }}>Седмица</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Група</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Зала</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '8%' }}>Сграда</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '10%' }}>Вид</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '17%' }}>Дисциплина</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '23%' }}>Преподавател</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {schedule.map(scheduleUnit => (
                <TableRow key={scheduleUnit.id} sx={{ backgroundColor: schedule.indexOf(scheduleUnit) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{scheduleUnit.day}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{scheduleUnit.timespan}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{scheduleUnit.week}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{scheduleUnit.group}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{scheduleUnit.roomNumber}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{scheduleUnit.schedulePlace}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{scheduleUnit.activityType}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd', width: '30%' }}>{scheduleUnit.activitySubjectCourseName}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd', width: '20%' }}>{scheduleUnit.activityTeacherFullNameWithRank}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
    </Container>
  );
};