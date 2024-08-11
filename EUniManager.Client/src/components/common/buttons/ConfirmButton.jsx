import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import CheckIcon from '@mui/icons-material/Check';

export default function ConfirmButton({ onClick, fontSize }) {
  return (
    <IconButton
      onClick={onClick}
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.confirmButton
      }}
    >
      <CheckIcon sx={{ fontSize }} />
    </IconButton>
  );
};