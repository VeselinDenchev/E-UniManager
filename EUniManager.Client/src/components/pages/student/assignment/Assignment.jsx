import React, { useState, useContext, useRef, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Draggable from 'react-draggable';
import {
  Container,
  Box,
  Typography,
  Divider,
  IconButton,
  TextField,
  Dialog,
  DialogContent,
  DialogTitle,
  Paper,
  Snackbar,
  Alert,
  CircularProgress
} from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import UploadFileIcon from '@mui/icons-material/UploadFile';
import EditIcon from '@mui/icons-material/Edit';
import DownloadIcon from '@mui/icons-material/Download';
import SendIcon from '@mui/icons-material/Send';
import { UserContext } from '../../../../contexts/UserContext';
import { getAssignmentByIdWithSolution } from '../../../../services/assignmentService';
import { download } from '../../../../services/fileService';
import { uploadAssignmentSolution } from '../../../../services/assignmentSolutionService';
import { getBase64 } from '../../../../utils/fileUtils';

function DraggablePaper(props) {
  const paperRef = useRef(null);

  return (
    <Draggable handle="#draggable-dialog-title" nodeRef={paperRef}>
      <Paper ref={paperRef} {...props} />
    </Draggable>
  );
}

export default function Assignment() {
  const [isOpened, setIsOpened] = useState(false);
  const [text, setText] = useState('');
  const [file, setFile] = useState(null);
  const [loading, setLoading] = useState(false);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');
  const [assignment, setAssignment] = useState({
    title: '',
    type: '',
    startDate: '',
    dueDate: '',
    description: '',
    solution: {
      id: null,
      fileId: null,
      createdAt: '',
      seenOn: '',
      uploadedOn: null,
      mark: null,
      markedOn: null,
      comment: null,
      fileExtension: null
    }
  });
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();
  const { assignmentId } = useParams();

  useEffect(() => {
    getAssignmentByIdWithSolution(assignmentId, bearerToken)
      .then(data => {
        console.log('Assignment data loaded:', data);
        setAssignment(data);
      })
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [assignmentId, bearerToken, navigate]);

  const handleOpen = () => {
    console.log('Dialog opened');
    setIsOpened(true);
  };

  const handleClose = () => {
    console.log('Dialog closed');
    setIsOpened(false);
  };

  const handleTextChange = (event) => {
    console.log('Text changed:', event.target.value);
    setText(event.target.value);
  };

  const handleFileChange = async (event) => {
    const selectedFile = event.target.files[0];
    console.log('File selected:', selectedFile);
    setFile(selectedFile);

    if (selectedFile) {
      await handleUpload(selectedFile);
    }
  };

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  const handleBack = () => navigate(-1); // Navigate back

  const handleDownload = async (fileId) => {
    try {
      await download(fileId, bearerToken);
    } catch (error) {
      console.error('Error downloading file:', error);
    }
  };

  const handleUpload = async (selectedFile) => {
    console.log('Handle upload triggered');
    setLoading(true); // Start loading animation
    try {
      let base64String = '';
      let mimeType = '';
      if (selectedFile) {
        const result = await getBase64(selectedFile);
        base64String = result.base64String;
        mimeType = result.mimeType;
      }

      const data = {
        assignmentId: assignmentId,
        file: selectedFile ? {
          bytes: base64String,
          mimeType: mimeType
        } : null,
        text: text
      };

      console.log('Uploading assignment solution:', data);

      const response = await uploadAssignmentSolution(assignment.solution.id, data, bearerToken);
      console.log('Upload response:', response);

      setSnackbarMessage('Файлът е качен успешно!');
      setSnackbarSeverity('success');
      setSnackbarOpen(true);

      setIsOpened(false);
      // Refresh assignment data
      const updatedAssignment = await getAssignmentByIdWithSolution(assignmentId, bearerToken);
      console.log('Updated assignment data:', updatedAssignment);
      setAssignment(updatedAssignment);
    } catch (error) {
      console.error('Error uploading file:', error);
      setSnackbarMessage('Възникна грешка при качването на файла');
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
    } finally {
      setLoading(false); // Stop loading animation
      setFile(null); // Reset the file input for re-upload
    }
  };

  const renderUploadButton = () => {
    if (assignment.type === 'Текст') {
      return (
        <IconButton
          sx={{
            backgroundColor: 'primary.main',
            color: 'white',
            mr: 2,
            '&:hover': {
              backgroundColor: 'primary.dark'
            },
            borderRadius: '50%',
            width: 40,
            height: 40
          }}
          onClick={handleOpen}
          disabled={loading}
        >
          <EditIcon />
        </IconButton>
      );
    }
    return (
      <IconButton
        sx={{
          backgroundColor: 'primary.main',
          color: 'white',
          mr: 2,
          '&:hover': {
            backgroundColor: 'primary.dark'
          },
          borderRadius: '50%',
          width: 40,
          height: 40
        }}
        component="label"
        disabled={loading}
      >
        <UploadFileIcon />
        <input type="file" hidden onChange={handleFileChange} />
      </IconButton>
    );
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
        <IconButton
          onClick={handleBack}
          sx={{
            backgroundColor: '#9e9e9e', // Neutral gray color
            color: 'white',
            mr: 1,
            '&:hover': {
              backgroundColor: '#757575'
            },
            borderRadius: '50%',
            width: 40,
            height: 40
          }}
          disabled={loading}
        >
          <ArrowBackIcon />
        </IconButton>
      </Box>

      <Box sx={{ textAlign: 'center', mb: 2 }}>
        <Typography variant="h4" gutterBottom>
          Задача
        </Typography>
      </Box>

      <Box sx={{ mb: 4, textAlign: 'left' }}>
        <Typography variant="h6" gutterBottom>{assignment.title}</Typography>
        <Typography variant="body1" gutterBottom>
          <strong>Срок за предаване:</strong> от: {assignment.startDate} до: {assignment.dueDate}
        </Typography>
        <Box sx={{ mt: 2 }}>
          <Typography variant="body1" component="div" sx={{ mb: 1 }}>
            <strong>Тип на резултата:</strong> {assignment.type}
          </Typography>
          <Typography variant="body1" component="div" sx={{ mb: 1, mt: 2 }}>
            <strong>Пояснения:</strong> {assignment.description}
          </Typography>
        </Box>
      </Box>
      
      <Divider sx={{ my: 2 }} />
      
      <Box sx={{ display: 'flex', justifyContent: 'flex-start', alignItems: 'center' }}>
        <Box sx={{ flex: 1, textAlign: 'left' }}>
          <Typography variant="h6">Изпълнение на задачата</Typography>
          <Typography variant="body1">
            <strong>Изпратена на:</strong> {assignment.solution.createdAt}<br />
            <strong>Видяна на:</strong> {assignment.solution?.seenOn}<br />
            <strong>Отговор на:</strong> {assignment.solution?.uploadedOn}<br />
            <strong>Отговор:</strong> {assignment.solution.fileId ? `${assignment.solution.fileId}.${assignment.solution.fileExtension}` : ''}
          </Typography>
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          {renderUploadButton()}
          {assignment.solution.fileId && (
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
              component="a"
              onClick={() => handleDownload(assignment.solution.fileId)}
              disabled={loading}
            >
              <DownloadIcon />
            </IconButton>
          )}
        </Box>
      </Box>

      <Dialog
        open={isOpened}
        onClose={handleClose}
        PaperComponent={DraggablePaper}
        aria-labelledby="draggable-dialog-title"
        maxWidth="md" // Set maximum width of the dialog
        fullWidth // Make the dialog take full width
      >
        <DialogTitle style={{ cursor: 'move' }} id="draggable-dialog-title">
          Изпрати решение
        </DialogTitle>
        <DialogContent>
          {assignment.type === 'Текст' ? (
            <TextField
              fullWidth
              multiline
              rows={10} // Keep the number of rows to maintain height
              variant="outlined"
              value={text}
              onChange={handleTextChange}
              sx={{ mt: 2 }}
              disabled={loading}
            />
          ) : (
            <Box sx={{ mt: 2 }}>
              <Typography variant="body1">Selected file: {file ? file.name : 'None'}</Typography>
            </Box>
          )}
          {assignment.type === 'Текст' ? (
            <IconButton
              sx={{
                backgroundColor: 'primary.main',
                color: 'white',
                mt: 2,
                '&:hover': {
                  backgroundColor: 'primary.dark'
                },
                borderRadius: '50%',
                width: 40, // Match the size of other buttons
                height: 40  // Match the size of other buttons
              }}
              onClick={handleUpload}
              disabled={loading}
            >
              <SendIcon />
            </IconButton>
          ) : null}
        </DialogContent>
      </Dialog>
      
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