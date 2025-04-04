import api from './api';

const authService = {
  // Función de inicio de sesión
  login: async (username, password) => {
    try {
      const response = await api.post('/auth/login', { username, password });
      // Siempre almacenar usuario independientemente del estado del token
      localStorage.setItem('user', JSON.stringify({
        id: response.data.id,
        username: response.data.username,
        email: response.data.email
      }));
      // Almacenar tokens (aunque ya no se usan)
      localStorage.setItem('token', 'fake-token');
      localStorage.setItem('refreshToken', 'fake-refresh-token');
      return {
        ...response.data,
        user: {
          id: response.data.id,
          username: response.data.username,
          email: response.data.email
        }
      };
    } catch (error) {
      throw error;
    }
  },

  // Función de cierre de sesión
  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('user');
  },

  // Obtener usuario actual
  getCurrentUser: () => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  },

  // Verificar si el usuario está autenticado
  isAuthenticated: () => {
    return !!localStorage.getItem('token');
  },
};

export default authService;