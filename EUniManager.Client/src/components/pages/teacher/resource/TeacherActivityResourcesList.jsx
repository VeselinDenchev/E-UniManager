import React, { useState, useContext, useEffect, useRef } from 'react';
import { useParams, useLocation, useNavigate } from 'react-router-dom';
import Resource from '../../../common/resource/Resource';
import {
  Container,
  Box,
  Typography,
  List,
  TextField,
  Snackbar,
  Alert,
  CircularProgress,
  MenuItem
} from '@mui/material';
import AddButton from '../../../common/buttons/AddButton';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import LocalLibraryIcon from '@mui/icons-material/LocalLibrary';
import Modal from '../../../common/modals/Modal';
import ConfirmationModal from '../../../common/modals/ConfirmationModal';
import { UserContext } from '../../../../contexts/UserContext';
import { getActivityResources, createResource, deleteResource, updateResource } from '../../../../services/resourcesService';
import { createAssignment, updateAssignment } from '../../../../services/assignmentService';
import { getBase64 } from '../../../../utils/fileUtils';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import DownloadButton from '../../../common/buttons/DownloadButton';
import DeleteButton from '../../../common/buttons/DeleteButton';
import UploadButton from '../../../common/buttons/UploadButton';
import SaveButton from '../../../common/buttons/SaveButton';

const emptyAssignment = {
  startDate: null,
  dueDate: null,
  type: 'Text' // Default internal value
};

// Reverse mapping to convert back from display value to internal value
const typeMapping = {
  'Текст': 'Text',
  'Файл': 'File'
};

