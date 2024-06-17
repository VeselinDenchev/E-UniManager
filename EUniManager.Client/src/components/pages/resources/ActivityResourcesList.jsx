import React from 'react';
import { useState, useContext, useEffect } from 'react';
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
import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf';
import ImageIcon from '@mui/icons-material/Image';
import DescriptionIcon from '@mui/icons-material/Description';
import InsertDriveFileIcon from '@mui/icons-material/InsertDriveFile';
import CreateIcon from '@mui/icons-material/Create';
import DownloadIcon from '@mui/icons-material/Download';
import VisibilityIcon from '@mui/icons-material/Visibility';
import SchoolIcon from '@mui/icons-material/School';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import { UserContext } from '../../../contexts/UserContext';
import { getActivityResources } from '../../../services/resourcesService';
import { download } from '../../../services/fileService';

const getIcon = (type) => {
  console.log(type);
  switch (type) {
    case 'pdf':
      return <PictureAsPdfIcon sx={{ color: '#ff0000', fontSize: 40 }} />;
    case 'png':
    case 'jpg':
    case 'gif':
    case 'tiff':
      return <ImageIcon sx={{ color: '#4caf50', fontSize: 40 }} />;
    case 'docx':
      return <DescriptionIcon sx={{ color: '#3b5998', fontSize: 40 }} />;
    case 'xslx':
      return <InsertDriveFileIcon sx={{ color: '#0d820d', fontSize: 40 }} />;
    case 'pptx':
      return <InsertDriveFileIcon sx={{ color: '#d24726', fontSize: 40 }} />;
    default:
      return <InsertDriveFileIcon sx={{ fontSize: 40 }} />;
  }
};

export default function ActivityResourcesList() {
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
          <MenuBookIcon sx={{ marginRight: 1, verticalAlign: 'middle', fontSize: '1.5rem' }} /> {/* Subject icon */}
          <Typography variant="h5" gutterBottom>{activity?.subjectCourseName}</Typography>
        </Box>
        <Box display="flex" alignItems="center">
          <SchoolIcon sx={{ marginRight: 1, verticalAlign: 'middle', fontSize: '1.25rem' }} /> {/* Teacher icon */}
          <Typography variant="h6" gutterBottom>{activity?.teacherFullNameWithRank}</Typography>
        </Box>
      </Box>
      <Box sx={{ mb: 2, textAlign: 'left', paddingTop: 5 }}>
        <List>
          {resources.map(resource => (
            <Paper key={resource.id} sx={{ mb: 3, p: 2, border: '1px solid #ddd' }}>
              <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', position: 'relative' }}>
                <ListItem sx={{ flexGrow: 1 }}>
                  <ListItemIcon sx={{ minWidth: '50px' }}>
                    {resource.file ? getIcon(resource.file.extension) : <InsertDriveFileIcon sx={{ fontSize: 40 }} />}
                  </ListItemIcon>
                  <ListItemText 
                    primary={resource.title} 
                    secondary={resource.info}
                    sx={{ wordBreak: 'break-word' }}
                  />
                </ListItem>
                {resource.file && (
                  <IconButton 
                    sx={{ 
                      backgroundColor: '#4caf50', 
                      color: 'white', 
                      '&:hover': {
                        backgroundColor: '#388e3c',
                        color: 'white'
                      }
                    }}
                    onClick={() => handleDownload(resource.file.id)}
                  >
                    <DownloadIcon />
                  </IconButton>
                )}
              </Box>
              {resource.assignment && (
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', mt: 2, position: 'relative' }}>
                  <ListItem sx={{ flexGrow: 1 }}>
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
                      backgroundColor: '#64b5f6', 
                      color: 'white', 
                      '&:hover': {
                        backgroundColor: '#42a5f5',
                        color: 'white'
                      }
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