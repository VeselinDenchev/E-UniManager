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
import { getStudentIndividualProtocols } from '../../../../services/individualProtocolService';

export default function StudentIndividualProtocolsList() {
  const [individualProtocols, setIndividualProtocols] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    getStudentIndividualProtocols(bearerToken)
      .then(data => setIndividualProtocols(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [bearerToken, navigate]);

  return (
    <Container sx={{ mt: 0 }}>
      <Box>
        <Typography variant="h4" gutterBottom>Индивидуални протоколи</Typography>
        <TableContainer component={Paper} elevation={6}>
          <Table sx={{ minWidth: 650, borderCollapse: 'collapse' }}>
            <TableHead>
              <TableRow sx={{ backgroundColor: '#1976d2' }}>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дисциплина</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Статус</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Създаден на</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {individualProtocols.map(protocol => (
                <TableRow key={protocol.id} sx={{ backgroundColor: individualProtocols.indexOf(protocol) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{protocol.subjectCourseName}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{protocol.status}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{protocol.createdAt}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Container>
  );
}