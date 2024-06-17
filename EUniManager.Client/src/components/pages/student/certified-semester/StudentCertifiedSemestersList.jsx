import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../../../../contexts/UserContext';
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
import { getStudentCertifiedSemesters } from '../../../../services/certifiedSemesterService';

export default function StudentSertifiedSemestersList() {
  const [certifiedSemesters, setCertifiedSemesters] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    getStudentCertifiedSemesters(bearerToken)
      .then(data => setCertifiedSemesters(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [bearerToken, navigate]);


  return (
    <Container sx={{ mt: 0 }}>
      <Box>
        <Typography variant="h4" gutterBottom>Заверени семестри</Typography>
        <TableContainer component={Paper} elevation={6}>
          <Table sx={{ minWidth: 650, borderCollapse: 'collapse' }}>
            <TableHead>
              <TableRow sx={{ backgroundColor: '#1976d2' }}>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>ПИН</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>СЕМЕСТЪР</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>ВИД</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>ДАТА</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {certifiedSemesters.map(semester => (
                <TableRow key={semester.id} sx={{ backgroundColor: certifiedSemesters.indexOf(semester) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{semester.studentPin}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{semester.semester}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{semester.educationType}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{semester.date}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Container>
  );
}
