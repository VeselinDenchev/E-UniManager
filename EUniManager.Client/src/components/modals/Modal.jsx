import React from 'react';
import { useState, useRef } from 'react';
import PropTypes from 'prop-types';
import Draggable from 'react-draggable';
import {
  Dialog,
  DialogContent,
  DialogTitle,
  Box,
  Typography,
  IconButton,
  Paper
} from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

const DraggablePaper = (props) => {
  const paperRef = useRef(null);

  return (
    <Draggable handle="#draggable-dialog-title" nodeRef={paperRef}>
      <Paper ref={paperRef} {...props} style={{ backgroundColor: 'white', boxShadow: '0px 3px 6px rgba(0, 0, 0, 0.16)' }} />
    </Draggable>
  );
};

const iconButtonStyle = {
  borderRadius: '50%',
  padding: '12px',
  color: 'white',
  marginLeft: '0.5rem',
};

const hoverStyles = {
  closeButton: {
    backgroundColor: '#f44336',
    '&:hover': {
      backgroundColor: '#d32f2f',
      color: 'white'
    }
  }
};

export default function Modal({ title, children, loading, trigger }) {
  const [isOpened, setIsOpened] = useState(false);

  const handleOpen = () => {
    setIsOpened(true);
  };

  const handleClose = () => {
    setIsOpened(false);
  };

  const handleBackdropClick = (event) => {
    if (event.target === event.currentTarget) {
      handleClose();
    }
  };

  return (
    <>
      <span onClick={handleOpen}>{trigger}</span>
      <Dialog
        open={isOpened}
        onClose={handleClose}
        PaperComponent={DraggablePaper}
        aria-labelledby="draggable-dialog-title"
        maxWidth="md"
        fullWidth
        onBackdropClick={handleBackdropClick}
        sx={{
          backdropFilter: 'blur(3px)', // Optional: to add a blur effect to the background
          backgroundColor: 'rgba(0, 0, 0, 0.5)' // To make the backdrop more opaque
        }}
      >
        <DialogTitle style={{ cursor: 'move', backgroundColor: 'white' }} id="draggable-dialog-title">
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <Typography variant="h6">{title}</Typography>
            <IconButton
              onClick={handleClose}
              sx={{
                ...iconButtonStyle,
                ...hoverStyles.closeButton,
                width: 36,
                height: 36,
                padding: 0,
              }}
              disabled={loading}
            >
              <CloseIcon sx={{ fontSize: 24 }} />
            </IconButton>
          </Box>
        </DialogTitle>
        <DialogContent style={{ backgroundColor: 'white' }}>
          {children}
        </DialogContent>
      </Dialog>
    </>
  );
};

Modal.propTypes = {
  title: PropTypes.string.isRequired,
  children: PropTypes.node.isRequired,
  loading: PropTypes.bool.isRequired,
  trigger: PropTypes.node.isRequired, // Trigger element to open the modal
};