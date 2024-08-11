import React, { useState, useContext, useEffect } from 'react';
import { useParams, useLocation, useNavigate } from 'react-router-dom';
import Resource from '../../../common/resource/Resource';
import { UserContext } from '../../../../contexts/UserContext';
import { getActivityResources } from '../../../../services/resourcesService';
import { Container, Box, Typography, List } from '@mui/material';
import SchoolIcon from '@mui/icons-material/School';
import MenuBookIcon from '@mui/icons-material/MenuBook';

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
            <Resource
              key={resource.id}
              resource={resource}
              isTeacher={false}
            />
          ))}
        </List>
      </Box>
    </Container>
  );
}