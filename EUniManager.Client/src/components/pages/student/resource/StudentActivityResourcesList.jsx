import React, { useState, useContext, useEffect } from 'react';
import { useParams, useLocation, useNavigate, Link } from 'react-router-dom';
import { 
    Container,
    Box,
    Typography,
    List,
    ListItem,
    ListItemText,
    ListItemIcon,
    IconButton,
    Paper
} from '@mui/material';
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile';
import CreateIcon from '@mui/icons-material/Create';
import DownloadIcon from '@mui/icons-material/Download';
import VisibilityIcon from '@mui/icons-material/Visibility';
import SchoolIcon from '@mui/icons-material/School';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import { UserContext } from '../../../../contexts/UserContext';
import { getActivityResources } from '../../../../services/resourcesService';
import { getIcon } from '../../../../utils/fileUtils';
import { download } from '../../../../services/fileService';

const iconButtonStyle = {
  borderRadius: '50%',
  padding: '12px',
  color: 'white',
};

const hoverStyles = {
  downloadButton: {
    backgroundColor: '#4caf50',
    '&:hover': {
      backgroundColor: '#388e3c',
      color: 'white'
    }
  },
  visibilityButton: {
    backgroundColor: '#64b5f6',
    '&:hover': {
      backgroundColor: '#42a5f5',
      color: 'white'
    }
  }
};

export default function StudentActivityResourcesList() {
  const { activityId } = useParams();
  const [resources, setResources] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const location = useLocation();
  const { activity } = location.state || {};
  const navigate = useNavigate();

  useEffect(() => {
    getActivityResources(activityId, bearerToken)
      .then(data => setResources(data))
      .catch(error => {
        console.log(error);
        navigate('/login'); // change with error page
      });
  }, [activityId, bearerToken, navigate]);

  const handleDownload = async (fileId) => {
    try {
      await download(fileId, bearerToken);
    } catch (error) {
      console.error('Error downloading file:', error);
    }
  };

  return (
    <Container maxWidth="lg" sx={{ mt: 4, mb: 4, paddingTop: 4, paddingLeft: 2, paddingRight: 2 }}>
      <Box sx={{ textAlign: 'left' }}>
        <Typography variant="h4" gutterBottom>Учебен курс</Typography>
        <Box display="flex" alignItems="center">
          <MenuBookIcon sx={{ marginRight: 1, verticalAlign: 'middle', fontSize: '1.5rem' }} />
          <Typography variant="h5">{activity?.subjectCourseName}</Typography>
        </Box>
        <Box display="flex" alignItems="center">
          <SchoolIcon sx={{ marginRight: 1, verticalAlign: 'middle', fontSize: '1.25rem' }} />
          <Typography variant="h6">{activity?.teacherFullNameWithRank}</Typography>
        </Box>
      </Box>
      <Box sx={{ mb: 2, textAlign: 'left', paddingTop: 5 }}>
        <List>
          {resources.map(resource => (
            <Paper key={resource.id} sx={{ mb: 3, p: 2, border: '1px solid #ddd' }}>
              <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', position: 'relative' }}>
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
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  {resource.file && (
                    <IconButton 
                      sx={{ 
                        ...iconButtonStyle,
                        ...hoverStyles.downloadButton
                      }}
                      onClick={() => handleDownload(resource.file.id)}
                    >
                      <DownloadIcon />
                    </IconButton>
                  )}
                </Box>
              </Box>
              {resource.assignment && (
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mt: 2, position: 'relative' }}>
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
                            {`Срок за предаване от: ${resource.assignment.startDate} до: ${resource.assignment.dueDate}`}
                          </Typography>
                        </>
                      }
                      sx={{ wordBreak: 'break-word' }}
                    />
                  </ListItem>
                  <IconButton 
                    sx={{ 
                      ...iconButtonStyle,
                      ...hoverStyles.visibilityButton
                    }}
                    component={Link}
                    to={`/students/assignments/${resource.assignment.id}`}
                  >
                    <VisibilityIcon />
                  </IconButton>
                </Box>
              )}
            </Paper>
          ))}
        </List>
      </Box>
    </Container>
  );
}