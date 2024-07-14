import React, { useState, useContext, useRef, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import {
  Container,
  Box,
  Typography,
  Divider,
  IconButton,
  TextField,
  Snackbar,
  Alert,
  CircularProgress
} from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import UploadFileIcon from '@mui/icons-material/UploadFile';
import EditIcon from '@mui/icons-material/Edit';
import DownloadIcon from '@mui/icons-material/Download';
import SendIcon from '@mui/icons-material/Send';
import Modal from '../../../modals/Modal';
import { UserContext } from '../../../../contexts/UserContext';
import { getAssignmentByIdWithSolution } from '../../../../services/assignmentService';
import { download } from '../../../../services/fileService';
import { submitAssignmentSolution } from '../../../../services/assignmentSolutionService';
import { getBase64 } from '../../../../utils/fileUtils';

const iconButtonStyle = {
  borderRadius: '50%',
  padding: '12px',
  color: 'white',
  // Removed marginLeft for consistency between buttons
};

const hoverStyles = {
  backButton: {
    backgroundColor: '#9e9e9e', // Neutral gray color
    '&:hover': {
      backgroundColor: '#757575',
      color: 'white'
    }
  },
  uploadButton: {
    backgroundColor: 'primary.main',
    '&:hover': {
      backgroundColor: 'primary.dark',
      color: 'white'
    }
  },
  downloadButton: {
    backgroundColor: '#4caf50',
    '&:hover': {
      backgroundColor: '#388e3c',
      color: 'white'
    }
  },
  sendButton: {
    backgroundColor: 'primary.main',
    '&:hover': {
      backgroundColor: 'primary.dark',
      color: 'white'
    }
  }
};

export default function Assignment() {
  const [text, setText] = useState('');
  const [uploadedFile, setUploadedFile] = useState(null);
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
      fileExtension: null,
      text: '',
      createdAt: '',
      seenOn: '',
      submittedOn: null,
      mark: null,
      markedOn: null,
      comment: null
    }
  });
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();
  const { assignmentId } = useParams();
  const fileInputRef = useRef(null);

  useEffect(() => {
    getAssignmentByIdWithSolution(assignmentId, bearerToken)
      .then(data => {
        console.log('Assignment data loaded:', data);
        setAssignment(data);
        setText(data.solution.text);
      })
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [assignmentId, bearerToken, navigate]);

  useEffect(() => {
    if (!uploadedFile) return;

    console.log('file is uploaded')

    handleSolutionSubmit();
  }, [uploadedFile]);

  const handleTextChange = (event) => {
    console.log('Text changed:', event.target.value);
    setText(event.target.value);
  };

  const handleFileChange = async (event) => {
    const selectedFile = event.target.files[0];
    console.log('File selected:', selectedFile);
    setUploadedFile(selectedFile);
    fileInputRef.current.value = '';
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

  const handleSolutionSubmit = async () => {
    console.log('Handle upload triggered');
    setLoading(true); // Start loading animation

    console.log(uploadedFile);
    console.log(text);
    const isFile = uploadedFile !== null;

    try {
      let base64String = '';
      let mimeType = '';
      if (uploadedFile) {
        console.log('file');
        const result = await getBase64(uploadedFile);
        base64String = result.base64String;
        mimeType = result.mimeType;
      }

      const data = {
        assignmentId: assignmentId,
        file: uploadedFile ? {
          bytes: base64String,
          mimeType: mimeType
        } : null,
        text: text
      };

      console.log('Uploading assignment solution:', data);

      const response = await submitAssignmentSolution(assignment.solution.id, data, bearerToken);
      console.log('Upload response:', response);

      const message = isFile ? 'Файлът е качен успешно'
                             : 'Отговорът е изпратен успешно';

      setSnackbarMessage(message);
      setSnackbarSeverity('success');
      setSnackbarOpen(true);

      // Refresh assignment data
      const updatedAssignment = await getAssignmentByIdWithSolution(assignmentId, bearerToken);
      console.log('Updated assignment data:', updatedAssignment);
      setAssignment(updatedAssignment);
    }
    catch (error) {
      const message = isFile ? 'Възникна грешка при качването на файла'
                             : 'Възникна грешка при изпращането на отговора';

      console.error(error);
      setSnackbarMessage(message);
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
    }
    finally {
      setLoading(false); // Stop loading animation
      setUploadedFile(null); // Reset the file input for re-upload
    }
  };

  const renderUploadButton = () => {
    if (assignment.type === 'Текст') {
      return (
        <IconButton
          sx={{
            ...iconButtonStyle,
            ...hoverStyles.uploadButton,
            marginLeft: 0 // Ensure no left margin for pencil icon
          }}
          disabled={loading}
        >
          <EditIcon />
        </IconButton>
      );
    }
    return (
      <IconButton
        sx={{
          ...iconButtonStyle,
          ...hoverStyles.uploadButton,
          marginLeft: 0 // Ensure no left margin for upload icon
        }}
        component="label"
        disabled={loading}
      >
        <UploadFileIcon />
        <input type="file" hidden onChange={handleFileChange} ref={fileInputRef} />
      </IconButton>
    );
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
        <IconButton
          onClick={handleBack}
          sx={{
            ...iconButtonStyle,
            ...hoverStyles.backButton,
            marginLeft: 0 // Ensure no left margin for back icon
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

      <Box sx={{ textAlign: 'left' }}>
        <Typography variant="h6" gutterBottom>{assignment.title}</Typography>
        <Typography variant="body1">
          <strong>Срок за предаване:</strong> от: {assignment.startDate} до: {assignment.dueDate}
        </Typography>
        <Typography variant="body1" component="div">
          <strong>Тип на резултата:</strong> {assignment.type}
        </Typography>
        <Typography variant="body1" component="div">
          <strong>Пояснения:</strong> {assignment.description}
        </Typography>
      </Box>
      
      <Divider sx={{ my: 2 }} />
      
      <Box sx={{ flex: 1, textAlign: 'left' }}>
        <Typography variant="h6" gutterBottom>Изпълнение на задачата</Typography>
        <Typography variant="body1">
          <strong>Изпратена на:</strong> {assignment.solution.createdAt}<br />
          <strong>Видяна на:</strong> {assignment.solution?.seenOn}<br />
          {assignment.solution && (
            <>
              <strong>Отговор на:</strong> {assignment.solution?.submittedOn}
              <br />
              <strong>Отговор:</strong> {assignment.solution.fileId ? `${assignment.solution.fileId}.${assignment.solution.fileExtension}` : (assignment.solution.text ?? '')}
            </>
          )}
        </Typography>
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 2 }}>
          <Modal
            title="Изпрати решение"
            loading={loading}
            trigger={renderUploadButton()}
          >
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
              uploadedFile && 
              <Box sx={{ mt: 2 }}>
                <Typography variant="body1">Избран файл: {uploadedFile.name}</Typography>
              </Box>
            )}
            {assignment.type === 'Текст' ? (
              <IconButton
                sx={{
                  ...iconButtonStyle,
                  ...hoverStyles.sendButton,
                  mt: 2
                }}
                onClick={handleSolutionSubmit}
                disabled={loading}
              >
                <SendIcon />
              </IconButton>
            ) : null}
          </Modal>
          {assignment.solution.fileId && (
            <IconButton
              sx={{
                ...iconButtonStyle,
                ...hoverStyles.downloadButton,
                ml: 2
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