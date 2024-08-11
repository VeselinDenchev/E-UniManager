import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import CommentIcon from '@mui/icons-material/Comment';

export default function CommentButton({ onClick, disabled }) {
  return (
    <IconButton
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.commentButton
      }}
      onClick={onClick}
      disabled={disabled}
    >
      <CommentIcon />
    </IconButton>
  );
}