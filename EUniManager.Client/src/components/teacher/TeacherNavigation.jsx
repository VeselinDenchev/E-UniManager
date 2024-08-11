import React, { useContext } from 'react';
import { useNavigate, NavLink } from 'react-router-dom';
import { AppBar, Box, Toolbar, Button, CssBaseline, IconButton, Tooltip } from '@mui/material';
import HomeIcon from '@mui/icons-material/Home';
import ScheduleIcon from '@mui/icons-material/Schedule';
import MenuBookIcon from '@mui/icons-material/MenuBook';
import LogoutIcon from '@mui/icons-material/Logout';
import { UserContext } from '../../contexts/UserContext';
import { RoleContext } from '../../contexts/RoleContext';
import logo from '../../assets/img/logo.webp'; // Adjust the import path as necessary

const mainNavItems = [
  { label: 'Начало', path: '/teachers/home', icon: <HomeIcon sx={{ fontSize: 30 }} /> },
  { label: 'График на занятията', path: '/teachers/schedule', icon: <ScheduleIcon sx={{ fontSize: 30 }} /> },
  { label: 'Учебни ресурси', path: '/teachers/activities', icon: <MenuBookIcon sx={{ fontSize: 30 }} /> },
];

export default function TeacherNavigation() {
  const { userLogout } = useContext(UserContext);
  const { setUserRole } = useContext(RoleContext);
  const navigate = useNavigate();

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
            edge="start"
            sx={{
              mr: 2,
              cursor: 'default',
              '&:focus': {
                outline: 'none'
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
                outline: 'none'
              }}
            />
          </IconButton>
          <Box sx={{ display: 'flex', flexGrow: 1, justifyContent: 'flex-end' }}>
            {mainNavItems.map((item) => (
              <Tooltip 
                key={item.label} 
                title={<span style={{ fontSize: '1.2em' }}>{item.label}</span>} 
                arrow
                sx={{
                  fontSize: '1.2em' // Increase font size
                }}
              >
                <Button
                  color="inherit"
                  component={NavLink}
                  to={item.path}
                  className="nav-item nav-link"
                  sx={{
                    display: 'flex',
                    alignItems: 'center',
                    margin: '0 10px',
                    '&:hover': {
                      backgroundColor: 'rgba(255, 255, 255, 0.2)',
                      color: 'white',
                    }
                  }}
                >
                  {item.icon}
                </Button>
              </Tooltip>
            ))}
            <Tooltip 
              title={<span style={{ fontSize: '1.2em' }}>Изход</span>} 
              arrow
              sx={{
                fontSize: '1.2em' // Increase font size
              }}
            >
              <Button
                color="inherit"
                component={NavLink}
                to="/logout"
                onClick={(e) => {
                  e.preventDefault(); // Prevents navigation
                  handleLogout();
                }}
                className="nav-item nav-link"
                sx={{
                  display: 'flex',
                  alignItems: 'center',
                  margin: '0 10px',
                  '&:hover': {
                    backgroundColor: 'rgba(255, 255, 255, 0.2)',
                    color: 'white',
                  },
                }}
              >
                <LogoutIcon sx={{ fontSize: 30 }} />
              </Button>
            </Tooltip>
          </Box>
        </Toolbar>
      </AppBar>
    </>
  );
}