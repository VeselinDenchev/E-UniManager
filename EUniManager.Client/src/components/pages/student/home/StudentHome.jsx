import React from 'react';
import { Container, Box, Typography, Grid, Button, Card, CardContent, CardMedia } from '@mui/material';
import SupportIcon from '@mui/icons-material/Support';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import SchoolIcon from '@mui/icons-material/School';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import ContactMailIcon from '@mui/icons-material/ContactMail';

export default function StudentHome() {
  return (
    <Container sx={{ mt: 4, mb: 4, paddingTop: 4 }}>
      <Box sx={{ textAlign: 'center', mb: 4 }}>
        <Typography variant="h3" gutterBottom sx={{ fontWeight: 'bold' }}>
          Добре дошли в студентския портал!
        </Typography>
        <Typography variant="h5" gutterBottom sx={{ color: 'gray' }}>
          Вашето пространство за информация и ресурси
        </Typography>
      </Box>

      <Grid container spacing={4}>
        <Grid item xs={12} md={6}>
          <Card sx={{ display: 'flex', backgroundColor: '#f0f4f8', boxShadow: 3 }}>
            <CardMedia>
              <AccountCircleIcon sx={{ fontSize: 80, color: '#1976d2', m: 2 }} />
            </CardMedia>
            <CardContent>
              <Typography variant="h5" component="div" sx={{ fontWeight: 'bold' }}>
                Персонален профил
              </Typography>
              <Typography variant="body1" sx={{ color: 'gray' }}>
                Лесен достъп до вашите лични и академични данни.
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} md={6}>
          <Card sx={{ display: 'flex', backgroundColor: '#f0f4f8', boxShadow: 3 }}>
            <CardMedia>
              <SchoolIcon sx={{ fontSize: 80, color: '#1976d2', m: 2 }} />
            </CardMedia>
            <CardContent>
              <Typography variant="h5" component="div" sx={{ fontWeight: 'bold' }}>
                Учебни материали
              </Typography>
              <Typography variant="body1" sx={{ color: 'gray' }}>
                Достъп до лекции, упражнения и курсови проекти, организирани по дисциплини и курсове.
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} md={6}>
          <Card sx={{ display: 'flex', backgroundColor: '#f0f4f8', boxShadow: 3 }}>
            <CardMedia>
              <AccessTimeIcon sx={{ fontSize: 80, color: '#1976d2', m: 2 }} />
            </CardMedia>
            <CardContent>
              <Typography variant="h5" component="div" sx={{ fontWeight: 'bold' }}>
                Графици
              </Typography>
              <Typography variant="body1" sx={{ color: 'gray' }}>
                Вижте графици за лекции, изпити и други важни събития.
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} md={6}>
          <Card sx={{ display: 'flex', backgroundColor: '#f0f4f8', boxShadow: 3 }}>
            <CardMedia>
              <SupportIcon sx={{ fontSize: 80, color: '#1976d2', m: 2 }} />
            </CardMedia>
            <CardContent>
              <Typography variant="h5" component="div" sx={{ fontWeight: 'bold' }}>
                Поддръжка и помощ
              </Typography>
              <Typography variant="body1" sx={{ color: 'gray' }}>
                Винаги до вас при нужда от помощ или съвет.
              </Typography>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      <Box sx={{ textAlign: 'left', mt: 4 }}>
        <Typography variant="h4" gutterBottom>
          Защо да изберете нас?
        </Typography>
        <Typography variant="body1" gutterBottom sx={{ ml: 2 }}>
          <strong>Удобство:</strong> <span style={{ color: '#1976d2' }}>Всички важни ресурси и информация на едно място.</span>
        </Typography>
        <Typography variant="body1" gutterBottom sx={{ ml: 2 }}>
          <strong>Лесна навигация:</strong> <span style={{ color: '#1976d2' }}>Интуитивен интерфейс, създаден с мисъл за вас.</span>
        </Typography>
        <Typography variant="body1" gutterBottom sx={{ ml: 2 }}>
          <strong>Актуалност:</strong> <span style={{ color: '#1976d2' }}>Винаги актуални и точни данни за вашето образование.</span>
        </Typography>
        <Typography variant="body1" gutterBottom sx={{ ml: 2 }}>
          <strong>Поддръжка:</strong> <span style={{ color: '#1976d2' }}>Винаги до вас при нужда от помощ или съвет.</span>
        </Typography>
      </Box>

      <Box sx={{ textAlign: 'left', mt: 4 }}>
        <Typography variant="h4" gutterBottom>
          Свържете се с нас
        </Typography>
        <Box sx={{ display: 'flex', alignItems: 'center', ml: 2 }}>
          <ContactMailIcon sx={{ fontSize: 40, color: '#1976d2', mr: 2 }} />
          <Typography variant="body1">
            Ако имате въпроси или нужда от помощ, не се колебайте да се свържете с нашия екип за поддръжка. Ние сме тук, за да ви помогнем!
          </Typography>
        </Box>
      </Box>
      
    </Container>
  );
}