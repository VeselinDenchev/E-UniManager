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
import { getStudentPayedTaxes } from '../../../../services/payedTaxService';

export default function StudentPayedTaxesList() {
  const [payedTaxes, setPayedTaxes] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();

  useEffect(() => {
    getStudentPayedTaxes(bearerToken)
      .then(data => setPayedTaxes(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [bearerToken, navigate]);

  return (
    <Container sx={{ mt: 0 }}>
      <Box>
        <Typography variant="h4" gutterBottom>Платени такси</Typography>
        <TableContainer component={Paper} elevation={6}>
          <Table sx={{ minWidth: 650, borderCollapse: 'collapse' }}>
            <TableHead>
              <TableRow sx={{ backgroundColor: '#1976d2' }}>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>№ такса</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>№ на документа</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дата на док.</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>За сем.</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>№ план</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Сума</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Валута</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Вид</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {payedTaxes.map(tax => (
                <TableRow key={tax.id} sx={{ backgroundColor: payedTaxes.indexOf(tax) % 2 === 0 ? '#e3f2fd' : '#bbdefb' }}>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{tax.taxNumber}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{tax.documentNumber}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{tax.documentDate}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{tax.semester}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{tax.planNumber}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{tax.amount}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>{tax.currency}</TableCell>
                  <TableCell align="center" sx={{ border: '1px solid #ddd' }}>Семестриална</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </Container>
  );
}
