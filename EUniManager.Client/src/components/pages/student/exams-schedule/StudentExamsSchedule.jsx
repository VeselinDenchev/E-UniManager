import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
    TableContainer,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    Paper,
    Table,
    Container,
    Box,
    Typography
} from '@mui/material';
import { UserContext } from '../../../../contexts/UserContext';
import { getExamsSchedule } from '../../../../services/examService.js';


export default function StudentExamsSchedule() {
  const [examSchedule, setExamSchedule] = useState([]);

  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    getExamsSchedule(bearerToken)
      .then(data => setExamSchedule(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [bearerToken, navigate]);

  return (
    <Container sx={{ mt: 4 }}>
      <Box sx={{ mb: 6 }}>
        <Typography variant="h4" gutterBottom>График на изпитите</Typography>
      </Box>
      <TableContainer component={Paper} elevation={6} sx={{ width: '100%' }}>
        <Table sx={{ width: '100%', tableLayout: 'fixed', borderCollapse: 'collapse' }}>
          <TableHead>
            <TableRow sx={{ backgroundColor: '#1976d2' }}>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '25%' }}>Дисциплина</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Вид</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '20%' }}>Преподавател</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd', width: '11%' }}>Дата</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Час</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Сграда</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Зала</TableCell>
              <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd'}}>Група</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {examSchedule.map(exam => (
              <TableRow key={exam.id} sx={{ backgroundColor: examSchedule.indexOf(exam) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.subjectCourseName}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.examType}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.examinerFullName}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.date}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.time}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.schedulePlace}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.roomNumber}</TableCell>
                <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{exam.groupNumber}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
}