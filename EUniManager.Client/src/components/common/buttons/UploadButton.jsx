import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import UploadFileIcon from '@mui/icons-material/UploadFile';

export default function UploadButton({ marginLeft = 0, inputRef, onChange, disabled }) {
  return (
    <IconButton
      component="label"
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.uploadButton,
        ml: marginLeft
      }}
      disabled={disabled}
    >
      <UploadFileIcon />
      <input
        type="file"
        hidden
        onChange={onChange}
        ref={inputRef}
      />
    </IconButton>
  );
};