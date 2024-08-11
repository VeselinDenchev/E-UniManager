import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import IconButton from '@mui/material/IconButton';
import SaveIcon from '@mui/icons-material/Save';

export default function SaveButton({ onClick, loading }) {
  return (
    <IconButton
      onClick={onClick}
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.saveButton,
      }}
      disabled={loading}
    >
      <SaveIcon />
    </IconButton>
  );
};