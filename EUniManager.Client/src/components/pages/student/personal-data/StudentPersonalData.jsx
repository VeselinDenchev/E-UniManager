import React, { useState, useContext, useEffect } from 'react';
import { Container, Box, Typography, TextField, Grid, Paper, IconButton, Collapse } from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { UserContext } from '../../../../contexts/UserContext';
import { getDetails } from '../../../../services/studentService';

export default function StudentPersonalData() {
  const [studentData, setStudentData] = useState({
    serviceData: {
      pin: '',
      studentStatus: '',
      planNumber: '',
      facultyNumber: '',
      groupNumber: '',
      enrolledInSemester: ''
    },
    personalData: {
      cityArea: {
        city: '',
        area: ''
      },
      gender: '',
      firstName: '',
      middleName: '',
      lastName: '',
      uniqueIdentifier: {
        uniqueIdentifierType: '',
        identifier: ''
      },
      insuranceNumber: '',
      birthDate: '',
      citizenship: '',
      idCard: {
        idNumber: '',
        dateIssued: ''
      },
      email: ''
    },
    permanentResidence: {
      cityArea: {
        city: '',
        area: ''
      },
      street: '',
      phoneNumber: ''
    },
    temporaryResidence: {
      cityArea: {
        city: '',
        area: ''
      },
      street: '',
      phoneNumber: ''
    },
    usualResidenceCountry: '',
    enrollment: {
      date: '',
      reason: '',
      mark: ''
    },
    diplomaOwned: {
      educationalAndQualificationDegree: '',
      series: '',
      number: '',
      registrationNumber: '',
      date: '',
      year: '',
      issuedByInstitutionType: '',
      institutionName: '',
      cityArea: {
        city: '',
        area: ''
      },
      specialty: '',
      professionalQualification: ''
    }
  });

  const { bearerToken } = useContext(UserContext);
  const [openSections, setOpenSections] = useState({
    serviceData: true,
    personalData: true,
    permanentResidence: true,
    temporaryResidence: true,
    usualResidenceCountry: true,
    enrollment: true,
    diplomaOwned: true,
  });

  const handleToggle = (section) => {
    setOpenSections((prevState) => ({
      ...prevState,
      [section]: !prevState[section],
    }));
  };

  useEffect(() => {
    getDetails(bearerToken)
      .then((data) => {
        // Ensure all null values are converted to empty strings to avoid the warning
        const sanitizedData = JSON.parse(
          JSON.stringify(data, (key, value) => (value === null ? '' : value))
        );
        setStudentData(sanitizedData);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [bearerToken]);

  return (
    <Container>
      <Box sx={{ mt: 12, mb: 8 }}>
        
        {/* Service Data Section */}
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Typography variant="h6">
              Служебни данни
            </Typography>
            <IconButton
              onClick={() => handleToggle('serviceData')}
              sx={{
                padding: 0,
                '&:hover': {
                  backgroundColor: 'transparent', // Remove hover background
                },
                '&:focus': {
                  outline: 'none', // Remove default focus outline
                },
                '&:active': {
                  backgroundColor: 'transparent', // Remove active state background
                },
              }}
            >
              <ExpandMoreIcon
                sx={{
                  transform: openSections.serviceData ? 'rotate(180deg)' : 'rotate(0deg)',
                  transition: 'transform 0.3s',
                  color: 'black',
                  borderRadius: 0, // Remove any border radius
                }}
              />
            </IconButton>
          </Box>
          <Collapse in={openSections.serviceData} sx={{ width: '100%', mt: openSections.serviceData ? 2 : 0 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="ПИН"
                  value={studentData.serviceData.pin}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Статус"
                  value={studentData.serviceData.studentStatus}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="План №"
                  value={studentData.serviceData.planNumber}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Факултетен №"
                  value={studentData.serviceData.facultyNumber}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Група"
                  value={studentData.serviceData.groupNumber}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Записан в сем"
                  value={studentData.serviceData.enrolledInSemester}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
            </Grid>
          </Collapse>
        </Paper>
        
        {/* Personal Data Section */}
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#e8e8e8' }}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Typography variant="h6">
              Лични данни
            </Typography>
            <IconButton
              onClick={() => handleToggle('personalData')}
              sx={{
                padding: 0,
                '&:hover': {
                  backgroundColor: 'transparent', // Remove hover background
                },
                '&:focus': {
                  outline: 'none', // Remove default focus outline
                },
                '&:active': {
                  backgroundColor: 'transparent', // Remove active state background
                },
              }}
            >
              <ExpandMoreIcon
                sx={{
                  transform: openSections.personalData ? 'rotate(180deg)' : 'rotate(0deg)',
                  transition: 'transform 0.3s',
                  color: 'black',
                  borderRadius: 0, // Remove any border radius
                }}
              />
            </IconButton>
          </Box>
          <Collapse in={openSections.personalData} sx={{ width: '100%', mt: openSections.personalData ? 2 : 0 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={4}>
                <TextField
                  fullWidth
                  label="Име"
                  value={studentData.personalData.firstName}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={4}>
                <TextField
                  fullWidth
                  label="Презиме"
                  value={studentData.personalData.middleName}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={4}>
                <TextField
                  fullWidth
                  label="Фамилия"
                  value={studentData.personalData.lastName}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label={studentData.personalData.uniqueIdentifier.uniqueIdentifierType}
                  value={studentData.personalData.uniqueIdentifier.identifier}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Лична карта"
                  value={studentData.personalData.idCard.idNumber}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="издадена на"
                  value={studentData.personalData.idCard.dateIssued}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Дата на раждане"
                  value={studentData.personalData.birthDate}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="в гр.(с.)"
                  value={`${studentData.personalData.cityArea.city}, ${studentData.personalData.cityArea.area}`}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Пол"
                  value={studentData.personalData.gender}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Гражданство"
                  value={studentData.personalData.citizenship}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Имейл"
                  value={studentData.personalData.email}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
            </Grid>
          </Collapse>
        </Paper>

        {/* Permanent Residence Section */}
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Typography variant="h6">
              Постоянно местоживеене
            </Typography>
            <IconButton
              onClick={() => handleToggle('permanentResidence')}
              sx={{
                padding: 0,
                '&:hover': {
                  backgroundColor: 'transparent', // Remove hover background
                },
                '&:focus': {
                  outline: 'none', // Remove default focus outline
                },
                '&:active': {
                  backgroundColor: 'transparent', // Remove active state background
                },
              }}
            >
              <ExpandMoreIcon
                sx={{
                  transform: openSections.permanentResidence ? 'rotate(180deg)' : 'rotate(0deg)',
                  transition: 'transform 0.3s',
                  color: 'black',
                  borderRadius: 0, // Remove any border radius
                }}
              />
            </IconButton>
          </Box>
          <Collapse in={openSections.permanentResidence} sx={{ width: '100%', mt: openSections.permanentResidence ? 2 : 0 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Гр. (с.)"
                  value={`${studentData.permanentResidence.cityArea.city}, ${studentData.permanentResidence.cityArea.area}`}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Улица"
                  value={studentData.permanentResidence.street}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Телефон"
                  value={studentData.permanentResidence.phoneNumber}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
            </Grid>
          </Collapse>
        </Paper>

        {/* Temporary Residence Section */}
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#e8e8e8' }}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Typography variant="h6">
              Временен адрес
            </Typography>
            <IconButton
              onClick={() => handleToggle('temporaryResidence')}
              sx={{
                padding: 0,
                '&:hover': {
                  backgroundColor: 'transparent', // Remove hover background
                },
                '&:focus': {
                  outline: 'none', // Remove default focus outline
                },
                '&:active': {
                  backgroundColor: 'transparent', // Remove active state background
                },
              }}
            >
              <ExpandMoreIcon
                sx={{
                  transform: openSections.temporaryResidence ? 'rotate(180deg)' : 'rotate(0deg)',
                  transition: 'transform 0.3s',
                  color: 'black',
                  borderRadius: 0, // Remove any border radius
                }}
              />
            </IconButton>
          </Box>
          <Collapse in={openSections.temporaryResidence} sx={{ width: '100%', mt: openSections.temporaryResidence ? 2 : 0 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Гр. (с.)"
                  value={`${studentData.temporaryResidence.cityArea.city}, ${studentData.temporaryResidence.cityArea.area}`}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Улица"
                  value={studentData.temporaryResidence.street}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Телефон"
                  value={studentData.temporaryResidence.phoneNumber}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
            </Grid>
          </Collapse>
        </Paper>

        {/* Usual Residence Section */}
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Typography variant="h6">
              Обичайно местоживеене
            </Typography>
            <IconButton
              onClick={() => handleToggle('usualResidenceCountry')}
              sx={{
                padding: 0,
                '&:hover': {
                  backgroundColor: 'transparent', // Remove hover background
                },
                '&:focus': {
                  outline: 'none', // Remove default focus outline
                },
                '&:active': {
                  backgroundColor: 'transparent', // Remove active state background
                },
              }}
            >
              <ExpandMoreIcon
                sx={{
                  transform: openSections.usualResidenceCountry ? 'rotate(180deg)' : 'rotate(0deg)',
                  transition: 'transform 0.3s',
                  color: 'black',
                  borderRadius: 0, // Remove any border radius
                }}
              />
            </IconButton>
          </Box>
          <Collapse in={openSections.usualResidenceCountry} sx={{ width: '100%', mt: openSections.usualResidenceCountry ? 2 : 0 }}>
            <Grid container spacing={3}>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  label="Държава"
                  value={studentData.usualResidenceCountry}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
            </Grid>
          </Collapse>
        </Paper>

        {/* Enrollment Section */}
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#e8e8e8' }}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Typography variant="h6">
              Записване
            </Typography>
            <IconButton
              onClick={() => handleToggle('enrollment')}
              sx={{
                padding: 0,
                '&:hover': {
                  backgroundColor: 'transparent', // Remove hover background
                },
                '&:focus': {
                  outline: 'none', // Remove default focus outline
                },
                '&:active': {
                  backgroundColor: 'transparent', // Remove active state background
                },
              }}
            >
              <ExpandMoreIcon
                sx={{
                  transform: openSections.enrollment ? 'rotate(180deg)' : 'rotate(0deg)',
                  transition: 'transform 0.3s',
                  color: 'black',
                  borderRadius: 0, // Remove any border radius
                }}
              />
            </IconButton>
          </Box>
          <Collapse in={openSections.enrollment} sx={{ width: '100%', mt: openSections.enrollment ? 2 : 0 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Дата на записване"
                  value={studentData.enrollment.date}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Основание"
                  value={studentData.enrollment.reason}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Оценка"
                  value={studentData.enrollment.mark}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
            </Grid>
          </Collapse>
        </Paper>

        {/* Diploma Section */}
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Box display="flex" alignItems="center" justifyContent="space-between">
            <Typography variant="h6">
              Притежава диплома
            </Typography>
            <IconButton
              onClick={() => handleToggle('diplomaOwned')}
              sx={{
                padding: 0,
                '&:hover': {
                  backgroundColor: 'transparent', // Remove hover background
                },
                '&:focus': {
                  outline: 'none', // Remove default focus outline
                },
                '&:active': {
                  backgroundColor: 'transparent', // Remove active state background
                },
              }}
            >
              <ExpandMoreIcon
                sx={{
                  transform: openSections.diplomaOwned ? 'rotate(180deg)' : 'rotate(0deg)',
                  transition: 'transform 0.3s',
                  color: 'black',
                  borderRadius: 0, // Remove any border radius
                }}
              />
            </IconButton>
          </Box>
          <Collapse in={openSections.diplomaOwned} sx={{ width: '100%', mt: openSections.diplomaOwned ? 2 : 0 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="OKСтепен"
                  value={studentData.diplomaOwned.educationalAndQualificationDegree}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={2}>
                <TextField
                  fullWidth
                  label="Серия"
                  value={studentData.diplomaOwned.series}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={2}>
                <TextField
                  fullWidth
                  label="№"
                  value={studentData.diplomaOwned.number}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={2}>
                <TextField
                  fullWidth
                  label="Рег. №"
                  value={studentData.diplomaOwned.registrationNumber}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Дата"
                  value={studentData.diplomaOwned.date}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Година"
                  value={studentData.diplomaOwned.year}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Издадена от"
                  value={studentData.diplomaOwned.issuedByInstitutionType}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="гр.(с.): "
                  value={`${studentData.diplomaOwned.cityArea.city}, ${studentData.diplomaOwned.cityArea.area}`}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Държава"
                  value="България" // Needs to be added to the backend
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Вид училище"
                  value={studentData.diplomaOwned.issuedByInstitutionType}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Специалност"
                  value={studentData.diplomaOwned.specialty}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                  fullWidth
                  label="Професионална квалификация"
                  value={studentData.diplomaOwned.professionalQualification}
                  InputProps={{
                    readOnly: true,
                    style: { color: 'black', fontWeight: 'bold' },
                  }}
                  disabled
                />
              </Grid>
            </Grid>
          </Collapse>
        </Paper>
      </Box>
    </Container>
  );
}