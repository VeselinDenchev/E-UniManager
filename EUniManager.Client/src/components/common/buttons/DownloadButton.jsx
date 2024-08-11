import React from 'react';
import { useContext, useState } from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import DownloadIcon from '@mui/icons-material/Download';
import { UserContext } from '../../../contexts/UserContext';
import { download } from '../../../services/fileService';
import { IconButton, Snackbar, Alert } from '@mui/material';

export default function DownloadButton({ fileId, marginLeft = 0, disabled }) {
  const { bearerToken } = useContext(UserContext);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');

  const handleDownload = async () => {
    try {
      await download(fileId, bearerToken);
      setSnackbarMessage('Файлът беше изтеглен успешно');
      setSnackbarSeverity('success');
      setSnackbarOpen(true);
    } catch (error) {
      console.error('Error downloading file:', error);
      setSnackbarMessage('Възникнва грешка при изтеглянето на файла');
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
    }
  };

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  return (
    <>
      <IconButton
        sx={{
          ...baseButtonStyle,
          ...buttonStyles.downloadButton,
          marginLeft
        }}
        onClick={handleDownload}
        disabled={disabled}
      >
        <DownloadIcon />
      </IconButton>
      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleSnackbarClose}
      >
        <Alert onClose={handleSnackbarClose} severity={snackbarSeverity} sx={{ width: '100%' }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </>
  );
}