import React, { useState, useEffect, useContext } from 'react';
import { useNavigate, NavLink } from 'react-router-dom';
import {
  AppBar,
  Box,
  Toolbar,
  Typography,
  Button,
  CssBaseline,
  IconButton,
  Drawer,
  List,
  ListItem,
  Divider,
  Tooltip
} from '@mui/material';
import { UserContext } from '../../contexts/UserContext';
import { RoleContext } from '../../contexts/RoleContext';
import { getHeaderData } from '../../services/studentService';
import logo from '../../assets/img/logo.webp';
import HomeIcon from '@mui/icons-material/Home';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import LogoutIcon from '@mui/icons-material/Logout';

const drawerWidth = 300;

const mainNavItemIconsFontSize = 35;

const navItems = [
  { label: 'Персонални данни', path: '/students/personal-data' },
  { label: 'Изучавани дисциплини', path: '/students/subjects' },
  { label: 'Молби / Заявления', path: '/students/request-applications' },
  { label: 'Индивидуални протоколи', path: '/students/individual-protocols' },
  { label: 'Заверени семестри', path: '/students/certified-semesters' },
  { label: 'Платени такси', path: '/students/payed-taxes' },
  { label: 'График на занятията', path: '/students/schedule/specialties' },
  { label: 'Филтриран график на занятията', path: '/students/schedule/filtered' },
  { label: 'График на изпитите', path: '/students/exams-schedule' }
];

const mainNavItems = [
  { label: 'Начало', path: '/students/home', icon: <HomeIcon sx={{ fontSize: mainNavItemIconsFontSize }} /> },
  { label: 'Учебни ресурси', path: '/students/activities', icon: <MenuBookIcon sx={{ fontSize: mainNavItemIconsFontSize }} /> },
];

