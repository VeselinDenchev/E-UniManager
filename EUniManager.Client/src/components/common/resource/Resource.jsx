import React from 'react';
import DownloadButton from '../buttons/DownloadButton';
import EditButton from '../buttons/EditButton';
import DeleteButton from '../buttons/DeleteButton';
import ViewButton from '../buttons/ViewButton';
import { getIcon } from '../../../utils/fileUtils';
import { Box, ListItem, ListItemText, ListItemIcon, Typography, Paper } from '@mui/material';
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile';
import CreateIcon from '@mui/icons-material/Create';

function getViewButtonUrl(isTeacher, assignmentId) {
    const baseUrl = isTeacher
      ? `/teachers/assignment-solutions/assignments/${assignmentId}`
      : `/students/assignments/${assignmentId}`;

    return baseUrl;
}

export default function Resource({
  resource,
  handleEdit,
  handleDelete,
  isTeacher,
}) {
  const hasResourceInfo = resource.title || resource.info || resource.file;
  const hasAssignmentInfo = resource.assignment && (resource.assignment.title || resource.assignment.description || resource.file);

  // Hide empty assignment resources
  if (!hasResourceInfo && !hasAssignmentInfo) {
    return null;
  }

  return (
    <Paper key={resource.id} sx={{ mb: 3, p: 2, border: '1px solid #ddd' }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', position: 'relative' }}>
        {hasResourceInfo && (
          <ListItem sx={{ flexGrow: 1, display: 'flex', alignItems: 'center' }}>
            <ListItemIcon sx={{ minWidth: '50px' }}>
              {resource.file ? getIcon(resource.file.extension) : <InsertDriveFileIcon sx={{ fontSize: 40 }} />}
            </ListItemIcon>
            <ListItemText
              primary={resource.title}
              secondary={resource.info}
              sx={{ wordBreak: 'break-word' }}
            />
          </ListItem>
        )}
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          {hasResourceInfo && (
            <>
              {isTeacher && (
                <>
                  <EditButton marginLeft='0.5rem' onClick={() => handleEdit(resource)} disabled={false} />
                  <DeleteButton onClick={() => handleDelete(resource.id, resource.title)} disabled={false}  />
                </>
              )}
              {resource.file && (
                <DownloadButton fileId={resource.file.id} marginLeft='0.5rem' disabled={false} />
              )}
            </>
          )}
        </Box>
      </Box>
      {resource.assignment && hasAssignmentInfo && (
        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mt: hasResourceInfo ? 2 : 0, position: 'relative' }}>
          <ListItem sx={{ flexGrow: 1, display: 'flex', alignItems: 'center' }}>
            <ListItemIcon sx={{ minWidth: '50px' }}>
              <CreateIcon sx={{ color: '#ff9800', fontSize: 40 }} />
            </ListItemIcon>
            <ListItemText
              primary={resource.assignment.title}
              secondary={
                <>
                  {resource.assignment.description}
                  <br />
                  <Typography variant="body2" component="span" color="textSecondary">
                    {`Срок за предаване от: ${new Date(resource.assignment.startDate).toLocaleString('bg-BG')} до: ${new Date(resource.assignment.dueDate).toLocaleString('bg-BG')}`}
                  </Typography>
                </>
              }
              sx={{ wordBreak: 'break-word' }}
            />
          </ListItem>
          {isTeacher && (
              <>
                <EditButton marginLeft='0.5rem' onClick={() => handleEdit(resource)} disabled={false} />
                <DeleteButton onClick={() => handleDelete(resource.id, resource.title)} disabled={false}  />
              </>
            )}
          <ViewButton 
            to={getViewButtonUrl(isTeacher, resource.assignment.id)}
            marginLeft={isTeacher ? '0.5rem' : '0'}
          />
        </Box>
      )}
    </Paper>
  );
}