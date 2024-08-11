import React from 'react';
import PropTypes from 'prop-types';
import Modal from './Modal';
import SendButton from '../buttons/SendButton';
import { TextField } from '@mui/material';

export default function SubmitTextModal({ title, text, onChange, onSubmit, loading, isOpen, onClose }) {
  return (
    <Modal
      title={title}
      loading={loading}
      isOpen={isOpen}
      onClose={onClose}
      footer={<SendButton onClick={onSubmit} disabled={loading} />}
    >
      <TextField
        fullWidth
        multiline
        rows={10} // Maintain height with 10 rows
        variant="outlined"
        value={text}
        onChange={onChange}
        sx={{ mt: 2 }}
        disabled={loading}
      />
    </Modal>
  );
}

SubmitTextModal.propTypes = {
  title: PropTypes.string.isRequired,
  text: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  onSubmit: PropTypes.func.isRequired,
  loading: PropTypes.bool.isRequired,
  isOpen: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
};