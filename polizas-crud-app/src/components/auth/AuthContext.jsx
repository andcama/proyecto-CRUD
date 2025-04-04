import { createContext, useState, useEffect, useContext } from 'react';
import authService from '../../services/authService';

// Crear contexto de autenticación
const AuthContext = createContext();

// Hook personalizado para usar el contexto de autenticación
export const useAuth = () => {
  return useContext(AuthContext);
};

// Componente proveedor de autenticación
export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Inicializar estado de autenticación al montar el componente
    const initAuth = () => {
      const currentUser = authService.getCurrentUser();
      setUser(currentUser);
      setLoading(false);
    };

    initAuth();
  }, []);

  // Función de inicio de sesión
  const login = async (username, password) => {
    try {
      const data = await authService.login(username, password);
      setUser(data.user);
      return { success: true };
    } catch (error) {
      console.error('Login error:', error);
      return {
        success: false,
        message: error.response?.data?.message || 'Error en inicio de sesión',
      };
    }
  };

  // Función de cierre de sesión
  const logout = () => {
    authService.logout();
    setUser(null);
  };

  // Valor del contexto
  const value = {
    user,
    loading,
    login,
    logout,
    isAuthenticated: !!user,
  };

  return (
    <AuthContext.Provider value={value}>
      {!loading && children}
    </AuthContext.Provider>
  );
};