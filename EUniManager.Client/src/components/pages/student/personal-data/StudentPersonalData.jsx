import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate} from 'react-router-dom';
import { Container, Box, Typography, TextField, Grid, Paper } from '@mui/material';
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
        firstName: '',
        middleName: '',
        lastName: '',
        uniqueIdentifier: {
            uniqueIdentifierType: '',
            identifier: ''
        },
        insuranceNumber: '',
        birthDate: '',
        gender: '',
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
        mark: 6
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
  })

  const { bearerToken } = useContext(UserContext);

  const navigate = useNavigate();

  useEffect(() => {
      getDetails(bearerToken)
      .then(data => setStudentData(data))
      .catch(error => {
          console.log(error);
          navigate('/login'); // change with error page
      });
}, [bearerToken, navigate]);

  return (
    <Container>
      <Box sx={{ mt: 12, mb: 8 }}>
        
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Typography variant="h6" gutterBottom>Служебни данни</Typography>
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
        </Paper>
        
        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#e8e8e8' }}>
          <Typography variant="h6" gutterBottom>Лични данни</Typography>
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
        </Paper>

        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Typography variant="h6" gutterBottom>Постоянно местоживеене</Typography>
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
        </Paper>

        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#e8e8e8' }}>
          <Typography variant="h6" gutterBottom>Временен адрес</Typography>
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
        </Paper>

        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Typography variant="h6" gutterBottom>Обичайно местоживеене</Typography>
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
        </Paper>

        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#e8e8e8' }}>
          <Typography variant="h6" gutterBottom>Записване</Typography>
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
        </Paper>

        <Paper elevation={6} sx={{ p: 3, mb: 4, backgroundColor: '#f7f7f7' }}>
          <Typography variant="h6" gutterBottom>Притежава диплома</Typography>
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
        </Paper>
      </Box>
    </Container>
  );
}
