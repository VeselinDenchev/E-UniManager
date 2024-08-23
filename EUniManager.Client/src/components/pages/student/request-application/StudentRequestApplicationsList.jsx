import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserContext } from '../../../../contexts/UserContext';
import { getStudentRequestApplications } from '../../../../services/requestApplicationService';
import DownloadButton from '../../../common/buttons/DownloadButton';
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

export default function StudentRequestApplicationsList() {
  const [requestApplications, setRequestApplications] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    getStudentRequestApplications(bearerToken)
      .then(data => setRequestApplications(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [bearerToken, navigate]);

  return (
    <Container sx={{ mt: 0 }}>
      <Box sx={{ mb: 6 }}>
        <Typography variant="h4" gutterBottom>Молби и заявления</Typography>
      </Box>
      <Box>
        <TableContainer component={Paper} elevation={6}>
          <Table sx={{ minWidth: 650, borderCollapse: 'collapse' }}>
            <TableHead>
              <TableRow sx={{ backgroundColor: '#1976d2' }}>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Номер</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>ПИН</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дата на регистрация</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Вид</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дата на резолюция</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Изтегли</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {requestApplications.map(requestApplication => (
                <TableRow key={requestApplication.id} sx={{ backgroundColor: requestApplications.indexOf(requestApplication) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{requestApplication.number}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{requestApplication.studentPin}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{requestApplication.registryDate}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{requestApplication.requestApplicationType}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{requestApplication.resolutionDate}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                    <DownloadButton fileId={requestApplication.fileId} disabled={false} />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Container>
  );
}