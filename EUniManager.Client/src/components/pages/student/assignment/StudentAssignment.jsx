import React, { useState, useContext, useRef, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import BackButton from '../../../common/buttons/BackButton';
import DownloadButton from '../../../common/buttons/DownloadButton';
import EditButton from '../../../common/buttons/EditButton';
import UploadButton from '../../../common/buttons/UploadButton';
import SubmitTextModal from '../../../common/modals/SubmitTextModal';
import { UserContext } from '../../../../contexts/UserContext';
import { getAssignmentByIdWithSolution } from '../../../../services/assignmentService';
import { submitAssignmentSolution } from '../../../../services/assignmentSolutionService';
import { getBase64 } from '../../../../utils/fileUtils';
import {
  Container,
  Box,
  Typography,
  Divider,
  Snackbar,
  Alert,
  CircularProgress,
} from '@mui/material';

export default function StudentAssignment() {
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
      comment: null,
    },
  });
  const { bearerToken } = useContext(UserContext);
  const navigate = useNavigate();
  const { assignmentId } = useParams();
  const fileInputRef = useRef(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    getAssignmentByIdWithSolution(assignmentId, bearerToken)
      .then((data) => {
        console.log('Assignment data loaded:', data);
        setAssignment(data);
        setText(data.solution.text);
      })
      .catch((error) => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [assignmentId, bearerToken, navigate]);

  useEffect(() => {
    if (!uploadedFile) return;

    console.log('file is uploaded');

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
        file: uploadedFile
          ? {
              bytes: base64String,
              mimeType: mimeType,
            }
          : null,
        text: text,
      };

      console.log('Uploading assignment solution:', data);

      const response = await submitAssignmentSolution(
        assignment.solution.id,
        data,
        bearerToken
      );
      console.log('Upload response:', response);

      const message = isFile ? 'Файлът е качен успешно' : 'Отговорът е изпратен успешно';

      setSnackbarMessage(message);
      setSnackbarSeverity('success');
      setSnackbarOpen(true);

      // Refresh assignment data
      const updatedAssignment = await getAssignmentByIdWithSolution(
        assignmentId,
        bearerToken
      );
      console.log('Updated assignment data:', updatedAssignment);
      setAssignment(updatedAssignment);
    } catch (error) {
      const message = isFile
        ? 'Възникна грешка при качването на файла'
        : 'Възникна грешка при изпращането на отговора';

      console.error(error);
      setSnackbarMessage(message);
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
    } finally {
      setLoading(false); // Stop loading animation
      setUploadedFile(null); // Reset the file input for re-upload
      setIsModalOpen(false); // Close the modal
    }
  };

  const renderUploadButton = () => {
    if (assignment.type === 'Текст') {
      return <EditButton onClick={() => setIsModalOpen(true)} disabled={loading} />;
    }

    return <UploadButton inputRef={fileInputRef} onChange={handleFileChange} disabled={loading} />;
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
        <BackButton disabled={loading} />
      </Box>

      <Box sx={{ textAlign: 'center', mb: 2 }}>
        <Typography variant="h4" gutterBottom>
          Задача
        </Typography>
      </Box>

      <Box sx={{ textAlign: 'left' }}>
        <Typography variant="h6" gutterBottom>
          {assignment.title}
        </Typography>
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
        <Typography variant="h6" gutterBottom>
          Изпълнение на задачата
        </Typography>
        <Typography variant="body1">
          <strong>Изпратена на:</strong> {assignment.solution.createdAt}
          <br />
          <strong>Видяна на:</strong> {assignment.solution?.seenOn}
          <br />
          {assignment.solution && (
            <>
              <strong>Отговор на:</strong> {assignment.solution?.submittedOn}
              <br />
              <strong>Отговор:</strong>{' '}
              {assignment.solution.fileId
                ? `${assignment.solution.fileId}${assignment.solution.fileExtension}`
                : assignment.solution.text ?? ''}
              <br />
              {assignment.solution.mark !== null && (
                <>
                  <strong>Оценка:</strong> {assignment.solution.mark}
                  <br />
                  <strong>Оценена на:</strong> {assignment.solution.markedOn}
                  <br />
                  {assignment.solution.comment && (
                    <>
                      <strong>Коментар за оценката:</strong> {assignment.solution.comment}
                      <br />
                    </>
                  )}
                </>
              )}
            </>
          )}
        </Typography>
        <Box sx={{ display: 'flex', alignItems: 'center', mt: 2 }}>
          {assignment.type === 'Текст' && (
            <>
              <EditButton onClick={() => setIsModalOpen(true)} disabled={loading} />
              <SubmitTextModal
                title='Изпрати решение'
                text={text}
                onChange={handleTextChange}
                loading={loading}
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
              />
            </>
          )}
          {assignment.type !== 'Текст' && renderUploadButton()}
          {assignment.solution.fileId && (
            <DownloadButton
              fileId={assignment.solution.fileId}
              marginLeft="0.5rem"
              disabled={loading}
            />
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