export default function StudentNavigation() {
  const [headerData, setHeaderData] = useState({
    pin: 0,
    planNumber: 0,
    facultyNumber: 0,
    fullName: '',
    groupNumber: 1,
    specialtyName: '',
    specialtyEducationType: '',
    certifiedSemesterCount: 0,
    universityEmail: ''
  });

  const [isDrawerOpen, setIsDrawerOpen] = useState(false);
  const { bearerToken, userLogout } = useContext(UserContext);
  const { setUserRole } = useContext(RoleContext);

  const navigate = useNavigate();

  useEffect(() => {
    getHeaderData(bearerToken)
      .then(data => setHeaderData(data))
      .catch(error => {
        console.log(error);
        navigate('/login');
      });
  }, [bearerToken]);

  const handleDrawerToggle = () => {
    setIsDrawerOpen(!isDrawerOpen);
  };

  const handleLogout = () => {
    userLogout();
    setUserRole({});
    navigate('/login');
  };

  return (
    <>
      <CssBaseline />
      <AppBar position="fixed" sx={{ top: 0, left: 0, right: 0 }}>
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerToggle}
            sx={{
              mr: 2,
              '&:focus': {
                outline: 'none',
              },
            }}
          >
            <img
              src={logo}
              alt="logo"
              style={{
                width: 40,
                height: 40,
                borderRadius: '50%',
                border: 'none',
                outline: 'none',
                cursor: 'pointer',
              }}
            />
          </IconButton>
          <Box sx={{ display: 'flex', alignItems: 'center', flexGrow: 1 }}>
            <Box
              sx={{
                display: 'flex',
                flexDirection: 'column',
                flexGrow: 1,
                alignItems: 'flex-start',
                maxWidth: '70%',
              }}
            >
              <Typography variant="body2" sx={{ fontWeight: 'bold', display: 'flex', flexWrap: 'wrap' }}>
                ПИН: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.pin}</span>
                <span style={{ marginLeft: 20 }}></span>План №: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.planNumber}</span>
                <span style={{ marginLeft: 20 }}></span>Факултетен №: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.facultyNumber}</span>
                <span style={{ marginLeft: 20 }}></span>Име: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.fullName}</span>
              </Typography>
              <Typography variant="body2" sx={{ fontWeight: 'bold', display: 'flex', flexWrap: 'wrap' }}>
                Група: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.groupNumber}</span>
                <span style={{ marginLeft: 20 }}></span>Статус: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.status}</span>
                <span style={{ marginLeft: 20 }}></span>Специалност: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.specialtyName}</span>
              </Typography>
              <Typography variant="body2" sx={{ fontWeight: 'bold', display: 'flex', flexWrap: 'wrap' }}>
                Форма: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.specialtyEducationType}</span>
                <span style={{ marginLeft: 20 }}></span>Брой заверки: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.certifiedSemesterCount}</span>
                <span style={{ marginLeft: 20 }}></span>Служебна поща: <span style={{ fontWeight: 'normal', marginLeft: 4 }}>{headerData.universityEmail}</span>
              </Typography>
            </Box>
          </Box>
          <Box sx={{ display: 'flex' }}>
            {mainNavItems.map((item) => (
            <Tooltip key={item.label} title={<span style={{ fontSize: '1.2em' }}>{item.label}</span>}  arrow>
              <Button
                key={item.label}
                color="inherit"
                component={NavLink}
                to={item.path}
                className="nav-item nav-link"
                sx={{
                  display: 'flex',
                  alignItems: 'center',
                  margin: '0 10px', // Adjust the margin as needed
                  '&:hover': {
                    backgroundColor: 'rgba(255, 255, 255, 0.2)', // Lighter hover effect
                    color: 'white',
                  }
                }}
              >
                {item.icon}
              </Button>
            </Tooltip>
            ))}
            <Tooltip title={<span style={{ fontSize: '1.2em' }}>Изход</span>}  arrow>
              <Button
                color="inherit"
                component={NavLink}
                to="/login"
                onClick={(e) => {
                  e.preventDefault(); // Prevents navigation
                  handleLogout();
                }}
                className="nav-item nav-link"
                sx={{
                  display: 'flex',
                  alignItems: 'center',
                  margin: '0 10px', // Adjust the margin as needed
                  '&:hover': {
                    backgroundColor: 'rgba(255, 255, 255, 0.2)', // Lighter hover effect
                    color: 'white',
                  },
                }}
              >
                <LogoutIcon sx={{ fontSize: mainNavItemIconsFontSize }} />
              </Button>
            </Tooltip>
          </Box>
        </Toolbar>
      </AppBar>
      <Drawer
        variant="temporary"
        open={isDrawerOpen}
        onClose={handleDrawerToggle}
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
        }}
      >
        <Toolbar />
        <Box sx={{ display: 'flex', justifyContent: 'center', p: 2 }}>
          <img
            src={logo}
            alt="logo"
            style={{
              width: 80,
              height: 80,
              borderRadius: '50%', // Apply rounded corners
              border: 'none', // Ensure there's no border
              outline: 'none', // Ensure there's no outline
            }}
          />
        </Box>
        <Divider />
        <List>
          {navItems.map((item) => (
            <ListItem key={item.label} sx={{ py: 0 }}>
              <Button
                component={NavLink}
                to={item.path}
                fullWidth
                sx={{
                  justifyContent: 'flex-start',
                  color: 'inherit',
                  textTransform: 'none',
                  fontWeight: 'bold',
                  padding: '10px 20px',
                  borderRadius: '4px',
                  transition: 'background-color 0.3s, box-shadow 0.3s',
                  '&:hover': {
                    backgroundColor: 'rgba(0, 0, 0, 0.08)',
                    boxShadow: '0 3px 5px rgba(0, 0, 0, 0.2)',
                  },
                  '&.active': {
                    backgroundColor: 'rgba(0, 0, 0, 0.1)',
                    fontWeight: 'bold',
                    boxShadow: '0 3px 5px rgba(0, 0, 0, 0.2)',
                  },
                }}
                onClick={handleDrawerToggle}
              >
                {item.label}
              </Button>
            </ListItem>
          ))}
        </List>
      </Drawer>
    </>
  );
}