export default function TeacherActivityResourcesList() {
  const { activityId } = useParams();
  const [resources, setResources] = useState([]);
  const { bearerToken } = useContext(UserContext);
  const location = useLocation();
  const { activity } = location.state || {};
  const navigate = useNavigate();
  const [title, setTitle] = useState('');
  const [resourceType, setResourceType] = useState('Info');
  const [info, setInfo] = useState('');
  const [uploadedFile, setUploadedFile] = useState(null);
  const [currentFileName, setCurrentFileName] = useState('');
  const [currentFileId, setCurrentFileId] = useState(null);
  const [isFileChanged, setIsFileChanged] = useState(false);
  const [loading, setLoading] = useState(false);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState('');
  const [snackbarSeverity, setSnackbarSeverity] = useState('success');
  const fileInputRef = useRef(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [resourceToDelete, setResourceToDelete] = useState(null);
  const [resourceToDeleteName, setResourceToDeleteName] = useState('');
  const [isEditMode, setIsEditMode] = useState(false);
  const [editingResourceId, setEditingResourceId] = useState(null);
  const [resourceAssignment, setResourceAssignment] = useState(emptyAssignment);

  useEffect(() => {
    fetchResources();
  }, [activityId, bearerToken, navigate]);

  const fetchResources = async () => {
    try {
      const data = await getActivityResources(activityId, bearerToken);
      setResources(data);
    } catch (error) {
      console.log(error);
      navigate('/login'); // Redirect to login or error page
    }
  };

  const handleFileChange = async (event) => {
    const selectedFile = event.target.files[0];
    console.log('File selected:', selectedFile);
    setUploadedFile(selectedFile);
    setIsFileChanged(true); // Set the flag to true when a new file is uploaded
    fileInputRef.current.value = '';
  };

  const handleAddResource = async () => {
    setLoading(true); // Start loading animation
    try {
      let base64String = '';
      let mimeType = '';
      if (uploadedFile) {
        const result = await getBase64(uploadedFile);
        base64String = result.base64String;
        mimeType = result.mimeType;
      }

      const data = {
        activityId: activityId,
        title: resourceType === 'Assignment' ? null : title,
        resourceType: resourceType,
        info: resourceType === 'Assignment' ? null : info,
        file: uploadedFile
          ? {
              bytes: base64String,
              mimeType: mimeType,
            }
          : null,
      };

      console.log('Creating new resource:', data);

      await createResource(data, bearerToken);

      await fetchResources();

      if (resourceType === 'Assignment') {
        const updatedResources = await getActivityResources(activityId, bearerToken);
        const latestResource = updatedResources[updatedResources.length - 1];
        const assignment = {
          resourceId: latestResource.id,
          title: title,
          type: resourceAssignment.type, // Correctly send type
          startDate: new Date(resourceAssignment.startDate.getTime() - (resourceAssignment.startDate.getTimezoneOffset() * 60000)).toISOString(),
          dueDate: new Date(resourceAssignment.dueDate.getTime() - (resourceAssignment.dueDate.getTimezoneOffset() * 60000)).toISOString(),
          description: info,
        };

        console.log('Creating new assignment:', assignment);

        await createAssignment(assignment, bearerToken);

        await fetchResources();
      }

      setSnackbarMessage('Новият ресурс е добавен успешно');
      setSnackbarSeverity('success');
      setSnackbarOpen(true);
    } catch (error) {
      console.error('Error creating resource:', error);
      setSnackbarMessage('Възникна грешка при добавянето на ресурса');
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
    } finally {
      setLoading(false); // Stop loading animation
      setTitle('');
      setResourceType('Info');
      setInfo('');
      setUploadedFile(null); // Reset the file input for re-upload
      setResourceAssignment(emptyAssignment);
      setIsModalOpen(false); // Close the modal
    }
  };

  const handleEditResource = async () => {
    setLoading(true); // Start loading animation
    try {
      if (resourceType === 'Assignment') {
        const assignmentData = {
          id: editingResourceId,
          title: title,
          startDate: new Date(resourceAssignment.startDate.getTime() - (resourceAssignment.startDate.getTimezoneOffset() * 60000)).toISOString(),
          dueDate: new Date(resourceAssignment.dueDate.getTime() - (resourceAssignment.dueDate.getTimezoneOffset() * 60000)).toISOString(),
          description: info,
        };

        console.log('Updating assignment:', assignmentData);

        await updateAssignment(editingResourceId, assignmentData, bearerToken);
      } else {
        let base64String = '';
        let mimeType = '';
        if (uploadedFile) {
          const result = await getBase64(uploadedFile);
          base64String = result.base64String;
          mimeType = result.mimeType;
        }

        const resourceData = {
          activityId: activityId,
          title: title,
          resourceType: resourceType,
          info: info,
          isFileChanged: isFileChanged, // Add the flag to the payload
          file: uploadedFile
            ? {
                bytes: base64String,
                mimeType: mimeType,
              }
            : null,
        };

        console.log('Updating resource:', resourceData);

        await updateResource(editingResourceId, resourceData, bearerToken);
      }

      setSnackbarMessage('Ресурсът беше успешно обновен');
      setSnackbarSeverity('success');
      setSnackbarOpen(true);

      await fetchResources();
    } catch (error) {
      console.error('Error updating resource:', error);
      setSnackbarMessage('Възникна грешка при обновяването на ресурса');
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
      throw error; // Rethrow the error to propagate it further
    } finally {
      setLoading(false); // Stop loading animation
      handleCloseModal(); // Close the modal and reset the state
    }
  };

  const handleUpdateResource = (resource) => {
    setTitle(resource.assignment ? resource.assignment.title : resource.title);
    setResourceType(resource.resourceType);
    setInfo(resource.assignment ? resource.assignment.description : resource.info);
    setUploadedFile(null);
    setIsEditMode(true);
    setEditingResourceId(resource.assignment ? resource.assignment.id : resource.id);
    setIsModalOpen(true);

    if (resource.file) {
      setCurrentFileName(`${resource.file.id}${resource.file.extension.startsWith('.') ? '' : '.'}${resource.file.extension}`);
      setCurrentFileId(resource.file.id); // Set current file id
    } else {
      setCurrentFileName('');
      setCurrentFileId(null);
    }

    if (resource.assignment) {
      setResourceAssignment({
        startDate: resource.assignment.startDate ? new Date(resource.assignment.startDate) : null,
        dueDate: resource.assignment.dueDate ? new Date(resource.assignment.dueDate) : null,
        type: typeMapping[resource.assignment.type] || '' // Ensure the correct internal value is set
      });
    } else {
      setResourceAssignment(emptyAssignment); // Reset to default if no assignment
    }
  };

  const openAddResourceModal = () => {
    setIsModalOpen(true);
    setIsEditMode(false);
    setTitle('');
    setResourceType('Info');
    setInfo('');
    setUploadedFile(null);
    setCurrentFileName(''); // Reset current file name
    setCurrentFileId(null); // Reset current file id
    setResourceAssignment(emptyAssignment);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  }

  const handleExitModal = () => {
    setIsEditMode(false);
    setEditingResourceId(null);
    setTitle('');
    setResourceType('Info');
    setInfo('');
    setUploadedFile(null);
    setIsFileChanged(false); // Reset the file changed flag
    setResourceAssignment(emptyAssignment);
    setCurrentFileName(''); // Reset current file name
    setCurrentFileId(null); // Reset current file id
  }

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  const handleDeleteResource = async () => {
    setLoading(true);
    try {
      await deleteResource(resourceToDelete, bearerToken);

      setSnackbarMessage('Ресурсът беше успешно изтрит');
      setSnackbarSeverity('success');
      setSnackbarOpen(true);

      await fetchResources();
    } catch (error) {
      console.error('Error deleting resource:', error);
      setSnackbarMessage('Възникна грешка при изтриването на ресурса');
      setSnackbarSeverity('error');
      setSnackbarOpen(true);
    } finally {
      setLoading(false);
      setIsDialogOpen(false);
      setTimeout(() => {
        setResourceToDelete(null);
        setResourceToDeleteName('');
      }, 300); // Delay clearing the state to allow the dialog to close
    }
  };

  const handleDeleteSelectedFile = () => {
    setUploadedFile(null);
    setCurrentFileName('');
    setCurrentFileId(null);
    setIsFileChanged(true); // Set the flag to true when a file is deleted
  };

  const openDeleteDialog = (resourceId, resourceOrAssignmentTitle) => {
    setResourceToDelete(resourceId);
    setResourceToDeleteName(resourceOrAssignmentTitle);
    setIsDialogOpen(true);
  };

  const closeDeleteDialog = () => {
    setIsDialogOpen(false);
    setTimeout(() => {
      setResourceToDelete(null);
      setResourceToDeleteName('');
    }, 300); // Delay clearing the state to allow the dialog to close
  };

  const handleResourceTypeChange = (event) => {
    setResourceType(event.target.value);
    // Reset dates when resource type changes
    setResourceAssignment(emptyAssignment);
  };

  const handleAssignmentTypeChange = (event) => {
    const selectedType = event.target.value;
    setResourceAssignment(prevState => ({
      ...prevState,
      type: selectedType // Set the display value
    }));
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Container maxWidth="lg" sx={{ mt: 4, mb: 4, paddingTop: 4, paddingLeft: 2, paddingRight: 2 }}>
        <Box sx={{ textAlign: 'left' }}>
          <Typography variant="h4" gutterBottom>Учебен курс</Typography>
          <Box display="flex" alignItems="center">
            <MenuBookIcon sx={{ marginRight: 1, verticalAlign: 'middle', fontSize: '1.5rem' }} />
            <Typography variant="h5">{activity?.courseName}</Typography>
          </Box>
          <Box display="flex" alignItems="center">
            <LocalLibraryIcon sx={{ marginRight: 1, verticalAlign: 'middle', fontSize: '1.25rem' }} />
            <Typography variant="h6">{activity?.specialtyName}</Typography>
          </Box>
        </Box>

        <Box sx={{ mb: 2, textAlign: 'right', paddingTop: 5 }}>
          <AddButton onClick={openAddResourceModal}>Добавяне на нов ресурс</AddButton>
        </Box>

        <Modal
          title={isEditMode ? 'Редактиране на ресурс' : 'Добавяне на нов ресурс'}
          loading={loading}
          isOpen={isModalOpen}
          onClose={handleCloseModal}
          onExited={handleExitModal}
          footer={<SaveButton onClick={isEditMode ? handleEditResource : handleAddResource} disabled={loading} />}
        >
          <TextField
            fullWidth
            variant="outlined"
            label="Заглавие"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            sx={{ mt: 2 }}
            disabled={loading}
          />
          <TextField
            select
            fullWidth
            variant="outlined"
            label="Тип ресурс"
            value={resourceType}
            onChange={handleResourceTypeChange}
            sx={{ mt: 2 }}
            disabled={loading} // No disabling based on edit mode
          >
            <MenuItem value="Info">Информация</MenuItem>
            <MenuItem value="Assignment">Задача</MenuItem>
          </TextField>
          <TextField
            fullWidth
            multiline
            rows={4}
            variant="outlined"
            label="Описание"
            value={info}
            onChange={(e) => setInfo(e.target.value)}
            sx={{ mt: 2 }}
            disabled={loading}
          />
          {resourceType === 'Assignment' && (
            <>
              <TextField
                select
                fullWidth
                variant="outlined"
                label="Тип задача"
                value={resourceAssignment.type} // Use the internal value for selection
                onChange={handleAssignmentTypeChange} // Directly update the type with display value
                sx={{ mt: 2 }}
                disabled={loading || isEditMode}
              >
                <MenuItem value="Text">Текст</MenuItem>
                <MenuItem value="File">Файл</MenuItem>
              </TextField>
              <Box sx={{ display: 'flex', justifyContent: 'space-between', gap: 2, mt: 2 }}>
                <DateTimePicker
                  label="Начална дата"
                  value={resourceAssignment.startDate}
                  onChange={(date) => setResourceAssignment(prevState => ({...prevState, startDate: date}))}
                  textField={(params) => <TextField {...params} fullWidth disabled={loading} />}
                  ampm={false} // 24-hour format
                />
                <DateTimePicker
                  label="Крайна дата"
                  value={resourceAssignment.dueDate}
                  onChange={(date) => setResourceAssignment(prevState => ({...prevState, dueDate: date}))}
                  renderInput={(params) => <TextField {...params} fullWidth disabled={loading} />}
                  ampm={false} // 24-hour format
                />
              </Box>
            </>
          )}
          <Box sx={{ display: 'flex', alignItems: 'center', mt: 2 }}>
            {resourceType !== 'Assignment' && (
              <>
                <Typography variant="body1">
                  <span style={{ fontWeight: 'bold' }}>Файл:</span> {currentFileName || uploadedFile ? uploadedFile ? uploadedFile.name : currentFileName : 'Няма качен файл'}
                </Typography>
                <UploadButton marginLeft='1rem' inputRef={fileInputRef} onChange={handleFileChange} disabled={loading} />
                {currentFileName && !uploadedFile && <DownloadButton fileId={currentFileId} marginLeft='0.5rem' disabled={loading} />}
                {(currentFileName || uploadedFile) && <DeleteButton onClick={handleDeleteSelectedFile} disabled={loading} />}
              </>
            )}
          </Box>
        </Modal>

        <Box sx={{ textAlign: 'left', paddingTop: 2 }}>
          <List>
            {resources.map((resource) => (
              <Resource
                key={resource.id}
                resource={resource}
                handleEdit={handleUpdateResource}
                handleDelete={() => openDeleteDialog(resource.id, resource.title ?? resource.assignment.title)}
                isTeacher={true}
              />
            ))}
          </List>
        </Box>

        <ConfirmationModal
          open={isDialogOpen}
          onClose={closeDeleteDialog}
          text={`Сигурни ли сте, че искате да изтриете ресурса ${resourceToDeleteName}?`}
          onConfirm={handleDeleteResource}
        />

        <Snackbar
          open={snackbarOpen}
          autoHideDuration={6000}
          onClose={handleSnackbarClose}
        >
          <Alert onClose={handleSnackbarClose} severity={snackbarSeverity} sx={{ width: '100%' }}>
            {snackbarMessage}
          </Alert>
        </Snackbar>
        {loading && (
          <Box
            sx={{
              position: 'fixed',
              top: 0,
              left: 0,
              width: '100%',
              height: '100%',
              backgroundColor: 'rgba(0, 0, 0, 0.5)',
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
              zIndex: 9999,
            }}
          >
            <CircularProgress />
          </Box>
        )}
      </Container>
    </LocalizationProvider>
  );
}