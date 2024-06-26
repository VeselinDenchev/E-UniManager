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
  Paper,
  IconButton 
} from '@mui/material';
import DownloadIcon from '@mui/icons-material/Download';
import { getStudentRequestApplications } from '../../../../services/requestApplicationService';
import { download } from '../../../../services/fileService'; // Import the downloadFile function

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

  const handleDownload = async (fileId) => {
    try {
      await download(fileId, bearerToken);
    } catch (error) {
      console.error('Error downloading file:', error);
    }
  };

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
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дата на регистр.</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Вид</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}>Дата на резол.</TableCell>
                <TableCell align="center" sx={{ fontWeight: 'bold', color: 'white', border: '1px solid #ddd' }}></TableCell>
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
                    <IconButton 
                      sx={{ 
                        backgroundColor: '#4caf50', 
                        color: 'white', 
                        '&:hover': {
                          backgroundColor: '#388e3c',
                          color: 'white'
                        },
                        borderRadius: '50%',
                        width: 40,
                        height: 40
                      }}
                      onClick={() => handleDownload(requestApplication.fileId)} // Update this line
                    >
                      <DownloadIcon />
                    </IconButton>
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