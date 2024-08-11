import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { Link } from 'react-router-dom';

export default function ViewButton({ to, onClick }) {
  return (
    <IconButton
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.visibilityButton
      }}
      component={Link}
      to={to || null}
      onClick={onClick || null}
    >
      <VisibilityIcon />
    </IconButton>
  );
};