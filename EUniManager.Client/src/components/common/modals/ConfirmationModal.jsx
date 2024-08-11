// ConfirmationModal.js
import React, { useRef } from 'react';
import Draggable from 'react-draggable';
import ConfirmButton from '../buttons/ConfirmButton';
import CloseButton from '../buttons/CloseButton';
import {
  Dialog,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Typography,
  Box,
  Paper,
} from '@mui/material';
import WarningIcon from '@mui/icons-material/Warning';

const DraggablePaper = (props) => {
  const paperRef = useRef(null);

  return (
    <Draggable handle="#draggable-dialog-title" nodeRef={paperRef}>
      <Paper ref={paperRef} {...props} style={{ backgroundColor: 'white', boxShadow: '0px 3px 6px rgba(0, 0, 0, 0.16)' }} />
    </Draggable>
  );
};

export default function ConfirmationModal({ open, onClose, text, onConfirm }) {
  return (
    <Dialog
      open={open}
      onClose={onClose}
      PaperComponent={DraggablePaper}
      aria-labelledby="draggable-dialog-title"
      maxWidth="xs" // Change to 'xs' for a narrower dialog
      fullWidth
      sx={{
        backgroundColor: 'rgba(0, 0, 0, 0.5)', // To make the backdrop more opaque
      }}
    >
      <DialogTitle
        id="draggable-dialog-title"
        style={{ backgroundColor: 'white', cursor: 'move', padding: 0 }}
      >
        <Box display="flex" justifyContent="center" alignItems="center" flexDirection="column" sx={{ padding: '16px 24px' }}>
          <WarningIcon sx={{ color: '#ff9800', fontSize: 60 }} />
          <Typography variant="h6" align="center" sx={{ fontWeight: 'bold' }}>
            Потвърждение
          </Typography>
        </Box>
      </DialogTitle>
      <DialogContent sx={{ padding: '32px 48px' }}>
        <Box display="flex" flexDirection="column" alignItems="center">
          <DialogContentText align="center" sx={{ fontSize: '1.2rem', color: 'black' }}>
            {text}
          </DialogContentText>
          <Box display="flex" justifyContent="center" sx={{ mt: 2, gap: '0.5rem' }}>
          <ConfirmButton onClick={onConfirm} fontSize={30} />
          <CloseButton
            onClick={onClose} 
            disabled={false}
            width={55}
            height={55}
            padding={0}
            fontSize={30} />
          </Box>
        </Box>
      </DialogContent>
    </Dialog>
  );
};