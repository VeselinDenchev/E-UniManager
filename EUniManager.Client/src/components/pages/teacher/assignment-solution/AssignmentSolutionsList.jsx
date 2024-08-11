import React, { useState, useEffect, useContext } from 'react';
import { useParams } from 'react-router-dom';
import { UserContext } from '../../../../contexts/UserContext';
import { getAllSolutionsToAssignment, updateMark, updateComment } from '../../../../services/assignmentSolutionService';
import BackButton from '../../../common/buttons/BackButton';
import DownloadButton from '../../../common/buttons/DownloadButton';
import ViewButton from '../../../common/buttons/ViewButton';
import EditButton from '../../../common/buttons/EditButton';
import CommentButton from '../../../common/buttons/CommentButton';
import SubmitTextModal from '../../../common/modals/SubmitTextModal';
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
  CircularProgress,
  FormControl,
  Select,
  MenuItem,
  Snackbar,
  Alert,
} from '@mui/material';

const gradeOptions = [
  { label: "Отличен (6)", value: 6 },
  { label: "Мн. добър (5)", value: 5 },
  { label: "Добър (4)", value: 4 },
  { label: "Среден (3)", value: 3 },
  { label: "Слаб (2)", value: 2 },
];

export default function AssignmentSolutionsList() {
  const { assignmentId } = useParams();
  const { bearerToken } = useContext(UserContext);
  const [solutions, setSolutions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentSolutionText, setCurrentSolutionText] = useState('');
  const [currentComment, setCurrentComment] = useState('');
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');
  const [selectedSolutionId, setSelectedSolutionId] = useState(null);

  useEffect(() => {
    const fetchSolutions = async () => {
      setLoading(true);
      try {
        const data = await getAllSolutionsToAssignment(assignmentId, bearerToken);
        setSolutions(data);
      } catch (error) {
        console.error('Error fetching solutions:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchSolutions();
  }, [assignmentId, bearerToken]);

  const handleMarkChange = async (id, newMark) => {
    const oldMark = solutions.find((solution) => solution.id === id).mark;
    const parsedMark = parseInt(newMark, 10);
    if (!isNaN(parsedMark) && parsedMark >= 2 && parsedMark <= 6) {
      setSolutions((prevSolutions) =>
        prevSolutions.map((solution) =>
          solution.id === id ? { ...solution, mark: parsedMark } : solution
        )
      );

      try {
        await updateMark(id, parsedMark, bearerToken);
        setSnackbarMessage('Оценката беше поставена успешно');
        setSnackbarSeverity('success');
        
        // Reset the comment to null upon successful mark update
        setSolutions((prevSolutions) =>
          prevSolutions.map((solution) =>
            solution.id === id ? { ...solution, comment: null } : solution
          )
        );

        setSnackbarOpen(true);
      } catch (error) {
        setSnackbarMessage('Възникна грешка при поставянето на оценката');
        setSnackbarSeverity('error');
        setSnackbarOpen(true);
        setSolutions((prevSolutions) =>
          prevSolutions.map((solution) =>
            solution.id === id ? { ...solution, mark: oldMark } : solution
          )
        );
      }
    }
  };

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  const handleView = (text) => {
    setCurrentSolutionText(text);
    setIsModalOpen(true);
  };

  const handleComment = (id, comment) => {
    setSelectedSolutionId(id);
    setCurrentComment(comment || '');
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setSelectedSolutionId(null);
    setTimeout(() => {
      setCurrentSolutionText('');
      setCurrentComment('');
    }, 300);
  };

  const handleCommentChange = (event) => {
    setCurrentComment(event.target.value);
  };

  const handleSubmitComment = async () => {
    setLoading(true);
    try {
      await updateComment(selectedSolutionId, currentComment, bearerToken);
      setSnackbarMessage('Публикуването на коментара беше успешно');
      setSnackbarSeverity('success');
  
      setSolutions((prevSolutions) =>
        prevSolutions.map((solution) =>
          solution.id === selectedSolutionId ? { ...solution, comment: currentComment } : solution
        )
      );
    } catch (error) {
      setSnackbarMessage('Публикуването на коментара не беше успешно');
      setSnackbarSeverity('error');
    } finally {
      setLoading(false);
      setSnackbarOpen(true);
      handleCloseModal();
    }
  };

  return (
    <Container sx={{ mt: 4, mb: 4 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
        <BackButton disabled={loading} />
      </Box>
      <Box sx={{ mb: 6 }}>
        <Typography variant="h4" gutterBottom>
          Решения
        </Typography>
      </Box>
      {loading ? (
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            height: '100vh',
          }}
        >
          <CircularProgress />
        </Box>
      ) : (
        <Box>
          <TableContainer component={Paper} elevation={6}>
            <Table sx={{ minWidth: 650, borderCollapse: 'collapse' }}>
              <TableHead>
                <TableRow sx={{ backgroundColor: '#1976d2' }}>
                  <TableCell
                    align="center"
                    sx={{
                      fontWeight: 'bold',
                      color: 'white',
                      border: '1px solid #ddd',
                    }}
                  >
                    Факултетен номер
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      fontWeight: 'bold',
                      color: 'white',
                      border: '1px solid #ddd',
                    }}
                  >
                    Име
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      fontWeight: 'bold',
                      color: 'white',
                      border: '1px solid #ddd',
                    }}
                  >
                    Решение
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      fontWeight: 'bold',
                      color: 'white',
                      border: '1px solid #ddd',
                      width: '200px',
                      minWidth: '200px',
                    }}
                  >
                    Оценка
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{
                      fontWeight: 'bold',
                      color: 'white',
                      border: '1px solid #ddd',
                    }}
                  >
                    Коментар на оценката
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {solutions.map((solution) => (
                  <TableRow
                    key={solution.id}
                    sx={{
                      backgroundColor:
                        solutions.indexOf(solution) % 2 === 0
                          ? '#e3f2fd'
                          : '#bbdefb',
                    }}
                  >
                    <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                      {solution.studentFacultyNumber}
                    </TableCell>
                    <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                      {solution.studentFullName}
                    </TableCell>
                    <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                      {solution.text ? (
                        <ViewButton onClick={() => handleView(solution.text)} />
                      ) : solution.fileId ? (
                        <DownloadButton
                          fileId={solution.fileId}
                          disabled={false}
                        />
                      ) : (
                        'Няма'
                      )}
                    </TableCell>
                    <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                      <FormControl variant="outlined" sx={{ width: '180px' }}>
                        <Select
                          value={solution.mark !== null ? solution.mark : ''}
                          onChange={(e) => handleMarkChange(solution.id, e.target.value)}
                          displayEmpty
                          sx={{ fontSize: '0.875rem' }} // Smaller font size for the dropdown text
                        >
                          <MenuItem value="" disabled sx={{ fontSize: '0.875rem' }}>
                            Няма
                          </MenuItem>
                          {gradeOptions.map((option) => (
                            <MenuItem key={option.value} value={option.value} sx={{ fontSize: '0.875rem' }}>
                              {option.label}
                            </MenuItem>
                          ))}
                        </Select>
                      </FormControl>
                    </TableCell>
                    <TableCell align="center" sx={{ border: '1px solid #ddd' }}>
                      {solution.mark !== null ? (
                        solution.comment ? (
                          <EditButton onClick={() => handleComment(solution.id, solution.comment)} />
                        ) : (
                          <CommentButton onClick={() => handleComment(solution.id, solution.comment)} />
                        )
                      ) : (
                        <Typography variant="body2">Няма</Typography>
                      )}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Box>
      )}
      <SubmitTextModal
        title="Коментар на оценката"
        text={currentComment}
        onChange={handleCommentChange}
        onSubmit={handleSubmitComment}
        loading={loading}
        isOpen={isModalOpen}
        onClose={handleCloseModal}
      />
      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleSnackbarClose}
      >
        <Alert onClose={handleSnackbarClose} severity={snackbarSeverity} sx={{ width: '100%' }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
      {loading && (
        <Box
          sx={{
            position: 'fixed',
            top: 0,
            left: 0,
            width: '100%',
            height: '100%',
            backgroundColor: 'rgba(0, 0, 0, 0.5)',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            zIndex: 9999,
          }}
        >
          <CircularProgress />
        </Box>
      )}
    </Container>
  );
}