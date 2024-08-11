import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';

export default function DeleteButton({ onClick, disabled }) {
  return (
    <IconButton
      onClick={onClick}
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.deleteButton,
        marginLeft: '0.5rem'
      }}
      disabled={disabled}
    >
      <DeleteIcon />
    </IconButton>
  );
}
