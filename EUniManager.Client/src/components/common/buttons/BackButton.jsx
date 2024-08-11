import React from 'react';
import { useNavigate } from 'react-router-dom';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';


export default function BackButton({ disabled }) {
  const navigate = useNavigate();
  const handleBack = () => navigate(-1); // Navigate back

  return (
    <IconButton
      onClick={handleBack}
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.backButton
      }}
      disabled={disabled}
    >
      <ArrowBackIcon />
    </IconButton>
  );
};