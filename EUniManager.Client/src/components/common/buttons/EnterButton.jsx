import React from 'react';
import { Link } from 'react-router-dom';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import InputIcon from '@mui/icons-material/Input';

export default function EnterButton({ to, state }) {
  return (
    <IconButton
      component={Link}
      to={to}
      state={state}
      rel="noopener noreferrer"
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.enterButton,
      }}
    >
      <InputIcon />
    </IconButton>
  );
};