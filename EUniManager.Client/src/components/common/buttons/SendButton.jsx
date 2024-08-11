import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';

export default function SendButton({ onClick, loading }) {
  return (
    <IconButton
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.sendButton,
        mt: 2,
      }}
      onClick={onClick}
      disabled={loading}
    >
      <SendIcon />
    </IconButton>
  );
};