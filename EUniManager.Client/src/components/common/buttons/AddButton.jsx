import React from 'react';
import { buttonStyles } from '../../../styles/buttonStyles';
import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';

export default function AddButton({ onClick, children }) {
  return (
    <Button
      variant="contained"
      startIcon={<AddIcon />}
      sx={{
        ...buttonStyles.addButton
      }}
      onClick={onClick}
    >
      {children}
    </Button>
  );
}