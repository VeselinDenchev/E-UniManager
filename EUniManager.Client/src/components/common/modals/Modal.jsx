import React, { useRef } from 'react';
import Draggable from 'react-draggable';
import PropTypes from 'prop-types';
import CloseButton from '../buttons/CloseButton';
import {
  Dialog,
  DialogContent,
  DialogTitle,
  Box,
  Typography,
  Paper,
} from '@mui/material';

const DraggablePaper = (props) => {
  const paperRef = useRef(null);

  return (
    <Draggable handle="#draggable-dialog-title" nodeRef={paperRef}>
      <Paper
        ref={paperRef}
        {...props}
        style={{ backgroundColor: 'white', boxShadow: '0px 3px 6px rgba(0, 0, 0, 0.16)' }}
      />
    </Draggable>
  );
};

export default function Modal({ title, children, loading, footer, isOpen, onClose }) {
  const handleBackdropClick = (event) => {
    if (event.target === event.currentTarget) {
      onClose();
    }
  };

  return (
    <Dialog
      open={isOpen}
      onClose={onClose}
      PaperComponent={DraggablePaper}
      aria-labelledby="draggable-dialog-title"
      maxWidth="md"
      fullWidth
      onBackdropClick={handleBackdropClick}
      sx={{
        backgroundColor: 'rgba(0, 0, 0, 0.5)', // To make the backdrop more opaque
      }}
    >
      <DialogTitle style={{ cursor: 'move', padding: 0 }} id="draggable-dialog-title">
        <Box
          sx={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            padding: '32px 48px', // Add padding to match the content padding
          }}
        >
          <Typography variant="h6" sx={{ fontWeight: 'bold' }}>
            {title}
          </Typography>
          <CloseButton 
            onClick={onClose}
            disabled={loading}
            width={36}
            height={36}
            padding={0}
            fontSize={24}
          />
        </Box>
      </DialogTitle>
      <DialogContent
        sx={{ padding: '32px 48px' }}
        style={{ backgroundColor: 'white' }}
      >
        {children}
        {footer && (
          <Box sx={{ display: 'flex', justifyContent: 'flex-end', mt: 2 }}>
            {footer}
          </Box>
        )}
      </DialogContent>
    </Dialog>
  );
}

Modal.propTypes = {
  title: PropTypes.string.isRequired,
  children: PropTypes.node.isRequired,
  loading: PropTypes.bool.isRequired,
  footer: PropTypes.node, // Footer element to include save button
  isOpen: PropTypes.bool.isRequired, // Control modal open state
  onClose: PropTypes.func.isRequired, // Function to close the modal
};