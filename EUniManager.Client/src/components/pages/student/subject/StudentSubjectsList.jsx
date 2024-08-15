import React, { useState, useEffect, useContext } from 'react';
import { getStudentSubjects } from '../../../../services/subjectService';
import { UserContext } from '../../../../contexts/UserContext';
import { useNavigate } from 'react-router-dom';
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
  Paper 
} from '@mui/material';

export default function StudentSubjectsList() {
  const [subjects, setSubjects] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    getStudentSubjects(bearerToken)
      .then(data => setSubjects(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [bearerToken, navigate]);

  return (
    <Container sx={{ mt: 0 }}>
      <Box sx={{ mb: 6 }}>
        <Typography variant="h4" gutterBottom>Резултати от изпитите</Typography>
      </Box>
      <Box>
        <TableContainer component={Paper} elevation={6}>
          <Table sx={{ borderCollapse: 'collapse' }}>
            <TableHead>
              <TableRow sx={{ backgroundColor: '#1976d2' }}>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Семестър</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дисциплина</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Лекции</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Упражнения</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Оценка</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Кредити</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Преподавател</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Протокол</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {subjects.map(subject => (
                <TableRow key={subject.id} sx={{ backgroundColor: subjects.indexOf(subject) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{subject.semester}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{subject.courseName}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{subject.lecturesCount}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{subject.exercisesCount}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                    {subject.markWithWords}
                    {subject.markNumeric && ` (${subject.markNumeric})`}
                  </TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{subject.creditsCount}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{subject.lecturerFullNameWithRank}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{subject.protocol}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Container>
  );
}