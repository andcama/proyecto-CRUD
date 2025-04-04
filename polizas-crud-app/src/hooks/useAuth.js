import { useContext } from 'react';
import { AuthContext } from '../components/auth/AuthContext';

// Hook personalizado para usar el contexto de autenticación
export const useAuth = () => {
  const context = useContext(AuthContext);
  
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  
  return context;
};