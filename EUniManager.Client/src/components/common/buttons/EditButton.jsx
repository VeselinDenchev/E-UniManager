import React from 'react';
import { baseButtonStyle, buttonStyles } from '../../../styles/buttonStyles';
import { IconButton } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';


export default function EditButton({ marginLeft = 0, onClick, disabled }) {
  return (
    <IconButton
      sx={{
        ...baseButtonStyle,
        ...buttonStyles.editButton,
        ml: {marginLeft}
      }}
      onClick={onClick}
      disabled = {disabled}
    >
      <EditIcon />
    </IconButton>
  );
};