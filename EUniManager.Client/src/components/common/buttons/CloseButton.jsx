import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

export default function CloseButton({ onClick, disabled, width = null, height = null, padding = null, fontSize }) {
  return (
    <IconButton
      onClick={onClick}
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.closeButton,
        width: width !== null ? width : undefined,
        height: height !== null ? height : undefined,
        padding: padding !== null ? padding : undefined,
      }}
      aria-label="close"
      disabled={disabled}
    >
      <CloseIcon sx={{ fontSize }} />
    </IconButton>
  );
};